using Hmxs_GMTK.Scripts.Shape;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Hmxs_GMTK.Scripts.Scene
{
    [RequireComponent(typeof(Collider2D))]
    public class ComponentContainer : MonoBehaviour
    {
        [Title("Info")]
        [SerializeField] [ReadOnly] private ShapeComponent component;

        public ShapeComponent Component => component;
    }
}