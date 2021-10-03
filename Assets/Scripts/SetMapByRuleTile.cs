using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using System;

public class SetMapByRuleTile : MonoBehaviour
{
    [SerializeField] RuleTile TileA;
    [SerializeField] RuleTile TileB;


    void Start()
    {
        Tilemap tilemap = GetComponent<Tilemap>();
        RuleTile platformTile;
        /* 
         Vector3Int[] position = new Vector3Int[size.x * size.y];
         for (int index = 0; index < position.Length; index++)
         {
             int row = index / size.x;
             int col = index % size.y;

             position[index] = new Vector3Int(row, col, 0);
             tilemap.SetTile(position[index], platformTile);
             //position[index] = new Vector3Int(-row, col, 0);
             //tilemap.SetTile(position[index], platformTile);


         }*/
        System.Random rand = new System.Random();
        
        for (int  i = -10; i < 11; i++)

        {
            for (int j = -5; j < 5; j++)
            {
                platformTile = rand.Next(10) % 2 == 0 ? TileA : TileB;
                tilemap.SetTile(new Vector3Int(i, j, 0), platformTile);
            }

        }
    }
    
    
}




/*public TileBase tileA;
public TileBase tileB;
public Vector2Int size;

void Start()
{
    Vector3Int[] positions = new Vector3Int[size.x * size.y];
    TileBase[] tileArray = new TileBase[positions.Length];

    for (int index = 0; index < positions.Length; index++)
    {
        positions[index] = new Vector3Int(index % size.x, index / size.y, 0);
        tileArray[index] = index % 2 == 0 ? tileA : tileB;
    }

    Tilemap tilemap = GetComponent<Tilemap>();
    tilemap.SetTiles(positions, tileArray);
   
}*/
