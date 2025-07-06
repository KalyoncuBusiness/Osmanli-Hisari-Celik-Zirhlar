using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float targetHight;

    private float _speed = 3f;

    private bool _enemiesShowing = false;

    void Start()
    {
        GameManager.OnFightStart += OnFightStart;
        GameManager.OnFightEnd += OnFightEnd;
    }

    private void OnFightEnd(object sender, GameManager.OnFightEndEventArgs e)
    {
        transform.position = new Vector3(0, -2.5f, -10f);
    }

    private void OnFightStart(object sender, GameManager.OnFightStartEventArgs e)
    {
        TimeTickManager.OnTick += OnTick;
    }

    private void OnTick(object sender, TimeTickManager.OnTickEventArgs e)
    {
        var targetPosition = new Vector3(0, targetHight, -10);

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * TimeTickManager.TICK_TIMER_MAX);

        if (Vector2.Distance(transform.position, targetPosition) <= 0.1f)
        {
            TimeTickManager.OnTick -= OnTick;
        }
    }

    public void ToggleEnemyCam()
    {
        if (_enemiesShowing)
        {
            ShowAllies();
            _enemiesShowing = false;
        }
        else
        {
            ShowEnemies();
            _enemiesShowing = true;
        }
    }

    private void ShowEnemies()
    {
        transform.position = new Vector3(0, 18.75f, -10);
    }

    private void ShowAllies()
    {
        transform.position = new Vector3(0, -2.5f, -10);
    }
}
