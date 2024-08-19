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

        public void Render()
        {
            var components = ContainerManager.Instance.GetComponents();
            if (components.Count == 0) return;
            StartCoroutine(StartRender(components));
        }

        private IEnumerator StartRender(List<ShapeComponent> components)
        {
            foreach (var component in components)
            {
                yield return StartCoroutine(component.Apply(sprite, transform, newSprite => sprite = newSprite));
            }
        }
    }
}