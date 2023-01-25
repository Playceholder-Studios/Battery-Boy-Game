using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class DrainableLight : MonoBehaviour, IInteractable
{
    [HideInInspector]
    public bool IsInteractable => true;
    public Light2D spotLight;
    public int currentChargeCount;
    /// <summary>
    /// The amount of times that you can "drain" the light
    /// </summary>
    [Min(0)]
    public int charges;
    [Min(0)]
    public int healAmount = 1;
    public float defaultPointLightOuterRadius;
    public float defaultIntensity;
    private float pointLightOuterRadiusIncrement
    {
        get
        {
            int _charges = charges;
            if (charges <= 0)
            {
                _charges = 1;
            }
            return defaultPointLightOuterRadius / _charges;
        }
    }
    private float intensityIncrement
    {
        get
        {
            int _charges = charges;
            if (charges <= 0)
            {
                _charges = 1;
            }
            return defaultIntensity / _charges;
        }
    }

    public void Interact()
    {
        if (!IsInteractable) { return; }

        spotLight.pointLightOuterRadius -= pointLightOuterRadiusIncrement;
        spotLight.intensity -= intensityIncrement;
        if (currentChargeCount > 0)
        {
            currentChargeCount--;
            GameManager.GetPlayer().HealPlayer(healAmount);
        }
    }

    void Awake()
    {
        spotLight = GetComponent<Light2D>();
        currentChargeCount = charges;
        defaultPointLightOuterRadius = spotLight.pointLightOuterRadius;
        defaultIntensity = spotLight.intensity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameTag.Player.ToString()))
        {
            GameManager.GetPlayer().currentInteractable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(GameTag.Player.ToString()))
        {
            GameManager.GetPlayer().currentInteractable = null;
        }
    }
}
