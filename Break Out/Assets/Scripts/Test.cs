using EHTool.DBKit;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update

    string message = "{\"0\":{\"score\":500,\"userId\":\"Pong\"},\"1\":{\"score\":600,\"userId\":\"Pong\"},\"2\":{\"score\":300,\"userId\":\"Pong\"},\"3\":{\"score\":\"4000\",\"userId\":\"Pong\"},\"4\":{\"score\":\"6800\",\"userId\":\"Pong\"},\"5\":{\"score\":\"2400\",\"userId\":\"Pong\"},\"6\":{\"score\":\"1000\",\"userId\":\"Pong\"},\"7\":{\"score\":\"2500\",\"userId\":\"Pong\"},\"8\":{\"score\":\"2300\",\"userId\":\"Pong\"},\"9\":{\"score\":\"1300\",\"userId\":\"Pong\"},\"10\":{\"score\":4400,\"userId\":\"Pong\"},\"11\":{\"score\":\"200\",\"userId\":\"Pong\"},\"12\":{\"score\":\"500\",\"userId\":\"Pong\"},\"13\":{\"score\":\"2700\",\"userId\":\"Pong\"},\"14\":{\"score\":\"2100\",\"userId\":\"Pong\"},\"15\":{\"score\":1900,\"userId\":\"Pong\"},\"16\":{\"score\":800,\"userId\":\"Pong\"},\"17\":{\"score\":2500,\"userId\":\"Pong\"},\"18\":{\"score\":\"200\",\"userId\":\"Pong\"},\"19\":{\"score\":\"700\",\"userId\":\"Pong\"}}";
    void Start()
    {
        GetRecordAtCallback(message);
    }
    public void GetRecordAtCallback(string value)
    {
        Debug.Log(value);
        Dictionary<object, Score> source = JsonUtility.FromJson<Dictionary<object, Score>>(value);
        IList<Score> temp = source.Values.ToList();

        Debug.Log(source.Count);

        foreach (object v in source.Values) {
            Debug.Log(v);
        }


        Debug.Log(temp.Count);

        for (int i = 0; i < temp.Count; i++)
        {
            Debug.Log(temp[i].score);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
