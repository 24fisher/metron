using System.Drawing;
using Metron.Core.Interfaces;

namespace MetronWPF.Models
{
    public class ColorWPF : IColor
    {
        public string GetColor(string color)
        {
            switch (color)
            {
                case "White":
                    return Color.White.ToKnownColor().ToString();

                case "Black":
                    return Color.Black.ToKnownColor().ToString();

                case "Red":
                    return Color.Red.ToKnownColor().ToString();

                case "Green":
                    return Color.Green.ToKnownColor().ToString();

                default:
                    return Color.Black.ToKnownColor().ToString();
            }
        }
    }
}