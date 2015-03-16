using UnityEngine;

namespace Assets.Scripts
{
    public class GridBuilder : MonoBehaviour
    {
        public GameObject DotPrefab;
        public int Height = 10;
        public float OffsetX = 0;
        public float OffsetZ = 0;
        public int Width = 10;

        private void Start()
        {
            if (DotPrefab == null)
            {
                Debug.LogError("Dot prefab not found");
            }
            else
            {
                for (var i = 0; i < Width; i++)
                {
                    for (var j = 0; j < Height; j++)
                    {
                        var newDot = (GameObject) Instantiate(DotPrefab, new Vector3(OffsetX + i*1.2f,  OffsetZ + j*1.2f,0), Quaternion.identity);
                        newDot.transform.parent = transform.parent;
                    }
                }
            }
        }

        private void Update()
        {
        }
    }
}