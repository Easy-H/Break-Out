using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [SerializeField] GameObject beforeContact = null;
    [SerializeField] GameObject ball;

    static GameObject created;
    static Transform tr;
    Rigidbody2D rb;
    
    [SerializeField] float movePower = 4;
    [SerializeField] float inputGravity = 3f;

    float horizontal;
    [SerializeField] float horizontalFactor;

    static int maxHp;
    static int hp = 3;

    [SerializeField] int setMaxHp = 5;
    [SerializeField] int setHP = 3;

    bool beforeInput = false;

    public static void AddHP() {
        if (hp < maxHp) {
            hp++;
            UIManager.instance.GaugeImage(1, maxHp, hp);
        }
    }

    public static void GetDamage(int damage) {
        if (hp > -5) {

            hp -= damage;
            UIManager.instance.GaugeImage(1, maxHp, hp);

            if (hp <= 0) {
                GameManager.instance.gameOver();
                return;
            }
        }
        Instantiate(created, tr.position + Vector3.up, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") && !GameObject.Equals(beforeContact, collision.gameObject))
        {
            GetDamage(1);
            Destroy(collision.gameObject);
            beforeContact = collision.gameObject;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") && !GameObject.Equals(beforeContact, collision.gameObject))
        {
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
            GetDamage(1);
            beforeContact = collision.gameObject;

        }
    }

    // Start is called before the first frame update
    void Start() {

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
        horizontal = Input.GetAxis("Horizontal");

#else
        if (!beforeInput)
        {
            if (horizontalFactor == 0)
            {

                if (Mathf.Abs(inputGravity * Time.deltaTime) > Mathf.Abs(horizontal))
                {
                    horizontal = 0;
                    return;
                }

                horizontal -= Mathf.Sign(horizontal) * inputGravity * Time.deltaTime;

                return;
            }
            else if (Mathf.Sign(horizontal) != Mathf.Sign(horizontalFactor))
            {
                horizontal += horizontalFactor * inputGravity * Time.deltaTime * 2;
            }
            horizontal += horizontalFactor * inputGravity * Time.deltaTime;
        }
#endif
        rb.velocity = new Vector3(horizontal * movePower, 0, 0);

    }

    public void HorizontalIn(float f) {
        horizontalFactor += f;

        if (Mathf.Abs(horizontalFactor) > 1)
            horizontalFactor = Mathf.Sign(horizontalFactor);

    }
    


}
