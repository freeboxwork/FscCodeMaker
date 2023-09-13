using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{

    public TextAsset textAsset;

    Datas datas;
    List<string> names;

    void Start()
    {
        datas = JsonUtility.FromJson<Datas>(textAsset.text);
        names = new List<string>();

        foreach (var data in datas.data)
        {
            var name = data.name;
            // 중복 없이 이름만 저장
            if (!names.Contains(name))
            {
                names.Add(name);
            }
        }

        // 이름 출력
        string namesdata = "";
        foreach (var name in names)
        {
            namesdata += name + "\n";

        }
        Debug.Log(namesdata);
    }



    // Update is called once per frame
    void Update()
    {

    }
}


[System.Serializable]
public class Datas
{
    public List<DataName> data;
}
[System.Serializable]
public class DataName
{
    public string name;
}