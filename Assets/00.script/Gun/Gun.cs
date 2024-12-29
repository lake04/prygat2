using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private float curTime = 0.2f;
    [SerializeField]
    private bool isShot = true;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject spawn;

    SpriteRenderer spriteRenderer;
    [SerializeField]
    ParticleSystem Effector2D;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetMouseButtonDown(0) && isShot == true)
        {
            StartCoroutine(shot());
            Effector2D.Play();
        }   

    }

    private IEnumerator shot()
    {
        isShot = false;
       
      /*  Effector2D.Play();*/
        Instantiate(bullet, spawn.transform.position, transform.rotation);
        
        yield return curTime;
        /*Effector2D.Stop();*/
        isShot = true;
    }
}
