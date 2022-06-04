using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserInput {
    [SerializeField] float movePower = 8f;
    [SerializeField] float inputGravity = 6f;
    [SerializeField] float deadGravity = 10f;

    float horizontal = 0;

    public float GetHorizontal(float horizontalFactor) {
        if (horizontalFactor == 0)
        {
            if (Mathf.Abs(inputGravity * Time.deltaTime) > Mathf.Abs(horizontal))
            {
                horizontal = 0;
            }
            else
            {
                horizontal -= Mathf.Sign(horizontal) * deadGravity * Time.deltaTime;
            }
        }
        else if (Mathf.Sign(horizontal) != Mathf.Sign(horizontalFactor))
        {
            horizontal += horizontalFactor * (inputGravity + deadGravity) * Time.deltaTime;
        }
        else
        {
            horizontal += horizontalFactor * inputGravity * Time.deltaTime;
        }

        if (Mathf.Abs(horizontal) > 1)
            horizontal = Mathf.Sign(horizontal);

        return horizontal * movePower;
    }
}

[System.Serializable]
public class HP {
    [SerializeField] int hp = 3;
    [SerializeField] int maxHp = 5;
    
    public void AddHP() {
        if (hp < maxHp) {
            hp++;
            UIManager.instance.GaugeImage(1, maxHp, hp);
        }
    }
    public bool GetDamaged(int damage) {
        if (hp > -5) {
            hp -= damage;
            UIManager.instance.GaugeImage(1, maxHp, hp);

            if (hp <= 0) {
                return false;
            }
        }
        return true;
    }
    public void Clear() {
        hp = -10;
    }
}

public class Player : MonoBehaviour
{
    public static Player instance = null;
    
    static Transform tr;
    Rigidbody2D rb;

    [SerializeField] UserInput userInput = null;
    [SerializeField] HP hp = null;
    [SerializeField] Vector2 range;

    float horizontalFactor;
    
    public void AddHP()
    {
        hp.AddHP();
    }

    public void GetDamage(int damage)
    {
        if (hp.GetDamaged(damage)) {
            GongManager.instance.Create();

        }
        else {
            GameManager.instance.gameOver();
        }
    }

    public void Clear() {
        hp.Clear();
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

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        tr = gameObject.transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
        
        horizontalFactor = 0;

    }

    // Update is called once per frame
    void Update()
    {
        /*
        //#if UNITY_STANDALONE_WIN
#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
        horizontalFactor = Input.GetAxisRaw("Horizontal");
#endif
        
        rb.velocity = new Vector3(userInput.GetHorizontal(horizontalFactor), 0, 0);*/

        tr.position = new Vector3(Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, range.x, range.y), tr.position.y, tr.position.z);
        
    }

    public void HorizontalIn(float f)
    {
        horizontalFactor += f;

        if (Mathf.Abs(horizontalFactor) > 1)
            horizontalFactor = Mathf.Sign(horizontalFactor);

    }
    

}
