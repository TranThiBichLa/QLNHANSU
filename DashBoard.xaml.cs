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
    /// Interaction logic for NhanVien.xaml
    /// </summary>
    public partial class DashBoard : Window
    {
        public DashBoard()
        {
            InitializeComponent();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Nạp UserNhanVienControl vào panelMainContent
            LoadUserControl(new CapNhat());
        }
        private void LoadUserControl(System.Windows.Controls.UserControl userControl )
        {
            // Xử lý cho WPF (không sử dụng Controls.Clear)
            panelMainContent.Children.Clear(); // Clear nội dung của Grid/Panel
            panelMainContent.Children.Add(userControl);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoadUserControl(new XetDuyetThongTin());

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            LoadUserControl(new QuanLyThongTin());
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            LoadUserControl(new CongTac());
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            // Đóng ứng dụng WPF
            System.Windows.Application.Current.Shutdown();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {// Thu nhỏ cửa sổ
            this.WindowState = WindowState.Minimized;

        }
    }
}
