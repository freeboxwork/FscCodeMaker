using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using ApiResultData;
using System.Linq;
using UnityEngine.Networking;
using System.IO;


public class FscCodeMaker : EditorWindow
{

    TextAsset jsonData;

    API_DATA apiData;
    EditorCoroutine editorCoroutine;

    List<ProductGroup> productGroups = new List<ProductGroup>();

    Vector2 scrollView;

    List<ApiAddressData> apiAddressDatas;

    List<FscCodeData> fscCodeDatas;

    bool isLoad = false;

    [MenuItem("INVENTIS/FscCodeMaker")]
    private static void ShowWindow()
    {
        var window = GetWindow<FscCodeMaker>();
        window.titleContent = new GUIContent("FscCodeMaker");
        window.Show();
    }

    [System.Obsolete]
    private void OnGUI()
    {



        EditorCustomGUI.GUI_Title("서버에서 DATA를 다운받고 FSC 코드를 추출 합니다.");


        if (isLoad)
        {
            GUI.color = UnityEngine.Color.cyan;
            GUILayout.Box("서버에서 데이터를 로드중입니다. 잠시만 기다려주세요.");
            GUI.color = UnityEngine.Color.white;
            return;
        }

        GUI_ShowApiAddressDatas();

        EditorCustomGUI.GUI_ObjectFiled_UI<TextAsset>(160f, "JsonData", ref jsonData);

        scrollView = EditorGUILayout.BeginScrollView(scrollView);
        if (productGroups.Count > 0)
        {
            GUI_ShowProductGroups();
        }
        EditorGUILayout.EndScrollView();
    }

    void OnEnable()
    {
        apiAddressDatas = new List<ApiAddressData>();
        GetApiAddressData();
    }

    void GetApiAddressData()
    {
        apiAddressDatas = AssetDatabase.FindAssets("t:ApiAddressData").Select(x => AssetDatabase.GUIDToAssetPath(x)).Select(x => AssetDatabase.LoadAssetAtPath<ApiAddressData>(x)).ToList();
    }


    void GUI_ShowApiAddressDatas()
    {
        if (apiAddressDatas.Count <= 0) return;

        GUILayout.BeginVertical("box");
        foreach (var data in apiAddressDatas)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label($"{data.countryCode}_{data.countryName}", "HelpBox");
            if (GUILayout.Button("LOAD", GUILayout.Width(50f)))
            {
                EditorCoroutineUtility.StartCoroutine(Login(data), this);
                //jsonData = AssetDatabase.LoadAssetAtPath<TextAsset>($"Assets/{data.countryCode}_{data.countryName}.json");
            }
            if (GUILayout.Button("SEL", GUILayout.Width(50f)))
            {
                EditorGUIUtility.PingObject(data);
                Selection.activeObject = data;
            }

            GUILayout.EndHorizontal();
        }

