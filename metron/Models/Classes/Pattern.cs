namespace Metron
{
    public class Pattern
    {
        private Pattern() { }
        public Pattern(int currentTickIndex = 0, string patternString = "1000")
        {
            if (patternString == "")
            {
                patternString = "1";
                currentTickIndex = 0;
            }
            
            PatternString = patternString;

            if (currentTickIndex < PatternString.Length)
            {
                CurrentTickIndex = currentTickIndex;
                CurrentTick = PatternString[CurrentTickIndex];
                
            }
            else
            {
                CurrentTickIndex = 0;
                CurrentTick = PatternString[0];
            }

        }
        public string PatternString { get;  set; } 
        public char CurrentTick { get; private set; } 
        public int CurrentTickIndex { get; set; } = 0;
        public static Pattern operator + (Pattern x, int y)
        {
            return new Pattern(x.CurrentTickIndex + y, x.PatternString);
        }
  
    }
}
