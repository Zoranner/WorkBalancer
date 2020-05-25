using HandyControl.Controls;
using System;
using System.Timers;
using System.Windows;
using System.Windows.Threading;
using Window = System.Windows.Window;

namespace WorkBalancer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private BingWallpaper _BingWallpaper;
        private Timer _RestTimer;
        private StrongNotice _StrongNotice;
        private Timer _WorkTimer;
        private SettingWindow _SettingWindow;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _RestTimer = new Timer(3000);
            _WorkTimer = new Timer(5000);
            _StrongNotice = new StrongNotice();
            _BingWallpaper = new BingWallpaper();
            _RestTimer.Elapsed += RestTimer_Elapsed;
            _WorkTimer.Elapsed += WorkTimer_Elapsed;
            _WorkTimer.Enabled = true;
            WindowState = WindowState.Minimized;
        }

        private void PopupButton_Clicked(object sender, RoutedEventArgs e)
        {
            Growl.Error("ErrorMessage", "MainMessage");
            if (_SettingWindow == null)
            {
                _SettingWindow = new SettingWindow();
            }
            _SettingWindow.Show();
        }

        private void MainWindow_StateChanged(object sender, EventArgs e)
        {
            //if(WindowState == WindowState.Minimized)
            //{
            //    return;
            //}

            //TryLoadImage();
        }

        private void WorkTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                BalanceSwitch(true);
            }));
        }

        private void RestTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                BalanceSwitch(false);
            }));
        }

        private void BalanceSwitch(bool state)
        {
            _RestTimer.Enabled = state;
            _WorkTimer.Enabled = !state;
            if (state)
            {
                Show();
                WindowState = WindowState.Maximized;
                TryLoadImage();
            }
            else
            {
                Hide();
                WindowState = WindowState.Minimized;
            }
        }

        private void TryLoadImage()
        {
            try
            {
                imageBlock.Source = _BingWallpaper.LoadImage();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Growl.Error(exception.Message, "MainMessage");
            }
        }

        private void BingWallpaper_LoadStarted()
        {
        }

        private void BingWallpaper_LoadFinished()
        {
        }

        protected override void OnClosed(EventArgs e)
        {
            var collections = Application.Current.Windows;

            foreach (Window window in collections)
            {
                if (window != this)
                {
                    window.Close();
                }
            }

            base.OnClosed(e);
        }
    }
}