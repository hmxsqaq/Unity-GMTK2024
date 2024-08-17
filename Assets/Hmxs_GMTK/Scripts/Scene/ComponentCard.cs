using Hmxs_GMTK.Scripts.Shape;
using UnityEngine;

namespace Hmxs_GMTK.Scripts.Scene
{
    [RequireComponent(typeof(Collider2D))]
    public class ComponentCard : MonoBehaviour
    {
        [SerializeField] private ShapeComponent component;

        public ShapeComponent Component => component;
    }
}