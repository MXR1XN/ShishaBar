using TMPro;
using UnityEngine;

public class LoadStart : MonoBehaviour
{
    public TextMeshProUGUI greeting;
    void Start()
    {
        greeting.text = $"Hello {User.UserName}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
