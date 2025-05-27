using NSubstitute;
using website_generator.Domain.Generation.Exceptions;
using website_generator.Domain.Generation.Sections;
using website_generator.Domain.Generation.Widgets;
using website_generator.Engine.Generation.Sections.Common;

namespace Test.Engine.Generation.Sections.Common
{
    public class SectionFactoryVerifierTests
    {
        private IWidgetFactoryCache _widgetFactoryCache;
        private ISectionFactoryVerifier _sectionFactoryVerifier;

        public SectionFactoryVerifierTests()
        {
            _widgetFactoryCache = Substitute.For<IWidgetFactoryCache>();
            _sectionFactoryVerifier = new SectionFactoryVerifier(_widgetFactoryCache);
        }

        [Fact]
        public void WhenVerifyingSection_GivenValidSection_ThenNoErrorsThrown()
        {
            // Assemble
            var section = new Section(
                new()
                {
                    "TestWidgetOne",
                    "TestWidgetTwo"
                });

            _widgetFactoryCache.Contains("TestWidgetOne").Returns(true);
            _widgetFactoryCache.Contains("TestWidgetTwo").Returns(true);

            // Act
            var exception = Record.Exception(
                () => _sectionFactoryVerifier.Verify(section)
                );

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void WhenVerifyingSection_GivenUnregisteredWidget_ThenErrorThrown()
        {
            // Assemble
            var section = new Section(
                new()
                {
                    "TestWidgetOne",
                    "TestWidgetTwo"
                });

            // Act
            var exception = Record.Exception(
                () => _sectionFactoryVerifier.Verify(section)
                );

            // Assert
            Assert.IsType<InvalidSectionException>(exception);
        }

        private IWidgetFactory GetWidgetFactory() => Substitute.For<IWidgetFactory>();
    }
}
