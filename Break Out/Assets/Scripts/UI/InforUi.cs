using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InforUi : MonoBehaviour {

    public static InforUi Instance { get; private set; }

    [SerializeField] Text _score = null;

    [SerializeField] Image _hp = null;

    private void Awake() {
        Instance = this;
    }

    public void SetScore(string content) {
        _score.text = content;
    }

    public void SetHp(float max, float now) {
        _hp.fillAmount = now / max;
    }

    public void ChangeTimeScale(float changeTimeScale) {
        Time.timeScale = changeTimeScale;
    }
    

}
