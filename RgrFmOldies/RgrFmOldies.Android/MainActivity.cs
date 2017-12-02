using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using RgrFmOldies.Droid.Services;
using Android.Support.V4.Content;
using Android.Content.PM;
using RgrFmOldies.Droid.Common;
using System.Timers;
using System.Threading.Tasks;
using RgrFmOldies.Droid.Background;
using RgrFmOldies.Droid.Models;
using System;

namespace RgrFmOldies.Droid
{
	[Activity (Label = "RGR Oldies", MainLauncher = true, Icon = "@drawable/icon", ConfigurationChanges = ConfigChanges.KeyboardHidden | ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
	public class MainActivity : Activity, IServiceConnection, View.IOnClickListener
    {
        private Timer _timer;
        private bool _isBound;
        private MusicPlayerService _musicPlayerService;

        private MusicPlayerBroadCastReceiver _broadcastReceiver = new MusicPlayerBroadCastReceiver();

        private ImageButton _btnPlay;
        private TextView _textViewArtist;
        private TextView _textViewTitle;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _btnPlay = (ImageButton)FindViewById(Resource.Id.playButton);
            _btnPlay.SetOnClickListener(this);

            _textViewArtist = (TextView)FindViewById(Resource.Id.artistTextView);
            _textViewTitle = (TextView)FindViewById(Resource.Id.titleTextView);

            DoBindService();
            InitServiceListener();

            if (_musicPlayerService != null)
            {
                if (_musicPlayerService.MediaPlayer == null)
                {
                    _btnPlay.SetImageResource(Resource.Drawable.ic_play_circle_filled_white_48dp);
                }
                else if (_musicPlayerService.MediaPlayer != null)
                {
                    _btnPlay.SetImageResource(Resource.Drawable.ic_pause_circle_outline_white_48dp);
                }
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _musicPlayerService?.Stop();

            DoUnbindService();
        }

        protected override void OnPause()
        {
            base.OnPause();
            _timer = null;
        }

        protected override void OnResume()
        {
            base.OnResume();
            _broadcastReceiver._activity = this;
            StartPlaylistRefresh();
        }

        protected override void OnStop()
        {
            base.OnResume();
            _timer = null;
        }

        private void InitServiceListener()
        {
            var intentFilter = new IntentFilter();
            intentFilter.AddAction(MusicPlayerService.PlayerStop);
            LocalBroadcastManager.GetInstance(this).RegisterReceiver(_broadcastReceiver, intentFilter);
        }

        private void DoBindService()
        {
            var intent = new Intent(ApplicationContext, typeof(MusicPlayerService));
            BindService(intent, this, Bind.AutoCreate);
            _isBound = true;
        }

        private void DoUnbindService()
        {
            if (_isBound)
            {
                UnbindService(this);
                _isBound = false;
            }
        }

        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            _musicPlayerService = ((MusicPlayerService.MusicPlayerServiceBinder)service).GetService();
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            _musicPlayerService = null;
        }

        public void OnClick(View v)
        {
            if (v.Equals(_btnPlay))
            {
                if (_musicPlayerService.MediaPlayer == null && Connectivity.IsConnected(ApplicationContext))
                {
                    _btnPlay.SetImageResource(Resource.Drawable.ic_pause_circle_outline_white_48dp);
                    _musicPlayerService.Play();
                }
                else if (_musicPlayerService.MediaPlayer != null)
                {
                    _btnPlay.SetImageResource(Resource.Drawable.ic_play_circle_outline_white_48dp);
                    _musicPlayerService.Stop();
                }
            }
        }

        private async Task OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            var timer = (Timer)sender;
            timer.Interval = 60000;
            PlaylistUpdaterTask task = new PlaylistUpdaterTask();
            task.Execute();
            var result = await task.GetResult();
            OnTaskComplete(result);
        }

        public void StartPlaylistRefresh()
        {
            if (_timer == null)
            {
                _timer = new Timer { Interval = 1000 };
                _timer.Elapsed += async (s, e) => await OnTimedEvent(s, e);
            }
            _timer.Enabled = true;
        }

        public void OnTaskComplete(PlaylistItem playlist)
        {
            if (playlist == null) return;
            RunOnUiThread(() =>
            {
                _textViewArtist.Text = playlist.Artist;
                _textViewTitle.Text = playlist.Title;
            });
        }
    }
}


