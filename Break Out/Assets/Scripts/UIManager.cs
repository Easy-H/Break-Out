using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [SerializeField] Text[] texts = null;

    [SerializeField] Image[] gaugeImages = null;
    [SerializeField] GameObject[] windowImages = null;

    private void Awake()
    {
        instance = this;
    }

    public void ChangeText(int idxText, string content)
    {
        if (texts[idxText])
        {
            texts[idxText].text = content;
        }
    }

    public void GaugeImage(int idxGauge, float max, float now)
    {
        if (gaugeImages[idxGauge])
        {
            gaugeImages[idxGauge].fillAmount = now / max;
        }
    }

    public void ChangeTimeScale(float changeTimeScale)
    {
        Time.timeScale = changeTimeScale;
    }
    
    public void CloseWindow(int idxImage)
    {

        if (windowImages[idxImage])
        {
            windowImages[idxImage].gameObject.SetActive(false);
        }

    }

    public void GotoScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void OpenWindow(int idxImage) {
        if (windowImages[idxImage]) {
            windowImages[idxImage].gameObject.SetActive(true);
        }

    }

}
