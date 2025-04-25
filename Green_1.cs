using System;
using System.Globalization;
using System.Linq;


namespace Lab_8
{
    public class Green_1 : Green
    {
        private (char, double)[] _output;

        public (char, double)[] Output => _output;

        public Green_1(string input) : base(input)
        {
            _output = null;
        }

        public override void Review()
        {
            if (Input == null) return;

            // нормализация
            string normalized = Input
                .Replace('ё', 'е')
                .Replace('Ё', 'Е');

            // 1) denomAll — для частот: все буквы
            int denomAll = normalized.Count(c => Char.IsLetter(c));

            // 2) counts — только русские буквы
            int[] counts = new int[33];
            foreach (char ch in normalized)
            {
                char c = char.ToLowerInvariant(ch);
                if (IsRussianLetter(c))
                    counts[GetRussianLetterIndex(c)]++;
            }

            // 3) asciiCount — английские a..z
            int asciiCount = normalized.Count(ch =>
            {
                char c = char.ToLowerInvariant(ch);
                return c >= 'a' && c <= 'z';
            });

            // 4) russianCount — русские буквы
            int russianCount = counts.Sum();

            var temp = new (char, double)[33];
            int ti = 0;
            for (int i = 0; i < counts.Length; i++)
            {
                if (counts[i] > 0)
                {
                    char letter = GetRussianLetterByIndex(i);
                    double freq = (double)counts[i] / denomAll;
                    temp[ti++] = (letter, freq);
                }
            }

            _output = temp
                .Take(ti)
                .OrderBy(p => p.Item1)
                .ToArray();
        }

        public override string ToString()
        {
            if (_output == null || _output.Length == 0) return string.Empty;
            return string.Join(Environment.NewLine, _output.Select(tuple => $"{tuple.Item1} - {FormatNumber(tuple.Item2)}"));
        }

        private static bool IsRussianLetter(char c)
        {
            return (c >= 'а' && c <= 'я') || c == 'ё';
        }

        private static int GetRussianLetterIndex(char c)
        {
            return c == 'ё' ? 32 : c - 'а';
        }

        private static string FormatNumber(double number)
        {
            return number.ToString("F4", CultureInfo.InvariantCulture);
        }

        private static char GetRussianLetterByIndex(int index)
        {
            return index == 32 ? 'ё' : (char)('а' + index);
        }
    }


}
