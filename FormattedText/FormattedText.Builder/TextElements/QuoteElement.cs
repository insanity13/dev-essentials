using System.Text;

namespace FormattedText.Builder.TextElements
{
    internal class QuoteElement(string text) : ITextElement
    {
        private readonly string _text = text;

        public void AppendAsPlainText(StringBuilder sb)
            => sb.Append($"> {_text}{Environment.NewLine}{Environment.NewLine}");

        public void AppendAsMarkdown(StringBuilder sb)
            => sb.Append($"> {_text}{Environment.NewLine}{Environment.NewLine}");

        public void AppendAsHtml(StringBuilder sb, string classPrefix)
        {
            var classAttr = string.IsNullOrEmpty(classPrefix) ? "" : $" class=\"{classPrefix}-quote\"";
            sb.Append($"<blockquote{classAttr}>")
              .Append(System.Net.WebUtility.HtmlEncode(_text))
              .Append("</blockquote>")
              .AppendLine();
        }
    }
}
