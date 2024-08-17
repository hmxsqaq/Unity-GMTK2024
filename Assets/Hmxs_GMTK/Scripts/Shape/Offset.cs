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
            var targetPosition = (Vector2)parent.position + offset;
            while (Vector2.Distance(parent.position, targetPosition) > threshold)
            {
                parent.position = Vector2.Lerp(parent.position, targetPosition, Time.deltaTime * lerpSpeed);
                yield return null;
            }
            parent.position = targetPosition;
        }
    }
}