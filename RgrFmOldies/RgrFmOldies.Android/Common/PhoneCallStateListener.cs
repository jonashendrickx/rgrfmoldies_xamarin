using Android.Telephony;
using RgrFmOldies.Droid.Services;

namespace RgrFmOldies.Droid.Common
{
    public class PhoneCallStateListener : PhoneStateListener
    {
        private MusicPlayerService _service;
        public PhoneCallStateListener(MusicPlayerService service)
        {
            _service = service;
        }

        public override void OnCallStateChanged(CallState state, string incomingNumber)
        {
            base.OnCallStateChanged(state, incomingNumber);
            switch (state)
            {
                case CallState.Ringing:
                    if (_service.MediaPlayer == null) break;
                    if (_service.MediaPlayer.IsPlaying)
                    {
                        _service.StopReceivingCall();
                    }
                    break;
                case CallState.Offhook:
                    break;
                case CallState.Idle:
                    break;
            }
        }
    }
}