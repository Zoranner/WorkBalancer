using HandyControl.Controls;
using HandyControl.Data;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media.Imaging;
using Window = System.Windows.Window;

namespace WorkBalancer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private StrongNotice _StrongNotice;
        private const string BING_XML_PATH = "http://cn.bing.com/HPImageArchive.aspx?idx=0&n=1";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _StrongNotice = new StrongNotice();
        }

        private void PopupButton_Clicked(object sender, RoutedEventArgs e)
        {
            Notification.Show(_StrongNotice, ShowAnimation.HorizontalMove, true);
        }

        private void MainWindow_Activated(object sender, EventArgs e)
        {
            try
            {
                var cachesPath = $"{AppDomain.CurrentDomain.BaseDirectory}\\caches";

                if (!Directory.Exists(cachesPath))
                {
                    Directory.CreateDirectory(cachesPath);
                }

                var todayImagePath = $"{cachesPath}\\bing-{DateTime.Now:yyyyMMdd}.jpg";
                Console.WriteLine(todayImagePath);

                if (!File.Exists(todayImagePath))
                {
                    WebRequest webRequest = WebRequest.Create(GetBingUrl());
                    WebResponse webResponse = webRequest.GetResponse();
                    using (Stream stream = webResponse.GetResponseStream())
                    {
                        var wallpaper = (Bitmap)Image.FromStream(stream);
                        wallpaper.Save(todayImagePath, ImageFormat.Jpeg);
                    }
                }

                imageBlock.Source = new BitmapImage(new Uri(todayImagePath, UriKind.Absolute));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private string GetBingUrl()
        {
            var xmlContent = CreateGetHttpResponse(BING_XML_PATH);
            var regex = new Regex("<Url>(?<ImageUrl>.*?)</Url>", RegexOptions.IgnoreCase);
            var collection = regex.Matches(xmlContent);
            string imageUrl = ("http://www.bing.com" + collection[0].Groups["ImageUrl"].Value);
            return imageUrl.Replace("&amp;", "&");
        }

        public string CreateGetHttpResponse(string url, int Timeout = 1000)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            request.UserAgent = null;
            request.Timeout = Timeout;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
            string result = streamReader.ReadToEnd();
            streamReader.Close();
            responseStream.Close();

            return result;
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
