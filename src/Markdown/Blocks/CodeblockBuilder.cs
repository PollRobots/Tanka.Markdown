﻿namespace Tanka.Markdown.Blocks
{
    using System.Text.RegularExpressions;
    using CSharpVerbalExpressions;
    using Markdown;

    public class CodeblockBuilder : IBlockBuilder
    {
        private readonly Regex _expression;

        public CodeblockBuilder()
        {
            _expression = VerbalExpressions.DefaultExpression
                .Add(@"\G", false)
                .Then("```")
                .Anything()
                .Then("```")
                .WithOptions(RegexOptions.Singleline)
                .ToRegex();
        }

        public bool CanBuild(int start, StringRange content)
        {
            var isMatch = _expression.IsMatch(content.Document, start);

            return isMatch;
        }

        public Block Build(int start, StringRange content, out int end)
        {
            // start from the actual first line of the content
            var contentStart = content.StartOfNextLine(start);

            // find the end ``` by skipping the first ```
            var contentEnd = content.IndexOf("```", contentStart) - 1;

            // skip line ending
            end = content.EndOfLine(contentEnd + 1, true);

            // use the content between ``` and ```
            return new Codeblock(content, contentStart, contentEnd);
        }
    }
}