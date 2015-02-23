using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GissataletMVC.Models
{
    public class SecretNumber
    {
        //Fält
        public const int MaxNumberOfGuesses = 7;
        private int? _number;
        private List<GuessedNumber> _guessedNumbers;
        private GuessedNumber _lastGuessedNumber;
        private string _guessNr;
        private string _outcomeGuess;

        // Egenskaper
        public bool CanMakeGuess
        {
            get
            {
                return MaxNumberOfGuesses == Count ? false : true;
            }
        }
        public int Count
        {
            get
            {
                return _guessedNumbers.Count;
            }
        }
        public IList<GuessedNumber> GuessedNumber
        {
            get
            {
                return _guessedNumbers.AsReadOnly();
            }
        }
        public GuessedNumber LastGuessedNumber
        {
            get
            {
                return _lastGuessedNumber;
            }
        }
        public int? Number
        {
            get
            {
                return !CanMakeGuess ? _number : null;
            }
            private set
            {
                _number = value;
            }
        }

        public string OutcomeGuess
        {
            get
            {
                return _outcomeGuess;
            }
            private set
            {
                _outcomeGuess = value;
            }
        }

        public void Initialize()
        {
            //testing this commit thing!
            _guessedNumbers.Clear();
            _lastGuessedNumber.Outcome = Outcome.Undefined;
            Random random = new Random();
            _number = random.Next(1, 100);
        }

        public string PrintGuesses(int count)
        {
            _guessNr = string.Empty;
            if (count > 0)
            {
                switch (count)
                {
                    case 1: _guessNr = "Första";
                        break;
                    case 2: _guessNr = "Andra";
                        break;
                    case 3: _guessNr = "Tredje";
                        break;
                    case 4: _guessNr = "Fjärde";
                        break;
                    case 5: _guessNr = "Femte";
                        break;
                    case 6: _guessNr = "Sjätte";
                        break;
                    case 7: _guessNr = "Sjunde";
                        break;
                }
                if (_lastGuessedNumber.Outcome != Outcome.Right)
                {
                    _guessNr = _guessNr + " gissningen";
                }
            }
            return _guessNr;
        }


        public Outcome MakeGuess(int guess)
        {
            if (guess < 1 || guess > 100)
            {
                throw new ArgumentOutOfRangeException();
            }
            _lastGuessedNumber = new GuessedNumber { Number = guess };

            if (CanMakeGuess)
            {
                if (_guessedNumbers.Any(x => x.Number == guess))
                {
                    _lastGuessedNumber.Outcome = Outcome.OldGuess;
                    OutcomeGuess = string.Format("Du har redan gissat på {0}, välj ett annat tal!", guess);
                }
                else
                {
                    if (guess > _number)
                    {
                        _lastGuessedNumber.Outcome = Outcome.High;
                        OutcomeGuess = string.Format("{0} är för högt!", guess);
                    }
                    else if (guess < _number)
                    {
                        _lastGuessedNumber.Outcome = Outcome.Low;
                        OutcomeGuess = string.Format("{0} är för lågt!", guess);
                    }
                    else if (guess == _number)
                    {
                        _lastGuessedNumber.Outcome = Outcome.Right;
                        string rightAnswer = PrintGuesses(Count + 1); // Räknar upp rätt försök när användare anger rätt tal  
                        OutcomeGuess = string.Format("Grattis! Du klarade det på {0} försöket!", rightAnswer.ToLower());
                    }
                    _guessedNumbers.Add(_lastGuessedNumber);

                }
            }
            if (!CanMakeGuess && LastGuessedNumber.Outcome != Outcome.Right)
            {
                _lastGuessedNumber.Outcome = Outcome.NoMoreGuesses;
                OutcomeGuess = string.Format("{0} Inga fler gissningar! Det hemliga talet är {1}", _outcomeGuess, _number);
            }
            return _lastGuessedNumber.Outcome;
        }

        public SecretNumber()
        {
            _guessedNumbers = new List<GuessedNumber>();
            Initialize();

        }
    }
}
