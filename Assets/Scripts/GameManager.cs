using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerInput playerInput;

    public PauseMenuController PauseMenu;

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

    public AudioClip levelMusic;

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
        AudioManager.Instance.SetMusic(levelMusic);
        AudioManager.Instance.PlayMusic();
        m_pauseMenu = PauseMenu.GetComponent<PauseMenuController>();
    }

    private void OnEnable()
    {
        playerInput.actions["Pause"].started += Pause;
        playerInput.actions["Mute"].started += ToggleMusic;
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
        ToggleMusic(ctx);
        m_pauseMenu?.TogglePause();
    }

    private void ToggleMusic(CallbackContext ctx)
    {
        AudioManager.Instance.ToggleMusic();
    }

    public static void ActivateGameOver()
    {
        SceneManager.LoadScene("Game Over");
    }

    public void EndGame()
    {
        GameEnded?.Invoke();
    }

    public static PlayerController GetPlayer()
    {
        return GameManager.Instance.PlayerController;
    }
}
