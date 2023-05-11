using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBlock : MonoBehaviour
{
    public Board board { get; private set; }
    public Vector3Int position { get; private set; }
    public TetrominoData data { get; private set; }

    public Vector3Int[] cells { get; private set; }
    public int rotationIndex { get; private set; }

    public float stepDelay = 1f;
    public float lockDelay = 0.5f;

    float stepTime;
    float lockTime;

    public void Initialize(Board board, Vector3Int position, TetrominoData data)
    {
        this.board = board;
        this.position = position;
        this.data = data;
        this.rotationIndex = 0;
        this.stepTime = Time.time + this.stepDelay;
        this.lockTime = 0f;

        if(this.cells==null)
        {
            this.cells = new Vector3Int[data.cells.Length];
        }

        for (int i = 0; i < data.cells.Length; i++)
        {
            this.cells[i] = (Vector3Int)data.cells[i];
        }
    }

    private void Update()
    {
        this.board.Clear(this);

        this.lockTime += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            Rotate(1);
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            Move(Vector2Int.left);
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            Move(Vector2Int.right);
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            Move(Vector2Int.down);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            HardDrop();
        }

        if(Time.time>=this.stepTime)
        {
            Step();
        }

        this.board.Set(this);
    }

    void Step()
    {
        stepTime = Time.time + stepDelay;

        Move(Vector2Int.down);

        if(lockTime>=lockDelay)
        {
            Lock() ;
        }
    }

    void Lock()
    {
        board.Set(this);
        board.ClearLine();
        board.SpawnBlock();
    }

    void HardDrop()
    {
        while (Move(Vector2Int.down))
        {
            continue;
        }

        Lock();
    }

    bool Move(Vector2Int transform)
    {
        Vector3Int newPos = this.position;
        newPos.x += transform.x;
        newPos.y += transform.y;

        bool valid = this.board.IsValidPos(this, newPos);

        if(valid)
        {
            this.position = newPos;
            this.lockTime = 0f;
        }

        return valid;
    }

    void Rotate(int dir)
    {
        int originalRotation = this.rotationIndex;
        this.rotationIndex = Wrap(this.rotationIndex + dir, 0, 4);

        RotationMatrix(dir);

        if(!TestWallkicks(this.rotationIndex,dir))
        {
            this.rotationIndex = originalRotation;
            RotationMatrix(-dir);
        }
    }

    void RotationMatrix(int dir)
    {
        for (int i = 0; i < this.data.cells.Length; i++)
        {
            Vector3 cell = this.cells[i];

            int x, y;

            switch (this.data.tetromino)
            {
                case Tetromino.I:
                case Tetromino.O:
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    x = Mathf.CeilToInt((cell.x * Data.RotationMatrix[0] * dir) + (cell.y * Data.RotationMatrix[1] * dir));
                    y = Mathf.CeilToInt((cell.x * Data.RotationMatrix[2] * dir) + (cell.y * Data.RotationMatrix[3] * dir));
                    break;

                default:
                    x = Mathf.RoundToInt((cell.x * Data.RotationMatrix[0] * dir) + (cell.y * Data.RotationMatrix[1] * dir));
                    y = Mathf.RoundToInt((cell.x * Data.RotationMatrix[2] * dir) + (cell.y * Data.RotationMatrix[3] * dir));
                    break;
            }
            this.cells[i] = new Vector3Int(x, y, 0);
        }
    }

    bool TestWallkicks(int rotationIndex, int rotationDir)
    {
        int wallKickIndex = GetWallkickIndex(rotationIndex, rotationDir);

        for (int i = 0; i < this.data.wallkicks.GetLength(1); i++)
        {
            Vector2Int transform = this.data.wallkicks[wallKickIndex, i];

            if(Move(transform))
            {
                return true;
            }
        }

        return false;
    }

    int GetWallkickIndex(int rotationIndex, int rotationDir)
    {
        int wallkickIndex = rotationIndex * 2;

        if (rotationDir < 0)
        {
            wallkickIndex--;
        }

        return Wrap(wallkickIndex, 0, this.data.wallkicks.GetLength(0));
    }

    int Wrap(int input,int min, int max)
    {
        if(input<min)
        {
            return max - (min - input) % (max - min);
        }
        else
        {
            return min + (input - min) % (max - min);
        }
    }
}
