using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Engine.Generation.Sections.Mocks;
using Test.Engine.Generation.Widgets.Mocks;
using website_generator.Domain.Generation.Section;
using website_generator.Domain.Generation.Widgets;
using website_generator.Engine.Generation.Sections.Common;

namespace Test.Engine.Generation.Sections
{
    public class SectionFactoryTests
    {
        private SectionFactoryBase _factory;
        private IWidgetFactoryCache _widgetFactoryCache;

        public SectionFactoryTests()
        {
            _widgetFactoryCache = Substitute.For<IWidgetFactoryCache>();
            _factory = new SectionFactoryMock(_widgetFactoryCache);
        }

        [Fact]
        public void WhenCreatingSection_GivenValidSectionSetup_ThenSectionCreated()
        {
            // Assemble
            _widgetFactoryCache.GetFactory("TestWidgetOne").Returns(GetWidgetFactory());
            _widgetFactoryCache.GetFactory("TestWidgetTwo").Returns(GetWidgetFactory());

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
        public void WhenCreatingSection_GivenUnregisteredWidget_ThenErrorThrown()
        {
            // Assemble
            _widgetFactoryCache.GetFactory("IncorrectWidgetOne").Returns(GetWidgetFactory());
            _widgetFactoryCache.GetFactory("IncorrectWidgetTwo").Returns(GetWidgetFactory());

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
