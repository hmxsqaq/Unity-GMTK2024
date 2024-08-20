using System;
using HighlightPlus2D;
using Hmxs_GMTK.Scripts.Shape;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Hmxs_GMTK.Scripts.Scene
{
    [RequireComponent(typeof(Collider2D))]
    public class ComponentContainer : MonoBehaviour
    {
        public static ComponentContainer SelectedContainer;

        [SerializeField] private Transform cardPoint;

        [Title("Info")]
        [SerializeField] [ReadOnly] private ShapeComponent component;
        [SerializeField] [ReadOnly] private ComponentCard storedCard;

        public ShapeComponent Component => component;
        public Vector3 CardPoint => cardPoint.position;

        private HighlightEffect2D _highlightEffect;

        private void Start()
        {
            _highlightEffect = GetComponent<HighlightEffect2D>();
        }

        private void Update()
        {
            _highlightEffect.highlighted = SelectedContainer == this;
        }

        // private void OnTriggerStay2D(Collider2D other)
        // {
        //     if (other.TryGetComponent(out ComponentCard card) && card.State == ComponentCard.CardState.IsDragged)
        //     {
        //         if (SelectedContainer == null)
        //         {
        //             SelectedContainer = this;
        //             return;
        //         }
        //         if (SelectedContainer == this) return;
        //
        //         if (Vector2.Distance(card.transform.position, cardPoint.position) < Vector2.Distance(SelectedContainer.cardPoint.position, cardPoint.position))
        //             SelectedContainer = this;
        //     }
        // }

        // private void OnTriggerExit2D(Collider2D other)
        // {
        //     if (other.TryGetComponent(out ComponentCard _))
        //         SelectedContainer = null;
        // }

        public void GetCard(ComponentCard card)
        {
            // if there is already a card in the container, pop it
            if (storedCard != null) PopCard();

            // store the card into the container
            component = card.Component;
            storedCard = card;

            SelectedContainer = null;
        }

        public void PopCard()
        {
            storedCard.Pop();
            storedCard = null;
            component = null;
            SelectedContainer = null;
        }

        public void ClearContainer()
        {
            if (storedCard != null)
            {
                storedCard.Pop();
                storedCard = null;
            }
            component = null;
            SelectedContainer = null;
        }
    }
}