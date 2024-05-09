using UnityEngine;

[System.Serializable]
public class Status {

    [SerializeField] private int _maxHP;
    [SerializeField] private int _nowHP;

    public Status()
    {
        _maxHP = 5;
        _nowHP = 3;
    }

    public Status(int maxHP, int nowHP)
    {
        _maxHP = maxHP;
        _nowHP = nowHP;
    }

    public void GetDamage(int amount)
    {
        _nowHP -= amount;
    }

    public int GetMaxValue() {
        return _maxHP;
    }

    public void SetHPMax() { 
        _nowHP = _maxHP;
    }

    public void AddHP(int amount)
    {
        _nowHP += amount;
        if (_nowHP > _maxHP) _nowHP = _maxHP;
    }

    public float GetHPRatio() {
        return (float)_nowHP / _maxHP;
    }

    public bool isAlive() { return (_nowHP > 0); }

}
