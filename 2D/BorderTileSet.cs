using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace OneKnight._2D {
    // Keeping this because I just wrote it... but most likely, use Unity's RuleTile (or my ColorRuleTile)
    public class BorderTileSet : ScriptableObject {
        public static int BitmaskFor(bool right, bool left, bool top, bool bottom) {
            int r = right ? 8 : 0;
            int l = left ? 4 : 0;
            int t = top ? 2 : 0;
            int b = bottom ? 1 : 0;
            return r|l|t|b;
        }
        public static int BitmaskFor(Vector3Int pos, IEnumerable<Vector3Int> occupied) {
            Vector3Int rightPos = pos+Vector3Int.right;
            Vector3Int leftPos = pos+Vector3Int.left;
            Vector3Int topPos = pos+Vector3Int.up;
            Vector3Int bottomPos = pos+Vector3Int.down;
            bool right = false;
            bool left = false;
            bool top = false;
            bool bottom = false;
            foreach(Vector3Int other in occupied) {
                if(other == rightPos)
                    right = true;
                if(other == leftPos)
                    left = true;
                if(other == topPos)
                    top = true;
                if(other == bottomPos)
                    bottom = true;
            }
            return BitmaskFor(right, left, top, bottom);
        }

        public TileBase all, noRight, noLeft, noTop, noBottom, topRight, topLeft, topBottom, bottomRight, bottomLeft, rightLeft, right, left, top, bottom, none;

        public TileBase TileFor(Vector3Int pos, IEnumerable<Vector3Int> occupied) {
            return TileFor(BitmaskFor(pos, occupied));
        }

        public TileBase TileFor(bool right, bool left, bool top, bool bottom) {
            return TileFor(BitmaskFor(right, left, top, bottom));
        }

        public TileBase TileFor(int bitmask) {
            switch(bitmask) {
                case 0b0000:
                    return none;
                case 0b0001:
                    return bottom;
                case 0b0010:
                    return top;
                case 0b0100:
                    return left;
                case 0b1000:
                    return right;
                case 0b0011:
                    return topBottom;
                case 0b0101:
                    return bottomLeft;
                case 0b0110:
                    return topLeft;
                case 0b1001:
                    return bottomRight;
                case 0b1010:
                    return topRight;
                case 0b1100:
                    return rightLeft;
                case 0b0111:
                    return noRight;
                case 0b1011:
                    return noLeft;
                case 0b1101:
                    return noTop;
                case 0b1110:
                    return noBottom;
                case 0b1111:
                    return all;
            }
            throw new UnityException("illegal tile bitmask: " + bitmask);
        }
    }
}