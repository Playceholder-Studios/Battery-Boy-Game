using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : Enemy
{
    [SerializeField]
    GameObject bullet;

    public float fireRate = 1f;
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

        base.Update();
    }

    void CheckIfTimeToFire() 
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0) 
        {
            Projectile pj = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Projectile>();
            pj?.SetDirection(new Vector3(-1, 0, 0));
            pj.range = 4;
            pj?.SetSize(1f);
            pj.source = this.gameObject;
            cooldownTimer = fireRate;
        }
    }
}
