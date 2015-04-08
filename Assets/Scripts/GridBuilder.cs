using UnityEngine;

namespace Assets.Scripts
{
    public class GridBuilder : MonoBehaviour
    {
        public GameObject DotPrefab;
        public float GridSize = 0.5f;
        public int Height;
        public int Width;
        public float ZigZagDelta = 2.5f;
    }
}