using Android.OS;
using RgrFmOldies.Droid.Models;
using RgrFmOldies.Droid.Services;
using System.Threading.Tasks;

namespace RgrFmOldies.Droid.Background
{
    public class PlaylistUpdaterTask : AsyncTask<object, object, Task<PlaylistItem>>
    {
        protected override async Task<PlaylistItem> RunInBackground(params object[] @params)
        {
            return await RgrOldiesWebService.GetPlaylistAsync();
        }
    }
}