using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour {

    [SerializeField] Character _target;

    [SerializeField] Color GuageColor;
    [SerializeField] Image _gauge;

    [SerializeField] Image _frame;
    [SerializeField] Image _slice;

    [SerializeField] float _frameSize;

    Status _targetStat;

    private void OnEnable()
    {
        if (_target == null) {
            Destroy(gameObject);
            return;
        }

        _targetStat = _target._stat;
        InstantiateSet();
    }

    public void InstantiateSet() {
        _gauge.fillAmount = (float)_targetStat._nowHP / _targetStat._maxHP;

        _frame.color = GuageColor;
        _slice.color = GuageColor;

        if (_targetStat._maxHP == 1) return;

        RectTransform[] slices = new RectTransform[_targetStat._maxHP - 1];
        slices[0] = _slice.GetComponent<RectTransform>();

        for (int i = 1; i < slices.Length; i++)
        {
            slices[i] = Instantiate(_slice).GetComponent<RectTransform>();
            slices[i].SetParent(_slice.transform.parent);
        }

        for (int i = 0; i < slices.Length; i++)
        {
            slices[i].anchorMin = new Vector2((1f / _targetStat._maxHP) * (i + 1), 0);
            slices[i].anchorMax = new Vector2((1f / _targetStat._maxHP) * (i + 1), 1);
            slices[i].anchoredPosition = Vector2.zero;
            slices[i].sizeDelta = new Vector2(_frameSize, 0);
            slices[i].gameObject.SetActive(true);
        }

    }

    public void SetHP() {
        _gauge.fillAmount = (float)_targetStat._nowHP / _targetStat._maxHP;

    }

}
