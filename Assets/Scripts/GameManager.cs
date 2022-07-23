using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    /// <summary>
    /// We apply this attribute to this property to be able to see it
    /// in the inspector and we can <strong>manually</strong> assign the player controller.
    /// </summary>
    [field: SerializeField]
    public PlayerController PlayerController { get; private set; }

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
}
