using System;
using System.Linq;


namespace Lab_8
{
    public class Green_3 : Green
    {
        private string[] _output;
        private string _letterSequence;
        public string[] Output => _output;
        private string LetterSequence => _letterSequence;
        
        public Green_3(string input, string letterSequence) : base(input)
        {
            _letterSequence = letterSequence?.ToLower();
            _output = null;
        }

        public override void Review()
        {
            if (Input == null || _letterSequence == null) return;
            // массив для хранения слов из предложения без повторений
            string[] wordsArray = Input.Split(new[] { ' ', '.', '!', '?', ',', ':', '\"', ';', '–', '(', ')', '[', ']', '{', '}', '/' }, StringSplitOptions.RemoveEmptyEntries)
                .Where(word => word.ToLower().Contains(_letterSequence))
                .Select(word => word.ToLower())
                .Distinct()
                .ToArray();
            _output = wordsArray;
        }

        public override string ToString()
        {
            if (_output == null || _output.Length == 0) return string.Empty;
            return string.Join(Environment.NewLine, _output.Select(item => $"{item.ToLower()}"));
        }
    }
}
