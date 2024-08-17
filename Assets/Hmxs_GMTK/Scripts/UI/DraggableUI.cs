using UnityEngine;
using UnityEngine.EventSystems;

namespace Hmxs_GMTK.Scripts.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class DraggableUI : MonoBehaviour, IDragHandler
    {
        private RectTransform _rectTransform;
        private Canvas _canvas;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvas = GetComponentInParent<Canvas>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }
    }
}