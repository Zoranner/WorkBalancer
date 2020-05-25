using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WorkBalancer
{
    public interface ISettingPanel
    {
        SettingErrorType Apply();
    }

    public enum SettingErrorType
    {
        None = 0,
        PinCodeLengthError,
        PinCodeMismatch,
    }
}
