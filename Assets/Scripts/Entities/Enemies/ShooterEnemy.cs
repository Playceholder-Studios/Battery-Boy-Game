using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : Enemy
{
    [SerializeField]
    GameObject bullet;

    public float fireRate = 1f;
    public Vector3 angle;
    public bool shouldTargetPlayer;
    private float cooldownTimer;

    // Start is called before the first frame update
    protected override void Start()
    {
        cooldownTimer = fireRate;

        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        CheckIfTimeToFire();
        if(shouldTargetPlayer && target != null)
        {
            angle = (target.position - transform.position).normalized;
        }

        base.Update();
    }

    void CheckIfTimeToFire() 
    {
        cooldownTimer -= Time.deltaTime;
        bool targetInRange = !shouldTargetPlayer || (target != null && isInRange(target.position));
        if (cooldownTimer <= 0 && targetInRange) 
        {
            Projectile pj = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Projectile>();
            pj?.SetDirection(angle);
            pj.range = 4;
            pj?.SetSize(1f);
            pj.source = this.gameObject;
            cooldownTimer = fireRate;
        }
    }
}
