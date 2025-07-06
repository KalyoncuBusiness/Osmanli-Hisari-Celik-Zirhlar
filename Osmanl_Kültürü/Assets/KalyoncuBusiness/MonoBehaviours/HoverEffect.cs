using KalyoncuBusiness.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


namespace KalyoncuBusiness.MonoBehaviours
{
    public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private UnityEvent onClick;

        private GameObject _hoverObject;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _hoverObject = UI_Sprite.CreateHoverImage(transform);

            _hoverObject.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            _hoverObject.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);

            _hoverObject.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            _hoverObject.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            _hoverObject.GetComponent<HoverImage>().OnClick += OnClick;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_hoverObject == null) return;

            Destroy(_hoverObject);
        }

        public void OnClick()
        {
            onClick?.Invoke();
        }
    }
}