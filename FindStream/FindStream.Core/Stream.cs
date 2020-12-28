using System.Collections.Generic;
using System.Linq;

namespace FindStream.Core
{
    public class Stream : IStream
    {

        private readonly List<string> _vowels = new List<string>(new string[] { "a", "e", "i", "o", "u" });
        private List<string> PassedLetter { get; set; }
        private string StreamText { get; set; }
        private int CurrentIndex { get; set; }

        public Stream()
        {
            PassedLetter = new List<string>();
            CurrentIndex = 0;
        }

        public char GetNext()
        {
            return StreamText[CurrentIndex];
        }

        public bool HasNext()
        {
            return CurrentIndex < StreamText.Length;
        }

        /// <summary>
        /// Metodos que encontra o primeiro caracter Vogal, após uma consoante e que não se repita no resto da stream.
        /// </summary>
        /// <param name="streamText">texto do stream</param>
        /// <returns>Se o retorno for um char vazio é porque não foi encontrado uma vogal sem repitir após consoante.</returns>
        public char FirstChar(string streamText)
        {
            this.StreamText = streamText;

            while (HasNext())
            {
                char currentLetter = GetNext();
                if (CurrentIndex > 0 && IsVowel(currentLetter))
                {
                    var previousLetter = StreamText[CurrentIndex - 1];
                    if (!IsVowel(previousLetter) && !HasVowelInTheRest(currentLetter))
                        return currentLetter;
                }

                PassedLetter.Add(currentLetter.ToString());
                CurrentIndex++;
            }

            return '\0';
        }

        private bool IsVowel(char letter)
        {
            return _vowels.Any(letter.ToString().ToLower().Trim().Contains);
        }

        private bool HasVowelInTheRest(char letter)
        {
            var rest = StreamText.Substring(CurrentIndex + 1, StreamText.Length - (CurrentIndex + 1));

            if(!string.IsNullOrWhiteSpace(rest))
             return rest.ToLower().Any(letter.ToString().ToLower().Trim().Contains);

            return false;
        }
    }
}
