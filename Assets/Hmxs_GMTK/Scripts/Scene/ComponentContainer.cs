using System;
using HighlightPlus2D;
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
            }
        }

        private SpriteRenderer _spriteRenderer;
        private HighlightEffect2D _highlightEffect;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _highlightEffect = GetComponent<HighlightEffect2D>();
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
            AudioManager.Instance.PlayPickUpSound();
            // without card in hand, directly click the container that have card in it
            if (storedCard != null && State == ContainerState.Interactable) PopCard();
        }

        public void SetComponent(ComponentCard card)
        {
            // if there is already a card in the container, pop it
            if (storedCard != null) PopCard();

            // store the card into the container
            component = card.Component;
            card.IsStored = true;
            card.gameObject.SetActive(false);
            storedCard = card;

            SelectedContainer = null;
        }

        public void ClearContainer()
        {
            if (storedCard != null)
            {
                Destroy(storedCard.gameObject);
                storedCard = null;
            }
            component = null;
            SelectedContainer = null;
            State = ContainerState.Empty;
        }

        private void SetColor()
        {
            switch (State)
            {
                case ContainerState.Selected:
                    _highlightEffect.highlighted = true;
                    break;
                case ContainerState.Interactable:
                    _spriteRenderer.color = storedCard.gameObject.GetComponent<SpriteRenderer>().color;
                    _highlightEffect.highlighted = false;
                    break;
                case ContainerState.Empty:
                    _spriteRenderer.color = Color.white;
                    _highlightEffect.highlighted = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void PopCard()
        {
            storedCard.gameObject.SetActive(true);
            storedCard.IsStored = false;
            storedCard = null;
            component = null;
            SelectedContainer = null;
        }
    }
}