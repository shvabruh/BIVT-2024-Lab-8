using System;
using System.Globalization;
using System.Linq;


namespace Lab_8
{
    class Green_2 : Green
    {
        private char[] _output;

        public char[] Output => _output;
        public Green_2(string input) : base(input)
        {
            _output = null;
        }

        public override void Review()
        {
            if (Input == null) return;

            // массив для хранения первых букв, передающихся в Input
            char[] firstLetters = Input.Split(new[] { ' ' ,'.', '!', '?', ',', ':', '\"', ';', '–', '(', ')', '[', ']', '{', '}', '/' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(word => word[0])
                .Select(char.ToLower)
                .ToArray();

            char[] distinctLetters = firstLetters
            .Distinct() // убираем дубликаты
            .ToArray(); 

            // массив для хранения частот появления букв
            int[] counts = new int[distinctLetters.Length];

            for (int i = 0; i < distinctLetters.Length; i++)
            {
                counts[i] = firstLetters.Count(c => c == distinctLetters[i]);
            }

            // массив для результатов
            var result = new (char letter, double frequency)[distinctLetters.Length];
            int resultIndex = 0;

            for (int i = 0; i < distinctLetters.Length; i++)
            {
                if (counts[i] > 0)
                {
                    char letter = distinctLetters[i];
                    double frequency = (double)counts[i] / firstLetters.Length;
                    result[resultIndex++] = (letter, frequency);
                }
            }

            
            var sortedResult = result
                .OrderByDescending(i => i.frequency)
                .ThenBy(i => i.letter)
                .ToArray();

            _output = sortedResult.Select(r => r.letter).ToArray();
            //Array.Sort(_output, (x, y) => x.Item1.CompareTo(y.Item1)); возможно неустойчивая сортировка
        }

        public override string ToString()
        {
            if (_output == null || _output.Length == 0) return string.Empty;
            return string.Join(", ", _output.Select(item => $"{item}"));
        }
    }
}
