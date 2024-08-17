using System;
using Hmxs_GMTK.Scripts.Shape;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Hmxs_GMTK.Scripts.Scene
{
    [RequireComponent(typeof(Collider2D))]
    public class ComponentContainer : MonoBehaviour
    {
        private enum ContainerState
        {
            Selected,
            Interactable,
            Empty
        }

        public static ComponentContainer SelectedContainer;

        [Title("Info")]
        [SerializeField] [ReadOnly] private ShapeComponent component;
        [SerializeField] [ReadOnly] private ComponentCard storedCard;
        [SerializeField] [ReadOnly] private ContainerState state = ContainerState.Empty;

        public ShapeComponent Component => component;

        private JuicyCollider2D _juicyCollider2D;

        private void Start() => _juicyCollider2D = GetComponent<JuicyCollider2D>();

        private void Update()
        {
            if (SelectedContainer == this) state = ContainerState.Selected;
            else if (storedCard != null) state = ContainerState.Interactable;
            else state = ContainerState.Empty;

            SetColor();
            _juicyCollider2D.SetInteractable(state == ContainerState.Interactable);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out ComponentCard _))
                SelectedContainer = this;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out ComponentCard _))
                SelectedContainer = null;
        }

        private void OnMouseDown()
        {
            // without card in hand, directly click the container that have card in it
            if (storedCard != null && state == ContainerState.Interactable) PopCard();
        }

        public void SetComponent(ComponentCard card)
        {
            // if there is already a card in the container, pop it
            if (storedCard != null) PopCard();

            // store the card into the container
            component = card.Component;
            card.gameObject.SetActive(false);
            storedCard = card;

            SelectedContainer = null;
        }

        private void SetColor()
        {
            var sprite = GetComponent<SpriteRenderer>();
            sprite.color = state switch
            {
                ContainerState.Selected => Color.red,
                ContainerState.Interactable => storedCard.gameObject.GetComponent<SpriteRenderer>().color,
                ContainerState.Empty => Color.gray,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void PopCard()
        {
            storedCard.gameObject.SetActive(true);
            storedCard = null;
            component = null;
            SelectedContainer = null;
        }
    }
}