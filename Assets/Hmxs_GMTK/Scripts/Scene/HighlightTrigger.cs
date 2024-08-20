using System;
using HighlightPlus2D;
using UnityEngine;

namespace Hmxs_GMTK.Scripts.Scene
{
    [RequireComponent(typeof(Collider2D), typeof(HighlightEffect2D))]
    public class HighlightTrigger : MonoBehaviour
    {
        private HighlightEffect2D _highlightEffect;

        private void Start() => _highlightEffect = GetComponent<HighlightEffect2D>();

        private void OnMouseEnter() => _highlightEffect.highlighted = true;

        private void OnMouseExit() => _highlightEffect.highlighted = false;
    }
}