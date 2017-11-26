using Android.Content;
using Android.Net;

namespace RgrFmOldies.Droid.Common
{
    public class Connectivity
    {
        public static bool IsConnected(Context context)
        {
            var cm = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService);
            return cm.ActiveNetworkInfo != null && cm.ActiveNetworkInfo.IsConnected;
        }
    }
}