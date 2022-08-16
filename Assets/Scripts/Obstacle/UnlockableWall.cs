using UnityEngine;

public class UnlockableWall : MonoBehaviour, IUnlockable
{
    public void Unlock()
    {
        Destroy(gameObject);
    }
}
