using BLL;
using DAL;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace QLNHANSU
{
    public partial class ThongKe : UserControl
    {
        private readonly ChamCongBLL chamCongBLL;
        private readonly HopDongLaoDongBLL hopDongLaoDongBLL;

        private readonly BangLuongBLL luongBLL;
        

        public ThongKe()
        {
            InitializeComponent();

            if (chartCoBan == null)
            {
                MessageBox.Show("Biểu đồ chưa được khởi tạo. Kiểm tra XAML.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            chamCongBLL = new ChamCongBLL();
            luongBLL = new BangLuongBLL();

            // Khởi tạo các lớp Access
            var hopDongAccess = new HopDongLaoDongAccess();
            var nhanVienAccess = new NhanVienAccess();

            // Truyền các đối tượng Access vào BLL
            hopDongLaoDongBLL = new HopDongLaoDongBLL(hopDongAccess, nhanVienAccess);
            this.DataContext = this; // Gán DataContext
            InitializeChart();
            cbNamCoBan.SelectedIndex = 0;
        }


        public ChartValues<int> DataValues { get; set; } = new ChartValues<int>();
        public List<string> MonthLabels { get; set; } = new List<string>
{
    "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5",
    "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10",
    "Tháng 11", "Tháng 12"
};





        private void InitializeChart()
        {
            // Khởi tạo giá trị mặc định
            DataValues = new ChartValues<int>();
            for (int i = 0; i < 12; i++)
            {
                DataValues.Add(0); // Giá trị ban đầu là 0 cho tất cả các tháng
            }

            MonthLabels = new List<string>
    {
        "1", "2", "3", "4", "5",
        "6", "7", "8", "9", "10",
        "11", "12"
    };

            chartCoBan.Series = new SeriesCollection
{
    new ColumnSeries
    {
        Title = "Số lượng nhân viên",
        Values = DataValues,
        DataLabels = true,
        LabelPoint = point => $"{point.Y}"
    }
};


            chartCoBan.AxisX = new AxesCollection
    {
        new Axis
    {
        Title = "Tháng",
        Labels = MonthLabels,
        LabelsRotation = 0 // Xoay nhãn, bạn có thể đặt giá trị 45 hoặc 90

    }
    };

            chartCoBan.AxisY = new AxesCollection
    {
        new Axis
        {
            Title = "Số lượng",
            LabelFormatter = value => value.ToString(), // Hiển thị số nguyên
            Separator = new LiveCharts.Wpf.Separator
            {
                Step = 1 // Hiển thị từng bước giá trị là 1
            },
            MinValue = 0, // Giá trị nhỏ nhất là 0
            MaxValue = 10 // Giá trị lớn nhất là 10 (có thể điều chỉnh theo dữ liệu)
        }

    };
        }





        private void cbNamCoBan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbNamCoBan.SelectedItem is ComboBoxItem selectedItem)
            {
                if (int.TryParse(selectedItem.Content.ToString(), out int selectedYear))
                {
                    UpdateChartData(selectedYear); // Cập nhật dữ liệu biểu đồ
                }
            }
        }

        private void UpdateChartData(int year)
        {
            // Lấy danh sách số lượng nhân viên distinct theo từng tháng
            var monthlyCounts = chamCongBLL.GetDistinctNhanVienByYear(year);

            // Kiểm tra nếu không có dữ liệu
            if (monthlyCounts.All(count => count == 0))
            {
                MessageBox.Show($"Không có dữ liệu chấm công trong năm {year}.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            // Cập nhật giá trị cho biểu đồ
            DataValues.Clear();
            foreach (var count in monthlyCounts)
            {
                DataValues.Add(count);
            }
        }
        private void cbNamViTri_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbNamViTri.SelectedItem is ComboBoxItem selectedYearItem &&
                cbViTri.SelectedItem is ComboBoxItem selectedViTriItem)
            {
                int selectedYear = int.Parse(selectedYearItem.Content.ToString());
                string selectedViTri = selectedViTriItem.Content.ToString();

                UpdateChart(selectedViTri, selectedYear);
            }
        }

        private void cbViTri_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbNamViTri_SelectionChanged(sender, e); // Gọi lại hàm để cập nhật biểu đồ
        }
        private void UpdateChart(string viTri, int year)
        {
            var data = hopDongLaoDongBLL.GetNhanVienTheoViTri(viTri, year);

            // Cập nhật trục X và Y
            var labels = data.Select(d => d.Item1).ToList(); // Danh sách tên phòng ban/bộ phận/chức vụ
            var values = data.Select(d => d.Item2).ToList(); // Danh sách số lượng nhân viên

            chartViTri.AxisX.Clear();
            chartViTri.AxisX.Add(new Axis
            {
                Title = viTri,
                Labels = labels
            });

            chartViTri.Series.Clear();
            chartViTri.Series.Add(new ColumnSeries
            {
                Title = "Số lượng nhân viên",
                Values = new ChartValues<int>(values),
               
                DataLabels = true,
                LabelPoint = point => $"{point.Y}"
            });

            chartViTri.AxisY.Clear();
            chartViTri.AxisY.Add(new Axis
            {
               Title = "Số lượng",
            LabelFormatter = value => value.ToString(), // Hiển thị số nguyên
            Separator = new LiveCharts.Wpf.Separator
            {
                Step = 1 // Hiển thị từng bước giá trị là 1
            },
            MinValue = 0, // Giá trị nhỏ nhất là 0
            MaxValue = 10 // Giá trị lớn nhất là 10 (có thể điều chỉnh theo dữ liệu)
            });
        }
        private void cbNamLuong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbNamLuong.SelectedItem is ComboBoxItem selectedItem)
            {
                if (int.TryParse(selectedItem.Content.ToString(), out int selectedYear))
                {
                    UpdateLuongChart(selectedYear);
                }
            }
        }

        private void UpdateLuongChart(int year)
        {
            var data = luongBLL.GetTongLuongTheoNam(year);

            // Lấy danh sách tháng và tổng lương
            var labels = data.Select(d => $"Tháng {d.Item1}").ToList(); // Danh sách tháng
            var values = data.Select(d => d.Item2).ToList(); // Tổng lương (đã chia cho 1.000.000)

            // Cập nhật trục X và Y
            chartLuong.AxisX.Clear();
            chartLuong.AxisX.Add(new Axis
            {
                Title = "Tháng",
                Labels = labels
            });

            chartLuong.Series.Clear();
            chartLuong.Series.Add(new ColumnSeries
            {
                Title = "Tổng lương (Triệu VND)",
                Values = new ChartValues<int>(values),
                DataLabels = true,
                LabelPoint = point => $"{point.Y}"
            });

            chartLuong.AxisY.Clear();
            chartLuong.AxisY.Add(new Axis
            {
                Title = "Tổng lương (Triệu VND)",
                LabelFormatter = value => value.ToString(), // Hiển thị số nguyên
                Separator = new LiveCharts.Wpf.Separator
                {
                    Step = 10 // Hiển thị từng bước giá trị là 1
                },
                MinValue = 0 // Giá trị nhỏ nhất là 0
                
            
        });
        }


    }
}
