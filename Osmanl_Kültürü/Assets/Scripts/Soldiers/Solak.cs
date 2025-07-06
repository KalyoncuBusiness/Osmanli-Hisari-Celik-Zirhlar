using KalyoncuBusiness.Utils;
using UnityEngine;

public class Solak : Soldier
{
    [SerializeField] private GameObject arrowPrefab;

    public override void Attack(Soldier target)
    {
        var arrow = Instantiate(arrowPrefab, transform);

        arrow.GetComponent<Arrow>().OnEnd += OnEnd;
        arrow.GetComponent<Arrow>().SetTarget(target);
    }

    private void OnEnd(object sender, Arrow.OnEndEventArgs e)
    {
        var damage = GetDamage();

        if (UtilsClass.ChanceControl(GetMissChance()))
        {
            UtilsClass.CreateWorldTextPopup(null, "Iska!", transform.position, 400, Color.white, new Vector3(transform.position.x, transform.position.y + 1), 1f);
            return;
        }

        if (UtilsClass.ChanceControl(GetCritChance()))
        {
            damage *= GetCritMultiplier();
        }

        var isDead = e.target.GetAttacked(damage);

        if (isDead)
        {
            GetTarget();
        }
    }

    public override bool GetAttacked(float damage)
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
}
