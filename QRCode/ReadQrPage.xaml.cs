using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using Microsoft.Win32;

namespace QRCode;

public partial class ReadQrPage : Page
{
    private QRCodeDecoder _decoder = new QRCodeDecoder();

    public ReadQrPage()
    {
        InitializeComponent();
    }

    private void ReadQrButton_OnClick(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Filter = "Png|*.png"
        };
        if (openFileDialog.ShowDialog() == true)
        {
            QrText.Text = _decoder.Decode(new QRCodeBitmapImage(new Bitmap(openFileDialog.FileName)));
            QrImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
        }
    }
}