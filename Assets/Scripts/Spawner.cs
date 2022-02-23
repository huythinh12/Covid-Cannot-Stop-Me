using UnityEngine;

[CreateAssetMenu(fileName ="Spawner", menuName ="Spawner Data")]
public class Spawner : ScriptableObject
{
    public string name;
    public string description;
    public GameObject spawnerPrefab;
    public int _health = 1;
    public float _speed = 1;
}
