using System.Text;

namespace FormattedText.Builder.TextElements
{
    internal class ItalicElement(string text) : ITextElement
    {
        private readonly string _text = text;

        public void AppendAsPlainText(StringBuilder sb) => sb.Append(_text);

        public void AppendAsMarkdown(StringBuilder sb) => sb.Append($"*{_text}*");

        public void AppendAsHtml(StringBuilder sb, string classPrefix)
        {
            var classAttr = string.IsNullOrEmpty(classPrefix) ? "" : $" class=\"{classPrefix}-italic\"";
            sb.Append($"<em{classAttr}>")
              .Append(System.Net.WebUtility.HtmlEncode(_text))
              .Append("</em>");
        }
    }
}
