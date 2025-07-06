using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Sprite[] images;

    private Image _image;
    private int index = 0;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    void Start()
    {
        _image.sprite = images[index];
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        index++;

        if (index >= images.Length)
        {
            SceneManager.LoadScene(1);
        }

        _image.sprite = images[index];
    }
}
