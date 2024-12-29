using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager instance = null;
   
    [SerializeField]
    public float maxHP = 20;
    public float currentHP;

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    [SerializeField]
    private int currentGold = 100;

    public GameObject obj;

    public Slider hpSlider;

    public Image imageScreen;

    public int CurrentGold
    {
        set => currentGold = Mathf.Max(0, value);
        get => currentGold;
    }
    public static GameManager Instance
    {
        get
        {
            if(null == instance) return null;
            return instance;
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(obj);
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
        currentHP = maxHP;
        imageScreen.enabled = false;
    }


    private void Update()
    {
        hpSlider.value = currentHP / MaxHP;

        if(currentHP <=0)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        StopCoroutine(HitAlphaAnimation());
        StartCoroutine(HitAlphaAnimation());

        if (currentHP <= 0)
        {

        }
    }

    private IEnumerator HitAlphaAnimation()
    {
        imageScreen.enabled = true;
        Color color = imageScreen.color;
        color.a = 0.4f;
        imageScreen.color = color;

        while (color.a >= 0.0f)
        {
            color.a -= Time.deltaTime;
            imageScreen.color = color;
            yield return null;
        }
    }
}
