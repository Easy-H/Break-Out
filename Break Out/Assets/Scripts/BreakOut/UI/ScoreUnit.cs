using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUnit : MonoBehaviour
{
    [SerializeField] Text _rank;
    [SerializeField] Text _name;
    [SerializeField] Text _score;

    public string GetInvestigation(int value) {
        if (value == 1) return "1st";
        if (value == 2) return "2nd";
        if (value == 3) return "3rd";
        return string.Format("{0}th", value);
    }

    // Start is called before the first frame update
    public void SetValue(int idx, string name, string score) {
        _rank.text = GetInvestigation(idx);
        _name.text = name;
        _score.text = score;
    }
}
