using System;
using System.Collections.Generic;
using Hmxs_GMTK.Scripts.Scene;
using Hmxs_GMTK.Scripts.Shape;
using Hmxs_GMTK.Scripts.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hmxs_GMTK.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private List<LevelSetting> levels;
        [SerializeField] private int currentLevelIndex;

        private void Start()
        {
            LoadLevel(levels[currentLevelIndex]);
        }

        // [Button]
        // private void SwitchLevel(LevelSetting level)
        // {
        //     Operator.Instance.PlayCoverAnimation(() => LoadLevel(level));
        // }

        [Button]
        public void LoadLevel(LevelSetting level)
        {
            currentLevelIndex = level.index;
            Photographer.Instance.TargetSprite.sprite = level.targetSprite;
            ContainerManager.Instance.ClearContainers();
            CardManager.Instance.LoadCards(level.componentCards);
        }
    }
}