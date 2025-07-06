using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KalyoncuBusiness.MonoBehaviours
{
    public class HoverImage : MonoBehaviour, IPointerClickHandler
    {
        public Action OnClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke();
        }
    }
}