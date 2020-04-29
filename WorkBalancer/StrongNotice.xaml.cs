using System.Threading;
using System.Windows;
using Window = System.Windows.Window;

namespace WorkBalancer
{
    /// <summary>
    /// StrongNotice.xaml 的交互逻辑
    /// </summary>
    public partial class StrongNotice
    {
        private int _DelayTimes;

        public StrongNotice()
        {
            InitializeComponent();
        }

        private void StrongNotice_Loaded(object sender, RoutedEventArgs e)
        {
            textBlock.Text = _DelayTimes.ToString();

            if(_DelayTimes == 0)
            {
                // 第一次弹出 推迟1分钟
                delayButton.Content = Application.Current.Resources["DelayOneMinute"];
            }
            else if(_DelayTimes == 1)
            {
                // 第二次弹出 推迟3分钟
                delayButton.Content = Application.Current.Resources["DelayThreeMinute"];
            }
            else
            {
                // 多于三次弹出 推迟5分钟
                delayButton.Content = Application.Current.Resources["DelayFiveMinute"];
            }
        }

        private void DelayButton_Click(object sender, RoutedEventArgs e)
        {
            _DelayTimes++;
            Close();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            _DelayTimes = 0;
            Close();
        }

        private void Close()
        {
            if (!(this is DependencyObject dependencyObject))
            {
                return;
            }

            if (Window.GetWindow(dependencyObject) is Window window)
            {
                window.Close();
            }
        }
    }
}
