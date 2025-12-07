using SpaceShooter;
using Towers;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class TowerAsset: ScriptableObject
    {
        public int goldCost = 15;
        public Sprite sprite;
        public Sprite GUISprite;
        public TurretProperties turretProperties;
        public float radius;
        public string nameOfTheTower;
        public EVariousMech Type;
    }
}

//� �������� ������� ��������� ������ �����, ��� ���: 
//1.���� ������ TowerAsset - ��� ScriptableObject
//2. ������ ������ ���-�� SO  ���� TowerAsset 
//3. �������� � ��� SO ������ ������� �����.
//4. ���� ������ TowerBuyControl, ������� ����� �� ������ ����� � ��������.
//������ ����� - ��� ���� ������ TowerBuyControl(������-������, �� ���. ���, ���� � ����� ���������
//�����). � � ���� ������� ��������: ����� ���������� �����.
//5. ������ ������� TowerBuyControl �������� �������� �������� Button,
//��� � OnClick ������ ����� Buy ������� TowerBuyControl.
//6. ����� �������, ����� ������������ ��� �� ��� ������,
//���������� ����� Buy �� ������� TowerBuyControl, ������� ��������
//����� TryBuild �� ������� TDPlayer � ��������� � ��������� ������ �����.
//7. TryBuild  ������������� ������ ������ - ������ ����� �����. ��� ���� ������ �������� 
//� ������� TDPlayer, � ������ TDPlayer ����� �� ��������� ������� � �������� PlayerCamp.
//8. ����� TryBuild  ����� �� � ������������������ ������� �������� ���� ������
//� SpriteRenderer-� � ����������� ��� ������ �� ������.