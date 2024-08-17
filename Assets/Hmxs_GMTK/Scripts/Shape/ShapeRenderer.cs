using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hmxs_GMTK.Scripts.Shape
{
    public class ShapeRenderer : MonoBehaviour
    {
        [Title("Settings")]
        [SerializeField] private List<ShapeComponent> components = new();

        [Title("Test")]
        [Button]
        private void RenderTest()
        {
            StartCoroutine(Render());
        }

        [Title("Info")]
        [SerializeField] [ReadOnly] private SpriteRenderer sprite;

        private IEnumerator Render()
        {
            foreach (var component in components)
            {
                yield return StartCoroutine(component.Apply(sprite, transform, newSprite => sprite = newSprite));
            }
        }
    }
}