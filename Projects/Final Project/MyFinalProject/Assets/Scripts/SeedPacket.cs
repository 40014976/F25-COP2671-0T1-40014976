using UnityEngine;

[CreateAssetMenu(menuName = "Farm/SeedPacket")]
public class SeedPacket : ScriptableObject
{
    public string cropName;
    public Sprite[] growthSprites;
    public Sprite coverImage;
    public float growthTime;
    public GameObject harvestablePrefab;
    public GameObject plantPrefab;
}
