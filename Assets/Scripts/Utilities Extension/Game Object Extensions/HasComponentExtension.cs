using UnityEngine;

public static class HasComponentExtension
{
    /// <summary>
    /// Check if a game object has a component.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T HasComponent<T>(this GameObject obj)
    {
        if (obj == null) return default(T);

        return obj.GetComponent<T>();
    }
}