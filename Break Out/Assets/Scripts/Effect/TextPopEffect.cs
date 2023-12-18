using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPopEffect : Effect {

    [SerializeField] TextMeshProUGUI _view;
    [SerializeField] TextMeshProUGUI[] _targetText;

    [SerializeField] string _text;

    [SerializeField] float _maxSize;
    [SerializeField] float _maintainTime;

    public override void On(Vector3 pos)
    {
        GetComponent<RectTransform>().position = pos;
        for (int i = 0; i < _targetText.Length; i++)
        {
            _targetText[i].alpha = 0;
        }
        StartCoroutine(_On());
    }

    public void SetText(string text)
    {
        _text = text;
        char[] chars = text.ToCharArray();
        float size = _maxSize * 0.8f;

        _targetText = new TextMeshProUGUI[chars.Length];

        for (int i = 0; i < chars.Length; i++)
        {
            _targetText[i] = Instantiate(_view);
            _targetText[i].text = chars[i].ToString();

            _targetText[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(size + (i - chars.Length / 2), 0);

        }
    }

    IEnumerator _On()
    {
        for (int i = 0; i < _targetText.Length; i++)
        {
            _targetText[i].fontSize = 0;
            _targetText[i].alpha = 1;
            float spendTime = 0;
            while (_effectTime > spendTime)
            {
                spendTime += Time.deltaTime;
                yield return null;

                _targetText[i].fontSize = Mathf.Lerp(0, _maxSize, spendTime / _effectTime);

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
