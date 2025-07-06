using KalyoncuBusiness.Utils;
using UnityEngine;

public class Saka : Soldier
{
    [SerializeField] private GameObject bombPrefab;

    public override void Attack(Soldier target)
    {
        var arrow = Instantiate(bombPrefab, transform);

        arrow.GetComponent<Arrow>().OnEnd += OnEnd;
        arrow.GetComponent<Arrow>().SetTarget(target);
    }

    public override Soldier GetTarget()
    {
        // Kendisinin gelmemesi gerekli

        var allies = GetAllies();

        Soldier minHealthAlly = null;
        float minHealth = 0;

        foreach (var ally in allies)
        {
            if (minHealthAlly == null)
            {
                minHealthAlly = ally;
                minHealth = ally.GetHealth() / ally.GetMaxHealth();

                continue;
            }

            if (ally.GetHealth() / ally.GetMaxHealth() < minHealth)
            {
                minHealthAlly = ally;
                minHealth = ally.GetHealth() / ally.GetMaxHealth();
            }
        }

        return minHealthAlly;
    }

    private void OnEnd(object sender, Arrow.OnEndEventArgs e)
    {
        var damage = GetDamage();

        if (UtilsClass.ChanceControl(GetMissChance()))
        {
            UtilsClass.CreateWorldTextPopup(null, "Miss!", transform.position, 400, Color.white, new Vector3(transform.position.x, transform.position.y + 1), 1f);
            return;
        }

        if (UtilsClass.ChanceControl(GetCritChance()))
        {
            damage *= GetCritMultiplier();
        }

        var isMaxHealth = e.target.Heal(damage);

        if (isMaxHealth)
        {
            GetTarget();
        }
    }
}
