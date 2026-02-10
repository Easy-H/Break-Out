using System.Collections.Generic;
using EasyH;

public class HPBarManager : Singleton<HPBarManager>
{
    private IDictionary<int, HPBarController> dic;

    protected override void OnCreate()
    {
        dic = new Dictionary<int, HPBarController>();
    }

    public void AddData(int id, HPBarController bar)
    {
        dic.Add(id, bar);
        UnityEngine.Debug.Log("Add: " + id);
    }

    public void UpdateData(int id, int curHP, int maxHP)
    {
        UnityEngine.Debug.Log("Update: " + id);
        if (!dic.ContainsKey(id)) return;
        dic[id].UpdateUI(curHP, maxHP);
    }

    public void RemoveData(int id)
    {
        dic.Remove(id);
    }    
}