using System.Text;

namespace FormattedText.Builder.TextElements
{
    internal class HeaderElement(string text, HeaderLevel level) : ITextElement
    {
        private readonly string _text = text;
        private readonly HeaderLevel _level = level;

        public void AppendAsPlainText(StringBuilder sb)
        {
            sb.Append(_text)
              .AppendLine()
              .AppendLine(new string('=', (int)_level * 2));
        }

        public void AppendAsMarkdown(StringBuilder sb)
        {
            sb.Append(new string('#', (int)_level))
              .Append(' ')
              .Append(_text)
              .AppendLine()
              .AppendLine();
        }

        public void AppendAsHtml(StringBuilder sb, string? classPrefix)
        {
            var classAttr = string.IsNullOrEmpty(classPrefix) ? "" : $" class=\"{classPrefix}-{_level.ToString().ToLowerInvariant()}\"";
            sb.Append($"<h{(int)_level}{classAttr}>")
              .Append(System.Net.WebUtility.HtmlEncode(_text))
              .Append($"</h{(int)_level}>")
              .AppendLine();
        }
    }
}
