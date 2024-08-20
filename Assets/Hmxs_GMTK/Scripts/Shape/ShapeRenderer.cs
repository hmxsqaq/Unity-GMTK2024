using System;
using System.Collections;
using System.Collections.Generic;
using Hmxs_GMTK.Scripts.Scene;
using Hmxs.Toolkit;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hmxs_GMTK.Scripts.Shape
{
    public class ShapeRenderer : SingletonMono<ShapeRenderer>
    {
        protected override void OnInstanceInit(ShapeRenderer instance) { }

        [Title("Info")]
        [SerializeField] [ReadOnly] private SpriteRenderer sprite;

        public void Render(Action callback = null)
        {
            Clear();
            var components = ContainerManager.Instance.GetComponents();
            StartCoroutine(StartRender(components, callback));
        }

        public void Clear()
        {
            sprite = null;
            foreach (Transform child in transform) Destroy(child.gameObject);
        }

        private IEnumerator StartRender(List<ShapeComponent> components, Action callback = null)
        {
            foreach (var component in components)
            {
                yield return StartCoroutine(component.Apply(sprite, transform, newSprite => sprite = newSprite));
            }
            callback?.Invoke();
        }
    }
}