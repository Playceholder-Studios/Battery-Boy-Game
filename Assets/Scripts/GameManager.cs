using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenuObject;
    
    private PauseMenu pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = pauseMenuObject.GetComponent<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPause(InputValue value)
    {
        pauseMenu?.TogglePause();
    }
}
