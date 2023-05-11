using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

public class Board : MonoBehaviour
{
    public TetrominoData[] tetrominoData;
    public Tilemap tilemap { get; private set; }
    public TileBlock activeTile { get; private set; }
    public Vector3Int spawnPos;
    public Vector2Int boardSize = new Vector2Int(10, 20);

    public GameObject gameOverText;
    
    public GameObject startAgainButton;
    
    public TMP_Text score;

    int scoreData = 1000;

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
        gameOverText.SetActive(false);
        startAgainButton.SetActive(false);
        SpawnBlock();
    }

    public void SpawnBlock()
    {
        int random = Random.Range(0, this.tetrominoData.Length);
        TetrominoData data = this.tetrominoData[random];

        this.activeTile.Initialize(this, this.spawnPos, data);

        if (IsValidPos(this.activeTile, this.spawnPos))
        {
            Set(this.activeTile);
        }
        else
        {
            GameOver();
        }

        
    }

    void GameOver()
    {
        this.tilemap.ClearAllTiles();
        Time.timeScale = 0;
        gameOverText.SetActive(true);
        startAgainButton.SetActive(true);
        
    }

    public void StartAgain()
    {
        gameOverText.SetActive(false);
        Time.timeScale = 1;
        startAgainButton.SetActive(false);
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

    public void ClearLine()
    {
        RectInt bound = this.Bounds;
        int row = bound.yMin;

        while (row < bound.yMax)
        {
            if (IsLineFull(row))
            {
                LineClear(row);
                   
                score.text = "Score : " + scoreData;
                scoreData += 1000;
            }
            else
            {
                row++;
            }
        }
    }

    bool IsLineFull(int row)
    {
        RectInt bound = this.Bounds;

        for (int i = bound.xMin; i < bound.xMax; i++)
        {
            Vector3Int pos = new Vector3Int(i, row, 0);

            if (!tilemap.HasTile(pos))
            {
                return false;
            }
        }

        return true;
    }

    void LineClear(int row)
    {
        RectInt bound = this.Bounds;

        for (int i = bound.xMin; i < bound.xMax; i++)
        {
            Vector3Int pos = new Vector3Int(i, row, 0);
            this.tilemap.SetTile(pos, null);
        }

        while (row < bound.yMax)
        {
            for (int i = bound.xMin; i < bound.xMax; i++)
            {
                Vector3Int pos = new Vector3Int(i, row + 1, 0);
                TileBase above = this.tilemap.GetTile(pos);

                pos = new Vector3Int(i, row, 0);
                tilemap.SetTile(pos, above);
            }

            row++;
        }
    }

}
