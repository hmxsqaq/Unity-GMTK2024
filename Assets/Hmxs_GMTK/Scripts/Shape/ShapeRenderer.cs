using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hmxs_GMTK.Scripts.Scene;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hmxs_GMTK.Scripts.Shape
{
    public class ShapeRenderer : MonoBehaviour
    {
        [Title("Settings")]
        [SerializeField] private List<ComponentContainer> componentContainers = new();

        [Title("Test")]
        [Button]
        private void RenderTest()
        {
            // components.Clear();
            // foreach (var container in componentContainers) components.Add(container.Component);
            StartCoroutine(StartRender());
        }

        [Title("Info")]
        [SerializeField] private List<ShapeComponent> components = new();
        [SerializeField] [ReadOnly] private SpriteRenderer sprite;

        private IEnumerator StartRender() => components.Select(component => StartCoroutine(component.Apply(sprite, transform, newSprite => sprite = newSprite))).GetEnumerator();
    }
}