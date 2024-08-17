using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hmxs_GMTK.Scripts.Shape
{
    public abstract class ShapeComponent : ScriptableObject
    {
        [Title("Settings")]
        [SerializeField] protected float lerpSpeed = 3f;
        [SerializeField] protected float threshold = 0.1f;

        public abstract IEnumerator Apply(SpriteRenderer sprite, Transform parent, Action<SpriteRenderer> setSprite);
    }
}