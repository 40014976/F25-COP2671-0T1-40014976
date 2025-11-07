using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class CropBlock
{
    public SeedPacket seed;

    public int growthStage = 0;
    public float growthTimer = 0;
    public float growthTime = 0f;
    public bool isTilled = false;
    public bool isWatered = false;

    public Vector3Int tilePos;
    public Vector2Int gridPos;
    private Tilemap farmTilemap;
    private GameObject plantPrefab;
    private GameObject spawnedPlant;
    private SpriteRenderer sr;

    public CropBlock(Vector3Int tilePos, Vector2Int gridPos, Tilemap farmTilemap, GameObject plantPrefab)
    {
        this.tilePos = tilePos;
        this.gridPos = gridPos;
        this.farmTilemap = farmTilemap;
        this.plantPrefab = plantPrefab;
    }

    public void TillSoil(TileBase dryTile)
    {
        if (!isTilled)
        {
            isTilled = true;
        }

        isWatered = false;
        farmTilemap.SetTile(tilePos, dryTile);
    }

    public void WaterSoil(TileBase wetTile)
    {
        if (!isTilled)
        {
            return;
        }

        isWatered = true;
        farmTilemap.SetTile(tilePos, wetTile);
    }

    public void PlantSeed(SeedPacket seedPacket, TileBase wetTile)
    {
        if (!isTilled)
        {
            return;
        }

        isWatered = farmTilemap.GetTile(tilePos) == wetTile;

        seed = seedPacket;
        growthStage = 0;
        growthTimer = 0f;
        growthTime = Random.Range(seed.growthTime * 0.75f, seed.growthTime * 1.25f);

        if(spawnedPlant != null)
        {
            sr = null;
            GameObject.Destroy(spawnedPlant);
        }

        spawnedPlant = GameObject.Instantiate(plantPrefab, farmTilemap.CellToWorld(tilePos) + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
        sr = spawnedPlant.GetComponent<SpriteRenderer>();
        sr.sprite = seed.growthSprites[growthStage];
    }

    public void Harvest()
    {
        if (!HarvestReady()) 
        {
            return;
        }

        if (spawnedPlant != null)
        {
            sr = null;
            GameObject.Destroy(spawnedPlant);
        }

        GameObject.Instantiate(seed.harvestablePrefab, tilePos, Quaternion.identity);
        farmTilemap.SetTile(tilePos, null);
        seed = null;
        growthStage = 0;
        growthTimer = 0f;
        isTilled = true;
        isWatered = false;
    }

    public void AdvanceGrowth(TileBase dryTile)
    {
        if (seed == null || !isWatered || spawnedPlant == null)
        {
            return;
        }

        growthTimer += Time.deltaTime;
        if (growthTimer >= growthTime && growthStage < seed.growthSprites.Length - 1)
        {
            growthStage++;
            growthTimer = 0f;

            if(sr == null)
            {
                sr = spawnedPlant.GetComponent<SpriteRenderer>();
            }

            sr.sprite = seed.growthSprites[growthStage];
            isWatered = false;
            farmTilemap.SetTile(tilePos, dryTile);
        }
    }

    public bool HarvestReady()
    {
        return seed != null && growthStage == seed.growthSprites.Length - 1;
    }
}
