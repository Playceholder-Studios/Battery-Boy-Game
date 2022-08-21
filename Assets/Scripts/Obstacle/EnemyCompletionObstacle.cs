using UnityEngine;
using System.Collections.Generic;

public class EnemyCompletionObstacle : MonoBehaviour
{
    public GameObject triggerListParent;
    
    public GameObject wallListParent;

    public GameObject enemyListParent;

    private List<OnTriggerEnter2DBroadcaster> triggerList = new List<OnTriggerEnter2DBroadcaster>();

    private List<GameObject> wallList = new List<GameObject>();

    private void OnEnable()
    {
        foreach (var trigger in triggerListParent.GetComponentsInChildren<OnTriggerEnter2DBroadcaster>(includeInactive: true))
        {
            trigger.OnTriggerEntered += ActivateRoom;
            triggerList.Add(trigger);
        }
    }

    private void Start()
    {
        foreach (Transform wall in wallListParent.transform)
        {
            wallList.Add(wall.gameObject);
        }

        // Prepare the room state
        SetTriggersActive(true);
        SetWallsActive(false);
        SetEnemiesActive(false);
    }

    private void Update()
    {
        if (enemyListParent.transform.childCount <= 0)
        {
            SetWallsActive(false);
        }
    }

    private void OnDisable()
    {
        foreach (var t in triggerList)
        {
            t.OnTriggerEntered -= ActivateRoom;
        }
    }

    private void ActivateRoom(Collider2D collision)
    {
        SetTriggersActive(false);
        SetWallsActive(true);
        SetEnemiesActive(true);
    }

    private void SetTriggersActive(bool value)
    {
        foreach (var trigger in triggerList)
        {
            trigger.gameObject.SetActive(value);
        }
    }

    private void SetWallsActive(bool value)
    {
        foreach (var wall in wallList)
        {
            wall.SetActive(value);
        }
    }

    private void SetEnemiesActive(bool value)
    {
        foreach (Transform enemy in enemyListParent.transform)
        {
            enemy.gameObject.SetActive(value);
        }
    }
}
