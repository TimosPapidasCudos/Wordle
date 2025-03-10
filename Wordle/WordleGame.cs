namespace Wordle
{
    public class WordleGame
    {
        public event Action StateChanged; //Maybe delete
        public string TargetWord { get; private set; }
        public WordleChar[][] Guesses { get; private set; }
        public int WordLength { get; private set; }
        public int CurrentGuess { get; private set; }
        public GameState State { get; private set; } = GameState.Playing;

        private int _maxGuesses;

        public WordleGame()
        {
            _maxGuesses = 6;
            WordLength = 5;
            CurrentGuess = 0;
            TargetWord = "WORDL";
            Initialize();
        }

        private void Initialize()
        {
            Guesses = new WordleChar[_maxGuesses][];
            for (int i = 0; i < _maxGuesses; i++)
            {
                Guesses[i] = new WordleChar[WordLength];
                for (int j = 0; j < WordLength; j++)
                {
                    Guesses[i][j] = new WordleChar();
                }
            }
        }

        public void SubmitGuess(string guess)
        {
            for (int i = 0; i < guess.Length; i++)
            {
                SubmitChar(guess[i], i);
            }
            UpdateGameState();
            CurrentGuess++;
        }
        private void SubmitChar(char toSubmit, int index)
        {
            if (toSubmit.Equals(TargetWord[index]))
            {
                Guesses[CurrentGuess][index].CorrectState = Correctness.Correct;
            }
            else if (TargetWord.Contains(toSubmit))
            {
                Guesses[CurrentGuess][index].CorrectState = Correctness.Present;
            }
            else
            {
                Guesses[CurrentGuess][index].CorrectState = Correctness.Incorrect;
            }
            Guesses[CurrentGuess][index].GuessedChar = toSubmit;
        }
        private void UpdateGameState()
        {
            bool correct = true;
            foreach (WordleChar g in Guesses[CurrentGuess])
            {
                if (g.CorrectState != Correctness.Correct)
                {
                    correct = false;
                }
            }
            if (correct)
            {
                State = GameState.Won;
            }
            else if (CurrentGuess >= _maxGuesses)
            {
                State = GameState.Lost;
            }
        }
    }
}
