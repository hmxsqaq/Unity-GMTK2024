using System;
using Hmxs_GMTK.Scripts.Shape;
using UnityEngine;

namespace Hmxs_GMTK.Scripts.Scene
{
    [RequireComponent(typeof(Collider2D))]
    public class ComponentCard : MonoBehaviour
    {
        public enum CardState
        {
            IsStored,
            ContainerSelected,
            IsDragged,
            Normal
        }

        [SerializeField] private ShapeComponent component;
        [SerializeField] private Sprite normalSprite;
        [SerializeField] private Sprite storedSprite;
        [SerializeField] private float rayLength = 1f;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float buffer = 0.2f;

        public Transform Position { get; set; }
        public ShapeComponent Component => component;

        private CardState _state = CardState.Normal;

        public CardState State
        {
            get => _state;
            private set
            {
                if (_state == value) return;
                _state = value;
                transform.rotation = _state == CardState.IsDragged ? Quaternion.Euler(0, 0, 15) : Quaternion.identity;
                SetSprite(_state is CardState.IsStored or CardState.ContainerSelected ? storedSprite : normalSprite);
                _spriteRenderer.sortingOrder = _state switch
                {
                    CardState.IsStored => -5,
                    CardState.ContainerSelected => 5,
                    CardState.IsDragged => 5,
                    CardState.Normal => -4,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        private Draggable2D _draggable;
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _collider;
        private Vector3 _storedPosition;
        private ComponentContainer _storedContainer;
        private float _counter;

        private void Start()
        {
            _draggable = GetComponent<Draggable2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<BoxCollider2D>();
            _counter = buffer;
        }

        private void Update()
        {
            if (State == CardState.IsStored) return;
            if (_counter < buffer || (ComponentContainer.SelectedContainer != null && _draggable.IsDragging)) State = CardState.ContainerSelected;
            else if (_draggable.IsDragging) State = CardState.IsDragged;
            else State = CardState.Normal;

            if (_counter < buffer) _counter += Time.deltaTime;
            transform.rotation = State == CardState.IsDragged ? Quaternion.Euler(0, 0, 15) : Quaternion.identity;
            transform.localScale = State == CardState.IsDragged ? transform.localScale * 1.1f : transform.localScale;
        }

        private void FixedUpdate()
        {
            if (!_draggable.IsDragging) return;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, rayLength, layerMask);
            if (hit.collider != null)
            {
                ComponentContainer container = hit.collider.GetComponent<ComponentContainer>();
                if (container != null)
                {
                    ComponentContainer.SelectedContainer = container;
                    return;
                }
            }

            hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, layerMask);
            if (hit.collider != null)
            {
                ComponentContainer container = hit.collider.GetComponent<ComponentContainer>();
                if (container != null)
                {
                    ComponentContainer.SelectedContainer = container;
                    return;
                }
            }

            ComponentContainer.SelectedContainer = null;
        }

        private void OnMouseDown()
        {
            AudioManager.Instance.PlayPickUpSound();
            // without card in hand, directly click the card
            if (State == CardState.IsStored)
            {
                _storedContainer.PopCard();
            }
            ComponentContainer.SelectedContainer = null;
        }

        private void OnMouseUp()
        {
            AudioManager.Instance.PlayPutDownSound();
            if (_counter < buffer)
            {
                State = CardState.Normal;
                return;
            }
            // with card in hand, click the container
            if (ComponentContainer.SelectedContainer != null)
            {
                _storedPosition = transform.position;
                transform.SetParent(ContainerManager.Instance.ContainerRoot);
                transform.position = ComponentContainer.SelectedContainer.CardPoint;
                State = CardState.IsStored;
                _storedContainer = ComponentContainer.SelectedContainer;
                ComponentContainer.SelectedContainer.GetCard(this);
            }
            else if (transform.position.y < CardManager.Instance.ReturnZoneY) ReturnToPosition();
            ComponentContainer.SelectedContainer = null;
        }

        public void ReturnToPosition() => transform.position = Position.position;

        public void Pop()
        {
            transform.SetParent(CardManager.Instance.CardsRoot);
            transform.position = _storedPosition;
            State = CardState.ContainerSelected;
            _storedContainer = null;
            _counter = 0;
        }

        private void SetCollider() => _collider.size = _spriteRenderer.sprite.bounds.size;

        private void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
            SetCollider();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.up * rayLength);
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayLength);
        }
    }
}