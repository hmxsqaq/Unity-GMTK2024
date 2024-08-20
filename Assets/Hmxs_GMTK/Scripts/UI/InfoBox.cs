using System.Collections;
using Hmxs.Toolkit;
using UnityEngine;
using UnityEngine.UI;

namespace Hmxs_GMTK.Scripts.UI
{
    public class InfoBox : MonoBehaviour
    {
        [SerializeField] private Text title;
        [SerializeField] private Text content;
        [SerializeField] private Image rangeImage;
        [SerializeField] private float expandSpeed;
        [SerializeField] private float autoHideTime;
        [SerializeField] private RectTransform rect;


        // public float PreferredHeight => text.preferredHeight + 50 + 187;
        [SerializeField] private float preferredHeight = 645;
        private float _originHeight;
        private bool _isShowing;

        private void Start()
        {
            _originHeight = rect.rect.height;
        }

        // private void Update() => AdjustUIPosition();

        public void ShowBox(string titleText, string contentText, Sprite image)
        {
            if (_isShowing) return;
            _isShowing = true;
            title.text = titleText;
            content.text = contentText;
            rangeImage.sprite = image;
            StartCoroutine(Show());
        }

        public void HideBox()
        {
            Destroy(gameObject);
        }

        private IEnumerator Show()
        {
            while (rect.rect.height < preferredHeight)
            {
                rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y + expandSpeed * Time.deltaTime * 50);
                yield return null;
            }
            AdjustUIPosition();
            _isShowing = false;
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