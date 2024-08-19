using Hmxs.Toolkit;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Hmxs_GMTK.Scripts
{
    public class AudioManager : SingletonMono<AudioManager>
    {
        protected override void OnInstanceInit(AudioManager instance) { }

        [SerializeField] private MMF_Player pickUpSound;
        [SerializeField] private MMF_Player putDownSound;
        [SerializeField] private MMF_Player switchButtonSound;

        public void PlayPickUpSound() => pickUpSound.PlayFeedbacks();
        public void PlayPutDownSound() => putDownSound.PlayFeedbacks();
        public void PlaySwitchButtonSound() => switchButtonSound.PlayFeedbacks();
    }
}