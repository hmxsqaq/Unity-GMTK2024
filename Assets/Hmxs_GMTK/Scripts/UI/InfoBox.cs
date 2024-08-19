using System.Collections;
using Hmxs.Toolkit;
using UnityEngine;
using UnityEngine.UI;

namespace Hmxs_GMTK.Scripts.UI
{
    public class InfoBox : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private float expandSpeed;
        [SerializeField] private float autoHideTime;
        [SerializeField] private RectTransform rect;
        [SerializeField] private RectTransform textRect;

        public float PreferredHeight => text.preferredHeight + 50 + 187;
        private float _originHeight;
        private bool _isShowing;
        private bool _isHiding;

        private void Start()
        {
            _originHeight = rect.rect.height;
            StartCoroutine(Show());
            this.AttachTimer(autoHideTime, HideBox);
        }

        private void Update()
        {
            AdjustUIPosition();
        }

        public void ShowBox(string content)
        {
            if (_isShowing) return;
            _isShowing = true;
            text.text = content;
            StartCoroutine(Show());
        }

        public void HideBox()
        {
            if (_isHiding) return;
            _isHiding = true;
            StartCoroutine(Hide());
        }

        private IEnumerator Show()
        {
            while (textRect.rect.height < text.preferredHeight + 50)
            {
                rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y + expandSpeed * Time.deltaTime * 50);
                yield return null;
            }
            _isShowing = false;
        }

        private IEnumerator Hide()
        {
            while (rect.rect.height > _originHeight)
            {
                rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y - expandSpeed * Time.deltaTime * 50);
                yield return null;
            }
            _isHiding = false;
            Destroy(gameObject);
        }

        private void AdjustUIPosition()
        {
            Vector3[] corners = new Vector3[4];
            rect.GetWorldCorners(corners);

            Vector3 position = rect.position;
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            if (corners[0].x < 0)
                position.x += 0 - corners[0].x;

            if (corners[2].x > screenWidth)
                position.x -= corners[2].x - screenWidth;

            if (corners[0].y < 0)
                position.y += 0 - corners[0].y;

            if (corners[2].y > screenHeight)
                position.y -= corners[2].y - screenHeight;

            rect.position = position;
        }
    }
}