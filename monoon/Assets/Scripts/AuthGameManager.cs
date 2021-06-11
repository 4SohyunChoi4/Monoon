
using UnityEngine;
using UnityEngine.SceneManagement;

public class AuthGameManager : MonoBehaviour
{
    public static AuthGameManager instance;

    //single tone loop? and change scene method

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeScene(int _sceneIndex)
    {
        SceneManager.LoadSceneAsync(_sceneIndex);
    }
}
