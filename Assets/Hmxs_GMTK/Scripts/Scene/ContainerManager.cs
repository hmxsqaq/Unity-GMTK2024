using System.Collections.Generic;
using System.Linq;
using Hmxs_GMTK.Scripts.Shape;
using Hmxs.Toolkit;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hmxs_GMTK.Scripts.Scene
{
    public class ContainerManager : SingletonMono<ContainerManager>
    {
        [Title("References")]
        [SerializeField] private List<ComponentContainer> containers;

        public List<ComponentContainer> Containers => containers;

        protected override void OnInstanceInit(ContainerManager instance) { }

        public void ClearContainers()
        {
            foreach (var container in containers)
                container.ClearContainer();
        }

        public List<ShapeComponent> GetComponents() =>
            (from container in containers where container.Component != null select container.Component).ToList();
    }
}