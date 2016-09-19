using Android.Util;

namespace Gcm.Client
{
    public class Logger
    {
        public static bool Enabled = false;

        public static void Debug(string msg)
        {
            if(Enabled)
            {
                Log.Debug("GCM-CLIENT", msg);
            }
        }
    }
}