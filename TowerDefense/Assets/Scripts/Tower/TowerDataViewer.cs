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
        // 출력해야 하는 타워 정보를 받아와서 저장
        currentTower = towerWeapon.GetComponent<TowerWeapon>();

        // 타워정보 Panel On
        gameObject.SetActive(true);

        // 타워 정보를 갱신
        UpdateTowerData();

        // 타워 오브젝트 주변에 표시되는 타워 공격 범위 Sprite On
        towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.Range);
    }

    public void OffPanel()
    {
        // 타워정보 Panel Off
        gameObject.SetActive(false);

        // 타워 공격 범위 Sprite Off
        towerAttackRange.OffAttackRange();
    }

    private void UpdateTowerData()
    {
        if(currentTower.WeaponType == WeaponType.Cannon || currentTower.WeaponType == WeaponType.Laser)
        {
            imageTower.rectTransform.sizeDelta = new Vector2(88, 59);
            textDamage.text = $"공격력 : {currentTower.Damage}";
        }
        else if(currentTower.WeaponType == WeaponType.Slow)
        {
            imageTower.rectTransform.sizeDelta = new Vector2(59, 59);
            textDamage.text = $"감속 : {currentTower.Slow * 100}%";
        }

        imageTower.sprite = currentTower.TowerSprite;
        
        textRate.text = $"공격속도 : {currentTower.Rate}";
        textRange.text = $"사거리 : {currentTower.Range}";
        textLevel.text = $"레벨 : {currentTower.Level}";
        textCost.text = currentTower.Level == currentTower.MaxLevel ? "MAX" : $"강화\n $ {currentTower.Cost}";
        textSell.text = $"판매\n $ {currentTower.SellCost}";

        // 업그레이드가 불가능해지면 버튼 비활성화
        buttonUpgrade.interactable = currentTower.Level < currentTower.MaxLevel ? true : false;
    }

    public void OnClickEventTowerUpgrade()
    {
        // 타워 업그레이드 시도 (성공 : true, 실패 : false)
        bool isSuccess = currentTower.Upgrade();

        if (isSuccess)
        {
            // 타워가 업그레이드 되었기 때문에 타워 정보 갱신
            UpdateTowerData();
            // 타워 주변에 보이는 공격 범위도 갱신
            towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.Range);
        }
        else
        {
            
        }
    }

    public void OnClickEventTowerSell()
    {
        // 타워 판매
        currentTower.Sell();

        // 선택한 타워가 사라져서 Panel, 공격범위 Off
        OffPanel();
    }
}
