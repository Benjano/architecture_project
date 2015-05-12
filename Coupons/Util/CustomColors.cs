using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Coupons.Util
{
    public class CustomColors
    {
        public static SolidColorBrush YELLOW = (SolidColorBrush)(new BrushConverter().ConvertFrom("#EEBA4C"));
        public static SolidColorBrush RED = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3493B"));
        public static SolidColorBrush BLUE = (SolidColorBrush)(new BrushConverter().ConvertFrom("#23B5AF"));
        public static SolidColorBrush BLUE_LIGHT = (SolidColorBrush)(new BrushConverter().ConvertFrom("#A9DDD9"));
        public static SolidColorBrush BLUE_DARK = (SolidColorBrush)(new BrushConverter().ConvertFrom("#3A3A3C"));
    }
}
