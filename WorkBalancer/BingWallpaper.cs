using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Zoranner.SuperEvents;

namespace WorkBalancer
{
    public class BingWallpaper
    {
        private const string BING_XML_PATH = "http://cn.bing.com/HPImageArchive.aspx?idx=0&n=1";
        
        public SuperEvent LoadStarted { get; } = new SuperEvent();

        public SuperEvent LoadFinished { get; } = new SuperEvent();

        public BitmapImage LoadImage()
        {
            LoadStarted?.Dispatch();

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
                
            LoadFinished?.Dispatch();

            return new BitmapImage(new Uri(todayImagePath, UriKind.Absolute));
        }

        private string GetBingUrl()
        {
            var xmlContent = CreateGetHttpResponse(BING_XML_PATH);
            var regex = new Regex("<Url>(?<ImageUrl>.*?)</Url>", RegexOptions.IgnoreCase);
            var collection = regex.Matches(xmlContent);
            string imageUrl = ("http://www.bing.com" + collection[0].Groups["ImageUrl"].Value);
            return imageUrl.Replace("&amp;", "&");
        }

        public string CreateGetHttpResponse(string url, int Timeout = 3000)
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
    }
}
