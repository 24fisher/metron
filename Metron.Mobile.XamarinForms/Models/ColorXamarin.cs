using Metron.Core.Interfaces;
using Xamarin.Forms;

namespace Metron.Mobile.XamarinForms.Models
{
    public class ColorXamarin : IColor
    {
        public string GetColor(string color)
        {
            switch (color)
            {
                case "White":
                     return Color.White.ToString();
                    
                case "Black":
                    return Color.Black.ToString();

                case "Red":
                    return Color.Red.ToString();

                case "Green":
                    return Color.Green.ToString();

                default:
                    return Color.Black.ToString();
            }
        }
    }
}