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

namespace Homework10._8
{
    /// <summary>
    /// Interaction logic for UserCheck.xaml
    /// </summary>
    public partial class UserCheck : Window
    {
        public UserCheck()
        {
            InitializeComponent();

        }

        
        private void Button_ConsLogin_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(1);
            mainWindow.Show();
            this.Close();
        }

        private void Button_ManagLogin_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(2);
            mainWindow.Show();
            this.Close();
        }
    }
}
