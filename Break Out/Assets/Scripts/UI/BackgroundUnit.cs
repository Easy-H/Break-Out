using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundUnit : MonoBehaviour
{

    [SerializeField] float _minSize; 
    [SerializeField] float _maxSize;

    [SerializeField] Color[] _colors;
    [SerializeField] SpriteRenderer _targetRenderer;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.one * Random.Range(_minSize, _maxSize);
        transform.position += Vector3.right * Random.Range(-1.4f, 1.4f);
        _targetRenderer.color = _colors[Random.Range(0, _colors.Length)];
    }

}
