using NSubstitute;
using Test.Engine.Generation.Widgets.Mocks;
using website_generator.Domain.Generation.Exceptions;
using website_generator.Domain.Generation.Widget;
using website_generator.Engine.Generation.Widgets;
using website_generator.Engine.Generation.Widgets.Common;

namespace Test.Engine.Generation.Widgets
{
    public class WidgetFactoryBaseTests
    {
        private IWidgetLoader _widgetLoader;
        private IWidgetVerifier _widgetVerifier;

        private WidgetFactoryBase<SinglePropertyWidgetMetadataMock> _widgetFactory;

        public WidgetFactoryBaseTests()
        {
            _widgetLoader = Substitute.For<IWidgetLoader>();
            _widgetLoader.LoadTemplateFromDisk("SinglePropertyWidgetMetadataMock")
                .Returns("<div>\r\n    {PropertyOne}\r\n</div>");

            _widgetVerifier = Substitute.For<IWidgetVerifier>();

            _widgetFactory = new WidgetFactoryMock(_widgetLoader, _widgetVerifier);
        }

        [Fact]
        public void WhenCreatingWidget_GivenValidInputs_ThenGetValidWidget()
        {
            // Assemble
            var metadata = new SinglePropertyWidgetMetadataMock();
            var expectedName = "SinglePropertyWidgetMetadataMock";
            var expectedContent = "<div>\r\n    PropertyOneValue\r\n</div>";

            // Act
            var result = _widgetFactory.CreateWidget(metadata);

            // Assert
            Assert.Equal(expectedName, result.Name);
            Assert.Equal(expectedContent, result.Content);
        }

        [Fact]
        public void WhenCreatingWidget_GivenInvalidInputs_ThenThrowsError()
        {
            // Assemble
            _widgetLoader.LoadTemplateFromDisk("SinglePropertyWidgetMetadataMock")
                .Returns("<div>\r\n    {PropertyNull}\r\n</div>");

            var metadata = new SinglePropertyWidgetMetadataMock();

            // Act
            var exception = Record.Exception(
                () => _widgetFactory.CreateWidget(metadata)
                );

            // Assert
            Assert.IsType<InvalidHTMLException>(exception);
        }
    }
}
