using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
public class DLCLoad : MonoBehaviour
{
    async void Start()
    {
        try
        {
            GameObject dlc = GameObject.Find("Field");
            if (dlc == null)
            {
                Debug.LogError("Could not find 'Field'");
                return;
            }

            ResponseDataDLC responseData = JsonConvert.DeserializeObject<ResponseDataDLC>(await HttpHelper.Get("http://markii2.atwebpages.com/db/getDLC.php", new List<KeyValuePair<string, string>> { new("test", "test") }));


            if (responseData != null && responseData.status == "success")
            {
                for (int i = 0; i < responseData.data.Count; i++)
                {
                    GameObject clonedRanking = Instantiate(dlc, dlc.transform.parent);
                    clonedRanking.name = dlc.name + "_" + (i + 1);

                    clonedRanking.transform.Find("Text Name").GetComponent<TextMeshProUGUI>().text = responseData.data[i].Name.ToString();
                    clonedRanking.transform.Find("Text Price").GetComponent<TextMeshProUGUI>().text = responseData.data[i].Price.ToString();
                    clonedRanking.transform.Find("Button").gameObject.SetActive(true);
                }
            }
            else
            {
                Debug.LogError("Error from the server or JSON");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error: {ex.Message}\n{ex.StackTrace}");
        }
    }

    void Update()
    {

    }
}