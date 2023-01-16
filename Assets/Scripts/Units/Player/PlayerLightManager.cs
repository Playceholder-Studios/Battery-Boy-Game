using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(PlayerController))]
public class PlayerLightManager : MonoBehaviour
{
    public Light2D playerLight;
    public float maxIntensity;
    public float minIntensity;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void OnEnable()
    {
        playerController.OnProjectileUpdate += UpdatePlayerLight;
    }

    public void OnDisable()
    {
        playerController.OnProjectileUpdate -= UpdatePlayerLight;
    }

    public void UpdatePlayerLight(int currentProjectileCount)
    {
        float currentPercentage = (float) currentProjectileCount / playerController.playerMaxProjectile;
        playerLight.intensity = Mathf.Max(minIntensity, maxIntensity * currentPercentage);
    }
}
