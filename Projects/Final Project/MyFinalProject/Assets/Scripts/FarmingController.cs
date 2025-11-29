using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using TMPro;

public class FarmingController : MonoBehaviour
{
    public CropManager cropManager;
    public List<SeedPacket> crops;
    public SeedPacket selectedSeed;
    public TMP_Text currentToolText;
    public enum Tool { Hoe, Water, Plant, Harvest }
    public Tool currentTool;
    public float interactionRadius = 3f;

    private void Start()
    {
        selectedSeed = crops[0];
        currentToolText.text = "Current selected plant: Garlic";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedSeed = crops[0];
            currentToolText.text = "Current selected plant: Garlic";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedSeed = crops[1];
            currentToolText.text = "Current selected plant: Potato";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedSeed = crops[2];
            currentToolText.text = "Current selected plant: Sunflower";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedSeed = crops[3];
            currentToolText.text = "Current selected plant: Beet";
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0f;

            CropBlock block = cropManager.GetCropBlockAtTile(worldPos);
            //Debug.Log("FarmingController: Block returned -> " + (block == null ? "NULL" : $"Block at cell {block.tilePos} grid {block.gridPos}"));

            Vector3 blockWorldPos = cropManager.groundTilemap.CellToWorld(block.tilePos) + new Vector3(0.5f, 0.5f, 0);
            float distance = Vector3.Distance(transform.position, blockWorldPos);

            if (block == null || distance > interactionRadius)
            {
                return;
            }

            switch (currentTool)
            {
                case Tool.Hoe:
                    block.TillSoil(cropManager.dryTile);
                    break;
                case Tool.Water:
                    block.WaterSoil(cropManager.wetTile);
                    break;

                case Tool.Plant:
                    if(selectedSeed != null)
                    {
                        block.PlantSeed(selectedSeed, cropManager.wetTile);
                        cropManager.AddToPlantedCrops(block);
                    }
                    break;

                case Tool.Harvest:
                    if (block.HarvestReady())
                    {
                        block.Harvest();
                        cropManager.RemoveFromPlantedCrops(block);
                    }
                    break;
            }
        }
    }
}
