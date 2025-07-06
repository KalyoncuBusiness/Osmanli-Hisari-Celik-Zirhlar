using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWin : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        GameManager.OnGameEnd += OnGameEnd;
    }

    private void OnGameEnd(object sender, GameManager.OnGameEndEventArgs e)
    {
        if (e.isWin)
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
