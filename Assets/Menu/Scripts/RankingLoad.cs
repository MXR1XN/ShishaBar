using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
public class RankingLoad : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
        try
        {
            GameObject ranking = GameObject.Find("Field");
            if (ranking == null)
            {
                Debug.LogError("Didn't find 'Field'");
                return;
            }

            ResponseData responseData = JsonConvert.DeserializeObject<ResponseData>(await HttpHelper.Get("http://markii2.atwebpages.com/db/getScores.php", new List<KeyValuePair<string, string>> { new("test", "test") }));


            if (responseData != null && responseData.status == "success")
            {
                for (int i = 0; i < responseData.data.Count; i++)
                {
                    GameObject clonedRanking = Instantiate(ranking, ranking.transform.parent);
                    clonedRanking.name = ranking.name + "_" + (i + 1);

                    clonedRanking.transform.Find("Text Rank").GetComponent<TextMeshProUGUI>().text = responseData.data[i].rank.ToString();
                    clonedRanking.transform.Find("Text Date").GetComponent<TextMeshProUGUI>().text = responseData.data[i].createdAt.ToString();
                    clonedRanking.transform.Find("Text Score").GetComponent<TextMeshProUGUI>().text = responseData.data[i].scour.ToString();
                    clonedRanking.transform.Find("Text User").GetComponent<TextMeshProUGUI>().text = responseData.data[i].userName.ToString();
                }
            }
            else
            {
                Debug.LogError("Error JSON");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error: {ex.Message}\n{ex.StackTrace}");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}