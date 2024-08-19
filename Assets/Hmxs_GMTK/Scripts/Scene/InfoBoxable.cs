using Hmxs_GMTK.Scripts.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hmxs_GMTK.Scripts.Scene
{
    [RequireComponent(typeof(Collider2D))]
    public class InfoBoxable : MonoBehaviour
    {
        [Title("References")]
        [SerializeField] private InfoBox infoBoxPrefab;
        [SerializeField] private Canvas canvas;

        [Title("Settings")]
        [SerializeField] private string content;
        [SerializeField] private float cd;

        private InfoBox _infoBox;
        private float _counter;

        private void Start()
        {
            _counter = cd;
        }

        private void Update()
        {
            if (_counter < cd) _counter += Time.deltaTime;
        }

        private void OnMouseEnter()
        {
            if (_infoBox != null || _counter < cd) return;
            _counter = 0;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                GameUtility.GetMouseScreenPosition(),
                canvas.worldCamera,
                out var localPoint);
            _infoBox = Instantiate(infoBoxPrefab, canvas.transform);
            RectTransform rect = _infoBox.GetComponent<RectTransform>();
            rect.anchoredPosition = localPoint;
            _infoBox.ShowBox(content);
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