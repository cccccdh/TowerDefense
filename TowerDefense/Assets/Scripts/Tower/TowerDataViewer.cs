using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerDataViewer : MonoBehaviour
{
    [SerializeField]
    private Image imageTower;
    [SerializeField]
    private TextMeshProUGUI textDamage;
    [SerializeField]
    private TextMeshProUGUI textRate;
    [SerializeField]
    private TextMeshProUGUI textRange;
    [SerializeField]
    private TextMeshProUGUI textLevel;
    [SerializeField]
    private TextMeshProUGUI textCost;
    [SerializeField]
    private TextMeshProUGUI textSell;
    [SerializeField]
    private TowerAttackRange towerAttackRange;
    [SerializeField]
    private Button buttonUpgrade;

    private TowerWeapon currentTower;

    private void Awake()
    {
        OffPanel();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OffPanel();
        }
    }

    public void OnPanel(Transform towerWeapon)
    {
        // ����ؾ� �ϴ� Ÿ�� ������ �޾ƿͼ� ����
        currentTower = towerWeapon.GetComponent<TowerWeapon>();

        // Ÿ������ Panel On
        gameObject.SetActive(true);

        // Ÿ�� ������ ����
        UpdateTowerData();

        // Ÿ�� ������Ʈ �ֺ��� ǥ�õǴ� Ÿ�� ���� ���� Sprite On
        towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.Range);
    }

    public void OffPanel()
    {
        // Ÿ������ Panel Off
        gameObject.SetActive(false);

        // Ÿ�� ���� ���� Sprite Off
        towerAttackRange.OffAttackRange();
    }

    private void UpdateTowerData()
    {
        if(currentTower.WeaponType == WeaponType.Cannon || currentTower.WeaponType == WeaponType.Laser)
        {
            imageTower.rectTransform.sizeDelta = new Vector2(88, 59);
            textDamage.text = $"���ݷ� : {currentTower.Damage}";
        }
        else if(currentTower.WeaponType == WeaponType.Slow)
        {
            imageTower.rectTransform.sizeDelta = new Vector2(59, 59);
            textDamage.text = $"���� : {currentTower.Slow * 100}%";
        }

        imageTower.sprite = currentTower.TowerSprite;
        
        textRate.text = $"���ݼӵ� : {currentTower.Rate}";
        textRange.text = $"��Ÿ� : {currentTower.Range}";
        textLevel.text = $"���� : {currentTower.Level}";
        textCost.text = currentTower.Level == currentTower.MaxLevel ? "MAX" : $"��ȭ\n $ {currentTower.Cost}";
        textSell.text = $"�Ǹ�\n $ {currentTower.SellCost}";

        // ���׷��̵尡 �Ұ��������� ��ư ��Ȱ��ȭ
        buttonUpgrade.interactable = currentTower.Level < currentTower.MaxLevel ? true : false;
    }

    public void OnClickEventTowerUpgrade()
    {
        // Ÿ�� ���׷��̵� �õ� (���� : true, ���� : false)
        bool isSuccess = currentTower.Upgrade();

        if (isSuccess)
        {
            // Ÿ���� ���׷��̵� �Ǿ��� ������ Ÿ�� ���� ����
            UpdateTowerData();
            // Ÿ�� �ֺ��� ���̴� ���� ������ ����
            towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.Range);
        }
        else
        {
            
        }
    }

    public void OnClickEventTowerSell()
    {
        // Ÿ�� �Ǹ�
        currentTower.Sell();

        // ������ Ÿ���� ������� Panel, ���ݹ��� Off
        OffPanel();
    }
}
