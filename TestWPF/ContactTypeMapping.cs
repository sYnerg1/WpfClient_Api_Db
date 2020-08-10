using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TestWPF.Models
{
    [ValueConversion(typeof(int), typeof(string))]
    public class ContactTypeMapping : IValueConverter
    {
        public ContactTypeMapping()
        {

        }

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            // Возвращаем строку в формате 123.456.789 руб.
           if((int)value == 1)
            {
                return "Phone";
            }
           else
            {
                return "Email";
            }
    
        }
        public int k;
        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return 1;
        }
    }
}
