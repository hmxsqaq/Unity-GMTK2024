using System;
using System.Collections;
using UnityEngine;

namespace Hmxs_GMTK.Scripts.Shape
{
    [CreateAssetMenu(menuName = "Shape/Scale", fileName = "Scale")]
    public class Scale : ShapeComponent
    {
        [SerializeField] private Vector2 scale;

        public override IEnumerator Apply(SpriteRenderer sprite, Transform parent, Action<SpriteRenderer> setSprite)
        {
            var targetScale = (Vector2)parent.localScale + scale;
            while (Vector2.Distance(parent.localScale, targetScale) > threshold)
            {
                parent.localScale = Vector2.Lerp(parent.localScale, targetScale, Time.deltaTime * lerpSpeed);
                yield return null;
            }
            parent.localScale = targetScale;
        }
    }
}