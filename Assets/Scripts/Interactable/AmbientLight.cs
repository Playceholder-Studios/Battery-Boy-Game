using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class AmbientLight : MonoBehaviour, IInteractable
{
    [HideInInspector]
    public bool IsInteractable => true;
    public Light2D spotLight;
    [Min(0)]
    public int healAmount = 1;
    public float healRate = 1;
    public float defaultPointLightOuterRadius;
    public float defaultIntensity;

    private float healTick;

    public void Interact()
    {

    }

    void Awake()
    {
        spotLight = GetComponent<Light2D>();
        defaultPointLightOuterRadius = spotLight.pointLightOuterRadius;
        defaultIntensity = spotLight.intensity;

        healTick = 1/healRate;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        healTick -= Time.deltaTime;
        if (collision.gameObject.CompareTag(GameTag.Player.ToString()) && healTick <= 0 && !GameManager.GetPlayer().playerHealth.IsFull())
        {
            GameManager.GetPlayer().HealPlayer(healAmount);
            healTick = 1/healRate;
        }
    }
}
