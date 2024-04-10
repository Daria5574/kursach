using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
    /// Логика взаимодействия для WindowAddBook.xaml
    /// </summary>
    public partial class WindowAddBook : Window
    {
        public WindowAddBook()
        {
            InitializeComponent();
            label1.Content = "Название*";
            label2.Content = "Путь к файлу";
            label3.Content = "Автор";
            label4.Content = "Количество печатных страниц";
            label5.Content = "Год написания";
            label6.Content = "Год издания";
            label7.Content = "ISBN";
            label8.Content = "Время на чтение";
            label9.Content = "О книге";
            label10.Content = "Возрастной рейтинг";
        }
    }
}
