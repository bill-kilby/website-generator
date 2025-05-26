using NSubstitute;
using website_generator.Domain.Generation.Widgets;
using website_generator.Engine.Generation.Widgets.Common;

namespace Test.Engine.Generation.Widgets.Common
{
    public class WidgetFactoryCacheTests
    {
        private int _factoryNum = 0;
        private IWidgetFactoryCache _factoryCache;

        public WidgetFactoryCacheTests()
        {
            var entries = new List<IWidgetFactory>()
            {
                GenerateFactoryMock(),
                GenerateFactoryMock()
            };
            _factoryCache = new WidgetFactoryCache(entries);
        }

        [Fact]
        public void WhenAccessingCache_GivenValidEntry_GetEntry()
        {
            // Assemble

            // Act
            var result = _factoryCache.GetFactory("TestFactory1");

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void WhenAccessingCache_GivenInvalidEntry_ThrowsError()
        {
            // Assemble

            // Act
            var exception = Record.Exception(
                () => _factoryCache.GetFactory("NonExistentEntry")
                );

            // Assert
            Assert.NotNull(exception);

        }

        private IWidgetFactory GenerateFactoryMock()
        {
            var name = $"TestFactory{_factoryNum}";
            _factoryNum++;

            var factory = Substitute.For<IWidgetFactory>();
            factory.Name.Returns(name);

            return factory;
        }
    }
}
