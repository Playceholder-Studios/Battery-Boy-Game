using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestMenuItem : MonoBehaviour
{
    [MenuItem("Scene/Tag Game Objects")]
    static void DoSomething()
    {
        GameObject[] gameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        var gos = FindObjectsOfType(typeof(Enemy));
        foreach ( var go in gos )
        {
            Debug.Log(go.name);
            
        }
    }    
}
