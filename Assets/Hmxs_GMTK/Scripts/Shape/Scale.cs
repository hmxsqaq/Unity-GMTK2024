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
            var targetScale = (Vector2)sprite.transform.localScale + scale;
            while (Vector2.Distance(sprite.transform.localScale, targetScale) > threshold)
            {
                sprite.transform.localScale = Vector2.Lerp(sprite.transform.localScale, targetScale, Time.deltaTime * lerpSpeed);
                yield return null;
            }
            sprite.transform.localScale = targetScale;
        }
    }
}