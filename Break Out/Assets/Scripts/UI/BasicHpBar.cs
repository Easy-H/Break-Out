using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicHpBar : GaugeUI {

    [SerializeField] Color GuageColor;
    [SerializeField] Image _gauge;

    [SerializeField] Image _frame;
    [SerializeField] Image _slice;

    [SerializeField] float _frameSize;

    public override void InstantiateSet(int max) {

        _frame.color = GuageColor;
        _slice.color = GuageColor;

        if (max == 1) return;

        RectTransform[] slices = new RectTransform[max - 1];
        slices[0] = _slice.GetComponent<RectTransform>();

        for (int i = 1; i < slices.Length; i++)
        {
            slices[i] = Instantiate(_slice).GetComponent<RectTransform>();
            slices[i].SetParent(_slice.transform.parent);
        }

        for (int i = 0; i < slices.Length; i++)
        {
            slices[i].anchorMin = new Vector2((1f / max) * (i + 1), 0);
            slices[i].anchorMax = new Vector2((1f / max) * (i + 1), 1);
            slices[i].anchoredPosition = Vector2.zero;
            slices[i].sizeDelta = new Vector2(_frameSize, 0);
            slices[i].gameObject.SetActive(true);
        }

    }

    public override void SetGauge(float amount) {
        _gauge.fillAmount = amount;

    }

}
