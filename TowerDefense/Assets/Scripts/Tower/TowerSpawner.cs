using System.Collections;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private TowerTemplate[] towerTemplate;          // Ÿ�� ���� (���ݷ�, ���ݼӵ� ��)
    [SerializeField]
    private EnemySpawner enemySpawner;              // ���� �ʿ� �����ϴ� �� ����Ʈ ������ ��� ����
    [SerializeField]
    private PlayerGold playerGold;                  // Ÿ�� �Ǽ� �� ��� ���Ҹ� ����
    private bool isOnTowerButton = false;           // Ÿ�� �Ǽ� ��ư�� �������� üũ
    private GameObject followTowerClone = null;     // �ӽ� Ÿ�� ��� �Ϸ� �� ������ ���� �����ϴ� ����
    private int towerType;                          // Ÿ�� �Ӽ�

    public void ReadyToSpawnTower(int type)
    {
        towerType = type;
        
        // ��ư�� �ߺ��ؼ� ������ ���� ����
        if (isOnTowerButton) return;

        // Ÿ�� �Ǽ� ���� ���� Ȯ��
        // Ÿ���� �Ǽ��� ��ŭ ���� ������ Ÿ�� �Ǽ� x
        if (towerTemplate[towerType].weapon[0].cost > playerGold.CurrentGold) return;

        isOnTowerButton = true;

        followTowerClone = Instantiate(towerTemplate[towerType].followTowerPrefab);

        StartCoroutine("OnTowerCancelSystem");
    }

    public void SpawnTower(Transform tileTransform)
    {
        if (!isOnTowerButton)   return;

        Tile tile = tileTransform.GetComponent<Tile>();

        // Ÿ�� �Ǽ� ���� ���� Ȯ��
        // 1. ���� Ÿ���� ��ġ�� �̹� Ÿ���� �Ǽ��Ǿ� ������ Ÿ�� �Ǽ� x
        if(tile.IsBuildTower == true )  return;

        // �ٽ� Ÿ�� �Ǽ� ��ư�� ������ Ÿ���� �Ǽ��ϵ��� ���� ����
        isOnTowerButton = false;

        // Ÿ���� �Ǽ��Ǿ� ������ ����
        tile.IsBuildTower = true;

        // Ÿ�� �Ǽ��� �ʿ��� ��� ����
        playerGold.CurrentGold -= towerTemplate[towerType].weapon[0].cost;

        // ������ Ÿ���� ��ġ�� Ÿ�� �Ǽ�    
        Vector3 position = tileTransform.position + Vector3.back;
        GameObject clone = Instantiate(towerTemplate[towerType].towerPrefabs, position, Quaternion.identity);

        // Ÿ�� ���⿡ enemySpawner ���� ����
        clone.GetComponent<TowerWeapon>().Setup(enemySpawner, playerGold, tile);

        // Ÿ���� ��ġ�߱� ������ ���콺�� ����ٴϴ� �ӽ� Ÿ�� ����
        Destroy(followTowerClone);

        StopCoroutine("OnTowerCancelSystem");
    }

    private IEnumerator OnTowerCancelSystem()
    {
        while (true)
        {
            // ESCŰ �Ǵ� ���콺 ������ ��ư�� ������ �� Ÿ�� �Ǽ� ���
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                isOnTowerButton = false;
                // ���콺�� ����ٴϴ� �ӽ� Ÿ�� ����
                Destroy(followTowerClone);
                break;
            }

            yield return null;
        }
    }
}
