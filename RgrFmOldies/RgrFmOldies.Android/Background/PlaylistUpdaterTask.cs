using Android.OS;
using RgrFmOldies.Droid.Services;
using System.Threading.Tasks;
using RgrFm.Models;

namespace RgrFmOldies.Droid.Background
{
    public class PlaylistUpdaterTask : AsyncTask<object, object, Task<PlaylistFeed>>
    {
        protected override async Task<PlaylistFeed> RunInBackground(params object[] @params)
        {
            return await RgrOldiesWebService.GetPlaylistAsync();
        }
    }
}