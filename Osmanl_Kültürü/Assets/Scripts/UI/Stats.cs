using TMPro;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public static Stats Instance;

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI fireRateText;
    [SerializeField] private TextMeshProUGUI critChanceText;
    [SerializeField] private TextMeshProUGUI critMultiplierText;
    [SerializeField] private TextMeshProUGUI dodgeChanceText;

    [SerializeField] private GameObject healthUpgrade;
    [SerializeField] private GameObject damageUpgrade;
    [SerializeField] private GameObject fireRateUpgrade;
    [SerializeField] private GameObject critChanceUpgrade;
    [SerializeField] private GameObject critMultiplierUpgrade;
    [SerializeField] private GameObject dodgeChanceUpgrade;

    private CanvasGroup _canvasGroup;

    private Soldier _soldier;

    private const float UPGRADE_COST = 10f;

    private void Awake()
    {
        Instance = this;

        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        GameManager.OnFightStart += OnFightStart;
        GameManager.OnFightEnd += OnFightEnd;

        CloseStats();
    }

    private void OnFightEnd(object sender, GameManager.OnFightEndEventArgs e)
    {

    }

    private void OnFightStart(object sender, GameManager.OnFightStartEventArgs e)
    {
        CloseStats();
    }

    public void ShowStats(Soldier soldier)
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.interactable = true;

        healthText.text = "Can: " + soldier.GetMaxHealth().ToString();
        damageText.text = "Hasar: " + soldier.GetDamage().ToString();
        fireRateText.text = "Saldýrý Hýzý: " + (20 / soldier.GetAttackCooldown()).ToString();
        critChanceText.text = "Kritik Þansý: " + soldier.GetCritChance().ToString();
        critMultiplierText.text = "Kritik Çarpaný: " + soldier.GetCritMultiplier().ToString();
        dodgeChanceText.text = "Kaçýnma Þansý: " + soldier.GetDodgeChance().ToString();

        _soldier = soldier;

        healthUpgrade.SetActive(false);
        damageUpgrade.SetActive(false);
        fireRateUpgrade.SetActive(false);
        critChanceUpgrade.SetActive(false);
        critMultiplierUpgrade.SetActive(false);
        dodgeChanceUpgrade.SetActive(false);

        if (_soldier.GetMaxHealth() < _soldier.SoldierTypeSO.health * 1.5)
        {
            healthUpgrade.SetActive(true);
        }

        if (_soldier.GetDamage() < _soldier.SoldierTypeSO.damage * 1.5)
        {
            damageUpgrade.SetActive(true);
        }

        if (_soldier.GetAttackCooldown() > _soldier.SoldierTypeSO.attackCooldown - 5)
        {
            fireRateUpgrade.SetActive(true);
        }

        if (_soldier.GetCritChance() < _soldier.SoldierTypeSO.critChance * 1.5)
        {
            critChanceUpgrade.SetActive(true);
        }

        if (_soldier.GetCritMultiplier() < _soldier.SoldierTypeSO.critMultiplier * 1.5)
        {
            critMultiplierUpgrade.SetActive(true);
        }

        if (_soldier.GetDodgeChance() < _soldier.SoldierTypeSO.dodgeChance * 1.5)
        {
            dodgeChanceUpgrade.SetActive(true);
        }
    }

    public void ShowStats(Soldier soldier, bool showUpgrade)
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.interactable = true;

        healthText.text = "Can: " + soldier.GetMaxHealth().ToString();
        damageText.text = "Hasar: " + soldier.GetDamage().ToString();
        fireRateText.text = "Saldýrý Hýzý: " + (20 / soldier.GetAttackCooldown()).ToString();
        critChanceText.text = "Kritik Þansý: " + soldier.GetCritChance().ToString();
        critMultiplierText.text = "Kritik Çarpaný: " + soldier.GetCritMultiplier().ToString();
        dodgeChanceText.text = "Kaçýnma Þansý: " + soldier.GetDodgeChance().ToString();

        _soldier = soldier;

        healthUpgrade.SetActive(false);
        damageUpgrade.SetActive(false);
        fireRateUpgrade.SetActive(false);
        critChanceUpgrade.SetActive(false);
        critMultiplierUpgrade.SetActive(false);
        dodgeChanceUpgrade.SetActive(false);

        if (showUpgrade)
        {
            if (_soldier.GetMaxHealth() < _soldier.SoldierTypeSO.health * 1.5)
            {
                healthUpgrade.SetActive(true);
            }

            if (_soldier.GetDamage() < _soldier.SoldierTypeSO.damage * 1.5)
            {
                damageUpgrade.SetActive(true);
            }

            if (_soldier.GetAttackCooldown() > _soldier.SoldierTypeSO.attackCooldown - 5)
            {
                fireRateUpgrade.SetActive(true);
            }

            if (_soldier.GetCritChance() < _soldier.SoldierTypeSO.critChance * 1.5)
            {
                critChanceUpgrade.SetActive(true);
            }

            if (_soldier.GetCritMultiplier() < _soldier.SoldierTypeSO.critMultiplier * 1.5)
            {
                critMultiplierUpgrade.SetActive(true);
            }

            if (_soldier.GetDodgeChance() < _soldier.SoldierTypeSO.dodgeChance * 1.5)
            {
                dodgeChanceUpgrade.SetActive(true);
            }
        }
    }

    public void ShowStats(SoldierTypeSO soldier)
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.interactable = true;

        healthText.text = "Can: " + soldier.health.ToString();
        damageText.text = "Hasar: " + soldier.damage.ToString();
        fireRateText.text = "Saldýrý Hýzý: " + (20 / soldier.attackCooldown).ToString();
        critChanceText.text = "Kritik Þansý: " + soldier.critChance.ToString();
        critMultiplierText.text = "Kritik Çarpaný: " + soldier.critMultiplier.ToString();
        dodgeChanceText.text = "Kaçýnma Þansý: " + soldier.dodgeChance.ToString();

        healthUpgrade.SetActive(false);
        damageUpgrade.SetActive(false);
        fireRateUpgrade.SetActive(false);
        critChanceUpgrade.SetActive(false);
        critMultiplierUpgrade.SetActive(false);
        dodgeChanceUpgrade.SetActive(false);
    }

    public void CloseStats()
    {
        _canvasGroup.alpha = 0f;
        _canvasGroup.interactable = false;

        _soldier = null;

        healthUpgrade.SetActive(false);
        damageUpgrade.SetActive(false);
        fireRateUpgrade.SetActive(false);
        critChanceUpgrade.SetActive(false);
        critMultiplierUpgrade.SetActive(false);
        dodgeChanceUpgrade.SetActive(false);

        try
        {
            GridManager.Instance.ResetSelectedCells();
            EnemyGridManager.Instance.ResetSelectedCells();
        }
        catch (System.Exception ex)
        {

            Debug.LogException(ex);
        }

    }

    public void UpgradeHealth()
    {
        if (MoneyManager.Instance.Money >= UPGRADE_COST)
        {
            _soldier.SetMaxHelth(_soldier.GetMaxHealth() + (_soldier.SoldierTypeSO.health * .1f));

            Instance.ShowStats(_soldier);

            MoneyManager.Instance.RemoveMoney(10f);
        }

    }

    public void UpgradeDamage()
    {
        if (MoneyManager.Instance.Money >= UPGRADE_COST)
        {

            _soldier.SetDamage(_soldier.GetDamage() + (_soldier.SoldierTypeSO.damage * .1f));

            Instance.ShowStats(_soldier);

            MoneyManager.Instance.RemoveMoney(10f);
        }
    }

    public void UpgradeFireRate()
    {
        if (MoneyManager.Instance.Money >= UPGRADE_COST)
        {
            _soldier.SetAttackCooldown(_soldier.GetAttackCooldown() - 1);

            Instance.ShowStats(_soldier);

            MoneyManager.Instance.RemoveMoney(10f);
        }
    }

    public void UpgradeCritChance()
    {
        if (MoneyManager.Instance.Money >= UPGRADE_COST)
        {
            _soldier.SetCritChance(_soldier.GetCritChance() + (_soldier.SoldierTypeSO.critChance * .1f));

            Instance.ShowStats(_soldier);

            MoneyManager.Instance.RemoveMoney(10f);
        }
    }

    public void UpgradeCritMultiplier()
    {
        if (MoneyManager.Instance.Money >= UPGRADE_COST)
        {
            _soldier.SetCritMultiplier(_soldier.GetCritMultiplier() + (_soldier.SoldierTypeSO.critMultiplier * .1f));

            Instance.ShowStats(_soldier);

            MoneyManager.Instance.RemoveMoney(10f);
        }
    }

    public void UpgradeDodgeChange()
    {
        if (MoneyManager.Instance.Money >= UPGRADE_COST)
        {
            _soldier.SetDodgeChance(_soldier.GetDodgeChance() + (_soldier.SoldierTypeSO.dodgeChance * .1f));

            Instance.ShowStats(_soldier);

            MoneyManager.Instance.RemoveMoney(10f);
        }
    }
}
