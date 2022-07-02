using UnityEngine;

[DisallowMultipleComponent]
public abstract class Skill : MonoBehaviour, ISkill
{
    public abstract void Activate();
}
