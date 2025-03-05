namespace Wordle
{
    public class WordleGame
    {
        public string TargetWord { get; private set; }
        public List<GuessWord> PreviousGuesses { get; private set; } = new List<GuessWord>();
        public GameState State { get; private set; } = GameState.Playing;

        private int _maxGuesses;

        public WordleGame()
        {
            TargetWord = "Wordl";
            _maxGuesses = 6;

        }

        public void SubmitGuess(String guess)
        {
            GuessWord guessedWord = new GuessWord(guess);
            PreviousGuesses.Add(guessedWord);
            guessedWord.EvaluateWord(TargetWord);
            if (guessedWord.Correct)
            {
                State = GameState.Won;
            }
            else if (PreviousGuesses.Count >= _maxGuesses)
            {
                State = GameState.Lost;
            }
        }
    }
}
