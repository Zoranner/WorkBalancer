namespace WorkBalancer.UserControls
{
    /// <summary>
    /// AdvancedSetting.xaml 的交互逻辑
    /// </summary>
    public partial class AdvancedSetting : ISettingPanel
    {
        public AdvancedSetting()
        {
            InitializeComponent();
        }

        public SettingErrorType Apply()
        {
            if (firstPinBox.Length != 6)
            {
                return SettingErrorType.PinCodeLengthError;
            }

            if (firstPinBox.Password != againPinBox.Password)
            {
                return SettingErrorType.PinCodeMismatch;
            }

            LocalConfigs.PinCode = firstPinBox.Password;

            return SettingErrorType.None;
        }
    }
}