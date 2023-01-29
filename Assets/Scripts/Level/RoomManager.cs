using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class RoomManager : MonoBehaviour
{
    public Enemy[] enemies;
    private int enemyCount;

    public PlayerConsumable[] drops;
    public Triggerable[] triggers;
    public GameObject[] spawns;
    public Vector3 lastEnemyPosition;

    private bool completed = false;

    protected void Start()
    {
        enemyCount = enemies.Length;
        foreach (Enemy enemy in enemies) 
        {
            enemy.OnDeath += () => onEnemyDeath(enemy);
        }
        foreach (GameObject obj in spawns) 
        {
            obj.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameTag.Player.ToString()))
        {
            foreach (GameObject obj in spawns) 
            {
                obj.SetActive(true);
            }
        }
    }

    private void onEnemyDeath(Enemy enemy)
    {
        enemyCount--;
        lastEnemyPosition = enemy.transform.position;
    }

    protected void Update()
    {
        if (enemyCount == 0 && !completed)
        {
            foreach (PlayerConsumable drop in drops)
            {
                drop.Spawn(lastEnemyPosition);
            }
            foreach (Triggerable trigger in triggers)
            {
                trigger.triggerEvent();
            }
            completed = true;
        }
    }
}