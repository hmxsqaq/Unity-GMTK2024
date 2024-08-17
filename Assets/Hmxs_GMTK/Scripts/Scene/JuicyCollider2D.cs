using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hmxs_GMTK.Scripts.Scene
{
    [RequireComponent(typeof(Collider2D))]
    public class JuicyCollider2D : MonoBehaviour
    {
        [Title("Settings")]
        [SerializeField] private float maxScale = 1.1f;
        [SerializeField] private float minScale = 0.9f;
        [SerializeField] [Range(0,1)] private float lerpSpeed = 0.15f;

        [Title("Info")]
        // [SerializeField] [ReadOnly] private bool interactable = true;
        private float _targetScale = 1;
        private float _currentScale = 1;
        private Vector3 _originalScale;

        // public void SetInteractable(bool value) => interactable = value;

        private void OnMouseEnter() => _targetScale = maxScale;
        private void OnMouseExit() => _targetScale = 1;
        private void OnMouseDown() => _targetScale = minScale;
        private void OnMouseUp() => _targetScale = maxScale;

        private void Start() => _originalScale = transform.localScale;

        private void Update()
        {
            // if (!interactable)
            // {
            //     _targetScale = 1;
            //     _currentScale = 1;
            //     transform.localScale = _originalScale;
            //     return;
            // }
            if (_currentScale.Equals(_targetScale)) return;
            _currentScale = Mathf.Lerp(_currentScale, _targetScale, lerpSpeed * Time.deltaTime * 50);
            transform.localScale = _originalScale * _currentScale;
        }
    }
}