using System;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Telephony;
using Java.Lang;
using RgrFmOldies.Droid.Common;
using Android.Support.V4.Content;

namespace RgrFmOldies.Droid.Services
{
    [Service]
    public class MusicPlayerService : Service, MediaPlayer.IOnErrorListener, MediaPlayer.IOnInfoListener, MediaPlayer.IOnPreparedListener
    {
        private IBinder _binder;
        private Intent _intent;
        public static readonly string PlayerStop = "com.jonashendrickx.rgrfmoldies.PlayerStop";


        public MediaPlayer MediaPlayer { get; private set; }

        public override void OnCreate()
        {
            PhoneCallStateListener phoneStateListener = new PhoneCallStateListener(this);
            TelephonyManager telephonyManager = (TelephonyManager)GetSystemService(TelephonyService);
            telephonyManager.Listen(phoneStateListener, PhoneStateListenerFlags.CallState);
            _intent = new Intent(ApplicationContext, typeof(MainActivity));
            _intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            return StartCommandResult.Sticky;
        }

        public void Play()
        {
            MediaPlayer = new MediaPlayer();
            var attributesBuilder = new AudioAttributes.Builder();
            attributesBuilder.SetLegacyStreamType(Stream.Music);
            attributesBuilder.SetContentType(AudioContentType.Music);
            MediaPlayer.SetAudioAttributes(attributesBuilder.Build()); MediaPlayer.SetOnErrorListener(this);
            MediaPlayer.SetOnInfoListener(this);
            MediaPlayer.SetOnPreparedListener(this);
            MediaPlayer.SetDataSource("http://stream.intronic.nl/rgrfm_oldiesradio");
            MediaPlayer.PrepareAsync();
        }

        public void StopReceivingCall()
        {
            Intent intent = new Intent(PlayerStop);
            LocalBroadcastManager.GetInstance(ApplicationContext).SendBroadcast(intent);
            Stop();
        }

        public void Stop()
        {
            if (MediaPlayer != null)
            {
                try
                {
                    MediaPlayer.Stop();
                }
                catch (IllegalStateException)
                {

                }
                finally
                {
                    MediaPlayer.Release();
                    MediaPlayer = null;
                }
            }
            StopForeground(true);
        }

        public override IBinder OnBind(Intent intent)
        {
            _binder = new MusicPlayerServiceBinder(this);
            return _binder;
        }

        public bool OnError(MediaPlayer mp, MediaError what, int extra)
        {
            HandleError();
            return true;
        }

        public bool OnInfo(MediaPlayer mp, MediaInfo what, int extra)
        {
            if ((int)what == 703 && extra == 0)
            {
                if (MediaPlayer == null)
                {
                    Play();
                }
                else
                {
                    if (MediaPlayer.IsPlaying)
                    {
                        if (!Connectivity.IsConnected(ApplicationContext))
                            HandleError();
                    }
                    else
                    {
                        Play();
                    }
                }
                return true;
            }
            return false;
        }

        private void HandleError()
        {
            Intent intent = new Intent(PlayerStop);
            LocalBroadcastManager.GetInstance(ApplicationContext).SendBroadcast(intent);
            Stop();
        }

        public void OnPrepared(MediaPlayer mp)
        {
            MediaPlayer.Start();
        }

        public class MusicPlayerServiceBinder : Binder
        {
            private readonly MusicPlayerService _service;

            public MusicPlayerServiceBinder(MusicPlayerService service)
            {
                _service = service;
            }

            public MusicPlayerService GetService()
            {
                return _service;
            }
        }
    }
}