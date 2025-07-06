using KalyoncuBusiness.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "New Soldier SO", menuName = "Custom Object/Create New Soldier Type"), System.Serializable]
public class SoldierTypeSO : BaseScriptableObject
{
    public string nameString;
    public Sprite icon;

    public float health;
    public float cost;
    public float speed;
    public float damage;
    public float missChance;
    public float attackCooldown;
    public float critChance;
    public float dodgeChance;
    public float critMultiplier;
    public float attackRange;

    public Transform prefab;
    public Transform visual;

    public bool isEnemy;
}
