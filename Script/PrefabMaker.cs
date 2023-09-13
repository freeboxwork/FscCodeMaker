
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;
using Unity.EditorCoroutines.Editor;
using System;

public class PrefabMaker : EditorWindow
{

    FscCodeDatas codeDatas;
    TextAsset textCodeData;
    DefaultAsset folderAsset;
    float labelWidth = 160f;
    string prefabName = "none";
    Vector2 scrollView;
    GameObject carObejct;

    bool isMakePrefab = false;


    public List<string> ItemCodeList = new List<string>();



    [MenuItem("INVENTIS/PrefabMaker")]
    private static void ShowWindow()
    {
        var window = GetWindow<PrefabMaker>();
        window.titleContent = new GUIContent("PrefabMaker");
        window.Show();
    }

    [System.Obsolete]
    private void OnGUI()
    {

        if (isMakePrefab)
        {
            GUILayout.Box("Prefab 생성중입니다. 잠시만 기다려주세요.");
            return;
        }

        EditorCustomGUI.GUI_Title("FSC Code 를 기준으로 차량을 조합하고 Prefab을 만듭니다.");
        EditorCustomGUI.GUI_ObjectFiled_UI(labelWidth, "CodeData", ref textCodeData);
        if (textCodeData != null)
        {
            EditorCustomGUI.GUI_Button("Get Code Data List", () => { codeDatas = GetCodeDatasByJson(); });
        }
        EditorCustomGUI.GUI_LineSpace(10f);
        if (codeDatas != null)
        {
            scrollView = GUILayout.BeginScrollView(scrollView);

            foreach (var data in codeDatas.data)
            {

                GUILayout.BeginHorizontal("BOX");
                // id
                EditorCustomGUI.GUI_Label(20f, "ID", data.id.ToString());
                // name
                EditorCustomGUI.GUI_Label(40f, "NAME", data.name);
                // fsc code view button
                EditorCustomGUI.GUI_Button_OutlineNone("View Fsc Code", () =>
                {
                    // display dialog fsc code
                    EditorUtility.DisplayDialog("Fsc Code", data.code, "OK");
                });

                // view button ( 차량 조합)
                EditorCustomGUI.GUI_Button_OutlineNone("View FSC", () =>
                {
                    prefabName = data.name;
                    CustomEvent.evnFscChange?.Invoke(data.code);
                });
                GUILayout.EndHorizontal();
            }


            EditorCustomGUI.GUI_LineSpace(10f);
            // Get Car root object
            if (carObejct == null)
            {
                // carObejct 찾아오는 버튼
                EditorCustomGUI.GUI_Button("Find Car Root Object", () =>
                {
                    FindCarRootObject();
                });
            }
            else
            {
                EditorCustomGUI.GUI_ObjectFiled_UI(labelWidth, "Car Root Object", ref carObejct);
            }


            // get defualt folder
            EditorCustomGUI.GUI_ObjectFiled_UI(labelWidth, "Prefab Save Folder", ref folderAsset);
            // make prefab button
            EditorCustomGUI.GUI_Button("Make Prefab", () =>
            {
                MakePrefabs();
            });

            GUILayout.EndScrollView();
        }



    }

    public void FindCarRootObject()
    {
        carObejct = GameObject.Find("CarParent");
        if (carObejct == null)
        {
            EditorUtility.DisplayDialog("Error", "CarParent Object를 찾을 수 없습니다.", "OK");
        }
    }

    FscCodeDatas GetCodeDatasByJson()
    {
        FscCodeDatas datas = JsonUtility.FromJson<FscCodeDatas>(textCodeData.text);
        return datas;
    }

    public void ViewFSC(string fscCode)
    {
        string[] strCarData = fscCode.Split(new char[] { '=' });

        string extRenderColor = strCarData[5];// 외장 컬러
        int itemCodeCount = Int32.Parse(strCarData[7]);
        ItemCodeList.Clear();
        for (int i = 0; i < itemCodeCount; i++)
        {
            ItemCodeList.Add(strCarData[8 + i]);
        }
        ModelVerification.instance.ChangeForRender(ItemCodeList, extRenderColor);
        Debug.Log(fscCode);
    }

    void OnEnable()
    {
        CustomEvent.Add_FSC_VIEW_Event(ViewFSC);

    }

    public void SetCodeDatas(List<FscCodeData> datas)
    {
        codeDatas = new FscCodeDatas();
        codeDatas.data = datas.ToList();

        EditorCoroutineUtility.StartCoroutine(MakePrefabCor(), this);

    }


    IEnumerator MakePrefabCor()
    {
        FindCarRootObject();
        yield return new WaitForEndOfFrame();

        yield return EditorCoroutineUtility.StartCoroutine(CreatFolder(), this);

        this.Focus();
        yield return new WaitForEndOfFrame();
        MakePrefabs();

    }

    IEnumerator CreatFolder()
    {
        string path = $"{Application.dataPath}/360Prefab";
        if (!System.IO.Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }
        folderAsset = AssetDatabase.LoadAssetAtPath<DefaultAsset>($"Assets/360Prefab");

        yield return new WaitForEndOfFrame();

        while (folderAsset == null)
        {
            yield return new WaitForSeconds(0.1f);
            folderAsset = AssetDatabase.LoadAssetAtPath<DefaultAsset>($"Assets/360Prefab");
        }
        //MakePrefabs();
    }

    // void MakePrefab()
    // {
    //     // prefabPath  가 없는 경우 알림
    //     if (folderAsset == null)
    //     {
    //         EditorUtility.DisplayDialog("Error", "Prefab Save Folder를 선택해주세요.", "OK");
    //         return;
    //     }
    //     // prefab save path 
    //     string prefabPath = $"{AssetDatabase.GetAssetPath(folderAsset)}/{prefabName}.prefab";
    //     try
    //     {
    //         // make prefab
    //         PrefabUtility.SaveAsPrefabAsset(carObejct, prefabPath);
    //     }
    //     catch
    //     {
    //         Debug.LogError("프리팹 생성 경로 오류");
    //     }
    // }

    async void MakePrefabs()
    {

        isMakePrefab = true;
        foreach (var data in codeDatas.data)
        {
            // change fsc
            CustomEvent.evnFscChange?.Invoke(data.code);
            // wait for change fsc
            await Task.Delay(1000);
            await Task.Yield();

            // prefabPath  가 없는 경우 알림
            if (folderAsset == null)
            {
                EditorUtility.DisplayDialog("Error", "Prefab Save Folder를 선택해주세요.", "OK");
                return;
            }

            // prefab save path
            string prefabPath = $"{AssetDatabase.GetAssetPath(folderAsset)}/{data.name}.prefab";
            try
            {
                // make prefab
                PrefabUtility.SaveAsPrefabAsset(carObejct, prefabPath);
            }
            catch
            {
                Debug.LogError("프리팹 생성 경로 오류");
            }

        }
        isMakePrefab = false;
    }

}



[System.Serializable]
public class FscCodeDatas
{
    public List<FscCodeData> data = new List<FscCodeData>();
}

[System.Serializable]
public class FscCodeData
{
    public int id;
    public string name;
    public string code;
}


