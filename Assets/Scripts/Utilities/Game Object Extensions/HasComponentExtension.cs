using UnityEngine;

public static class HasComponentExtension
{
    /// <summary>
    /// Check if a game object has a component.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool HasComponent<T>(this GameObject obj)
    {
        if (obj == null) return false;
        
        return obj.GetComponent<T>() != null;
    }
}