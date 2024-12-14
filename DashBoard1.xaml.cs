using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QLNHANSU
{
    /// <summary>
    /// Interaction logic for DashBoard1.xaml
    /// </summary>
    public partial class DashBoard1 : Window
    {
        public DashBoard1()
        {
            InitializeComponent();
        }
        private void LoadUserControl(System.Windows.Controls.UserControl userControl)
        {
            // Xử lý cho WPF (không sử dụng Controls.Clear)
            panelMainContent.Children.Clear();
            panelMainContent.Children.Add(userControl);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadUserControl(new XttCoBan());
        }

      
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();

        }

        

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            LoadUserControl(new XttHDLD());

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoadUserControl(new XttCongTac());

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {



            LoadUserControl(new DangKi1());
        }

    }
}
