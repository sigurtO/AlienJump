using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartUi : MonoBehaviour
{

    [SerializeField]
    private GameManager gameManager;

    public void onStartClick()
    {
        SceneManager.LoadScene("Level1");
    }

    public void onHardCoreClick()
    {
        gameManager.gameIsHardCore = true;
        SceneManager.LoadScene("Level1");

    }
}