        GUILayout.EndVertical();
    }

    void GUI_ShowProductGroups()
    {
        foreach (var group in productGroups)
        {
            GUILayout.BeginHorizontal("BOX");
            EditorGUILayout.LabelField(group.name + $" ({group.products.Count})");
            EditorCustomGUI.GUI_Button("make code", () =>
            {
                MakeFscData(group.name);
            });
            GUILayout.EndHorizontal();
        }
    }

    void MakeFscData(string groupName)
    {
        var group = GetProductByGroupName(groupName);

        fscCodeDatas = new List<FscCodeData>();
        int i = 0;
        foreach (var product in group.products)
        {

            var id = i;
            var fsc = product.fscDependencies[0].fsc;

            // code make
            var titleCode = $"4#3D={product.modelEnum}={product.modelYearEnum}={product.trimEnum}={product.variantEnum}";
            var exColorCode = $"{product.fscDependencies[0].colorCombinations[0].colors[0].colorCode}";
            var inColorCode = $"{product.fscDependencies[0].colorCombinations[0].colors[1].colorId}";
            string itemCode = "";
            int itemCount = 0;
            for (int j = 0; j < product.fscDependencies[0].specOptions.Count; j++)
            {
                if (product.fscDependencies[0].specOptions[j].specOptionItems.Count > 1)
                {
                    for (int k = 0; k < product.fscDependencies[0].specOptions[j].specOptionItems.Count; k++)
                    {
                        itemCode += $"{product.fscDependencies[0].specOptions[j].specOptionItems[k].itemCode}";
                        if (k < product.fscDependencies[0].specOptions[j].specOptionItems.Count - 1)
                        {
                            itemCode += "+";
                        }
                        itemCount++;
                    }
                }
                else
                {
                    itemCode += $"{product.fscDependencies[0].specOptions[j].specOptionItems[0].itemCode}";
                    if (j < product.fscDependencies[0].specOptions.Count - 1)
                    {
                        itemCode += "=";
                    }
                    itemCount++;
                }

            }
            var resultCode = $"{titleCode}={exColorCode}={inColorCode}={itemCount}={itemCode}";

            FscCodeData fscCodeData = new FscCodeData();
            fscCodeData.id = id;
            fscCodeData.name = product.modelDisplayName + "_" + fsc;
            fscCodeData.code = resultCode;
            fscCodeDatas.Add(fscCodeData);

            i++;
        }

        foreach (var data in fscCodeDatas)
        {
            Debug.Log(data.name);
            Debug.Log(data.code);
        }

        EditorCoroutineUtility.StartCoroutine(AddFscData(), this);

    }

    ProductGroup GetProductByGroupName(string groupName)
    {
        return productGroups.Find(x => x.name == groupName);
    }

    void Make()
    {
        EditorCoroutineUtility.StartCoroutine(MakeFscCode(), this);
    }

    IEnumerator MakeFscCode()
    {

        jsonData = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/jsonData.json");

        while (jsonData == null)
        {
            yield return new WaitForSeconds(0.1f);
            jsonData = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/jsonData.json");
        }

        yield return new WaitForEndOfFrame();

        apiData = JsonUtility.FromJson<API_DATA>(jsonData.text);
        yield return new WaitForEndOfFrame();

        // Debug.Log(apiData.result.products[0].modelDisplayName);

        yield return EditorCoroutineUtility.StartCoroutine(SetGroup(), this);

    }

    IEnumerator SetGroup()
    {
        productGroups.Clear();

        yield return new WaitForEndOfFrame();

        foreach (var product in apiData.result.products)
        {

            var name = $"[{product.modelEnum}] {product.modelDisplayName} {product.modelYearEnum}";
            if (productGroups.Any(x => x.name == name))
            {
                var group = productGroups.Find(x => x.name == name);
                group.products.Add(product);
            }
            else
            {
                var group = new ProductGroup();
                group.name = name;
                group.products.Add(product);
                productGroups.Add(group);
            }

        }

        yield return new WaitForEndOfFrame();

        isLoad = false;
    }

    IEnumerator AddFscData()
    {
        yield return new WaitForEndOfFrame();

        var window = GetWindow<PrefabMaker>();
        if (window == null)
        {
            window.Show();
        }

        yield return new WaitForEndOfFrame();
        window.SetCodeDatas(fscCodeDatas);
        window.Focus();



        yield return null;

    }

    // LOAD API DATA
    IEnumerator Login(ApiAddressData apiData)
    {
        isLoad = true;

        WWWForm form = new WWWForm();
        form.AddField("tenancyName", apiData.tenancyName);
        form.AddField("usernameOrEmailAddress", apiData.usernameOrEmailAddress);
        form.AddField("password", apiData.password);

        using (UnityWebRequest www = UnityWebRequest.Post(apiData.endpoint + apiData.loginApiEndpoint, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else

            {
                LoginResponse response = JsonUtility.FromJson<LoginResponse>(www.downloadHandler.text);
                string authToken = response.result;
                Debug.Log(response.result);
                EditorCoroutineUtility.StartCoroutine(PostRequest(apiData, authToken), this);
            }
        }
    }

    IEnumerator PostRequest(ApiAddressData apiData, string authToken)
    {
        UnityWebRequest www = new UnityWebRequest(apiData.endpoint + apiData.productApiEndpoint, "POST");
        www.SetRequestHeader("Authorization", "Bearer " + authToken);


        string jsonBody = "{\"key\":\"value\"}";
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");


        yield return www.SendWebRequest();


        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Debug.Log("Response: " + www.downloadHandler.text);
        }

        // www.downloadHandler.text 를 assets 폴더에 저장
        string path = Application.dataPath + "/jsonData.json";
        File.WriteAllText(path, www.downloadHandler.text);

        yield return new WaitForEndOfFrame();
        //yield return new WaitForSeconds(1f);

        EditorCoroutineUtility.StartCoroutine(MakeFscCode(), this);
    }





}


[System.Serializable]
public class ProductGroup
{
    int id;
    public string name;
    public List<Product> products = new List<Product>();
}