﻿namespace Tanka.Markdown
{
    public class HeadingFactory : BlockFactoryBase
    {
        public override bool IsMatch(string currentLine, string nextLine)
        {
            if (currentLine.StartsWith("#"))
            {
                return true;
            }

            return false;
        }

        public override Block Create()
        {
            return new Heading();
        }
    }
}