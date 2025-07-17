using System.Text;

namespace FormattedText.Builder.TextElements
{
    internal class CodeBlockElement(string code, ProgrammingLanguage? language = null) : ITextElement
    {
        private readonly string _code = code;
        private readonly ProgrammingLanguage? _language = language;

        public void AppendAsPlainText(StringBuilder sb)
            => sb.Append(_code).AppendLine().AppendLine();

        public void AppendAsMarkdown(StringBuilder sb)
            => sb.Append($"```{_language}{Environment.NewLine}".ToLowerInvariant())
               .Append(_code)
               .AppendLine()
               .Append($"```{Environment.NewLine}{Environment.NewLine}");

        public void AppendAsHtml(StringBuilder sb, string? classPrefix)
        {
            var classAttr = string.IsNullOrEmpty(classPrefix) ? string.Empty : $" class=\"{classPrefix}-codeblock\"";
            var languageAttr = !_language.HasValue ? "" : $" data-language=\"{_language.ToString().ToLowerInvariant()}\"";

            sb.Append($"<pre{classAttr}{languageAttr}><code>")
              .Append(System.Net.WebUtility.HtmlEncode(_code))
              .Append("</code></pre>")
              .AppendLine();
        }
    }
}
