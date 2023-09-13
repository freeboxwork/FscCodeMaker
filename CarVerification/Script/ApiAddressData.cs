using UnityEngine;
using UnityEditor;

public class ApiAddressData : ScriptableObject
{
    public string countryCode;
    public string countryName;
    public string endpoint;
    public string loginApiEndpoint;
    public string productApiEndpoint;

    public string tenancyName;
    public string usernameOrEmailAddress;
    public string password;

}


public class ApiAddressDataEditor : EditorWindow
{
    private ApiAddressData apiAddressData;

    [MenuItem("INVENTIS/Api Address Data")]
    public static void ShowWindow()
    {
        GetWindow<ApiAddressDataEditor>("Api Address Data");
    }

    private void OnEnable()
    {
        apiAddressData = AssetDatabase.LoadAssetAtPath<ApiAddressData>("Assets/ApiAddressData.asset");
    }

    private void OnGUI()
    {
        if (apiAddressData == null)
        {
            apiAddressData = CreateInstance<ApiAddressData>();
            AssetDatabase.CreateAsset(apiAddressData, "Assets/ApiAddressData.asset");
            AssetDatabase.SaveAssets();
        }

        EditorGUILayout.LabelField("Endpoint", EditorStyles.boldLabel);
        apiAddressData.endpoint = EditorGUILayout.TextField("Endpoint", apiAddressData.endpoint);
        apiAddressData.loginApiEndpoint = EditorGUILayout.TextField("Login API Endpoint", apiAddressData.loginApiEndpoint);
        apiAddressData.productApiEndpoint = EditorGUILayout.TextField("Product API Endpoint", apiAddressData.productApiEndpoint);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Authentication", EditorStyles.boldLabel);
        apiAddressData.tenancyName = EditorGUILayout.TextField("Tenancy Name", apiAddressData.tenancyName);
        apiAddressData.usernameOrEmailAddress = EditorGUILayout.TextField("Username or Email Address", apiAddressData.usernameOrEmailAddress);
        apiAddressData.password = EditorGUILayout.PasswordField("Password", apiAddressData.password);

        if (GUILayout.Button("Save"))
        {
            EditorUtility.SetDirty(apiAddressData);
            AssetDatabase.SaveAssets();
        }
    }
}



