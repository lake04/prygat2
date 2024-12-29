using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("player")]
    public float moveSpeed = 5f;
    bool isJumping = false;
    public float jumpPower = 10f;

    //공격
    [Header("attack")]
    private float curTime;
    public Transform rigntPos;
    public Transform leftPos;
    public Vector2 boxSize;

    [Header("Ui")]
    [SerializeField]
    private bool isLoda;

    [Header("Gun")]
    [SerializeField]
    private GameObject gun;

    SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody2D;
    private new Collider2D Collider2D;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
       
    }
    private void Awake()
    {
      
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        attack();

        if (spriteRenderer.flipX == true)
        {
            gun.transform.rotation = Quaternion.Euler(0,0,0);
        }
        else gun.transform.rotation = Quaternion.Euler(0,180, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground")) isJumping = true;
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HpMap"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(2);
                
                Debug.Log("파쿠르 맵 입장");
            }
        }
       else if (collision.gameObject.CompareTag("Book"))
        {
            SceneManager.LoadScene(1);
            GameManager.instance.currentHP++;
            
        }
    }
      
    
    private void FixedUpdate()
    {
        MoveTo();
    }

    #region 이동
    void MoveTo()
    {
        if (Input.GetButton("Horizontal"))
        {
            float h = Input.GetAxisRaw("Horizontal");
            Vector3 dir = new Vector3(h, 0f, 0f).normalized;
            transform.Translate(dir * moveSpeed * Time.deltaTime);
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
    }

    void Jump()
    {
        if (!isJumping)
            return;

        //Prevent Velocity amplification.
        rigidbody2D.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigidbody2D.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }
    #endregion

    private void attack()
    {
        if (Input.GetKeyDown(KeyCode.V) && curTime <= 0)
        {

            if (spriteRenderer.flipX == true)
            {
                Collider2D[] collider2D = Physics2D.OverlapBoxAll(leftPos.position, boxSize, 0);
                foreach (Collider2D collider in collider2D)
                {
                    if (collider.tag == "Enemy") collider.GetComponent<EnemyHP>().TakeDamage(2);

                }
                curTime = 0.5f;
                Debug.Log("leftAttack");
            }
            if (spriteRenderer.flipX == false)
            {
                Collider2D[] collider2D = Physics2D.OverlapBoxAll(rigntPos.position, boxSize, 0);
                foreach (Collider2D collider in collider2D)
                {
                    if (collider.tag == "Enemy") collider.GetComponent<EnemyHP>().TakeDamage(2);

                }
                curTime = 0.5f;
                Debug.Log("RightAttack");
            }
        }

        else curTime -= Time.deltaTime;


    }
    private void OnDrawGizmos()
    {
        //공격 범위
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(leftPos.position, boxSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(rigntPos.position, boxSize);

    }
}
