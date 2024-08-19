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
            testButton.onClick.AddListener(ShapeRenderer.Instance.Render);
            startButton.onClick.AddListener(ShapeRenderer.Instance.Render);
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
            monitor.color = Color.red;
            StartCoroutine(PlayCoverAnimationCoroutine((() => CardManager.Instance.SwitchTo(ComponentType.Shape))));
        }

        private void SwitchToBlue()
        {
            monitor.color = Color.blue;
            StartCoroutine(PlayCoverAnimationCoroutine((() => CardManager.Instance.SwitchTo(ComponentType.Rotate))));
        }

        private void SwitchToYellow()
        {
            monitor.color = Color.yellow;
            StartCoroutine(PlayCoverAnimationCoroutine((() => CardManager.Instance.SwitchTo(ComponentType.Scale))));
        }

        private void SwitchToPurple()
        {
            monitor.color = new Color(0.5f, 0, 0.5f);
            StartCoroutine(PlayCoverAnimationCoroutine((() => CardManager.Instance.SwitchTo(ComponentType.Mask))));
        }

        public void PlayCoverAnimation(Action callback)
        {
            StartCoroutine(PlayCoverAnimationCoroutine(callback));
        }

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