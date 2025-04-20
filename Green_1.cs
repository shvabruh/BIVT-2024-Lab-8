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

            // массив для хранения русских букв
            int[] counts = new int[33]; // 32 буквы + 'ё'
            int totalLetters = 0;

            foreach (var c in Input.ToLowerInvariant())
            {
                if (IsRussianLetter(c))
                {
                    int index = GetRussianLetterIndex(c);
                    counts[index]++;
                    totalLetters++;
                }
            }

            if (totalLetters == 0)
            {
                _output = Array.Empty<(char, double)>();
                return;
            }

            // массив для результатов
            var result = new (char, double)[33];
            int resultIndex = 0;

            for (int i = 0; i < counts.Length; i++)
            {
                if (counts[i] > 0)
                {
                    char letter = GetRussianLetterByIndex(i);
                    double frequency = (double)counts[i] / totalLetters;
                    result[resultIndex++] = (letter, frequency);
                }
            }

            _output = result.Take(resultIndex).OrderBy(i => i.Item1).ToArray();
            
            //Array.Sort(_output, (x, y) => x.Item1.CompareTo(y.Item1)); возможно неустойчивая сортировка
        }

        public override string ToString()
        {
            if (_output == null || _output.Length == 0) return string.Empty;
            return string.Join(Environment.NewLine, _output.Select(tuple => $"{tuple.Item1} - {FormatNumber(Math.Round(tuple.Item2,4))}"));
        }

        private static bool IsRussianLetter(char c)
        {
            return (c >= 'а' && c <= 'я') || c == 'ё';
        }

        private static int GetRussianLetterIndex(char c)
        {
            if (c == 'ё') return 32;
            return c - 'а';
        }

        private static string FormatNumber(double number)
        {
            return number.ToString("G4", CultureInfo.InvariantCulture);
        }

        private static char GetRussianLetterByIndex(int index)
        {
            if (index == 32) return 'ё';
            return (char)('а' + index);
        }
    }


}
