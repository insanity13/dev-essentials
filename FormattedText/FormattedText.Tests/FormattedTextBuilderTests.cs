using FormattedText.Builder;

namespace FormattedText.Tests
{
    public class FormattedTextBuilderTests
    {
        [Fact]
        public void AddText_ShouldAppendPlainText()
        {
            // Arrange
            var builder = new FormattedTextBuilder();

            // Act
            builder.AddText("Hello");
            var result = builder.AsPlainText();

            // Assert
            Assert.Equal("Hello", result);
        }

        [Fact]
        public void AddHeader_ShouldRenderCorrectHeaderLevel()
        {
            // Arrange
            var builder = new FormattedTextBuilder();

            // Act
            builder.AddHeader("Title", HeaderLevel.H2);
            var plainText = builder.AsPlainText();
            var markdown = builder.AsMarkdown();
            var html = builder.AsHtml();

            // Assert
            Assert.Contains($"Title{Environment.NewLine}===={Environment.NewLine}", plainText);
            Assert.Contains($"## Title{Environment.NewLine}{Environment.NewLine}", markdown);
            Assert.Contains("<h2>Title</h2>", html);
        }

        [Fact]
        public void AddBoldAndItalic_ShouldRenderCorrectFormatting()
        {
            // Arrange
            var builder = new FormattedTextBuilder();

            // Act
            builder.AddBold("bold").AddText(" and ").AddItalic("italic");
            var plainText = builder.AsPlainText();
            var markdown = builder.AsMarkdown();
            var html = builder.AsHtml();

            // Assert
            Assert.Equal("bold and italic", plainText);
            Assert.Equal("**bold** and *italic*", markdown);
            Assert.Contains("<strong>bold</strong> and <em>italic</em>", html);
        }

        [Fact]
        public void AddLink_ShouldRenderCorrectLink()
        {
            // Arrange
            var builder = new FormattedTextBuilder();

            // Act
            builder.AddLink("Google", "https://google.com");
            var plainText = builder.AsPlainText();
            var markdown = builder.AsMarkdown();
            var html = builder.AsHtml();

            // Assert
            Assert.Equal("Google (https://google.com)", plainText);
            Assert.Equal("[Google](https://google.com)", markdown);
            Assert.Contains("<a href=\"https://google.com\">Google</a>", html);
        }

        [Fact]
        public void NumberedList_ShouldRenderCorrectly()
        {
            // Arrange
            var builder = new FormattedTextBuilder();

            // Act
            builder.StartNumberedList()
                .AddListItem("First")
                .AddListItem("Second")
                .EndList();

            var plainText = builder.AsPlainText();
            var markdown = builder.AsMarkdown();
            var html = builder.AsHtml();

            // Assert
            Assert.Contains($"1. First{Environment.NewLine}2. Second{Environment.NewLine}", plainText);
            Assert.Contains($"1. First{Environment.NewLine}2. Second{Environment.NewLine}", markdown);
            Assert.Contains("<ol", html);
            Assert.Contains("<li>First</li>", html);
            Assert.Contains("<li>Second</li>", html);
        }

        [Fact]
        public void AlphabeticalList_ShouldRenderCorrectly()
        {
            // Arrange
            var builder = new FormattedTextBuilder();

            // Act
            builder.StartAlphabeticalList()
                .AddListItem("First")
                .AddListItem("Second")
                .EndList();

            var plainText = builder.AsPlainText();
            var html = builder.AsHtml();

            // Assert
            Assert.Contains($"a. First{Environment.NewLine}b. Second{Environment.NewLine}", plainText);
            Assert.Contains("<ul style=\"list-style-type: lower-alpha\">", html);
        }

        [Fact]
        public void AddCodeBlock_ShouldRenderWithLanguage()
        {
            // Arrange
            var builder = new FormattedTextBuilder();

            // Act
            builder.AddCodeBlock("var x = 5;", ProgrammingLanguage.CSharp);
            var plainText = builder.AsPlainText();
            var markdown = builder.AsMarkdown();
            var html = builder.AsHtml();

            // Assert
            Assert.Contains($"var x = 5;{Environment.NewLine}{Environment.NewLine}", plainText);
            Assert.Contains($"```csharp{Environment.NewLine}var x = 5;{Environment.NewLine}```{Environment.NewLine}{Environment.NewLine}", markdown);
            Assert.Contains("<pre data-language=\"csharp\">", html);
            Assert.Contains("<code>var x = 5;</code>", html);
        }

        [Fact]
        public void AddQuote_ShouldRenderBlockquote()
        {
            // Arrange
            var builder = new FormattedTextBuilder();

            // Act
            builder.AddQuote("This is a quote");
            var plainText = builder.AsPlainText();
            var markdown = builder.AsMarkdown();
            var html = builder.AsHtml();

            // Assert
            Assert.Contains($"> This is a quote{Environment.NewLine}{Environment.NewLine}", plainText);
            Assert.Contains($"> This is a quote{Environment.NewLine}{Environment.NewLine}", markdown);
            Assert.Contains("<blockquote>This is a quote</blockquote>", html);
        }

        [Fact]
        public void ComplexDocument_ShouldRenderCorrectly()
        {
            // Arrange
            var builder = new FormattedTextBuilder();

            // Act
            builder.AddHeader("Document Title", HeaderLevel.H1)
                .AddText("This is a ")
                .AddBold("complex")
                .AddText(" document with ")
                .AddItalic("various")
                .AddText(" elements.")
                .AddLineBreak()
                .StartNumberedList()
                    .AddListItem("First item")
                    .AddListItem("Second item")
                .EndList()
                .AddCodeBlock("console.log('Hello');", ProgrammingLanguage.JavaScript)
                .AddQuote("Important note");

            var plainText = builder.AsPlainText();
            var markdown = builder.AsMarkdown();
            var html = builder.AsHtml("doc");

            // Assert
            Assert.Contains($"Document Title{Environment.NewLine}=={Environment.NewLine}", plainText);
            Assert.Contains("**complex**", markdown);
            Assert.Contains("<ol", html);
            Assert.Contains("class=\"doc-h1\"", html);
            Assert.Contains("```javascript", markdown);
        }

        [Fact]
        public void HtmlOutput_ShouldUseClassPrefix()
        {
            // Arrange
            var builder = new FormattedTextBuilder();

            // Act
            builder.AddHeader("Title", HeaderLevel.H1)
                .AddBold("Important")
                .AddText(" text")
                .AddItalic("italic");

            var html = builder.AsHtml("prefix");

            // Assert
            Assert.Contains("class=\"prefix-h1\"", html);
            Assert.Contains("class=\"prefix-bold\"", html);
            Assert.Contains("class=\"prefix-italic\"", html);
        }

        [Fact]
        public void EndList_WithoutStart_ShouldThrowException()
        {
            // Arrange
            var builder = new FormattedTextBuilder();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => builder.EndList());
        }

        [Fact]
        public void AddListItem_WithoutList_ShouldThrowException()
        {
            // Arrange
            var builder = new FormattedTextBuilder();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => builder.AddListItem("item"));
        }
    }
}
