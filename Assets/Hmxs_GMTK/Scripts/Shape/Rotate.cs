using System;
using System.Collections;
using UnityEngine;

namespace Hmxs_GMTK.Scripts.Shape
{
    [CreateAssetMenu(menuName = "Shape/Rotate", fileName = "Rotate")]
    public class Rotate : ShapeComponent
    {
        [SerializeField] private float angle;

        public override IEnumerator Apply(SpriteRenderer sprite, Transform parent, Action<SpriteRenderer> setSprite)
        {
            var targetAngle = parent.eulerAngles.z + angle;
            while (Mathf.Abs(parent.eulerAngles.z - targetAngle) > threshold)
            {
                parent.eulerAngles = new Vector3(0, 0, Mathf.Lerp(parent.eulerAngles.z, targetAngle, Time.deltaTime * lerpSpeed));
                yield return null;
            }
            parent.eulerAngles = new Vector3(0, 0, targetAngle);
        }

        private void OnValidate() => type = ComponentType.Rotate;
    }
}