using FormattedText.Builder.TextElements;
using System.Text;

namespace FormattedText.Builder
{
    public class FormattedTextBuilder
    {
        private readonly List<ITextElement> _elements = [];
        private readonly Stack<ListElement> _listStack = new();

        public FormattedTextBuilder AddHeader(string text, HeaderLevel level = HeaderLevel.H1)
        {
            _elements.Add(new HeaderElement(text, level));
            return this;
        }

        public FormattedTextBuilder AddBold(string text)
        {
            _elements.Add(new BoldElement(text));
            return this;
        }

        public FormattedTextBuilder AddItalic(string text)
        {
            _elements.Add(new ItalicElement(text));
            return this;
        }

        public FormattedTextBuilder AddLink(string text, string url)
        {
            _elements.Add(new LinkElement(text, url));
            return this;
        }

        public FormattedTextBuilder StartNumberedList()
        {
            _listStack.Push(new ListElement(true));
            return this;
        }

        public FormattedTextBuilder StartAlphabeticalList(bool uppercase = false)
        {
            _listStack.Push(new ListElement(false, uppercase));
            return this;
        }

        public FormattedTextBuilder AddListItem(string text)
        {
            if (_listStack.Count == 0)
                throw new InvalidOperationException("No list started. Call StartNumberedList or StartAlphabeticalList first.");

            _listStack.Peek().Items.Add(text);
            return this;
        }

        public FormattedTextBuilder EndList()
        {
            if (_listStack.Count == 0)
                throw new InvalidOperationException("No list to end.");

            var list = _listStack.Pop();
            _elements.Add(list);
            return this;
        }

        public FormattedTextBuilder AddCodeBlock(string code, ProgrammingLanguage? language = null)
        {
            _elements.Add(new CodeBlockElement(code, language));
            return this;
        }

        public FormattedTextBuilder AddQuote(string text)
        {
            _elements.Add(new QuoteElement(text));
            return this;
        }

        public FormattedTextBuilder AddLineBreak()
        {
            _elements.Add(new LineBreakElement());
            return this;
        }

        public FormattedTextBuilder AddText(string text)
        {
            _elements.Add(new TextElement(text));
            return this;
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
    }
}
