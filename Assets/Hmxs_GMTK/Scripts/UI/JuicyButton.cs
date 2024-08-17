using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Hmxs_GMTK.Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public class JuicyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Title("Settings")]
        [SerializeField] private float maxScale = 1.1f;
        [SerializeField] private float minScale = 0.9f;
        [SerializeField] [Range(0,1)] private float lerpSpeed = 0.15f;

        private float _targetScale = 1;
        private float _currentScale = 1;
        private Vector3 _originalScale;
        private Button _button;

        public void OnPointerEnter(PointerEventData eventData) => _targetScale = maxScale;
        public void OnPointerExit(PointerEventData eventData) => _targetScale = 1;
        public void OnPointerDown(PointerEventData eventData) => _targetScale = minScale;
        public void OnPointerUp(PointerEventData eventData) => _targetScale = maxScale;

        private void Start()
        {
            _originalScale = transform.localScale;
            _button = GetComponent<Button>();
        }

        private void Update()
        {
            if (!_button.interactable)
            {
                _targetScale = 1;
                _currentScale = 1;
                transform.localScale = _originalScale;
                return;
            }
            if (_currentScale.Equals(_targetScale)) return;
            _currentScale = Mathf.Lerp(_currentScale, _targetScale, lerpSpeed * Time.deltaTime * 50);
            transform.localScale = _originalScale * _currentScale;
        }
    }
}