﻿namespace Tanka.MarkdownTests
{
    using System.Linq;
    using FluentAssertions;
    using Markdown;

    public class MarkdownParserFactsBase
    {
        private string _markdown;
        private MarkdownParser _parser;

        public MarkdownDocument Document { get; set; }

        protected void ThenDocumentChildAtIndexShouldMatch<T>(int index, object expected) where T : Block
        {
            Block child = Document.Blocks.ElementAtOrDefault(index);
            child.Should().NotBeNull("Should have child block of type {0} at {1}", typeof (T).FullName, index);

            child.ShouldHave().AllRuntimeProperties().EqualTo(expected);
        }

        protected void ThenDocumentChildrenShouldHaveCount(int count)
        {
            Document.Blocks.Should().HaveCount(count);
        }

        protected void GivenMarkdownParserWithDefaults()
        {
            _parser = new MarkdownParser();
        }

        protected void GivenTheMarkdown(string markdown)
        {
            _markdown = markdown;
        }

        protected void WhenTheMarkdownIsParsed()
        {
            Document = _parser.Parse(_markdown);
        }
    }
}