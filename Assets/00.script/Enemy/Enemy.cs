using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyDestroyType { Kill = 0, Arrive }
public class Enemy : MonoBehaviour
{
    private int wayPointCount;
    private Transform[] wayPoints;
    private int currentIndex = 0;
    private Movement2D movement2D;
    private EnemySpawner enemySpawner;
    [SerializeField]
    private int gold = 10;

    public void Setup(EnemySpawner enemySpawner, Transform[] wayPoints)
    {
        movement2D = GetComponent<Movement2D>();
        this.enemySpawner = enemySpawner;

        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        transform.position = wayPoints[currentIndex].position;

        StartCoroutine("OnMove");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Flag")
        {
            OnDie(EnemyDestroyType.Arrive);
        }
    }


    private IEnumerator OnMove()
    {
        NextMoveTo();

        while (true)
        {

            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed)
            {
                NextMoveTo();
            }

            yield return null;
        }
    }
    private void NextMoveTo()
    {
        if (currentIndex < wayPointCount - 1)
        {
            transform.position = wayPoints[currentIndex].position;
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else
        {
            gold = 0;
            //Destroy(gameObject);
            OnDie(EnemyDestroyType.Arrive);
            
        }
    }

    public void OnDie(EnemyDestroyType type)
    {
        enemySpawner.DestroyEnemy(type, this, gold);
        
    }

}
