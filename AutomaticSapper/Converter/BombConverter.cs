using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace AutomaticSapper.Converter
{
    public class BombConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bomb = value as string;
            var image = new Image
                {
                    Source = new BitmapImage(new Uri("../bomb_2.png", UriKind.Relative)),
                    Height = 15,
                    Width = 15
                };
            if (bomb != null && bomb.Equals("^"))
                return image;

            if (bomb == null || !bomb.Equals("&")) return bomb;
            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
