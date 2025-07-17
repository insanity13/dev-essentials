using System.Text;

namespace FormattedText.Builder.TextElements
{
    internal class ListElement(bool isNumbered, bool isUppercase = false) : ITextElement
    {
        public bool IsNumbered { get; } = isNumbered;
        public bool IsUppercase { get; } = isUppercase;
        public List<string> Items { get; } = new List<string>();

        public void AppendAsPlainText(StringBuilder sb)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                var prefix = IsNumbered ? $"{i + 1}. " : $"{(IsUppercase ? (char)('A' + i) : (char)('a' + i))}. ";
                sb.AppendLine($"{prefix}{Items[i]}");
            }
            sb.AppendLine();
        }

        public void AppendAsMarkdown(StringBuilder sb)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                var prefix = IsNumbered ? $"{i + 1}. " : $"{(IsUppercase ? (char)('A' + i) : (char)('a' + i))}. ";
                sb.AppendLine($"{prefix}{Items[i]}");
            }
            sb.AppendLine();
        }

        public void AppendAsHtml(StringBuilder sb, string? classPrefix)
        {
            var listType = IsNumbered ? "ol" : "ul";
            var classAttr = string.IsNullOrEmpty(classPrefix) ? "" : $" class=\"{classPrefix}-list {(IsNumbered ? "numbered" : "alphabetical")}\"";
            var style = IsUppercase && !IsNumbered ? " style=\"list-style-type: upper-alpha\"" : "";

            sb.AppendLine($"<{listType}{classAttr}{style}>");

            foreach (var item in Items)
            {
                sb.AppendLine($"<li>{System.Net.WebUtility.HtmlEncode(item)}</li>");
            }

            sb.AppendLine($"</{listType}>");
        }
    }
}
