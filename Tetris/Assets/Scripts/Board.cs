using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }
    public TetrominoData[] tetrominoData;

    private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();

        for (int i = 0; i < this.tetrominoData.Length; i++)
        {
            this.tetrominoData[i].Initialize();
        }
    }

    private void Start()
    {
        SpawnTile();
    }

    void SpawnTile()
    {
        int random = Random.Range(0, this.tetrominoData.Length);
        TetrominoData data = this.tetrominoData[random];
    }

    public void Set(Tile tile)
    {
        for (int i = 0; i < tile.cells.Length; i++)
        {
            Vector3Int tilePos = tile.cells[i] + tile.pos;
            this.tilemap.SetTile(tilePos, tile.data.tile);
        }
    }
}
