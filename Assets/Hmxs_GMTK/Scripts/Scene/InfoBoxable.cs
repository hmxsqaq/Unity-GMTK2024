using System.Collections;
using Hmxs_GMTK.Scripts.UI;
using Hmxs.Toolkit;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hmxs_GMTK.Scripts.Scene
{
    [RequireComponent(typeof(Collider2D))]
    public class InfoBoxable : MonoBehaviour
    {
        [Title("References")]
        [SerializeField] private InfoBox infoBoxPrefab;

        private static Canvas Canvas => GameManager.Instance.infoCanvas;

        [Title("Settings")]
        [SerializeField] private string title;
        [SerializeField] private string content;
        [SerializeField] private Sprite image;
        [SerializeField] private float cd;

        private InfoBox _infoBox;
        private float _counter;

        private void Start() => _counter = cd;

        private void Update()
        {
            if (_counter < cd) _counter += Time.deltaTime;
        }

        private void OnMouseEnter()
        {
            if (_infoBox != null || _counter < cd) return;
            _counter = 0;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                Canvas.transform as RectTransform,
                GameUtility.GetMouseScreenPosition(),
                Canvas.worldCamera,
                out var localPoint);
            _infoBox = Instantiate(infoBoxPrefab, Canvas.transform);
            Timer.Register(4f, () =>
            {
                if (_infoBox != null) Destroy(_infoBox.gameObject);
            });
            RectTransform rect = _infoBox.GetComponent<RectTransform>();
            rect.anchoredPosition = localPoint;
            _infoBox.ShowBox(title, content, image);
        }

        private void OnMouseExit()
        {
            if (_infoBox == null) return;
            _infoBox?.HideBox();
        }

        private void OnMouseDown()
        {
            if (_infoBox == null) return;
            _infoBox?.HideBox();
        }
    }
}