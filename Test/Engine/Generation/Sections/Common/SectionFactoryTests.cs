using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Engine.Generation.Sections.Mocks;
using Test.Engine.Generation.Widgets.Mocks;
using website_generator.Domain.Generation.Exceptions;
using website_generator.Domain.Generation.Sections;
using website_generator.Domain.Generation.Widgets;
using website_generator.Engine.Generation.Sections.Common;

namespace Test.Engine.Generation.Sections
{
    public class SectionFactoryTests
    {
        private SectionFactoryBase _factory;
        private ISectionFactoryVerifier _sectionFactoryVerifier;

        public SectionFactoryTests()
        {
            _sectionFactoryVerifier = Substitute.For<ISectionFactoryVerifier>();
            _factory = new SectionFactoryMock(_sectionFactoryVerifier);
        }

        [Fact]
        public void WhenCreatingSection_GivenValidSectionSetup_ThenSectionCreated()
        {
            // Assemble
            var expected = new Section(
                new()
                {
                    "TestWidgetOne",
                    "TestWidgetTwo"
                }
            );

            // Act
            var result = _factory.CreateSection();

            // Assert
            Assert.Equal(expected.Widgets[0], result.Widgets[0]);
            Assert.Equal(expected.Widgets[1], result.Widgets[1]);
        }

        [Fact]
        public void WhenCreatingSection_GivenInvalidSectionSetup_ThenErrorThrown()
        {
            // Assemble
            _sectionFactoryVerifier
                .WhenForAnyArgs(x => x.Verify(Arg.Any<Section>()))
                .Do(_ => throw new Exception());

            // Act
            var exception = Record.Exception(
                () => _factory.CreateSection()
                );

            // Assert
            Assert.NotNull(exception);
        }

        private IWidgetFactory GetWidgetFactory() => Substitute.For<IWidgetFactory>();
    }
}
