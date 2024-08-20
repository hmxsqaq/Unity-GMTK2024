using System;
using System.Collections;
using System.Collections.Generic;
using Hmxs_GMTK.Scripts.Scene;
using Hmxs_GMTK.Scripts.Shape;
using Hmxs.Toolkit;
using Hmxs.Toolkit.Plugins.Fungus.FungusTools;
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
        [SerializeField] private SpriteRenderer testNumber;
        [SerializeField] private List<Sprite> testNumberSprite;
        [SerializeField] private List<Sprite> monitorSprite;

        protected override void OnInstanceInit(Operator instance) { }

        private void Start()
        {
            switchButtonRed.onClick.AddListener(SwitchToRed);
            switchButtonBlue.onClick.AddListener(SwitchToBlue);
            switchButtonYellow.onClick.AddListener(SwitchToYellow);
            switchButtonPurple.onClick.AddListener(SwitchToPurple);
            testButton.onClick.AddListener(() =>
            {
                GameManager.Instance.TestNumberLeft--;
                if (GameManager.Instance.TestNumberLeft <= 0)
                {
                    testNumber.sprite = null;
                    testButton.interactable = false;
                }
                else
                    testNumber.sprite = testNumberSprite[GameManager.Instance.TestNumberLeft - 1];
                ShapeRenderer.Instance.Render();
            });

            startButton.onClick.AddListener(() =>
            {
                ShapeRenderer.Instance.Render((() =>
                {
                    Photographer.Instance.GetResultArea();
                    GameManager.Instance.PushResult("You destroyed " + (int)(Photographer.Instance.GetResult() * 100) + "% of the target");
                    Photographer.Instance.TargetSprite.sprite = null;
                    Photographer.Instance.ShapeResult.sprite = null;
                    ShapeRenderer.Instance.Clear();
                    FlowchartManager.ExecuteBlock("Launch");
                }));
            });
        }

        public void ResetNumber()
        {
            GameManager.Instance.TestNumberLeft = 3;
            testNumber.sprite = testNumberSprite[GameManager.Instance.TestNumberLeft - 1];
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

        public void SetMonitor() => monitor.sprite = monitorSprite[0];

        // Shape
        private void SwitchToRed()
        {
            SetPause(true);
            AudioManager.Instance.PlaySwitchButtonSound();
            monitor.sprite = monitorSprite[0];
            PlayCoverAnimation(() => CardManager.Instance.SwitchTo(ComponentType.Shape), () => SetPause(false));
        }

        // Rotate
        private void SwitchToBlue()
        {
            SetPause(true);
            AudioManager.Instance.PlaySwitchButtonSound();
            monitor.sprite = monitorSprite[1];
            PlayCoverAnimation(() => CardManager.Instance.SwitchTo(ComponentType.Rotate), () => SetPause(false));
        }

        // Scale
        private void SwitchToYellow()
        {
            SetPause(true);
            AudioManager.Instance.PlaySwitchButtonSound();
            monitor.sprite = monitorSprite[2];
            PlayCoverAnimation(() => CardManager.Instance.SwitchTo(ComponentType.Scale), () => SetPause(false));
        }

        // Mask
        private void SwitchToPurple()
        {
            SetPause(true);
            AudioManager.Instance.PlaySwitchButtonSound();
            monitor.sprite = monitorSprite[3];
            PlayCoverAnimation(() => CardManager.Instance.SwitchTo(ComponentType.Mask), () => SetPause(false));
        }

        public void PlayCoverAnimation(Action callback1 = null, Action callback2 = null)
        {
            StartCoroutine(PlayCoverAnimationCoroutine(callback1, callback2));
        }

        public void CloseCover() => coverAnimator.Play($"close");
        public void OpenCover() => coverAnimator.Play($"open");

        private IEnumerator PlayCoverAnimationCoroutine(Action callback1 = null, Action callback2 = null)
        {
            coverAnimator.Play($"close");
            float animationLength = coverAnimator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animationLength);
            callback1?.Invoke();
            yield return new WaitForSeconds(0.5f);
            coverAnimator.Play($"open");
            animationLength = coverAnimator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animationLength);
            callback2?.Invoke();
        }
    }
}