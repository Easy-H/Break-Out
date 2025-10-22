using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameSet : MonoBehaviour
{
    [SerializeField] InputField _name;

    public void OpenKeyboard() {
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }
    public void SetName() {
        PlayerManager.Instance.PlayerName = _name.text;
    }

}
