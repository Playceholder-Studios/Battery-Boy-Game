using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EMP : Skill
{
    public float strength = 1.0f;
    public GameObject empParticleSystem;
    private ParticleSystem ps;

    public override void Activate()
    {
        gameObject.SetActive(true);
        ps?.Play();
    }

    private void Awake()
    {
        gameObject.SetActive(false);
        ps = empParticleSystem.GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            var enemy = other.GetComponent<Enemy>();
            var dirToMove = Vector3.Normalize(other.transform.position - transform.position);
            enemy.MoveToTarget(other.transform.position + (dirToMove * strength), 10.0f, true);
            gameObject.SetActive(false);
        }

    }
}
