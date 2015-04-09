using UnityEngine;

namespace Assets.Scripts
{
    public class GridBuilder : MonoBehaviour
    {
        public static GridBuilder Instance;
        public GameObject DotPrefab;
        public DotScript[] Dots;
        public float GridSize = 0.5f;
        public int Height;
        public int Width;
        public float ZigZagDelta = 2.5f;

        public void Awake()
        {
            Instance = this;
        }
    }
}