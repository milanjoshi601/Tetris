# Tetris
Tetris (Russian: Тетрис)[a] is a puzzle video game created by the Soviet software engineer Alexey Pajitnov in 1985.[1] It has been published by several companies for multiple platforms, most prominently during a dispute over the appropriation of the rights in the late 1980s. After a significant period of publication by Nintendo, the rights reverted to Pajitnov in 1996, who co-founded the Tetris Company with Henk Rogers to manage licensing.

# Tetromino
A tetromino is a geometric shape composed of four squares, connected orthogonally (i.e. at the edges and not the corners).[1][2] Tetrominoes, like dominoes and pentominoes, are a particular type of polyomino. The corresponding polycube, called a tetracube, is a geometric shape composed of four cubes connected orthogonally.

A popular use of tetrominoes is in the video game Tetris created by the Soviet game designer Alexey Pajitnov, which refers to them as tetriminos.[3] The tetrominoes used in the game are specifically the one-sided tetrominoes.

Source : - https://en.wikipedia.org/wiki/Tetromino


# Tetrominos Data Implementation

For eg: - 
This Line Draws "I" Tetromino into the grid
{ Tetromino.I, new Vector2Int[] { new Vector2Int(-1, 1), new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 2, 1) } },

![I](https://github.com/milanjoshi601/Tetris/assets/132807484/daff3529-fc54-40f2-943b-845cc7328dd6)

This Line Draws "Z" Tetromino into the grid
{ Tetromino.Z, new Vector2Int[] { new Vector2Int(-1, 1), new Vector2Int( 0, 1), new Vector2Int( 0, 0), new Vector2Int( 1, 0) } },

![Z](https://github.com/milanjoshi601/Tetris/assets/132807484/c54661eb-feeb-478d-b303-15542b5bcc01)

# Tetrominos Rotation
For tetrominos rotation i have used the following refrence to implement in code
https://strategywiki.org/wiki/File:Tetris_rotation_super.png
https://tetris.fandom.com/wiki/Tetris_Wiki
