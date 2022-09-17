using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class Player : MonoBehaviour {
    public static Player instance = null;

    Transform tr;
    Rigidbody2D rb;

    [SerializeField] HP hp = null;
    [SerializeField] Vector2 range = Vector2.one;

    float horizontalFactor;

    public void AddHP() {
        hp.AddHP();
    }

    public void GetDamage(int damage) {
        if (hp.GetDamaged(damage)) {
            GongManager.Instance.Create();

        }
        else {
            GameManager.Instance.gameOver();
        }
    }

    public void Clear() {
        hp.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Bullet")) {
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
            int damage = collision.gameObject.GetComponent<Bullet>().GetDamage();
            if (damage > 0)
                GetDamage(damage);

        }
    }

    // Start is called before the first frame update
    void Start() {
        tr = gameObject.transform;
        rb = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update() {

        tr.position = new Vector3(Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, range.x, range.y), tr.position.y, tr.position.z);

    }


}
