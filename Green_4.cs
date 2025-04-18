using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_8
{
    class Green_4 : Green
    {
        private string[] _output;

        public string[] Output => _output;

        public Green_4(string input) : base(input)
        {
            _output = null;
        }

        public override void Review()
        {
            if (Input == null) return;

            string[] wordsArray = Input.Split(new[] { ' ', '.', '!', '?', ',', ':', '\"', ';', '–', '(', ')', '[', ']', '{', '}', '/' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < wordsArray.Length; i++)
            {
                wordsArray[i] = wordsArray[i].Trim();
            }

            // реализуем сортировку пузырьком без стандартного метода Compare,
            // а с написанным методом ManualStringCompare
            for (int i = 0; i < wordsArray.Length; i++)
            {
                for (int j = 0; j < wordsArray.Length - 1 - i; j++)
                {
                    if (ManualStringCompare(wordsArray[j], wordsArray[j + 1]) > 0)
                    {
                        string temp = wordsArray[j];
                        wordsArray[j] = wordsArray[j + 1];
                        wordsArray[j + 1] = temp;
                    }
                }
            }
            _output = wordsArray;
        }

        public override string ToString()
        {
            if (_output == null || _output.Length == 0) return string.Empty;
            return string.Join(Environment.NewLine, _output);
        }

        /// <summary>
        /// метод для сравнения строк с игнорированием регистра
        /// </summary>
        /// <param name="str1"> 1 строка </param>
        /// <param name="str2"> 2 строка </param>
        /// <returns> возвращает числовой результат сравнения строк </returns>
        private int ManualStringCompare(string str1, string str2)
        {
            int length = Math.Min(str1.Length, str2.Length);

            for (int i = 0; i < length; i++)
            {
                char char1 = char.ToLower(str1[i]);
                char char2 = char.ToLower(str2[i]);

                if (char1 < char2) return -1;
                if (char1 > char2) return 1;
            }

            // если строки равны до меньшей из их длин 
            if (str1.Length < str2.Length) return -1;
            if (str1.Length > str2.Length) return 1;

            // если строки равны 
            return 0;
        }
    }
}
