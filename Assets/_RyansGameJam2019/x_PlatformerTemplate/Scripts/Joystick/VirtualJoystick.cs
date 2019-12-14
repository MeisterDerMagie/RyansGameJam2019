using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Platformer
{
    public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        Image bgImg; //Border of UI Joystick
        Image joyImg; //Joystic

        public static Vector3 joystickMoveDir;

        public static bool isAttack;

        private void Start()
        {
            bgImg = GetComponent<Image>();
            joyImg = transform.GetChild(0).GetComponent<Image>(); //Get first child Image to chache joystick
        }

        //Method triggers when finger presses screen
        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        //The method works when the finger moves
        public void OnDrag(PointerEventData eventData)
        {
            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
            {
                //We carry out calculations depending on the size of the joystick
                pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
                pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

                float x = (bgImg.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
                float y = (bgImg.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;

                Vector3 dir = new Vector3(x, 0, y);
                dir = (dir.magnitude > 1) ? dir.normalized : dir;

                //Move child image
                joyImg.rectTransform.anchoredPosition = new Vector3(dir.x * (bgImg.rectTransform.sizeDelta.x / 3), dir.z * (bgImg.rectTransform.sizeDelta.y / 3));

                if (dir.x > 0)
                    dir.x = Mathf.Ceil(dir.x);
                else if (dir.x < 0)
                    dir.x = Mathf.Floor(dir.x);

                joystickMoveDir = dir;

            }
        }

        //The method works when the finger up
        public void OnPointerUp(PointerEventData eventData)
        {
            //reset child image position
            joystickMoveDir = Vector3.zero;
            joyImg.rectTransform.anchoredPosition = Vector3.zero;
        }

    }
}