using UnityEngine;

[CreateAssetMenu(fileName = "New Trap", menuName = "Game/Traps/Trap")]
public class TrapsSCRO : ScriptableObject
{
    public string name;
    public GameObject prefab;
    public float yPosition;
}
