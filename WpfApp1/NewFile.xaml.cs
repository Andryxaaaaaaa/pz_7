using System;
using System.Collections.Generic;
using System.IO;
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
public  static class Vars
{
    public static string a;
}

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для NewFile.xaml
    /// </summary>
    public partial class NewFile : Window
    {
        public NewFile()
        {
            InitializeComponent();
        }

        public void OK_Click(object sender, RoutedEventArgs e)
        {
            CreateFile(name_file.Text);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void CreateFile(string nameFile)//создание файла
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            StreamWriter sw = new StreamWriter($"{path}data\\{nameFile}.txt");
            sw.Write("");
            sw.Close();
            Vars.a = name_file.Text;
            this.Close();
        }
    }
}
