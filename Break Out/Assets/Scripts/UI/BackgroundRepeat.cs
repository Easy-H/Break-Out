using UnityEngine;
using System.Collections;

public class BackgroundRepeat : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 1.2f;
    private Material thisMaterial;
    void Start()
    {
        thisMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (GameManager.coll != 0)
            return;
        Vector2 newOffset = thisMaterial.mainTextureOffset;
        newOffset.Set(0, newOffset.y + (scrollSpeed * Time.deltaTime * PhaseManager.level));
        thisMaterial.mainTextureOffset = newOffset;
    }
}