using System.Text;

namespace FormattedText.Builder.TextElements
{
    internal class LineBreakElement : ITextElement
    {
        public void AppendAsPlainText(StringBuilder sb) => sb.AppendLine();

        public void AppendAsMarkdown(StringBuilder sb) => sb.AppendLine();

        public void AppendAsHtml(StringBuilder sb, string classPrefix) => sb.Append("<br/>");
    }
}
