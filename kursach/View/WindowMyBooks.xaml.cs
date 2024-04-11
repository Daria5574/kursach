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
    /// Логика взаимодействия для WindowMyBooks.xaml
    /// </summary>
    public partial class WindowMyBooks : Window
    {
        public WindowMyBooks()
        {
            InitializeComponent();
        }
        private void NavigateToMainPage(object sender, MouseButtonEventArgs e)
        {
            WindowMainPage wMainPage = new WindowMainPage();
            wMainPage.Show();
            Close();
        }

        private void NavigateToAddBook(object sender, MouseButtonEventArgs e)
        {
            WindowAddBook wAddBook = new WindowAddBook();
            wAddBook.Show();
            Close();
        }
    }
}
