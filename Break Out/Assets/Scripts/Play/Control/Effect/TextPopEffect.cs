using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPopEffect : Effect
{
    [SerializeField] TextMeshProUGUI _targetText;

    [SerializeField] float _minSize;
    [SerializeField] float _maxSize;

    public override void On(Vector3 pos) {
        GetComponent<RectTransform>().position = pos;
        _targetText.alpha = 0;
        StartCoroutine(_On());
    }

    IEnumerator _On() {
        float spendTime = 0;
        while (_effectTime > spendTime)
        {
            spendTime += Time.deltaTime;
            yield return null;

            _targetText.fontSize = Mathf.Lerp(_minSize, _maxSize,  spendTime / _effectTime);
            _targetText.alpha = 1 - spendTime / _effectTime;

        }
        Destroy(gameObject);
    }

}
