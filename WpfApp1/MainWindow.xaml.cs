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
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;


namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string proshliy = "";
        public MainWindow()
        {
            InitializeComponent();
            FileInfo fl = new FileInfo($"{AppDomain.CurrentDomain.BaseDirectory}data\\{Vars.a}.txt");
            
            if(Vars.a != null )RightText.Text = $"{DateTime.Now} размер{fl.Length}";
        }
        Boolean bold = false;
        Boolean kurs = false;
        Boolean underline = false;
        Boolean save = false;

        private void TextKeyDownEvent(object sender, KeyEventArgs k)
        {
            
            RightText.Text = $"{DateTime.Now} размер{new FileInfo($"{AppDomain.CurrentDomain.BaseDirectory}data\\{Vars.a}.txt").Length/1024}КБ";
            if (k.Key == Key.Tab)//табуляция
            {
                k.Handled = true;
                txtbox.AcceptsTab = true;
                
            }
            if(k.Key == Key.Enter)
            {
                txtbox.AcceptsReturn = true;
            }   
            if (k.Key == Key.LeftShift && list_files.Items.Contains(Vars.a) == false) list_files.Items.Add(Vars.a);
            int row = txtbox.GetLineIndexFromCharacterIndex(txtbox.CaretIndex);
            int col = txtbox.CaretIndex - txtbox.GetLineIndexFromCharacterIndex(row);
           
            if(list_files.SelectedItem != null && list_files.SelectedItem != proshliy)
            {
                txtbox.Text = File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}data\\{list_files.Items.GetItemAt(list_files.SelectedIndex).ToString()}.txt");
                proshliy = list_files.SelectedItem.ToString();
                LeftText.Text = $"строка: {row + 1} столбец: {col + 1} Требуется сохранение {proshliy}";
            }
            LeftText.Text = $"строка: {row + 1} столбец: {col + 1} Требуется сохранение";//выводит снизу информацию(прям снизу у программы)

        }
      

        private void New(object sender, RoutedEventArgs e)
        {
            NewFile NewFileWindow = new NewFile();
            
            if (NewFileWindow.ShowDialog() == false)
            {

                list_files.Items.Add(Vars.a);
            }

        }

        private void SaveFile(object sender, RoutedEventArgs e)
        {
           
            StreamWriter sw = new StreamWriter($"{AppDomain.CurrentDomain.BaseDirectory}data\\{Vars.a}.txt");
            sw.Write(txtbox.Text);
            sw.Close();
            save = true;
            int row = txtbox.GetLineIndexFromCharacterIndex(txtbox.CaretIndex);
            int col = txtbox.CaretIndex - txtbox.GetLineIndexFromCharacterIndex(row);
            LeftText.Text = $"строка: {row + 1} столбец: {col + 1} Не требуется сохранение";//выводит снизу информацию(прям снизу у программы)

        }
        private void DeleteFile(object sender, RoutedEventArgs e)
        {
            File.Delete(Vars.a);
            list_files.Items.Remove(Vars.a);
            txtbox.Clear();//функция очистки файла
        }

        private void Kursiv(object sender, RoutedEventArgs e)//курсив текста(когда он будто немножко падает)
        {
            if (kurs == false)
            {
                txtbox.FontStyle = FontStyles.Italic;
                kurs = true;
            }
            else
            {
                txtbox.FontStyle = FontStyles.Normal;
                kurs = false;
            }
        }

        private void Bold_(object sender, RoutedEventArgs e)
        {
            if (bold == false)
            {
                txtbox.FontWeight = FontWeights.Bold;//жирный текст
                bold = true;
            }
            else
            {
                txtbox.FontWeight = FontWeights.Normal;
                bold = false;
            }
        }

        private void Podcherk_(object sender, RoutedEventArgs e)
        {
            if (underline == false)
            {
                txtbox.TextDecorations = TextDecorations.Underline;//подчеркнутный текст
                underline = true;
            }
            else
            {
                txtbox.TextDecorations = TextDecorations.Baseline;
                underline = false;
            }
        }

        private void Create_Sample(object sender,RoutedEventArgs e)//пример текста
        {
            StreamWriter sw = new StreamWriter($"{AppDomain.CurrentDomain.BaseDirectory}data\\file1.txt");
            sw.Write(txtbox.Text);
            sw.Close();
        }

        private void Load_Sample(object sender, RoutedEventArgs e)//пример текста
        {
            txtbox.Text = File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}data\\file1.txt");
        }
    
        private void About_authors(object sender, RoutedEventArgs e)//о авторе
        {
            txtbox.Text = "Андрей Полянский";
        }

        private void About_program(object sender,RoutedEventArgs e)//о программе
        {
            txtbox.Text = "Здесь пока ничего нет";
        }

        private void Open(object sender,RoutedEventArgs e)//open
        {
            Window_open wp = new Window_open();
            if(wp.ShowDialog() == false)
            {
                list_files.Items.Add(Vars.a);
            }
        }
    }
}
