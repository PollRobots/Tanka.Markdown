﻿namespace Markdown.HtmlTests
{
    using System.Text;
    using FluentAssertions;
    using Tanka.Markdown;
    using Tanka.Markdown.Gist;
    using Tanka.Markdown.Html;
    using Xunit;

    public class ExtendByAddingNewBlockRenderer
    {
        [Fact]
        public void EmbedGist()
        {
            // arrange
            var markdown = new StringBuilder();
            markdown.AppendLine("https://gist.github.com/pekkah/8304465");

            var expectedHtml = new StringBuilder();
            expectedHtml.Append("<script src=\"https://gist.github.com/pekkah/8304465.js\"></script>");

            var parserOptions = MarkdownParserOptions.Defaults;
            parserOptions.BlockFactories.Insert(0, new GistFactory());

            var parser = new MarkdownParser(parserOptions);

            // act
            Document document = parser.Parse(markdown.ToString());

            HtmlRendererOptions options = HtmlRendererOptions.Defaults;
            options.Renderers.Insert(0, new GistBlockRenderer());
            var renderer = new HtmlRenderer(options);

            string html = renderer.Render(document).Replace("\r\n", "");

            // assert
            html.ShouldBeEquivalentTo(expectedHtml.ToString());
        }
    }
}