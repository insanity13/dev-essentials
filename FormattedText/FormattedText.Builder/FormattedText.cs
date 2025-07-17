using System.Text;

namespace FormattedText.Builder
{
    public class FormattedText
    {
        private readonly IReadOnlyCollection<ITextElement> _elements = [];

        internal FormattedText(IReadOnlyCollection<ITextElement> elements)
        { 
            _elements = elements;
        }

        public string AsPlainText()
        {
            var sb = new StringBuilder();
            foreach (var element in _elements)
                element.AppendAsPlainText(sb);

            return sb.ToString();
        }

        public string AsMarkdown()
        {
            var sb = new StringBuilder();
            foreach (var element in _elements)
                element.AppendAsMarkdown(sb);

            return sb.ToString();
        }

        public string AsHtml(string? classPrefix = null)
        {
            var sb = new StringBuilder();
            foreach (var element in _elements)
                element.AppendAsHtml(sb, classPrefix);

            return sb.ToString();
        }

        public override string ToString() => AsPlainText();
    }
}
