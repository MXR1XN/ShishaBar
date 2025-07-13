using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogIn : MonoBehaviour
{
    public TextMeshProUGUI output;
    public TMP_InputField login;
    public TMP_InputField password;

    public async void LoginSystem()
    {
        string url = "link to database";

        var formData = new List<KeyValuePair<string, string>>
        {
            new ("email", login.text),
            new ("pass", password.text)
        };

        var response = await HttpHelper.Post(url, formData);
        HandleResponse(response);
       
    }

    private void HandleResponse(string response)
    {
        var jsonResponse = JsonUtility.FromJson<Response>(response);
        if (jsonResponse.status == "success")
        {
            User.Initialize(int.Parse(HttpHelper.ExtractValue(response, "Id")), HttpHelper.ExtractValue(response, "userName"), login.text);
            gameObject.AddComponent<Menu>().LoadScreen(4);
        }
        else
        {
            output.text = "Log In error: " + jsonResponse.message;
        }
    }
}

