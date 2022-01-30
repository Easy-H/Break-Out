using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject ball = null;

    static GameObject created;
    static Transform tr;
    Rigidbody2D rb;

    [SerializeField] float movePower = 4;
    [SerializeField] float inputGravity = 3f;
    [SerializeField] float deadGravity = 3f;

    float horizontal;
    [SerializeField] float horizontalFactor;

    static int maxHp;
    static int hp = 3;

    [SerializeField] int setMaxHp = 5;
    [SerializeField] int setHP = 3;
    
    public static void AddHP()
    {
        if (hp < maxHp)
        {
            hp++;
            UIManager.instance.GaugeImage(1, maxHp, hp);
        }
    }

    public static void GetDamage(int damage)
    {
        if (hp > -5)
        {

            hp -= damage;
            UIManager.instance.GaugeImage(1, maxHp, hp);

            if (hp <= 0)
            {
                GameManager.instance.gameOver();
                return;
            }
        }
        Instantiate(created, tr.position + Vector3.up, Quaternion.identity);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") )
        {
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
            int damage = collision.gameObject.GetComponent<Bullet>().GetDamage();
            if (damage > 0)
                GetDamage(damage);

        }
    }

    // Start is called before the first frame update
    void Start()
    {

        tr = gameObject.transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
        created = ball;

        hp = setHP;
        maxHp = setMaxHp;
        horizontal = 0;
        horizontalFactor = 0;

    }

    // Update is called once per frame
    void Update()
    {
        //#if UNITY_STANDALONE_WIN
#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
        horizontalFactor = Input.GetAxisRaw("Horizontal");
#endif

        if (horizontalFactor == 0)
        {
            if (Mathf.Abs(inputGravity * Time.deltaTime) > Mathf.Abs(horizontal)) {
                horizontal = 0;
            }
            else {
                horizontal -= Mathf.Sign(horizontal) * deadGravity * Time.deltaTime;
            }
        }
        else if (Mathf.Sign(horizontal) != Mathf.Sign(horizontalFactor))
        {
            horizontal += horizontalFactor * (inputGravity + deadGravity) * Time.deltaTime;
        }
        else {
            horizontal += horizontalFactor * inputGravity * Time.deltaTime;
        }

        if (Mathf.Abs(horizontal) > 1)
            horizontal = Mathf.Sign(horizontal);

        rb.velocity = new Vector3(horizontal * movePower, 0, 0);
        
    }

    public void HorizontalIn(float f)
    {
        horizontalFactor += f;

        if (Mathf.Abs(horizontalFactor) > 1)
            horizontalFactor = Mathf.Sign(horizontalFactor);

    }
    

}
