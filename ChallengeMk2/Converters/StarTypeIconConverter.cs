using System;
using System.Globalization;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ChallengeMk2.Converters
{
    class StarTypeIconConverter : IValueConverter
    {
        readonly Dictionary<string, string> StarTypeIcons = new Dictionary<string, string>
        {
            { "O (Blue-White) Star", "StarType_O.svg" },
            { "B (Blue-White) Star", "StarType_B.svg" },
            { "A (Blue-White) Star", "StarType_A.svg" },
            { "F (White) Star", "StarType_F.svg" },
            { "G (White-Yellow) Star", "StarType_G.svg" },
            { "K (Yellow-Orange) Star", "StarType_K.svg" },
            { "M (Red dwarf) Star", "StarType_M.svg" },
            { "L (Brown dwarf) Star", "StarType_L.svg" },
            { "T (Brown dwarf) Star", "StarType_T.svg" },
            { "Y (Brown dwarf) Star", "StarType_Y.svg" },
            { "T Tauri Star", "StarType_TTauri.svg" },
            { "Herbig Ae/Be Star", "StarType_Herbig.svg" },
            { "Wolf-Rayet Star", "StarType_WolfRayet.svg" },
            { "White Dwarf Star", "StarType_WhiteDwarf.svg" },
            { "Neutron Star", "StarType_Neutron.svg" },
            { "Black Hole", "StarType_BlackHole.svg" },
        };

        readonly string whiteDwarfType = "StarType_WhiteDwarf.svg";
        readonly string otherType = "StarType_Other.svg";
        readonly string path = "resource://ChallengeMk2.Resources.";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string iconName;

            if (StarTypeIcons.ContainsKey(value.ToString()))
            {
                iconName = StarTypeIcons[value.ToString()];
            }
            else
            {
                iconName = value.ToString().Contains("White") && value.ToString().Contains("Dwarf") ? whiteDwarfType : otherType;
            }

            return path + iconName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException($"{nameof(NullStringConverter)} cannot be used on TWO-WAY bindings.");
    }
}
