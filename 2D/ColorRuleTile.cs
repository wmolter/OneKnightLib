using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace OneKnight._2D {
    [CreateAssetMenu(menuName ="2D/Tiles/Color Rule Tile")]
    public class ColorRuleTile : RuleTile {
        public Color color;

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData) {
            base.GetTileData(position, tilemap, ref tileData);
            tileData.color = color;
        }
    }
}