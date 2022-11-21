﻿using System.Collections.Generic;
using System.IO;

namespace GuildOfHeroes.ConsoleInterface
{
    public static class ArtProvider
    {
        public static IReadOnlyList<string> Title { get; private set; }

        public static void Load()
        {
            LoadTitle();
        }

        private static void LoadTitle()
        {
            var title = new List<string>();
            using (var reader = new StreamReader("Art/Title.txt"))
            {
                while (!reader.EndOfStream)
                    title.Add(reader.ReadLine());
            }
            Title = title;
        }
    }
}
