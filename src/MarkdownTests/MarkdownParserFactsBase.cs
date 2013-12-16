﻿namespace Tanka.MarkdownTests
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using FluentAssertions;
    using Markdown;
    using Markdown.Blocks;

    public class MarkdownParserFactsBase
    {
        private string _markdown;
        private MarkdownParser _parser;

        public Document Document { get; set; }

        protected void ThenDocumentChildAtIndexShouldMatch<T>(int index, object expected) where T : Block
        {
            Block child = Document.Blocks.ElementAtOrDefault(index);
            child.Should().NotBeNull("Should have child block of type {0} at {1}", typeof (T).FullName, index);

            child.ShouldHave().AllRuntimeProperties().EqualTo(expected);
        }

        protected void ThenDocumentChildAtIndexShould<T>(int index, Action<T> assert) where T : Block
        {
            var child = Document.Blocks.ElementAtOrDefault(index) as T;
            child.Should().NotBeNull("Should have child block of type {0} at {1}", typeof(T).FullName, index);

            assert(child);
        }

        protected void ThenDocumentChildAtIndexShouldMatch<T>(int index, object expected,
            Expression<Func<T, object>> but) where T : Block
        {
            var child = Document.Blocks.ElementAtOrDefault(index) as T;
            child.Should().NotBeNull("Should have child block of type {0} at {1}", typeof (T).FullName, index);

            child.ShouldHave().AllPropertiesBut(but).EqualTo(expected);
        }

        protected void ThenDocumentChildAtIndexShouldBe(int index, Type expectedType)
        {
            Block child = Document.Blocks.ElementAtOrDefault(index);
            child.Should().NotBeNull("Should have child block of type {0} at {1}", expectedType.FullName, index);
            child.GetType().ShouldBeEquivalentTo(expectedType);
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

        protected void ThenListAtIndexShouldMatch(int index, params string[] items)
        {
            var child = Document.Blocks.ElementAtOrDefault(index) as ListBlock;
            child.Should().NotBeNull("Should have child block of type {0} at {1}", typeof(ListBlock).FullName, index);

            var hasAll = true;
            foreach (var item in items)
            {
                if (!child.Items.Contains(item))
                    hasAll = false;
            }

            hasAll.ShouldBeEquivalentTo(true);
        }
    }
}