using UnityEngine;
using TMPro;

public class ToolbarController : MonoBehaviour
{
    public FarmingController farm;
    public TMP_Text currentToolText;

    public void OnHoe() => farm.currentTool = FarmingController.Tool.Hoe;
    public void OnWater() => farm.currentTool = FarmingController.Tool.Water;
    public void OnSeed() => farm.currentTool = FarmingController.Tool.Plant;
    public void OnGather() => farm.currentTool = FarmingController.Tool.Harvest;
}
