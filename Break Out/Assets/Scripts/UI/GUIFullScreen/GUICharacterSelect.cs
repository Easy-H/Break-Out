using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using static PlayerManager;

public class GUICharacterSelect : GUICustomFullScreen
{
    List<CharacterInfor> _infor;
    int _idx = 0;

    [SerializeField] Text _characterName;
    [SerializeField] Text _characterCondition;
    [SerializeField] Image _characterImg;
    [SerializeField] SpriteAtlas _atlas;

    public override void Open()
    {
        base.Open();
        _infor = PlayerManager.Instance.GetCharacterInfor();
        _idx = PlayerManager.Instance.NowKey;
    }

    public void ChangeIdx(int amount) {
        _idx += amount;
        _idx = ((_infor.Count + _idx) % _infor.Count);

        _ShowInfor(_idx);

    }

    public void Select() { 
        PlayerManager.Instance.NowKey = _infor[_idx].Id;
        
    }

    private void _ShowInfor(int idx)
    {
        CharacterInfor infor = _infor[idx];
        IQuestChecker check = QuestData.GetQuestChecker(infor.Condition);

        _characterName.text = infor.Name;
        _characterCondition.text = check.ToString();
        _characterImg.sprite = _atlas.GetSprite(infor.Sprite);
    }
}
