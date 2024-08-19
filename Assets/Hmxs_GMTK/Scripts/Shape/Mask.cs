using System;
using System.Collections;
using UnityEngine;

namespace Hmxs_GMTK.Scripts.Shape
{
    [CreateAssetMenu(menuName = "Shape/Mask", fileName = "Mask")]
    public class Mask : ShapeComponent
    {
        [SerializeField] private SpriteMask mask;

        public override IEnumerator Apply(SpriteRenderer sprite, Transform parent, Action<SpriteRenderer> setSprite)
        {
            var newMask = Instantiate(mask, parent, true);
            newMask.transform.localPosition = Vector3.zero;
            newMask.backSortingOrder = sprite != null ? sprite.sortingOrder - 1 : -1;
            newMask.frontSortingOrder = sprite != null ? sprite.sortingOrder + 1 : 1;

            var targetScale = newMask.transform.localScale;
            newMask.transform.localScale = Vector3.zero;
            while (Vector3.Distance(newMask.transform.localScale, targetScale) > threshold)
            {
                newMask.transform.localScale = Vector3.Lerp(newMask.transform.localScale, targetScale, Time.deltaTime * lerpSpeed);
                yield return null;
            }
            newMask.transform.localScale = targetScale;
        }

        private void OnValidate() => type = ComponentType.Mask;
    }
}