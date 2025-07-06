using System.Collections;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    public static SoundEffectPlayer Instance;

    public AudioSource src;
    public AudioClip attack, damage, bowAttack, button, die, heal, win, lose;

    private void Awake()
    {
        Instance = this;
    }

    public void Attack()
    {
        src.PlayOneShot(attack, 0.5f);
    }

    public void Damage()
    {
        src.PlayOneShot(damage, 0.5f);
    }
    public void BowAttack()
    {
        src.PlayOneShot(bowAttack, 0.5f);
    }
    public void Button()
    {
        src.PlayOneShot(button, 0.5f);
    }
    public void Die()
    {
        src.PlayOneShot(die, 0.5f);
    }
    public void Heal()
    {
        src.PlayOneShot(heal, 0.5f);
    }
    public void Win()
    {
        src.Pause();
        src.PlayOneShot(win, 1f);
        DelayedNotify();
    }

    public void Lose()
    {
        src.Pause();
        src.PlayOneShot(lose, 0.5f);
        DelayedNotify();
    }

    private IEnumerator DelayedNotify()
    {
        yield return new WaitForSeconds(2);
        src.UnPause();
    }
}
