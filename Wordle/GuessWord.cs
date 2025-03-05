namespace Wordle
{
    public class GuessWord
    {
        public readonly GuessChar[] GuessedWord;

        public bool Correct { get; private set; }
        public GuessWord(string guessedWord)
        {
            GuessedWord = new GuessChar[guessedWord.Length];
            for (int index = 0; index < guessedWord.Length; index++)
            {
                GuessedWord[index] = new GuessChar(guessedWord[index]);
            }
        }

        public void EvaluateWord(string referenceWord)
        {
            for (int index = 0; index < GuessedWord.Length; index++)
            {
                if (GuessedWord[index].GuessedChar.Equals(referenceWord[index]))
                {
                    GuessedWord[index].CorrectState = Correctness.Correct;
                }
                else if (referenceWord.Contains(GuessedWord[index].GuessedChar))
                {
                    GuessedWord[index].CorrectState = Correctness.Present;
                }
                else
                {
                    GuessedWord[index].CorrectState = Correctness.Incorrect;
                }
            }
            bool correct = true;
            foreach(GuessChar g in GuessedWord)
            {
                if (g.CorrectState != Correctness.Correct)
                {
                    correct = false;
                }
            }
            Correct = correct;
        }
    }
}
