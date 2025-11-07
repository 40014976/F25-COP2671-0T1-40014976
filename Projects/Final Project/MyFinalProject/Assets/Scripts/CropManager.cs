using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class CropManager : MonoBehaviour
{
    public Tilemap farmTilemap;
    public Tilemap groundTilemap;
    public TileBase dryTile;
    public TileBase wetTile;

    private CropBlock[,] cropGrid;

    private List<CropBlock> plantedCrops = new List<CropBlock>();
    public GameObject plantPrefab;

    void Start()
    {
        CreateGridUsingTilemap(groundTilemap);
    }

    public void CreateGridUsingTilemap(Tilemap tilemap)
    {
        tilemap.CompressBounds();
        BoundsInt bounds = tilemap.cellBounds;

        cropGrid = new CropBlock[bounds.size.x, bounds.size.y];

        int created = 0;
        foreach (var pos in bounds.allPositionsWithin)
        {
            Vector2Int gridPos = new Vector2Int(pos.x - bounds.xMin, pos.y - bounds.yMin);
            CreateGridBlock(gridPos, pos);
            created++;
        }
    }

    public void CreateGridBlock(Vector2Int gridPos, Vector3Int tilePos)
    {
        CropBlock block = new CropBlock(tilePos, gridPos, farmTilemap, plantPrefab);
        cropGrid[gridPos.x, gridPos.y] = block;
    }

    public void AddToPlantedCrops(CropBlock block)
    {
        if (!plantedCrops.Contains(block))
        {
            plantedCrops.Add(block);
        }
    }

    public void RemoveFromPlantedCrops(CropBlock block)
    {
        if (plantedCrops.Contains(block))
        {
            plantedCrops.Remove(block);
        }
    }

    public CropBlock GetCropBlockAtTile(Vector3 worldPosition)
    {
        Vector3Int tilePos = groundTilemap.WorldToCell(worldPosition);

        int gridX = tilePos.x - groundTilemap.cellBounds.xMin;
        int gridY = tilePos.y - groundTilemap.cellBounds.yMin;

        if (gridX < 0 || gridY < 0 || gridX >= cropGrid.GetLength(0) || gridY >= cropGrid.GetLength(1) || cropGrid == null)
        {
            return null;
        }

        return cropGrid[gridX, gridY];
    }

    // Update is called once per frame
    void Update()
    {
        if (plantedCrops.Count <= 0)
        {
            return;
        }

        foreach (var crop in plantedCrops)
        {
            crop.AdvanceGrowth(dryTile);
        }
    }
}
