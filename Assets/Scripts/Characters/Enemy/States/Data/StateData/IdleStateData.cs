using UnityEngine;

[CreateAssetMenu(fileName = "newIdleStateData", menuName = "Data/State Data/Idle State")]
public class IdleStateData : ScriptableObject
{
    public float MinIdleTime;
    public float MaxIdleTime;
}