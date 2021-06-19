using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake() 
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(gameObject);
        }
    }

    public void ChangeScene(int _scenceIndex)
    {
        SceneManager.LoadSceneAsync(_scenceIndex);
    }
}
