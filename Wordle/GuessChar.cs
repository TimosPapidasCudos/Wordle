namespace Wordle
{
    public class GuessChar
    {
        public Correctness CorrectState { get; set; }
        public char GuessedChar { get; }
        public GuessChar(char guessedChar)
        {
            GuessedChar = guessedChar;
        }
    }
}
