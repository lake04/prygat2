using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f;

    private void Awake()
    {
       Destroy(gameObject,1);
    }

    void Update()
    {
        
        if (transform.rotation.y == 0) 
        {
            transform.Translate(transform.right *-1  * moveSpeed * Time.deltaTime);
        }
        else 
        {
            transform.Translate(transform.right * moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") //���� �±׿� ������
        {
            Destroy(gameObject); //���� ������Ʈ ����
            collision.GetComponent<EnemyHP>().TakeDamage(1);

        }
    }
}
