using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
[CreateAssetMenu]
public class NewCustomRuleTile : RuleTile<NewCustomRuleTile.Neighbor> {
    public bool customField;
    public TileBase[] Tiles;
    public class Neighbor : RuleTile.TilingRule.Neighbor {
        public const int Any = 1;
        public const int Empty = 2;
    }

    public override bool RuleMatch(int neighbor, TileBase tile) {
        switch (neighbor) {
            case Neighbor.Empty: return GetEmpty(tile);
            case Neighbor.Any: return GetAny(tile);
        }
        return base.RuleMatch(neighbor, tile);
    }

    bool GetAny(TileBase tile)
    {
        return Tiles.Contains(tile);
    }
    bool GetEmpty(TileBase tile)
    {
        return tile == null;

    }
}