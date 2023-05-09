using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public TetrominoData[] tetrominoData;
    public Tilemap tilemap { get; private set; }
    public TileBlock activeTile { get; private set; }
    public Vector3Int spawnPos;
    public Vector2Int boardSize = new Vector2Int(10, 20);

    public RectInt Bounds
    {
        get
        {
            Vector2Int pos = new Vector2Int(-this.boardSize.x / 2, -this.boardSize.y / 2);
            return new RectInt(pos, this.boardSize);
        }
    }

    private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activeTile = GetComponentInChildren<TileBlock>();
        for (int i = 0; i < this.tetrominoData.Length; i++)
        {
            this.tetrominoData[i].Initialize();
        }
    }

    private void Start()
    {
        SpawnBlock();
    }

    public void SpawnBlock()
    {
        int random = Random.Range(0, this.tetrominoData.Length);
        TetrominoData data = this.tetrominoData[random];

        this.activeTile.Initialize(this, this.spawnPos, data);
        Set(this.activeTile);
    }

    public void Set(TileBlock block)
    {
        for (int i = 0; i < block.cells.Length; i++)
        {
            Vector3Int tilePos = block.cells[i] + block.position;

            this.tilemap.SetTile(tilePos, block.data.tile);

        }
    }

    public void Clear(TileBlock block)
    {
        for (int i = 0; i < block.cells.Length; i++)
        {
            Vector3Int tilePos = block.cells[i] + block.position;

            this.tilemap.SetTile(tilePos, null);

        }
    }

    public bool IsValidPos(TileBlock block,Vector3Int pos)
    {
        RectInt bound = this.Bounds;
        for (int i = 0; i < block.cells.Length; i++)
        {
            Vector3Int blockPos = block.cells[i] + pos;

            if(!bound.Contains((Vector2Int)blockPos))
            {
                return false;
            }
            if(this.tilemap.HasTile(blockPos))
            {
                return false;
            }
        }
        return true;
    }
}
