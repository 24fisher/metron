using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Metron
{
    public class Pattern
    {
        private char[] _patternChars;
        private string _patternString;
        public EventHandler OnNextTaktHandler;

        #region Indexer
        public char this[int charIndex]
        {
            get => _patternChars[charIndex];
            set => _patternChars[charIndex] = value;
        }
        #endregion

        #region Constructor
        private Pattern() { }
        public Pattern(int currentTickIndex = 0, string patternString = "1000")
        {
            SetNewPatern(patternString);
            CurrentTickIndex = 0;
        }
        #endregion

        #region Private members
        private void SetMeasure()
        {
            Measure = PatternString.Length + "/" + 4;
        }
        private void SetNewPatern(string patternString)
        {

            // Input checks
            if (patternString == "") //Empty input string? => Setting default values
            {
                patternString = "1";

            }
            if (patternString.Length > 20) // Max string length is 20 => Trimming string; tick index default value
            {
                patternString = patternString.Substring(0, 20);

            }
            _patternChars = patternString.ToCharArray();
            _patternString = patternString;
            SetMeasure();
        }
        #endregion

        #region Public members
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
                OnNextTaktHandler.Invoke(this,new EventArgs());
            }

        }
        #endregion

        #region Properties
        public char CurrentTick { get; private set; }
        public int CurrentTickIndex { get; set; } = 0;
        public string Measure { get; private set; }
        public string PatternString
        {
            get { return _patternString; }
            set
            {
                SetNewPatern(value);
            }
        }
        #endregion


        
    }
}
