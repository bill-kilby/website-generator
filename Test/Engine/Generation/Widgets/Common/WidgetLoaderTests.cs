using website_generator.Domain.Generation.Widgets;
using website_generator.Engine.Generation.Widgets.Common;

namespace Test.Engine.Generation.Widgets.Common
{
    public class WidgetLoaderTests
    {
        private IWidgetLoader _widgetLoader;

        public WidgetLoaderTests()
        {
            _widgetLoader = new WidgetLoader();
        }

        [Fact]
        public void WhenLoadingWidget_GivenWidgetExists_ThenGetsHTML()
        {
            // Assemble
            var expected = "<div>\r\n    This is a test widget!\r\n</div>";

            // Act
            var html = _widgetLoader.LoadTemplateFromDisk("TestWidget");

            // Assert
            Assert.Equal(expected, html);
        }

        [Fact]
        public void WhenLoadingWidget_GivenWidgetDoesNotExist_ThenThrowsErrors()
        {
            // Assemble

            // Act
            var exception = Record.Exception(
                () => _widgetLoader.LoadTemplateFromDisk("BKDEV")
                );

            // Assert
            Assert.IsType<FileNotFoundException>(exception);
        }
    }
}
