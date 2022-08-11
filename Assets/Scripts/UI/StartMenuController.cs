using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public UIDocument uiDoc;

    private Button startButton;
    private Button quitButton;
    private VisualElement root;

    void Start()
    {
        root = uiDoc.rootVisualElement;
        startButton = root.Query<Button>("StartGame").First();
        quitButton = root.Query<Button>("Quit").First();

        startButton.clicked += StartGameButtonPressed;
        quitButton.clicked += QuitGameButtonPressed;
    }

    void StartGameButtonPressed()
    {
        SceneManager.LoadScene("BasicLevel");
    }

    void LoadGameButtonPressed()
    {
        // TODO: Implement load game button
    }

    void QuitGameButtonPressed()
    {
        Application.Quit();
    }
}
