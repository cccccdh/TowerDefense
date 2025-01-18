using UnityEngine;

[CreateAssetMenu]
public class TowerTemplate : ScriptableObject
{
    public GameObject towerPrefabs;         // Ÿ�� ������ ���� ������
    public GameObject followTowerPrefab;    // �ӽ� Ÿ�� ������
    public Weapon[] weapon;                 // ���� �� Ÿ��(����) ����

    [System.Serializable]
    public struct Weapon
    {
        public Sprite sprite;       // �������� Ÿ�� �̹���
        public float damage;        // ���ݷ�
        public float slow;          // ���� �ۼ�Ʈ (0.2 = 20%)
        public float rate;          // ���ݼӵ�
        public float range;         // ���� ����
        public int cost;            // �ʿ� ���
        public int sell;            // Ÿ�� �Ǹ� �� ȹ�� ���
    }
}
