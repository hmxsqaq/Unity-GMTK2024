using System.Collections.Generic;
using Hmxs_GMTK.Scripts.Shape;
using Hmxs_GMTK.Scripts.UI;
using Hmxs.Toolkit;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hmxs_GMTK.Scripts.Scene
{
    public class CardManager : SingletonMono<CardManager>, IContainer
    {
        [Title("References")]
        [SerializeField] private List<ComponentCard> cards = new();
        [SerializeField] private Transform cardsRoot;
        [SerializeField] private List<Transform> alignPoints;

        [Title("Info")]
        [SerializeField] [ReadOnly] private ComponentType currentType = ComponentType.Shape;
        [SerializeField] [ReadOnly] private List<ComponentCard> displayedCards = new();

        protected override void OnInstanceInit(CardManager instance) { }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.TryGetComponent(out ComponentCard card)) return;
            ComponentContainer.SelectedContainer = this;
            if (other.TryGetComponent(out Draggable2D draggable) && draggable.IsDragging)
                cards.Remove(card);
        }

        public void LoadCards(List<ComponentCard> newCards)
        {
            if (cards.Count != 0)
            {
                cards.Clear();
                foreach (Transform child in cardsRoot.transform) Destroy(child.gameObject);
            }

            newCards.ForEach(card => cards.Add(Instantiate(card, cardsRoot)));

            currentType = ComponentType.Shape;
            SwitchTo(currentType);
        }

        public void SwitchTo(ComponentType type)
        {
            currentType = type;
            displayedCards.Clear();

            foreach (var card in cards)
            {
                if (card.Component.Type == type)
                {
                    card.gameObject.SetActive(true);
                    displayedCards.Add(card);
                }
                else
                    card.gameObject.SetActive(false);
            }

            AlignCards();
        }

        private void AlignCards()
        {
            for (var i = 0; i < displayedCards.Count; i++)
            {
                var card = displayedCards[i];
                if (i >= alignPoints.Count)
                {
                    Debug.LogError("Not enough align points");
                    return;
                }
                card.transform.position = alignPoints[i].position;
            }
        }

        public void SetComponent(ComponentCard card)
        {
            cards.Add(card);
            SwitchTo(currentType);
        }
    }
}