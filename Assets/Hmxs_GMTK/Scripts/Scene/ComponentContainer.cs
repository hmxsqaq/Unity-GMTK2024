using System;
using Hmxs_GMTK.Scripts.Shape;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Hmxs_GMTK.Scripts.Scene
{
    [RequireComponent(typeof(Collider2D))]
    public class ComponentContainer : MonoBehaviour, IContainer
    {
        private enum ContainerState
        {
            Selected,
            Interactable,
            Empty
        }

        public static IContainer SelectedContainer;

        [Title("Info")]
        [SerializeField] [ReadOnly] private ShapeComponent component;
        [SerializeField] [ReadOnly] private ComponentCard storedCard;
        [SerializeField] [ReadOnly] private ContainerState state;

        public ShapeComponent Component => component;

        private ContainerState State
        {
            get => state;
            set
            {
                if (state == value) return;
                state = value;
                SetColor();
                _juicyCollider2D.Interactable = State == ContainerState.Interactable;
            }
        }

        private JuicyCollider2D _juicyCollider2D;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _juicyCollider2D = GetComponent<JuicyCollider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (SelectedContainer == (IContainer)this) State = ContainerState.Selected;
            else if (storedCard != null) State = ContainerState.Interactable;
            else State = ContainerState.Empty;
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
            if (storedCard != null && State == ContainerState.Interactable) PopCard();
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

        public void ClearContainer()
        {
            storedCard = null;
            component = null;
            SelectedContainer = null;
            State = ContainerState.Empty;
        }

        private void SetColor()
        {
            _spriteRenderer.color = State switch
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