using System;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Phis
{
    /// <summary>
    /// Логика взаимодействия для WindowException.xaml
    /// </summary>
    public partial class WindowException : Window
    {
        private Bitmap _bitmap;
        private BitmapSource _source;
        private readonly string _message;

        private BitmapSource GetSource()
        {
            if (_bitmap == null)
            {
                _bitmap = new Bitmap(@".\Source\3213566478");
            }
            var handle = _bitmap.GetHbitmap();
            return Imaging.CreateBitmapSourceFromHBitmap(
                    handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }
        public WindowException(string ex)
        {
            InitializeComponent();
            textBox.Text = ex;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _source = GetSource();
            image.Source = _source;
            ImageAnimator.Animate(_bitmap, OnFrameChanged);
        }
        private void FrameUpdatedCallback()
        {
            ImageAnimator.UpdateFrames();
            _source?.Freeze();
            _source = GetSource();
            image.Source = _source;
            InvalidateVisual();
        }
        private void OnFrameChanged(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                                    new Action(FrameUpdatedCallback));
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
