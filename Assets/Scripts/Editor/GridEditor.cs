using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor
{
    [CustomEditor(typeof (GridBuilder))]
    public class GridEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Create Grid"))
            {
                CreateGrid();
            }
            if (GUILayout.Button("Delete Childern"))
            {
                DeleteChildern();
            }
        }

        private void CreateGrid()
        {
            var grid = (GridBuilder) target;
            grid.Dots = new DotScript[grid.Width*grid.Height];

            for (var i = 0; i < grid.Width; i++)
            {
                for (var j = 0; j < grid.Height; j++)
                {
                    var newDot = (GameObject) Instantiate(grid.DotPrefab, new Vector3(-2.4f + i*grid.GridSize, -4.21f + j*grid.GridSize, 0), Quaternion.identity);

                    if (j%2 == 0)
                    {
                        newDot.transform.position = new Vector3(-2.4f + grid.ZigZagDelta + i*grid.GridSize, -4.21f + j*grid.GridSize, 0);
                    }


                    newDot.transform.parent = grid.transform;

                    var dotScript = newDot.GetComponent<DotScript>();
                    dotScript.GridIndex = new Vector2(i, j);


                    grid.Dots[j*grid.Width + i] = dotScript;
                }
            }
        }

        private void DeleteChildern()
        {
            var grid = (GridBuilder) target;
            var childs = new Transform[grid.transform.childCount];

            for (var i = 0; i < grid.transform.childCount; i++)
            {
                childs[i] = grid.transform.GetChild(i);
            }
            for (var i = 0; i < childs.Length; i++)
            {
                DestroyImmediate(childs[i].gameObject);
            }
        }
    }
}