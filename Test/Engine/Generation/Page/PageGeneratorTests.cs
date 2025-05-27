using NSubstitute;
using Test.Engine.Generation.Sections.Mocks;
using website_generator.Domain.Generation.Sections;
using website_generator.Engine.Generation.Page;

namespace Test.Engine.Generation.Page
{
    public class PageGeneratorTests
    {
        [Fact]
        public void WhenGeneratingPage_GivenValidSetup_ThenPageGenerated()
        {
            // Assemble
            var sectionFactoryVerifier = Substitute.For<ISectionFactoryVerifier>();
            var sections = new List<ISectionFactory>()
            {
                new SectionFactoryMock(sectionFactoryVerifier)
            };
            var generator = new PageGenerator(sections);

            // Act
            var result = generator.Generate();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void WhenGeneratingPage_GivenNoSections_ThenEmptyPageGenerated()
        {
            // Assemble
            var sections = new List<ISectionFactory>();
            var generator = new PageGenerator(sections);

            // Act
            var result = generator.Generate();

            // Assert
            Assert.Equal("", result);
        }
    }
}
