using Microsoft.Win32;
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
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Human_Resources_Management_System.UserControls
{
    /// <summary>
    /// Interaction logic for Signature.xaml
    /// </summary>
    public partial class Signature : UserControl
    {
        public Signature()
        {
            InitializeComponent();
            // Set up DrawingAttributes for pen pressure sensitivity
            var drawingAttributes = new DrawingAttributes
            {
                Color = System.Windows.Media.Colors.Black,
                Width = 2,
                Height = 2,
                IgnorePressure = false, // Enable pressure sensitivity
                FitToCurve = true // Smooth the strokes
            };

            SignatureCanvas.DefaultDrawingAttributes = drawingAttributes;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PNG Image|*.png",
                Title = "Save Signature"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    RenderTargetBitmap rtb = new RenderTargetBitmap((int)SignatureCanvas.ActualWidth,
                                                                     (int)SignatureCanvas.ActualHeight,
                                                                     96, 96, System.Windows.Media.PixelFormats.Default);
                    rtb.Render(SignatureCanvas);

                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(rtb));
                    encoder.Save(fs);
                }
            }
        }

        // Clear the signature
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            SignatureCanvas.Strokes.Clear();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }
    }
}
