
namespace Lab_8
{
    public abstract class Green
    {
        private string input;

        public string Input
        {
            get 
            {
                if (input == null) return string.Empty;
                return input;
            }
        }
      
        protected Green(string input)
        {
            this.input = input;
        }
        public abstract void Review();
    }
}
