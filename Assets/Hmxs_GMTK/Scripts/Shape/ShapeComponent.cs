using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hmxs_GMTK.Scripts.Shape
{
    public enum ComponentType
    {
        Shape,
        Mask,
        Scale,
        Rotate
    }

    public abstract class ShapeComponent : ScriptableObject
    {
        [Title("Settings")]
        [SerializeField] protected float lerpSpeed = 3f;
        [SerializeField] protected float threshold = 0.1f;
        [SerializeField] protected ComponentType type;

        public ComponentType Type => type;

        public abstract IEnumerator Apply(SpriteRenderer sprite, Transform parent, Action<SpriteRenderer> setSprite);
    }
}