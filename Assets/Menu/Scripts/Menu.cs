using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void LoadScreen(int index)
    {
        SceneManager.LoadScene(index);
    }
}
