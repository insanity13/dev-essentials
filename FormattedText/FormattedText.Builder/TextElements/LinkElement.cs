using System.Text;

namespace FormattedText.Builder.TextElements
{
    internal class LinkElement(string text, string url) : ITextElement
    {
        private readonly string _text = text;
        private readonly string _url = url;

        public void AppendAsPlainText(StringBuilder sb) => sb.Append($"{_text} ({_url})");

        public void AppendAsMarkdown(StringBuilder sb) => sb.Append($"[{_text}]({_url})");

        public void AppendAsHtml(StringBuilder sb, string? classPrefix)
        {
            var classAttr = string.IsNullOrEmpty(classPrefix) ? "" : $" class=\"{classPrefix}-link\"";
            sb.Append($"<a{classAttr} href=\"{System.Net.WebUtility.HtmlEncode(_url)}\">")
              .Append(System.Net.WebUtility.HtmlEncode(_text))
              .Append("</a>");
        }
    }
}
