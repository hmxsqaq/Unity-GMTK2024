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
            var targetAngle = sprite.transform.eulerAngles.z + angle;
            while (Mathf.Abs(sprite.transform.eulerAngles.z - targetAngle) > threshold)
            {
                sprite.transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(sprite.transform.eulerAngles.z, targetAngle, Time.deltaTime * lerpSpeed));
                yield return null;
            }
            sprite.transform.eulerAngles = new Vector3(0, 0, targetAngle);
        }
    }
}