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
using System.Windows.Navigation;
using System.Windows.Shapes;
using kursach.View;

namespace kursach
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Authorization_Click(object sender, RoutedEventArgs e)
        {
            WindowAuthorization wAut = new WindowAuthorization();
            wAut.Show();
            Close();
        }

        private void Button_Registration_Click(object sender, RoutedEventArgs e)
        {
            WindowRegistration wReg = new WindowRegistration();
            wReg.Show();
            Close();
        }
    }
}
