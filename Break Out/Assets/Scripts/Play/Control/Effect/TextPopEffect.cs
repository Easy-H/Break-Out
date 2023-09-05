using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPopEffect : Effect
{
    [SerializeField] TextMeshProUGUI[] _targetText;

    [SerializeField] float _minSize;
    [SerializeField] float _maxSize;
    [SerializeField] float _maintainTime;

    public override void On(Vector3 pos) {
        GetComponent<RectTransform>().position = pos;
        for (int i = 0; i < _targetText.Length; i++)
        {
            _targetText[i].alpha = 0;
        }
        StartCoroutine(_On());
    }

    IEnumerator _On() {
        for (int i = 0; i < _targetText.Length; i++)
        {
            _targetText[i].fontSize = _minSize;
            _targetText[i].alpha = 1;
            float spendTime = 0;
            while (_effectTime > spendTime)
            {
                spendTime += Time.deltaTime;
                yield return null;

                _targetText[i].fontSize = Mathf.Lerp(_minSize, _maxSize, spendTime / _effectTime);

            }

        }
        yield return new WaitForSeconds(_maintainTime);

        for (int i = 0; i < _targetText.Length; i++)
        {
            float spendTime = 0;
            while (_effectTime > spendTime)
            {
                spendTime += Time.deltaTime;
                yield return null;

                _targetText[i].alpha = 1 - spendTime / _effectTime;

            }

        }

        EndEffect();
    }

}
