using NSubstitute;
using Test.Engine.Generation.Sections.Mocks;
using website_generator.Domain.Generation.Sections;
using website_generator.Engine.Generation.Sections.Common;

namespace Test.Engine.Generation.Sections.Common
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
    }
}
