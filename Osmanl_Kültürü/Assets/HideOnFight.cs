using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class HideOnFight : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        _canvasGroup.alpha = 1.0f;
        _canvasGroup.interactable = true;

        GameManager.OnFightStart += OnFightStart;
        GameManager.OnFightEnd += OnFightEnd;
    }

    private void OnFightEnd(object sender, GameManager.OnFightEndEventArgs e)
    {
        _canvasGroup.alpha = 1.0f;
        _canvasGroup.interactable = true;
    }

    private void OnFightStart(object sender, GameManager.OnFightStartEventArgs e)
    {
        _canvasGroup.alpha = 0f;
        _canvasGroup.interactable = false;
    }

    private void Reset()
    {
        var canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            gameObject.AddComponent<CanvasGroup>();
        }
    }
}
