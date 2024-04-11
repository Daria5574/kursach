using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace kursach.View
{
    /// <summary>
    /// Логика взаимодействия для WindowAddAuthor.xaml
    /// </summary>
    public partial class WindowAddAuthor : Window
    {
        public WindowAddAuthor()
        {
            InitializeComponent();
            label1.Content = "Имя автора";
            label2.Content = "Фамилия автора*";
            label3.Content = "Об авторе";
        }
    }
}
