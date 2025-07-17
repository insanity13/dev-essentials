using System.Text;

namespace FormattedText.Builder.TextElements
{
    internal class TextElement(string text) : ITextElement
    {
        private readonly string _text = text;

        public void AppendAsPlainText(StringBuilder sb) => sb.Append(_text);

        public void AppendAsMarkdown(StringBuilder sb) => sb.Append(_text);

        public void AppendAsHtml(StringBuilder sb, string classPrefix)
            => sb.Append(System.Net.WebUtility.HtmlEncode(_text));
    }
}
