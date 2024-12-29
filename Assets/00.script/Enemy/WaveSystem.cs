using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves;
    [SerializeField]
    private EnemySpawner enemyspawner;
    private int currentWaveIndex = -1; //���� ���̺� �ε���

    //���̺� ���� ����� ���� Get �������� (���� ���̺� , �� ������)
    public int CurrentWave => currentWaveIndex + 1; //������ 0�̱� ������ 1����
    public int MaxWave => waves.Length;

    private void Start()
    {
        StartWave();
    }

    public void StartWave()
    {
        if (enemyspawner.EnemyList.Count == 0 && currentWaveIndex < waves.Length - 1)
        {
            currentWaveIndex++;
            enemyspawner.StartWave(waves[currentWaveIndex]);
        }
    }
}

[System.Serializable]
public struct Wave
{
    public float spawnTime; //���� ���̺� �� ���� �ֱ�
    public int maxEenmyCount; //���� ���̺� �� ���� ����
    public GameObject[] enemyPrefabs;  //���� ���̺� �� ���� ����
}
