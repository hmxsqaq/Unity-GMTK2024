using System.Collections.Generic;
using Hmxs_GMTK.Scripts.Scene;
using UnityEngine;

namespace Hmxs_GMTK.Scripts
{
    [CreateAssetMenu(fileName = "Level", menuName = "Level")]
    public class LevelSetting : ScriptableObject
    {
        public int index;
        public string levelName;
        public List<ComponentCard> componentCards;
        public Sprite mapSprite;
    }
}