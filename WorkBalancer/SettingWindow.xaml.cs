using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WorkBalancer
{
    /// <summary>
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow
    {
        private double _TitleHeight;

        public SettingWindow()
        {
            InitializeComponent();
        }

        private void SettingWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _TitleHeight = Height - windowBorder.ActualHeight;
            Height = contentBorder.ActualHeight + _TitleHeight;
        }

        private void ConfirmButton_OnClick(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void ApplyButton_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
