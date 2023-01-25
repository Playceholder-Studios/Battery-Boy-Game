using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameOverMenuController : MonoBehaviour
{
    public UIDocument uiDoc;

    private Button restartButton;
    private Button quitButton;
    private VisualElement root;

    void Start()
    {
        root = uiDoc.rootVisualElement;
        restartButton = root.Query<Button>("RestartGame").First();
        quitButton = root.Query<Button>("Quit").First();

        restartButton.clicked += RestartGameButtonPressed;
        quitButton.clicked += QuitGameButtonPressed;
    }

    void RestartGameButtonPressed()
    {
        SceneManager.LoadScene("Main Level");
    }

    void QuitGameButtonPressed()
    {
        Application.Quit();
    }
}
