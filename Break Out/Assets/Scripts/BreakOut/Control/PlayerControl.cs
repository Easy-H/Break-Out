using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Vector2 _min;
    [SerializeField] private Vector2 _max;
    [SerializeField] private float _up;
    
    private void Update()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        SetPos(pos.x, pos.y + _up);

    }

    public void SetPos(float xPos, float yPos)
    {
        transform.position = new Vector3(Mathf.Clamp(xPos, _min.x, _max.x), Mathf.Clamp(yPos, _min.y, _max.y), transform.position.z);

    }
    
}
