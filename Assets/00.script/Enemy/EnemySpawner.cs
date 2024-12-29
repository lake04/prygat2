using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    /*  [SerializeField]
      private GameObject enemyPrefab;*/
    [SerializeField]
    private GameObject enemyHPSliderPrefeb;
    [SerializeField]
    private Transform canvasTransform;
    /*[SerializeField]
    private float spawnTime;*/
    [SerializeField]
    private Transform[] wayPoints;
    [SerializeField]
    private GameManager gameManager;
    private Wave currentWave; //현제 웨이브 정보
    private int currentEnemyCount;
    private List<Enemy> enemyList;

    public List<Enemy> EnemyList => enemyList;

    public int CurrentEnemyCount => currentEnemyCount;
    public int MaxEnemyCount => currentWave.maxEenmyCount;

    private void Awake()
    {
        //적 리스트 메모리 할당
        enemyList = new List<Enemy>();
        //StartCoroutine("SpawnEnemy");
    }
    public void StartWave(Wave wave)
    {
        //매개변수로 받아온 웨이브 정보 저장
        currentWave = wave;
        currentEnemyCount = currentWave.maxEenmyCount;
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        //현제 웨이브에서 생성한 적 숫자
        int spawnEnemyCount = 0;
        while (spawnEnemyCount < currentWave.maxEenmyCount)
        {
            //GameObject clone = Instantiate(enemyPrefab);
            //현재 웨이브에 생성되어야 하는 적의 숫자만큼 적을 생성하고 코루틴 종료
            int enemyIndex = Random.Range(0, currentWave.enemyPrefabs.Length);
            GameObject clone = Instantiate(currentWave.enemyPrefabs[enemyIndex]);
            Enemy enemy = clone.GetComponent<Enemy>();

            enemy.Setup(this, wayPoints);
            enemyList.Add(enemy);

            SpawnEnemyHPSlider(clone);

            spawnEnemyCount++;

            yield return new WaitForSeconds(currentWave.spawnTime);
        }
    }

    public void DestroyEnemy(EnemyDestroyType type, Enemy enemy, int gold)
    {
        if (type == EnemyDestroyType.Arrive)
        {
            GameManager.instance.TakeDamage(1);
        }
        else if (type == EnemyDestroyType.Kill)
        {
            gameManager.CurrentGold += gold;
        }
        currentEnemyCount--;
        enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    private void SpawnEnemyHPSlider(GameObject enemy)
    {
        GameObject sliderClone = Instantiate(enemyHPSliderPrefeb);
        sliderClone.transform.SetParent(canvasTransform);
        sliderClone.transform.localScale = Vector3.one;

        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());      
    }
}
