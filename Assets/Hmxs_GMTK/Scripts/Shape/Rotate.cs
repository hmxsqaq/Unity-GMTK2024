using System;
using System.Collections;
using UnityEngine;

namespace Hmxs_GMTK.Scripts.Shape
{
    [CreateAssetMenu(menuName = "Shape/Rotate", fileName = "Rotate")]
    public class Rotate : ShapeComponent
    {
        [SerializeField] private float angle;
        [SerializeField] private float duration;

        public override IEnumerator Apply(SpriteRenderer sprite, Transform parent, Action<SpriteRenderer> setSprite)
        {
            Quaternion startRotation = parent.rotation;
            Quaternion targetRotation = startRotation * Quaternion.Euler(0, 0, -angle);
            float elapsedTime = 0;
            while (elapsedTime < duration)
            {
                parent.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            parent.rotation = targetRotation;
        }

        private void OnValidate() => type = ComponentType.Rotate;
    }
}