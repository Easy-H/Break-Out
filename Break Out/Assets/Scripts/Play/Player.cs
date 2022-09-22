using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HP {

}

public class Player : MonoBehaviour {
    public static Player Instance { get; private set; }

    Transform tr;
    Rigidbody2D rb;

    [SerializeField] int hp = 3;
    [SerializeField] int maxHp = 5;

    [SerializeField] Vector2 range = Vector2.one;

    float horizontalFactor;

    public void AddHP() {
        hp += 1;
        if (hp > maxHp)
            hp = maxHp;
        InforUi.Instance.SetHp(maxHp, hp);
    }

    public void GetDamage(int damage) {
        hp -= damage;

        InforUi.Instance.SetHp(maxHp, hp);

        if (hp > 0) { GongManager.Instance.Create(); }
        else { GameManager.Instance.gameOver(); }
        
    }

    public void Clear() {
        hp = -10;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Bullet")) {

            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);

            int damage = collision.gameObject.GetComponent<Bullet>().GetDamage();

            if (damage > 0)
                GetDamage(damage);

        }
        else if (collision.CompareTag("Item")) {
            AddHP();
            Destroy(collision.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start() {
        tr = gameObject.transform;
        rb = gameObject.GetComponent<Rigidbody2D>();

        Instance = this;

    }

    // Update is called once per frame
    void Update() {

        tr.position = new Vector3(Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, range.x, range.y), tr.position.y, tr.position.z);

    }


}
