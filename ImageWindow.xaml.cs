using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace QLNHANSU
{
    public partial class ImageWindow : Window
    {
        public ImageWindow(byte[] imageData)
        {
            InitializeComponent();
            if (imageData != null)
            {
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = ms;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    ImageControl.Source = bitmap;
                }
            }
        }

       
    }
}
