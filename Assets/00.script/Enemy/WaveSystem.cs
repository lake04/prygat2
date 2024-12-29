using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves;
    [SerializeField]
    private EnemySpawner enemyspawner;
    private int currentWaveIndex = -1; //현제 웨이브 인덱스

    //웨이브 정보 출력을 위한 Get 프로피터 (현제 웨이브 , 총 웨에브)
    public int CurrentWave => currentWaveIndex + 1; //시작이 0이기 때문에 1더함
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
    public float spawnTime; //현제 웨이브 적 생성 주기
    public int maxEenmyCount; //현제 웨이브 적 등장 숫자
    public GameObject[] enemyPrefabs;  //현제 웨이브 적 등장 종류
}
