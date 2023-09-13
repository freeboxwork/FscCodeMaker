using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
//using Unity.EditorCoroutines.Editor;

public class LoadAPI : MonoBehaviour
{

    // api
    private string endpoint = "https://ssc-apidev-hmca.hyundaisvc.com";
    private string loginApiEndpoint = "/api/Account";
    private string productApiEndpoint = "/api/services/app/product/GetAllProductsIncludingDepedencies?Abp.Localization.CultureName=en-US";

    private string tenancyName = "default";
    private string usernameOrEmailAddress = "admin";
    private string password = "123qwe";

    private string authToken;

    void Start()
    {
        StartCoroutine(Login());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("tenancyName", tenancyName);
        form.AddField("usernameOrEmailAddress", usernameOrEmailAddress);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post(endpoint + loginApiEndpoint, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else

            {
                //Debug.Log(www.downloadHandler.text);
                LoginResponse response = JsonUtility.FromJson<LoginResponse>(www.downloadHandler.text);
                authToken = response.result;
                Debug.Log(response.result);
                // here parse the authToken from the response
                //authToken = ""; // replace with actual parsing
                StartCoroutine(PostRequest());
            }
        }
    }


    IEnumerator PostRequest()
    {
        UnityWebRequest www = new UnityWebRequest(endpoint + productApiEndpoint, "POST");
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
        string path = Application.dataPath + "/test.txt";
        File.WriteAllText(path, www.downloadHandler.text);


    }


}

[System.Serializable]
public class LoginResponse
{
    public string result;
    public string targetUrl;
    public bool success;
    public string error;
    public bool unAuthorizedRequest;
    public bool __abp;
}
