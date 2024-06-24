using UnityEngine;
using UnityEngine.EventSystems;
using Joystick = Zparta.Joystick_Pack.Scripts.Base.Joystick;

namespace Zparta.Joystick_Pack.Scripts.Joysticks
{
    public class FloatingJoystick : Joystick
    {
        private bool _hasPrimaryTouch;
        private int _primaryTouchId;

        protected override void Start()
        {
            base.Start();

            background.gameObject.SetActive(false);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            Debug.LogWarning(_hasPrimaryTouch);
        
            if (!_hasPrimaryTouch)
            {
                _hasPrimaryTouch = true;
                _primaryTouchId = eventData.pointerId;
                background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
                background.gameObject.SetActive(true);
                
                base.OnPointerDown(eventData);
            }
        }

        public override void OnDrag(PointerEventData eventData)
        {
            if(eventData.pointerId != _primaryTouchId) return;
            
            base.OnDrag(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            Debug.LogWarning("Аттака");
            
            if(eventData.pointerId == _primaryTouchId)
            {
                background.gameObject.SetActive(false);
                base.OnPointerUp(eventData);
                _hasPrimaryTouch = false;
            }
        }

        protected override void HideJoystick()
        {
            background.gameObject.SetActive(false);
            _hasPrimaryTouch = false;
            base.HideJoystick();
        }
    }
}