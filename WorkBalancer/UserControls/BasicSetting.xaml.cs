namespace WorkBalancer.UserControls
{
    /// <summary>
    /// BasicSetting.xaml 的交互逻辑
    /// </summary>
    public partial class BasicSetting : ISettingPanel
    {
        public BasicSetting()
        {
            InitializeComponent();
        }

        public SettingErrorType Apply()
        {
            LocalConfigs.AutoStartup = true;

            return SettingErrorType.None;
        }
    }
}