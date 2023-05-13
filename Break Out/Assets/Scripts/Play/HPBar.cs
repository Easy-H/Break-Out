using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] Image _gauge;

    public void SetHP(int amount, int max) {
        _gauge.fillAmount = (float)amount / max;
    }

}
