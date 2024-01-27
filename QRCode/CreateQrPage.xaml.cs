using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using MessagingToolkit.QRCode.Codec;
using Microsoft.Win32;

namespace QRCode;

public partial class CreateQrPage : Page
{
    private QRCodeEncoder _encoder = new QRCodeEncoder();
    private Bitmap _bitmap;
    
    public CreateQrPage()
    {
        InitializeComponent();
    }

    private void CreateQrButton_OnClick(object sender, RoutedEventArgs e)
    {
        _encoder.QRCodeScale = 8;
        _bitmap = _encoder.Encode(QrText.Text);
        QrImage.Source = BitmapToBitmapImage();
        SaveFileDialog saveFileDialog = new SaveFileDialog
        {
            Filter = "Png|*.png",
            FileName = QrText.Text
        };
        if (saveFileDialog.ShowDialog()==true)
        {
            _bitmap.Save(saveFileDialog.FileName,ImageFormat.Png);
        }
    }

    private BitmapImage BitmapToBitmapImage()
    {
        BitmapImage bitmapImage = new BitmapImage();
        using (MemoryStream memory = new MemoryStream())
        {
            _bitmap.Save(memory,ImageFormat.Png);
            memory.Position = 0;
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = memory;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();
            bitmapImage.Freeze();
            return bitmapImage;
        }
    }
}