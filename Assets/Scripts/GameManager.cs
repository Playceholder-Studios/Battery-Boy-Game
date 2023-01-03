using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerInput playerInput;

    public GameObject PauseMenu;

    /// <summary>
    /// Called when the game ends.
    /// </summary>
    public Action GameEnded;

    /// <summary>
    /// We apply this attribute to this property to be able to see it
    /// in the inspector and we can <strong>manually</strong> assign the player controller.
    /// </summary>
    [field: SerializeField]
    public PlayerController PlayerController { get; private set; }

    private PauseMenuController m_pauseMenu;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic();
        m_pauseMenu = PauseMenu.GetComponent<PauseMenuController>();
    }

    private void OnEnable()
    {
        playerInput.actions["Pause"].started += Pause;
    }

    private void OnDisable()
    {
        if (playerInput != null)
        {
            playerInput.actions["Pause"].started -= Pause;
        }
    }

    private void Pause(CallbackContext ctx)
    {
        m_pauseMenu?.TogglePause();
    }

    public void EndGame()
    {
        GameEnded?.Invoke();
    }
}
