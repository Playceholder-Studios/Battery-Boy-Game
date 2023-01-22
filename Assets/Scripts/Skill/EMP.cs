using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EMP : Skill
{
    public float pushStrength = 1.0f;
    public GameObject empParticleSystem;
    private ParticleSystem ps;
    private Collider2D col;

    public override void Activate()
    {
        col.enabled = true;
        ps?.Play();
    }

    private void Awake()
    {
        ps = empParticleSystem.GetComponent<ParticleSystem>();
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            var enemy = other.GetComponent<Enemy>();
            var dirToMove = Vector3.Normalize(other.transform.position - transform.position);
            enemy.MoveToTarget(other.transform.position + (dirToMove * pushStrength), 10.0f, true);
            col.enabled = false;
        }
    }
}
