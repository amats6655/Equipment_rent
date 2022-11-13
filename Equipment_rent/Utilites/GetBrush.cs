using System.Windows.Media;

namespace Equipment_rent.Utilites
{
    internal class GetBrush
    {

        public static Brush getBrush(char character)
        {

            var converter = new BrushConverter();
            Brush BgColor;
            if (character == 'А' || character == 'Ж' || character == 'Н' || character == 'Ф' || character == 'Ы')
            {
                BgColor = (Brush)converter.ConvertFromString("#1098ad");
                return BgColor;
            }
            if (character == 'Б' || character == 'З' || character == 'О' || character == 'Х' || character == 'Ь')
            {
                BgColor = (Brush)converter.ConvertFromString("#1e88e5");
                return BgColor;
            }
            if (character == 'В' || character == 'И' || character == 'П' || character == 'Ц' || character == 'Э')
            {
                BgColor = (Brush)converter.ConvertFromString("#ff8f00");
                return BgColor;
            }
            if (character == 'Г' || character == 'Й' || character == 'Р' || character == 'Ч' || character == 'Ю')
            {
                BgColor = (Brush)converter.ConvertFromString("#ff5252");
                return BgColor;
            }
            if (character == 'Д' || character == 'К' || character == 'С' || character == 'Ш' || character == 'Я')
            {
                BgColor = (Brush)converter.ConvertFromString("#0ca678");
                return BgColor;
            }
            if (character == 'Е' || character == 'Л' || character == 'Т' || character == 'Щ')
            {
                BgColor = (Brush)converter.ConvertFromString("#6741d9");
                return BgColor;
            }
            if (character == 'Ё' || character == 'М' || character == 'У' || character == 'Ъ')
            {
                BgColor = (Brush)converter.ConvertFromString("#ff6d00");
                return BgColor;
            }
            if (character == 'D')
            {
                BgColor = (Brush)converter.ConvertFromString("#DC143C");
                return BgColor;
            }
            if (character == 'R')
            {
                BgColor = (Brush)converter.ConvertFromString("#90ee90");
                return BgColor;
            }
            else return _ = (Brush)converter.ConvertFromString("#1098ad");
        }
    }
}
