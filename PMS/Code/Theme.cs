using System.Collections.Generic;
using System.Drawing;

namespace BPMS.Code
{
    public enum ThemePreset
    {
        Default,
        Red,
        Blue,
        Yellow,
        Green,
        Black,
        TKE
    }

    public class Theme
    {
        protected static Dictionary<ThemePreset, Theme> ThemeDict = new Dictionary<ThemePreset, Theme>()
        {
            { ThemePreset.Default, new Theme { Name = ThemePreset.Default.ToString(),
                MenuBackColor = Color.Gainsboro,
                MenuForeColor = Color.Black,
                MainBackColor = SystemColors.Control,
                MainForeColor = Color.Black,
                NewTeamButtonBackColor = Color.LimeGreen,
                NewTeamButtonForeColor =Color.Black,
                QueueTeamButtonBackColor = Color.Green,
                QueueTeamButtonForeColor = Color.Black,
                WinnerButtonBackColor = SystemColors.ControlLight,
                WinnerButtonForeColor = Color.Black,
                WhosPlayingBackColor = Color.LightGray
            }},
            { ThemePreset.Blue, new Theme { Name = ThemePreset.Blue.ToString(),
                MenuBackColor = Color.SteelBlue,
                MenuForeColor = Color.MidnightBlue,
                MainBackColor = Color.CornflowerBlue,
                MainForeColor = Color.MidnightBlue,
                NewTeamButtonBackColor = Color.LightSkyBlue,
                NewTeamButtonForeColor =Color.Black,
                QueueTeamButtonBackColor = Color.DeepSkyBlue,
                QueueTeamButtonForeColor = Color.Black,
                WinnerButtonBackColor = Color.Blue,
                WinnerButtonForeColor = Color.MidnightBlue,
                WhosPlayingBackColor = Color.RoyalBlue
            }},
            { ThemePreset.Red, new Theme { Name = ThemePreset.Red.ToString(),
                MenuBackColor = Color.LightCoral,
                MenuForeColor = Color.Firebrick,
                MainBackColor = Color.IndianRed,
                MainForeColor = Color.Maroon,
                NewTeamButtonBackColor = Color.Red,
                NewTeamButtonForeColor =Color.Red,
                QueueTeamButtonBackColor = Color.MistyRose,
                QueueTeamButtonForeColor = Color.Red,
                WinnerButtonBackColor = Color.Salmon,
                WinnerButtonForeColor = Color.Red,
                WhosPlayingBackColor = Color.Brown
            }},
            { ThemePreset.Yellow, new Theme { Name = ThemePreset.Yellow.ToString(),
                MenuBackColor = Color.Goldenrod,
                MenuForeColor = Color.Olive,
                MainBackColor = Color.Khaki,
                MainForeColor = Color.Black,
                NewTeamButtonBackColor = Color.LemonChiffon,
                NewTeamButtonForeColor =Color.Olive,
                QueueTeamButtonBackColor = Color.Gold,
                QueueTeamButtonForeColor = Color.Olive,
                WinnerButtonBackColor = Color.Yellow,
                WinnerButtonForeColor = Color.Black,
                WhosPlayingBackColor = Color.DarkKhaki
            }},
            { ThemePreset.Black, new Theme {Name = ThemePreset.Black.ToString(),
                MenuBackColor = Color.Black,
                MenuForeColor = Color.WhiteSmoke,
                MainBackColor = SystemColors.ControlDarkDark,
                MainForeColor = Color.WhiteSmoke,
                NewTeamButtonBackColor = Color.Gray,
                NewTeamButtonForeColor =Color.Black,
                QueueTeamButtonBackColor = Color.DimGray,
                QueueTeamButtonForeColor = Color.Black,
                WinnerButtonBackColor = Color.Black,
                WinnerButtonForeColor = Color.White,
                WhosPlayingBackColor = SystemColors.InactiveCaptionText
            }},
            { ThemePreset.Green, new Theme {Name = ThemePreset.Green.ToString(),
                MenuBackColor = Color.DarkSeaGreen,
                MenuForeColor = Color.DarkGreen,
                MainBackColor = Color.LimeGreen,
                MainForeColor = Color.DarkOliveGreen,
                NewTeamButtonBackColor = Color.PaleGreen,
                NewTeamButtonForeColor =Color.Black,
                QueueTeamButtonBackColor = Color.Green,
                QueueTeamButtonForeColor = Color.Black,
                WinnerButtonBackColor = Color.PaleGreen,
                WinnerButtonForeColor = Color.DarkOliveGreen,
                WhosPlayingBackColor = Color.DarkSeaGreen
            }},
            { ThemePreset.TKE, new Theme {Name = ThemePreset.TKE.ToString(),
                MenuBackColor = Color.Gray,
                MenuForeColor = Color.Crimson,
                MainBackColor = Color.DarkGray,
                MainForeColor = Color.DarkRed,
                NewTeamButtonBackColor = Color.Firebrick,
                NewTeamButtonForeColor =Color.DimGray,
                QueueTeamButtonBackColor = Color.Maroon,
                QueueTeamButtonForeColor = Color.DimGray,
                WinnerButtonBackColor = Color.Gray,
                WinnerButtonForeColor = Color.DarkRed,
                WhosPlayingBackColor = Color.Maroon
            }}
        };
        public string Name { get; set; }
        public Color MenuBackColor { get; set; }
        public Color MenuForeColor { get; set; }
        public Color MainBackColor { get; set; }
        public Color MainForeColor { get; set; }
        public Color NewTeamButtonBackColor { get; set; }
        public Color NewTeamButtonForeColor { get; set; }
        public Color QueueTeamButtonBackColor { get; set; }
        public Color QueueTeamButtonForeColor { get; set; }
        public Color WinnerButtonBackColor { get; set; }
        public Color WinnerButtonForeColor { get; set; }
        public Color WhosPlayingBackColor { get; set; }

        public static Theme GetTheme( ThemePreset themeSelection)
        {
            return ThemeDict[themeSelection];
        }

        /// <summary>
        /// Gets a string of color names for the save file. This will eventually not be needed.
        /// </summary>
        /// <returns></returns>
        public string[] ThemeColors()
        {
            return new string[] { MenuBackColor.Name, MenuForeColor.Name, MainBackColor.Name,
                MainForeColor.Name, NewTeamButtonBackColor.Name, QueueTeamButtonBackColor.Name,
                NewTeamButtonForeColor.Name, WinnerButtonBackColor.Name, WhosPlayingBackColor.Name };
        }
    }
}
