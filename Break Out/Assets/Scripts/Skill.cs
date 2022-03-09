using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{

    protected static float skillPoint;
    static float needSkillPoint;

    [SerializeField] float setSkillPoint = 8;

    /*
    public static void AddSkillPoint() {
        if (skillPoint < needSkillPoint) {
            skillPoint++;
            UIManager.getUIManager().GaugeImage(0, needSkillPoint, skillPoint);
        }

    }
    */
    public void UseSkill() {
        if ((skillPoint < needSkillPoint))
            return;

        Player.instance.AddHP();
        skillPoint = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        skillPoint = 0;
        needSkillPoint = setSkillPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (skillPoint < needSkillPoint) {
            skillPoint += Time.deltaTime;
        }
        UIManager.instance.GaugeImage(0, needSkillPoint, skillPoint);

        if (Input.GetKeyDown(KeyCode.Space))
            UseSkill();
            
    }
}
