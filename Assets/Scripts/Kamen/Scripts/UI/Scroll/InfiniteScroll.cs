using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

namespace Kamen.UI
{
    public class InfiniteScroll : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IScrollHandler
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Canvas _currentCanvas;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private ScrollContent _scrollContent;

        [Header("Main Settings")]
        [SerializeField] private float _outOfBoundsThreshold;
        [SerializeField] private bool _isUseSnap;

        [Header("Snap Settigns")]
        [SerializeField] private float _snapSpeed;
        [SerializeField] private float _minVelocityToDisableInertia;

        [Header("Variables")]
        private Vector2 _lastDragPosition;
        private float _currentOutOfBoundsThreshold;

        [Header("Infinity scroll variables")]
        private bool _isPositiveScroll;
        private int _scrollDirection;
        private float _thresholdValue;
        private float _itemPositionValue;

        [Header("Snap scroll variables")]
        private int _selectedPanelId;
        private Vector3 _snapPosition;
        private bool _isScrolling;
        private float _scrollVelocity;

        [Header("Nearest panel variables")]
        private float _nearestPosition;
        private float _nearestDistance;

        #endregion

        #region Unity Methods

        private void Start()
        {
            Initialize();
        }
        private void FixedUpdate()
        {
            if (!_isUseSnap) return;

            SearchNearestPanel();
            _scrollVelocity = Mathf.Abs(_scrollContent.Type == ScrollContent.ScrollType.Horizontal ? _scrollRect.velocity.x : _scrollRect.velocity.y);

            if (_scrollVelocity < _minVelocityToDisableInertia && !_isScrolling) _scrollRect.inertia = false;
            if (_isScrolling || _scrollVelocity > _minVelocityToDisableInertia) return;

            SetSnapPosition();
        }

        #endregion

        #region Control Methods

        private void Initialize()
        {
            _currentOutOfBoundsThreshold = _outOfBoundsThreshold * _currentCanvas.transform.localScale.x;
            _scrollContent.Initialize();

            _scrollRect.onValueChanged.AddListener(HandleScroll);
        }

        #endregion

        #region Scroll Methods

        public void OnScroll(PointerEventData eventData)
        {
            SetDirection(eventData);
        }
        private void HandleScroll(Vector2 handlePosition)
        {
            int currentItemIndex = _isPositiveScroll ? _scrollRect.content.childCount - 1 : 0;
            Transform currentItem = _scrollRect.content.GetChild(currentItemIndex);
            if (!ReachedThreshold(currentItem)) return;

            int endItemIndex = _isPositiveScroll ? 0 : _scrollRect.content.childCount - 1;
            Transform endItem = _scrollRect.content.GetChild(endItemIndex);
            Vector3 newPosition = CalculateNewPosition(endItem);

            currentItem.position = newPosition;
            currentItem.SetSiblingIndex(endItemIndex);
        }
        public void Scrolling(bool scroll)
        {
            _isScrolling = scroll;
            if (scroll) _scrollRect.inertia = true;
        }

        #endregion

        #region Drag Methods

        public void OnBeginDrag(PointerEventData eventData)
        {
            _lastDragPosition = eventData.position;
            Scrolling(true);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Scrolling(false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            SetDirection(eventData);
        }

        #endregion

        #region Calculate Methods

        private void SetDirection(PointerEventData eventData)
        {
            switch (_scrollContent.Type)
            {
                case ScrollContent.ScrollType.Horizontal:
                    _isPositiveScroll = eventData.position.x > _lastDragPosition.x;
                    break;
                case ScrollContent.ScrollType.Vertical:
                    _isPositiveScroll = eventData.position.y < _lastDragPosition.y;
                    break;
            }
            _lastDragPosition = eventData.position;
            _scrollDirection = _isPositiveScroll ? 1 : -1;
        }
        private Vector3 CalculateNewPosition(Transform endItem)
        {
            Vector3 newPosition = endItem.position;
            switch (_scrollContent.Type)
            {
                case ScrollContent.ScrollType.Horizontal:
                    newPosition.x = endItem.position.x - (_scrollContent.ChildWidth + _scrollContent.ItemSpacing) * _scrollDirection;
                    newPosition.z = transform.position.z;
                    break;
                case ScrollContent.ScrollType.Vertical:
                    newPosition.y = endItem.position.y + (_scrollContent.ChildHeight + _scrollContent.ItemSpacing) * _scrollDirection;
                    newPosition.z = transform.position.z;
                    break;
            }
            return newPosition;
        }
        private bool ReachedThreshold(Transform item)
        {
            switch (_scrollContent.Type)
            {
                case ScrollContent.ScrollType.Horizontal:
                    _thresholdValue = transform.position.x + (_scrollContent.Width / 2f + _currentOutOfBoundsThreshold) * _scrollDirection;
                    _itemPositionValue = item.position.x - _scrollContent.ChildWidth / 2f * _scrollDirection;
                    return _isPositiveScroll ? _itemPositionValue > _thresholdValue : _itemPositionValue < _thresholdValue;

                case ScrollContent.ScrollType.Vertical:
                    _thresholdValue = transform.position.y - (_scrollContent.Height / 2f + _currentOutOfBoundsThreshold) * _scrollDirection;
                    _itemPositionValue = item.position.y + _scrollContent.ChildHeight / 2f * _scrollDirection;
                    return _isPositiveScroll ? _itemPositionValue < _thresholdValue : _itemPositionValue > _thresholdValue;
            }

            return false;
        }
        private void SetSnapPosition()
        {
            _snapPosition = _scrollRect.content.position;

            switch (_scrollContent.Type)
            {
                case ScrollContent.ScrollType.Horizontal:
                    _snapPosition.x = Mathf.SmoothStep(_scrollRect.content.position.x, _scrollRect.content.position.x + CalculateDifference(), _snapSpeed * Time.fixedDeltaTime);

                    break;
                case ScrollContent.ScrollType.Vertical:
                    _snapPosition.y = Mathf.SmoothStep(_scrollRect.content.position.y, _scrollRect.content.position.y + CalculateDifference(), _snapSpeed * Time.fixedDeltaTime);

                    break;
            }
            _scrollRect.content.position = _snapPosition;
        }
        private void SearchNearestPanel()
        {
            _nearestPosition = float.MaxValue;
            for (int i = 0; i < _scrollRect.content.childCount; i++)
            {
                _nearestDistance = Vector3.Distance(transform.position, _scrollRect.content.GetChild(i).position);
                if (_nearestDistance < _nearestPosition)
                {
                    _nearestPosition = _nearestDistance;
                    _selectedPanelId = i;
                }
            }
        }
        private float CalculateDifference()
        {
            return _scrollContent.Type switch
            {
                ScrollContent.ScrollType.Horizontal => transform.position.x - _scrollRect.content.GetChild(_selectedPanelId).position.x,
                ScrollContent.ScrollType.Vertical => transform.position.y - _scrollRect.content.GetChild(_selectedPanelId).position.y,
                _ => 0f
            };
        }

        #endregion
    }
}