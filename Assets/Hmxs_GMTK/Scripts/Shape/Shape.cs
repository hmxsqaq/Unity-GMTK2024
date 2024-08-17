using System;
using System.Collections;
using UnityEngine;

namespace Hmxs_GMTK.Scripts.Shape
{
    [CreateAssetMenu(menuName = "Shape/Shape", fileName = "Shape")]
    public class Shape : ShapeComponent
    {
        [SerializeField] private SpriteRenderer shape;

        public override IEnumerator Apply(SpriteRenderer sprite, Transform parent, Action<SpriteRenderer> setSprite)
        {
            var newSprite = Instantiate(shape, parent, true);
            newSprite.sortingOrder = sprite != null ? sprite.sortingOrder + 2 : 0;

            var targetScale = newSprite.transform.localScale;
            newSprite.transform.localScale = Vector3.zero;
            while (Vector3.Distance(newSprite.transform.localScale, targetScale) > threshold)
            {
                newSprite.transform.localScale = Vector3.Lerp(newSprite.transform.localScale, targetScale, Time.deltaTime * lerpSpeed);
                yield return null;
            }
            newSprite.transform.localScale = targetScale;

            setSprite(newSprite);
        }
    }
}