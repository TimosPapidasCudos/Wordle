namespace Wordle
{
    public class WordleChar
    {
        public Correctness CorrectState { get; set; } = Correctness.Unknown;
        public char GuessedChar { get; set; }
    }
}
