using System;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed;

    private Soldier _target;

    private bool _isThrowed = false;

    public class OnEndEventArgs : EventArgs
    {
        public Soldier target;
    }

    public event EventHandler<OnEndEventArgs> OnEnd;

    private void OnDisable()
    {
        _isThrowed = false;

        TimeTickManager.OnTick -= OnTick;
    }

    private void OnDestroy()
    {
        _isThrowed = false;

        TimeTickManager.OnTick -= OnTick;
    }

    private void OnTick(object sender, TimeTickManager.OnTickEventArgs e)
    {
        if (!_isThrowed)
        {
            return;
        }

        if (_target == null)
        {
            _isThrowed = false;

            TimeTickManager.OnTick -= OnTick;

            Destroy(this.gameObject);

            return;
        }

        Vector3 targetPosition = Vector3.zero;
        Vector3 position = Vector3.zero;

        try
        {
            targetPosition = _target.transform.position;
            position = transform.position;
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        var direction = targetPosition - position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        transform.position = Vector2.MoveTowards(position, targetPosition, speed * TimeTickManager.TICK_TIMER_MAX);

        var distance = Vector2.Distance(transform.position, _target.transform.position);

        if (distance <= .5f)
        {
            if (OnEnd != null) OnEnd(this, new OnEndEventArgs { target = _target });

            _isThrowed = false;

            TimeTickManager.OnTick -= OnTick;

            Destroy(this.gameObject);
        }
    }

    public void SetTarget(Soldier target)
    {
        _target = target;

        _isThrowed = true;

        TimeTickManager.OnTick += OnTick;
    }
}
