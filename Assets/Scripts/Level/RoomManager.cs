using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class RoomManager : MonoBehaviour
{
    public Dictionary<string, Enemy> enemies = new Dictionary<string, Enemy>();

    public PlayerConsumable[] drops;
    public Triggerable[] triggers;
    public GameObject[] spawns;
    public Vector3 lastEnemyPosition;

    private bool completed = false;

    protected void Start()
    {
        foreach (Transform enemyTransform in this.transform.Find("Enemies"))
        {
            Enemy enemy = enemyTransform.gameObject.GetComponent<Enemy>();
            enemies.Add(enemy.name, enemy);
            enemy.OnDeath += () => onEnemyDeath(enemy.name);
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
            foreach ((string id, Enemy enemy) in enemies)
            {
                enemy.SetTarget(GameManager.GetPlayer().transform);
            }
        }
    }

    private void onEnemyDeath(string enemyId)
    {
        Enemy enemy = enemies[enemyId];
        lastEnemyPosition = enemy.transform.position;
        enemies.Remove(enemyId);
    }

    protected void Update()
    {
        if (enemies.Count == 0 && !completed)
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