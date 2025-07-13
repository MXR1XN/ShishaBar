using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text.RegularExpressions;

public class SignIn : MonoBehaviour
{
    public TextMeshProUGUI outputUserName;
    public TextMeshProUGUI outputEmail;
    public TextMeshProUGUI outputpassword;
    public TextMeshProUGUI outputpasswordConfirm;
    public TMP_InputField email;
    public TMP_InputField userName;
    public TMP_InputField password;
    public TMP_InputField passwordConfirm;
    public async void checkExist(int index)
    {
        string url, displayError, requirements;
        List<KeyValuePair<string, string>> formData;
        TextMeshProUGUI output;
        if (index == 0)
        {
            url = "link_toDB";
            formData = new List<KeyValuePair<string, string>>
            {
                new ("email", email.text)
            };
            output = outputEmail;
            displayError = "email";
            requirements = !Regex.IsMatch(email.text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$") ? "Error: wrong email format" : "";
        }
        else
        {
            url = "link_toDB";
            formData = new List<KeyValuePair<string, string>>
            {
                new ("userName", userName.text)
            };
            output = outputUserName;
            displayError = "user name";
            requirements = userName.text.Length < 3 ? "Error: minimum user name lenght is 4" : "";
        }
        output.text = requirements != "" ? requirements : JsonUtility.FromJson<Response>(await HttpHelper.Get(url, formData)).status == "successE" ? $"Error: User with this {displayError} allready exist" : "";
    }

    public void checkNext()
    {
        if(userName.text.Length < 3)
        {
            outputUserName.text = "Error: minimum user name lenght is 4";
        }
        if (!Regex.IsMatch(email.text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
        {
            outputEmail.text = "Error: wrong email format";
        }

        if (outputEmail.text == "" && outputUserName.text == "")
        {
            gameObject.AddComponent<Menu>().LoadScreen(3);
            User.Email = email.text;
            User.UserName = userName.text;
        }
    }

    public void checkPassword(int index)
    {
        if (index == 0)
            outputpassword.text = password.text.Length < 8 ? "Error: minimum password lenght is 8" : "";
        else
            outputpasswordConfirm.text = password.text != passwordConfirm.text ? "Error: passwords are not the same" : "";
    }

    public async void createAcc()
    {
        if (outputpassword?.text == "" && outputpasswordConfirm?.text == "")
        {
            var formData = new List<KeyValuePair<string, string>>
        {
            new("email", User.Email),
            new("userName", User.UserName),
            new("pass", password.text)
        };

            var response = await HttpHelper.Post("link_toDB", formData);
            var jsonResponse = JsonUtility.FromJson<Response>(response);

            if (jsonResponse.status == "success")
            {
                User.Id = int.Parse(HttpHelper.ExtractValue(response, "Id"));
                gameObject.AddComponent<Menu>().LoadScreen(4);
            }
        }
    }


}
