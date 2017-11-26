using Android.Content;
using Android.Widget;

namespace RgrFmOldies.Droid.Services
{
    [BroadcastReceiver(Enabled = true, Exported = false)]
    public class MusicPlayerBroadCastReceiver : BroadcastReceiver
    {
        public MainActivity _activity;

        public MusicPlayerBroadCastReceiver() : base()
        {

        }

        public MusicPlayerBroadCastReceiver(MainActivity activity) : base()
        {
            _activity = activity;
        }

        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action.Equals(MusicPlayerService.PlayerStop))
            {
                _activity.RunOnUiThread(() =>
                {
                    var button = _activity.FindViewById<ImageButton>(Resource.Id.playButton);
                    button.SetImageResource(Resource.Drawable.ic_play_circle_outline_white_48dp);
                });
            }
        }
    }
}