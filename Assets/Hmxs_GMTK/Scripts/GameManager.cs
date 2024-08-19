using System.Collections.Generic;
using Hmxs_GMTK.Scripts.Scene;
using Hmxs_GMTK.Scripts.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hmxs_GMTK.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private List<LevelSetting> levels;
        [SerializeField] private LevelSetting currentLevel;

        [Button]
        private void SwitchLevel(LevelSetting level)
        {
            currentLevel = level;
            Operator.Instance.PlayCoverAnimation(() => CardManager.Instance.LoadCards(level.componentCards));
        }
    }
}