using System.Collections.Generic;
using System.Linq;
using Hmxs_GMTK.Scripts.Shape;
using Hmxs.Toolkit;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hmxs_GMTK.Scripts.Scene
{
    public class CardManager : SingletonMono<CardManager>
    {
        [Title("References")]
        [SerializeField] private List<ComponentCard> cards = new();
        [SerializeField] private Transform cardsRoot;
        [SerializeField] private List<Transform> alignPoints;
        [SerializeField] private Transform returnZone;

        [Title("Info")]
        [SerializeField] [ReadOnly] private ComponentType currentType = ComponentType.Shape;

        public float ReturnZoneY => returnZone.position.y;

        protected override void OnInstanceInit(CardManager instance) { }

        public void LoadCards(List<ComponentCard> newCards)
        {
            if (cards.Count != 0)
            {
                cards.Clear();
                foreach (Transform child in cardsRoot.transform)
                {
                    Destroy(child.gameObject);
                }
            }

            int posIndex = 0;
            foreach (var card in newCards.Select(newCard => Instantiate(newCard, cardsRoot)))
            {
                card.Position = alignPoints[posIndex++];
                if (posIndex >= alignPoints.Count) posIndex = 0;
                card.ReturnToPosition();
                cards.Add(card);
            }

            currentType = ComponentType.Shape;
            SwitchTo(currentType);
        }


        public void SwitchTo(ComponentType type)
        {
            currentType = type;

            foreach (var card in cards)
                card.gameObject.SetActive(card.Component.Type == type && !card.IsStored);
        }
    }
}