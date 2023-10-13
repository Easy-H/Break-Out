using UnityEngine;

[System.Serializable]
public class Status {

    public int _maxHP;
    public int _nowHP;

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

    public void AddHP(int amount)
    {
        _nowHP += amount;
        if (_nowHP > _maxHP) _nowHP = _maxHP;
    }

    public bool isAlive() { return (_nowHP > 0); }

}
