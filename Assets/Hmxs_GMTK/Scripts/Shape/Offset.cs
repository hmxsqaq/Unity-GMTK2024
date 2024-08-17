using System;
using System.Collections;
using UnityEngine;

namespace Hmxs_GMTK.Scripts.Shape
{
    public class Offset : ShapeComponent
    {
        [SerializeField] private Vector2 offset;

        public override IEnumerator Apply(SpriteRenderer sprite, Transform parent, Action<SpriteRenderer> setSprite)
        {
            var targetPosition = (Vector2)sprite.transform.position + offset;
            while (Vector2.Distance(sprite.transform.position, targetPosition) > threshold)
            {
                sprite.transform.position = Vector2.Lerp(sprite.transform.position, targetPosition, Time.deltaTime * lerpSpeed);
                yield return null;
            }
            sprite.transform.position = targetPosition;
        }
    }
}