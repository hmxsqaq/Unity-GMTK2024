using System.Collections.Generic;
using Hmxs_GMTK.Scripts.Scene;
using Hmxs_GMTK.Scripts.Shape;
using Hmxs_GMTK.Scripts.UI;
using Hmxs.Toolkit;
using Hmxs.Toolkit.Plugins.Fungus.FungusTools;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Hmxs_GMTK.Scripts
{
    public class GameManager : SingletonMono<GameManager>
    {
        protected override void OnInstanceInit(GameManager instance) { }

        [SerializeField] private List<LevelSetting> levels;
        [SerializeField] private int currentLevelIndex = 0;
        [SerializeField] private Text resultText;

        public Canvas infoCanvas;

        public int TestNumberLeft { get; set; } = 3;

        public void NextLevel()
        {
            if (currentLevelIndex < levels.Count - 1)
            {
                currentLevelIndex++;
                FlowchartManager.ExecuteBlock("StartLevel" + currentLevelIndex);
            }
            else
            {
                FlowchartManager.ExecuteBlock("End");
            }
        }

        [Button]
        public void SwitchLevel(LevelSetting level)
        {
            Operator.Instance.PlayCoverAnimation(() => LoadLevel(level));
        }

        [Button]
        public void LoadLevel(LevelSetting level)
        {
            Photographer.Instance.TargetSprite.sprite = level.targetSprite;
            Photographer.Instance.MapSprite.sprite = level.mapSprite;
            CardManager.Instance.LoadCards(level.componentCards);
            Operator.Instance.SetMonitor();
            Photographer.Instance.GetTargetArea();
        }

        public void ClearLevel()
        {
            ContainerManager.Instance.ClearContainers();
            CardManager.Instance.ClearCards();
            ShapeRenderer.Instance.Clear();
            Photographer.Instance.TargetSprite.sprite = null;
            Photographer.Instance.ShapeResult.sprite = null;
            Photographer.Instance.MapSprite.sprite = null;
            Operator.Instance.ResetNumber();
        }

        public void PushResult(string result)
        {
            resultText.text = result;
        }

        public void ReturnToMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}