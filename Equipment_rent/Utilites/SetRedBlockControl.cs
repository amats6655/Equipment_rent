using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Equipment_rent.Utilites
{
    public class SetRedBlockControl
    {
         public static void RedBlockControl(Window window, string blockName)
        {
            Control block = window.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }
    }
}
