using UnityEngine;

[CreateAssetMenu(menuName = "SctiptableData/PlayerData", fileName = "Player Data")]
public class PlayerData : ScriptableObject
{
    public int money;
    public int energy;
    public AudioClip walkClip;

    public Color color;
    public string playerName;

    // Start is called before the first frame update
    void Awake()
    {

    }
}