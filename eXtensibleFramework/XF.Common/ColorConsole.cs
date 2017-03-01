// <copyright company="eXtensible Solutions LLC" file="ColorConsole.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;

    public static class ColorConsole
    {
        public static void WriteLine(ConsoleColor color, string text, params object[] args)
        {

            Console.Title = "eXtensible.Frameowrk";
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text, args);
            Console.ForegroundColor = currentColor;
        }
    }

}
