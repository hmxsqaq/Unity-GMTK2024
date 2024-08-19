using System;
using System.Collections;
using Hmxs_GMTK.Scripts.Scene;
using Hmxs_GMTK.Scripts.Shape;
using Hmxs.Toolkit;
using UnityEngine;
using UnityEngine.UI;

namespace Hmxs_GMTK.Scripts.UI
{
    public class Operator : SingletonMono<Operator>
    {
        [SerializeField] private Button switchButtonRed;
        [SerializeField] private Button switchButtonBlue;
        [SerializeField] private Button switchButtonYellow;
        [SerializeField] private Button switchButtonPurple;
        [SerializeField] private Image monitor;
        [SerializeField] private Button testButton;
        [SerializeField] private Button startButton;

        [SerializeField] private Animator coverAnimator;

        protected override void OnInstanceInit(Operator instance) { }

        private void Start()
        {
            switchButtonRed.onClick.AddListener(SwitchToRed);
            switchButtonBlue.onClick.AddListener(SwitchToBlue);
            switchButtonYellow.onClick.AddListener(SwitchToYellow);
            switchButtonPurple.onClick.AddListener(SwitchToPurple);
            testButton.onClick.AddListener(() => ShapeRenderer.Instance.Render());
            startButton.onClick.AddListener(() => ShapeRenderer.Instance.Render());
        }

        public void SetPause(bool isPause)
        {
            switchButtonRed.interactable = !isPause;
            switchButtonBlue.interactable = !isPause;
            switchButtonYellow.interactable = !isPause;
            switchButtonPurple.interactable = !isPause;
            testButton.interactable = !isPause;
            startButton.interactable = !isPause;
        }

        public void SwitchTo(ComponentType type)
        {
            switch (type)
            {
                case ComponentType.Shape:
                    SwitchToRed();
                    break;
                case ComponentType.Rotate:
                    SwitchToBlue();
                    break;
                case ComponentType.Scale:
                    SwitchToYellow();
                    break;
                case ComponentType.Mask:
                    SwitchToPurple();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private void SwitchToRed()
        {
            SetPause(true);
            AudioManager.Instance.PlaySwitchButtonSound();
            monitor.color = Color.red;
            PlayCoverAnimation(() =>
            {
                CardManager.Instance.SwitchTo(ComponentType.Shape);
                SetPause(false);
            });
        }

        private void SwitchToBlue()
        {
            SetPause(true);
            AudioManager.Instance.PlaySwitchButtonSound();
            monitor.color = Color.blue;
            PlayCoverAnimation(() =>
            {
                CardManager.Instance.SwitchTo(ComponentType.Rotate);
                SetPause(false);
            });
        }

        private void SwitchToYellow()
        {
            SetPause(true);
            AudioManager.Instance.PlaySwitchButtonSound();
            monitor.color = Color.yellow;
            PlayCoverAnimation(() =>
            {
                CardManager.Instance.SwitchTo(ComponentType.Scale);
                SetPause(false);
            });
        }

        private void SwitchToPurple()
        {
            SetPause(true);
            AudioManager.Instance.PlaySwitchButtonSound();
            monitor.color = new Color(0.5f, 0, 0.5f);
            PlayCoverAnimation(() =>
            {
                CardManager.Instance.SwitchTo(ComponentType.Mask);
                SetPause(false);
            });
        }

        public void PlayCoverAnimation(Action callback)
        {
            StartCoroutine(PlayCoverAnimationCoroutine(callback));
        }

        public void CloseCover() => coverAnimator.Play($"close");
        public void OpenCover() => coverAnimator.Play($"open");

        private IEnumerator PlayCoverAnimationCoroutine(Action callback)
        {
            coverAnimator.Play($"close");
            float animationLength = coverAnimator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animationLength);
            callback?.Invoke();
            yield return new WaitForSeconds(0.5f);
            coverAnimator.Play($"open");
        }
    }
}