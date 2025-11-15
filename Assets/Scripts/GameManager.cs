using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void OnReplay()
    {
        SceneManager.LoadScene("Game");
    }
}
