using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class StairTile : Tile{
    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        base.RefreshTile(position, tilemap);
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
    }

    [Header("ModifyPlayerSpeed")]
    public Vector2 modifier;
    public Vector2 ModifyWalkingSpeed(float H, float V){
        float x,y;
        x=Mathf.Sign(H)*modifier.x;
        y=V+modifier.y;
        return new Vector2(x,y);

    }
}
