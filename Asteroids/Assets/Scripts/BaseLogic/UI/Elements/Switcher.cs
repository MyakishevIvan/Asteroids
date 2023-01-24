using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Asteroids.UI.Elements
{
    public class Switcher : BaseUiElement, IPointerClickHandler
    {
        [SerializeField] private RectTransform toggleIndicator;
        [SerializeField] private Image bg;
        [SerializeField] private float tweenTime;
        
        private float _onXPos;
        private float _offXPos;
        private Action _onClickAction;
        private Action _offClickAction;
        private bool _isOn;
        private float _currentTweenTime;

        private void Awake()
        {
            _onXPos = toggleIndicator.anchoredPosition.x;
            _offXPos = bg.rectTransform.rect.width + _onXPos - toggleIndicator.rect.width;
        }

        public void Init(bool isOn, Action onClikcAction, Action offClickAction)
        {
            _isOn = isOn;
            _onClickAction = onClikcAction;
            _offClickAction = offClickAction;
            ToggleMove(isOn, true);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SetValue();
        }

        private void SetValue()
        {
            _isOn = !_isOn;
            ToggleMove(_isOn);
        }

        private void ToggleMove(bool isOn, bool fastMove = false)
        {
            Move(isOn, fastMove);
            var currentAction = _isOn ? _onClickAction : _offClickAction;
            currentAction.Invoke();
        }

        private void Move(bool isOn, bool fastMove)
        {
            _currentTweenTime = fastMove ? 0f : tweenTime;
            
            if (isOn)
                toggleIndicator.DOAnchorPosX(_onXPos, _currentTweenTime);
            else
                toggleIndicator.DOAnchorPosX(_offXPos, _currentTweenTime);
        }
    }
}