using System.Runtime.InteropServices;

namespace Wordle
{
    public class WordleGame
    {
        public event Action StateChanged;
        public string TargetWord { get; private set; }
        public WordleChar[][] Guesses { get; private set; }
        public GameState State { get; private set; } = GameState.Playing;

        private int _maxGuesses;
        private int _wordLength;
        private int _currentGuess = 0;
        private int _currentChar = 0;
        private KeyboardService _keyboardService;

        public WordleGame(KeyboardService keyboardService)
        {
            _keyboardService = keyboardService;
            _keyboardService.OnKeyPressed += HandleInput;
            _maxGuesses = 6;
            _wordLength = 5;
            TargetWord = "WORDL";
            Initialize();
        }

        private void Initialize()
        {
            Guesses = new WordleChar[_maxGuesses][];
            for (int i = 0; i < _maxGuesses; i++)
            {
                Guesses[i] = new WordleChar[_wordLength];
                for (int j = 0; j < _wordLength; j++)
                {
                    Guesses[i][j] = new WordleChar();
                }
            }
        }

        private void HandleInput(char input)
        {
            switch (input)
            {
                case '\n':
                    if (_currentChar == _wordLength)
                    {
                        SubmitGuess();
                    }
                    break;
                case '\b':
                    if (_currentChar > 0)
                    {
                        _currentChar--;
                        Guesses[_currentGuess][_currentChar].GuessedChar = ' ';
                    }
                    break;
                default:
                    if (input >= 'A' && input <= 'Z' && _currentChar <= _wordLength)
                    {
                        Guesses[_currentGuess][_currentChar].GuessedChar = input;
                        _currentChar++;
                    }
                    break;
            }
            StateChanged?.Invoke();
        }

        public void SubmitGuess()
        {
            for (int i = 0; i < Guesses[_currentGuess].Length; i++)
            {
                SubmitChar(i);
            }
            UpdateGameState();
            _currentGuess++;
            _currentChar = 0;
        }
        private void SubmitChar(int index)
        {
            if (Guesses[_currentGuess][index].GuessedChar.Equals(TargetWord[index]))
            {
                Guesses[_currentGuess][index].CorrectState = Correctness.Correct;
            }
            else if (TargetWord.Contains(Guesses[_currentGuess][index].GuessedChar))
            {
                Guesses[_currentGuess][index].CorrectState = Correctness.Present;
            }
            else
            {
                Guesses[_currentGuess][index].CorrectState = Correctness.Incorrect;
            }
        }
        private void UpdateGameState()
        {
            bool correct = true;
            foreach (WordleChar g in Guesses[_currentGuess])
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
            else if (_currentGuess >= _maxGuesses)
            {
                State = GameState.Lost;
            }
        }
    }
}
