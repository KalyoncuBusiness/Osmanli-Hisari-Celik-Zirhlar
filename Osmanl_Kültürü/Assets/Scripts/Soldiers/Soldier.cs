using KalyoncuBusiness;
using KalyoncuBusiness.Utils;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Soldier : MonoBehaviour
{
    #region SoldierStats

    private float _maxHealth;
    private float _health;
    private float _speed;
    private float _damage;
    private float _missChance;
    private float _attackCooldown;
    private float _critChance;
    private float _dodgeChance;
    private float _critMultiplier;
    private float _attackRange;

    #endregion

    public static Soldier Create(Vector3 worldPosition, SoldierTypeSO soldierTypeSO)
    {
        if (!soldierTypeSO.isEnemy)
        {
            MoneyManager.Instance.RemoveMoney(soldierTypeSO.cost);
        }

        Transform placedObjectTransform = Instantiate(soldierTypeSO.prefab, worldPosition, Quaternion.Euler(0, 0, 0));

        Soldier soldier = placedObjectTransform.GetComponent<Soldier>();

        soldier._isEnemy = soldierTypeSO.isEnemy;
        soldier._soldierTypeSO = soldierTypeSO;

        soldier._maxHealth = soldierTypeSO.health;
        soldier._health = soldier._maxHealth;
        soldier._speed = soldierTypeSO.speed;
        soldier._damage = soldierTypeSO.damage;
        soldier._missChance = soldierTypeSO.missChance;
        soldier._attackCooldown = soldierTypeSO.attackCooldown;
        soldier._critChance = soldierTypeSO.critChance;
        soldier._dodgeChance = soldierTypeSO.dodgeChance;
        soldier._critMultiplier = soldierTypeSO.critMultiplier;
        soldier._attackRange = soldierTypeSO.attackRange;

        return soldier;
    }

    private bool _isAlive;

    private bool _isEnemy;
    private SoldierTypeSO _soldierTypeSO;

    private Soldier _target;
    private bool _isWalking;

    private int _attackedTick;

    public bool IsEnemy => _isEnemy;
    public SoldierTypeSO SoldierTypeSO => _soldierTypeSO;

    private FloatingHealthBar _healthBar;
    private Animator _animator;

    private void Awake()
    {
        _healthBar = GetComponentInChildren<FloatingHealthBar>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.OnFightStart += OnFight;
    }

    public virtual void OnFight(object e, OnFightStartEventArgs eventArgs)
    {
        TimeTickManager.OnTick += OnTick;

        _isAlive = true;

        _health = _maxHealth;

        if (_healthBar != null) _healthBar.UpdateHealthBar(_health, _maxHealth);

        _target = GetTarget();
    }

    public virtual Soldier GetTarget()
    {
        var target = GetClosestEnemy();

        _isWalking = true;

        if (_animator != null)
        {
            _animator.SetTrigger("Walk");
        }

        return target;
    }

    private void OnTick(object sender, TimeTickManager.OnTickEventArgs e)
    {
        if (_target == null && GetEnemies().Count > 0)
        {
            _target = GetTarget();
        }

        if (SoldierTypeSO.id == "cddb4010-0814-480d-9942-9274e17a6dfc" || SoldierTypeSO.id == "2e6fedde-a0f6-4e17-8177-5d66f3ab0b32")
        {
            _target = GetTarget();
        }

        if (_target != null)
        {
            var targetPosition = _target.transform.position;
            var position = transform.position;

            var direction = targetPosition - position;
            direction.Normalize();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(Vector3.forward * angle);

            var distance = Vector2.Distance(transform.position, _target.transform.position);

            if (distance > GetAttackRange())
            {
                _isWalking = true;
            }
        }

        if (_isWalking && _target != null)
        {
            Walk();

            var distance = Vector2.Distance(transform.position, _target.transform.position);

            if (distance <= _attackRange)
            {
                _isWalking = false;

                _attackedTick = e.tick;
            }
        }

        if (!_isWalking && _target != null && e.tick >= _attackedTick + _attackCooldown)
        {
            Attack(_target);

            if (_animator != null)
            {
                _animator.SetTrigger("Attack");
            }

            if (SoldierTypeSO.attackRange > 2)
            {
                SoundEffectPlayer.Instance.BowAttack();
            }
            else
            {
                SoundEffectPlayer.Instance.Attack();
            }

            _attackedTick = e.tick;
        }

    }

    private void Walk()
    {
        var targetPosition = _target.transform.position;
        var position = transform.position;

        transform.position = Vector2.MoveTowards(position, targetPosition, _speed * TimeTickManager.TICK_TIMER_MAX);
    }

    public virtual void Attack(Soldier target)
    {
    }

    public virtual bool GetAttacked(float damage)
    {
        if (UtilsClass.ChanceControl(GetDodgeChance()))
        {
            UtilsClass.CreateWorldTextPopup(null, "Kaçýnma!", transform.position, 400, Color.magenta, new Vector3(transform.position.x, transform.position.y + 1), 1f);
            return false;
        }

        UtilsClass.CreateWorldTextPopup(null, damage.ToString(), transform.position, 400, Color.red, new Vector3(transform.position.x, transform.position.y + 1), 1f);

        var remainingHealt = DealDamage(damage);

        if (remainingHealt <= 0)
        {
            KillSelf();

            return true;
        }

        return false;
    }

    private Soldier GetClosestEnemy()
    {
        var enemies = GetEnemies();

        Soldier closestEnemy = null;
        float closestDistance = 0;

        foreach (var enemy in enemies)
        {
            var distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (closestEnemy == null)
            {
                closestEnemy = enemy;
                closestDistance = distance;
                continue;
            }

            if (closestDistance > distance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    private Soldier GetClosestAlly()
    {
        var allies = GetAllies();

        Soldier closestAlly = null;
        float closestDistance = 0;

        foreach (var ally in allies)
        {
            if (closestAlly == null)
            {
                closestAlly = ally;
                continue;
            }

            var distance = Vector2.Distance(transform.position, ally.transform.position);

            if (closestDistance > distance)
            {
                closestDistance = distance;
                closestAlly = ally;
            }
        }

        return closestAlly;
    }

    public List<Soldier> GetEnemies()
    {
        var soldiers = FindObjectsOfType<Soldier>();

        List<Soldier> enemies = new List<Soldier>();

        foreach (var soldier in soldiers)
        {
            if (soldier.IsEnemy != IsEnemy)
            {
                enemies.Add(soldier);
            }
        }

        return enemies;
    }

    public List<Soldier> GetAllies()
    {
        var soldiers = FindObjectsOfType<Soldier>();

        List<Soldier> allies = new List<Soldier>();

        foreach (var soldier in soldiers)
        {
            if (soldier.SoldierTypeSO.id == "cddb4010-0814-480d-9942-9274e17a6dfc" || soldier.SoldierTypeSO.id == "2e6fedde-a0f6-4e17-8177-5d66f3ab0b32")
            {
                continue;
            }

            if (soldier.IsEnemy == IsEnemy)
            {
                allies.Add(soldier);
            }
        }

        return allies;
    }

    #region StatControls

    public float GetHealth()
    {
        return _health;
    }

    public float GetMaxHealth()
    {
        return _maxHealth;
    }

    public void SetMaxHelth(float health)
    {
        _maxHealth = health;
    }

    public float DealDamage(float damage)
    {
        _health -= damage;

        if (damage > 0)
        {
            Instantiate(Assets.i.p_Blood, transform.position, Quaternion.identity);
        }

        if (_healthBar != null) _healthBar.UpdateHealthBar(_health, _maxHealth);

        SoundEffectPlayer.Instance.Damage();

        return _health;
    }

    public bool Heal(float value)
    {
        bool isMaxHealth = false;

        _health += value;

        if (value > 0)
        {
            Instantiate(Assets.i.p_Heal, transform.position, Quaternion.identity);
        }

        if (_health > _maxHealth)
        {
            _health = _maxHealth;
            isMaxHealth = true;
        }

        if (_healthBar != null) _healthBar.UpdateHealthBar(_health, _maxHealth);

        SoundEffectPlayer.Instance.Heal();

        return isMaxHealth;
    }

    public float GetDamage()
    {
        return _damage;
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    public float GetAttackCooldown()
    {
        return _attackCooldown;
    }

    public void SetAttackCooldown(float attackCooldown)
    {
        _attackCooldown = attackCooldown;
    }

    public float GetMissChance()
    {
        return _missChance;
    }

    public float GetCritChance()
    {
        return _critChance;
    }

    public void SetCritChance(float critChance)
    {
        _critChance = critChance;
    }

    public float GetDodgeChance()
    {
        return _dodgeChance;
    }

    public void SetDodgeChance(float dodgeChance)
    {
        _dodgeChance = dodgeChance;
    }

    public float GetCritMultiplier()
    {
        return _critMultiplier;
    }

    public void SetCritMultiplier(float critMultiplier)
    {
        _critMultiplier = critMultiplier;
    }

    public float GetAttackRange()
    {
        return _attackRange;
    }

    public void SetAttackRange(float attackRange)
    {
        _attackRange = attackRange;
    }

    public void SetIsWalking(bool value)
    {
        _isWalking = value;
    }

    #endregion

    public void DestroySelf()
    {
        if (!_isAlive)
        {
            return;
        }

        MoneyManager.Instance.AddMoney(_soldierTypeSO.cost);

        TimeTickManager.OnTick -= OnTick;

        Destroy(this.gameObject);

        _isAlive = false;
    }

    public void DestroySelf(bool money)
    {
        if (!_isAlive)
        {
            return;
        }

        if (money)
        {
            MoneyManager.Instance.AddMoney(_soldierTypeSO.cost);
        }

        TimeTickManager.OnTick -= OnTick;

        Destroy(this.gameObject);

        _isAlive = false;
    }

    public void KillSelf()
    {
        if (!_isAlive)
        {
            return;
        }

        if (IsEnemy)
        {
            MoneyManager.Instance.AddMoney(_soldierTypeSO.cost);
        }

        SoundEffectPlayer.Instance.Die();

        TimeTickManager.OnTick -= OnTick;

        Destroy(gameObject);

        _isAlive = false;
    }

    private void OnDisable()
    {
        TimeTickManager.OnTick -= OnTick;
        GameManager.OnFightStart -= OnFight;

        _isAlive = false;
    }

    private void OnDestroy()
    {
        TimeTickManager.OnTick -= OnTick;
        GameManager.OnFightStart -= OnFight;

        _isAlive = false;
    }

}
