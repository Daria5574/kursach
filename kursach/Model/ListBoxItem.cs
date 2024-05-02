using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach.Model
{
    public class ListBoxItem
    {
        public int ThemeId { get; set; }
        public string ThemeName { get; set; }
        public bool IsSelected { get; set; }
    }

}
