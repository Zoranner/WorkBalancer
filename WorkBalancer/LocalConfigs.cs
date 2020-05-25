namespace WorkBalancer
{
    public class LocalConfigs
    {
        private static volatile LocalConfigs _Singleton;
        private static readonly object _SingletonLock = new object();

        public static bool AutoStartup { get; set; }

        public static string PinCode { get; set; }

        public static LocalConfigs CreateInstance()
        {
            if (_Singleton != null)
            {
                return _Singleton;
            }

            lock (_SingletonLock)
            {
                if (_Singleton != null)
                {
                    return _Singleton;
                }

                _Singleton = new LocalConfigs();
            }

            return _Singleton;
        }

        public static void Load()
        {
        }

        public static void Save()
        {
        }
    }
}