using System;
using Metron.Core.Data;

namespace Metron.Core.Models
{
    public class BeatPattern
    {
        private char[] _patternChars;
        private string _patternString;
        public EventHandler OnNextTaktHandler;


        public BeatPattern(int currentTickIndex, string patternString)
        {
            SetNewPatern(patternString);
            CurrentTickIndex = currentTickIndex;
        }

        #region Indexer

        public char this[int charIndex]
        {
            get => _patternChars[charIndex];
            set => _patternChars[charIndex] = value;
        }

        #endregion


        public char CurrentTick { get; private set; }
        public int CurrentTickIndex { get; set; }

        public string Measure { get; private set; }

        public string PatternString
        {
            get => _patternString;
            set => SetNewPatern(value);
        }


        private void SetMeasure()
        {
            Measure = PatternString.Length + "/" + 4;
        }

        private void SetNewPatern(string patternString)
        {
            // Input checks
            if (string.IsNullOrEmpty(patternString)) //Empty input string? => Setting default values
                patternString = Constants.DefaultPatternString;
            if (patternString.Length >
                Constants
                    .MaximumBeatPatternLength) // Max string length is Constants.MaximumBeatPatternLength => Trimming string; tick index default value
                patternString = patternString.Substring(0, Constants.MaximumBeatPatternLength);
            _patternChars = patternString.ToCharArray();
            _patternString = patternString;
            SetMeasure();
        }

        public void NextTick()
        {
            CurrentTickIndex++;

            if (CurrentTickIndex < _patternChars.Length) //Current tick index is inside tick pattern
            {
                CurrentTick = _patternChars[CurrentTickIndex];
            }
            else //Current tick index is out of bounds
            {
                CurrentTickIndex = 0;
                CurrentTick = PatternString[0];
                OnNextTaktHandler.Invoke(this, EventArgs.Empty);
            }
        }
    }
}