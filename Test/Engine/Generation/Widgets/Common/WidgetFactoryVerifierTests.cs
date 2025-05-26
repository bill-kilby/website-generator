using HtmlAgilityPack;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Engine.Generation.Widgets.Mocks;
using website_generator.Domain.Generation.Common;
using website_generator.Domain.Generation.Exceptions;
using website_generator.Domain.Generation.Widget;
using website_generator.Engine.Generation.Widgets.Common;
using Xunit.Sdk;

namespace Test.Engine.Generation.Widgets.Common
{
    public class WidgetFactoryVerifierTests
    {
        private IWidgetFactory _widgetFactory;
        private IHtmlVerifier _htmlVerifier;

        public WidgetFactoryVerifierTests()
        {
            _widgetFactory = Substitute.For<IWidgetFactory>();
            _htmlVerifier = Substitute.For<IHtmlVerifier>();
        }

        [Fact]
        public void WhenVerifyingWidgetLoader_GivenValidHTML_ThenNoErrorsThrown()
        {
            // Assemble
            var expected = "<div></div>";
            var widgetLoader = Substitute.For<IWidgetLoader>();
            widgetLoader.LoadTemplateFromDisk("Test-Widget").Returns(expected);

            var verifier = new WidgetFactoryVerifier(widgetLoader, _htmlVerifier);

            // Act
            var exception = Record.Exception(
                () => verifier.Verify(_widgetFactory, GetValidMetadata())
                );

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void WhenVerifyingFactory_GivenInvalidHTML_ThenErrorThrown()
        {
            // Assemble
            var expected = "<div</div>";
            var widgetLoader = Substitute.For<IWidgetLoader>();
            widgetLoader.LoadTemplateFromDisk("Test-Widget").Returns(expected);

            _htmlVerifier
                .When(x => x.Verify(expected))
                .Do(x => throw new InvalidHTMLException(""));

            var verifier = new WidgetFactoryVerifier(widgetLoader, _htmlVerifier);

            // Act
            var exception = Record.Exception(
                () => verifier.Verify(_widgetFactory, GetValidMetadata())
                );

            // Assert
            Assert.IsType<InvalidHTMLException>(exception);
        }

        [Fact]
        public void WhenVerifyingFactory_GivenNoHTML_ThenErrorThrown()
        {
            // Assemble
            var widgetLoader = Substitute.For<IWidgetLoader>();
            widgetLoader.LoadTemplateFromDisk("Test-Widget").Throws(new FileNotFoundException());

            var verifier = new WidgetFactoryVerifier(widgetLoader, _htmlVerifier);

            // Act
            var exception = Record.Exception(
                () => verifier.Verify(_widgetFactory, GetValidMetadata())
                );

            // Assert
            Assert.IsType<FileNotFoundException>(exception);
        }

        [Fact]
        public void WhenVerifyingFactory_GivenValidMetadata_ThenNoErrorsThrown()
        {
            // Assemble
            var expected = "<div>{PropertyOne}</div>";
            var widgetLoader = Substitute.For<IWidgetLoader>();
            widgetLoader.LoadTemplateFromDisk("Test-Widget").Returns(expected);

            var metadata = new SinglePropertyWidgetMetadataMock("Test-Widget", "PropertyValue");

            var verifier = new WidgetFactoryVerifier(widgetLoader, _htmlVerifier);

            // Act
            var exception = Record.Exception(
                () => verifier.Verify(_widgetFactory, metadata)
                );

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void WhenVerifyingFactory_GivenMetadataWithTooFewFields_ThenErrorThrown()
        {
            // Assemble
            var expected = "<div>{PropertyOne}</div>";
            var widgetLoader = Substitute.For<IWidgetLoader>();
            widgetLoader.LoadTemplateFromDisk("Test-Widget").Returns(expected);

            var metadata = new WidgetMetadataMock("Test-Widget");

            var verifier = new WidgetFactoryVerifier(widgetLoader, _htmlVerifier);

            // Act
            var exception = Record.Exception(
                () => verifier.Verify(_widgetFactory, metadata)
                );

            // Assert
            Assert.IsType<InvalidMetadataException>(exception);
        }

        [Fact]
        public void WhenVerifyingFactory_GivenMetadataWithTooManyFields_ThenErrorThrown()
        {
            // Assemble
            var expected = "<div></div>";
            var widgetLoader = Substitute.For<IWidgetLoader>();
            widgetLoader.LoadTemplateFromDisk("Test-Widget").Returns(expected);

            var metadata = new SinglePropertyWidgetMetadataMock("Test-Widget", "PropertyValue");

            var verifier = new WidgetFactoryVerifier(widgetLoader, _htmlVerifier);

            // Act
            var exception = Record.Exception(
                () => verifier.Verify(_widgetFactory, metadata)
                );

            // Assert
            Assert.IsType<InvalidMetadataException>(exception);
        }

        [Fact]
        public void WhenVerifyingFactory_GivenMetadataWithIncorrectNames_ThenErrorThrown()
        {
            // Assemble
            var expected = "<div>{RandomPropertyName}</div>";
            var widgetLoader = Substitute.For<IWidgetLoader>();
            widgetLoader.LoadTemplateFromDisk("Test-Widget").Returns(expected);

            var metadata = new SinglePropertyWidgetMetadataMock("Test-Widget", "PropertyValue");

            var verifier = new WidgetFactoryVerifier(widgetLoader, _htmlVerifier);

            // Act
            var exception = Record.Exception(
                () => verifier.Verify(_widgetFactory, metadata)
                );

            // Assert
            Assert.IsType<InvalidMetadataException>(exception);
        }

        private WidgetMetadata GetValidMetadata()
        {
            return new WidgetMetadataMock("Test-Widget");
        }
    }
}
