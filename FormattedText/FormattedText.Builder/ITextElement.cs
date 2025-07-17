using System.Text;

namespace FormattedText.Builder
{
    internal interface ITextElement
    {
        void AppendAsPlainText(StringBuilder sb);
        void AppendAsMarkdown(StringBuilder sb);
        void AppendAsHtml(StringBuilder sb, string? classPrefix);
    }
}
