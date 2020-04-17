using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Tools;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BalanceWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            //ConfigHelper.Instance.SetWindowDefaultStyle();

        }

        private void ButtonClose_OnClick(object sender, RoutedEventArgs e)
        {
            Notification.Show(new StrongNotice("BalanceWork"), ShowAnimation.HorizontalMove, false);
        }

        private bool isBlink;
        public bool IsBlink
        {
            get => isBlink;
            set => isBlink = value;
        }
    }
}
