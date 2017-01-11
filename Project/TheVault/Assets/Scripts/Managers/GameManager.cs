using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{

    public override void UpdateNormal()
    {
        GameInput();
    }

    private void GameInput()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
