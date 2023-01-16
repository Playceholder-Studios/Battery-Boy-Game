using UnityEngine;

/// <summary>
/// Interface for projectile objects.
/// </summary>
public interface IProjectile
{
    #region Public Methods
    public void OnCollision();
    #endregion Public Methods
}
