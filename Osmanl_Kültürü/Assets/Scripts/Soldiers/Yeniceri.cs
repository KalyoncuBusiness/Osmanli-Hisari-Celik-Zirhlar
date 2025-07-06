using KalyoncuBusiness.Utils;
using UnityEngine;

public class Yeniceri : Soldier
{

    public override void Attack(Soldier target)
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

        var isDead = target.GetAttacked(damage);

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
