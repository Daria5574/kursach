using kursach.Model;
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
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;


namespace kursach.View
{
    /// <summary>
    /// Логика взаимодействия для WindowReader.xaml
    /// </summary>
    public partial class WindowReader : Window
    {
        Dictionary<string, string> chapters = new Dictionary<string, string>();

        public WindowReader(Book currentBook)
        {
            InitializeComponent();

            DataContext = currentBook;
            string filePath = currentBook.The_Path_To_The_File;
            numberTextBox.Text = 16.ToString();

            if (File.Exists(filePath))
            {
                XDocument document = XDocument.Load(filePath);
                XNamespace fb2Namespace = "http://www.gribuser.ru/xml/fictionbook/2.0";

                var paragraphs = document.Descendants(fb2Namespace + "p").ToList();

                string text = "";
                foreach (XElement paragraph in paragraphs)
                {
                    text += paragraph.Value + "\n";
                }
                textBlock.Text = text;
                
                string currentChapterTitle = "";

                foreach (XElement element in document.Descendants())
                {
                    if (element.Name == fb2Namespace + "section")
                    {
                        currentChapterTitle = "";
                    }
                    else if (element.Name == fb2Namespace + "title")
                    {
                        currentChapterTitle = element.Value;
                        chapters.Add(currentChapterTitle, ""); 
                    }
                    else if (element.Name == fb2Namespace + "p")
                    {
                        if (!string.IsNullOrEmpty(currentChapterTitle))
                        {
                            chapters[currentChapterTitle] += element.Value + "\n";
                        }
                    }
                }

                foreach (string chapterTitle in chapters.Keys)
                {
                    string modifiedChapterTitle = Regex.Replace(chapterTitle, @"(?<=\p{Ll})(?=\p{Lu})", " ");
                    contentListBox.Items.Add(modifiedChapterTitle);
                }

            }
            else
            {
                MessageBox.Show("Файл не найден!");
            }

        }
        private void chaptersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedChapter = (string)contentListBox.SelectedItem;
            string originalChapterTitle = chapters.FirstOrDefault(x => Regex.Replace(x.Key, @"(?<=\p{Ll})(?=\p{Lu})", " ") == selectedChapter).Key;

            textBlock.Text = chapters[originalChapterTitle];
        }
        private void upButton_Click(object sender, RoutedEventArgs e)
        {
            double value = double.Parse(numberTextBox.Text);
            value++;

            numberTextBox.Text = value.ToString();
            textBlock.FontSize = value;
        }

        private void downButton_Click(object sender, RoutedEventArgs e)
        {
            double value = double.Parse(numberTextBox.Text);
            value--;

            numberTextBox.Text = value.ToString();
            textBlock.FontSize = value;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            double value = double.Parse(numberTextBox.Text);
            numberTextBox.Text = value.ToString();
            textBlock.FontSize = value;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Book currentBook = (Book)DataContext;
            WindowBookDetails wAuthor = new WindowBookDetails(currentBook);
            wAuthor.Show();
            Close();
        }

 
    }

}

