using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor
{
    [CustomEditor(typeof (Grid))]
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
            var grid = (Grid) target;

            for (var i = 0; i < grid.Width; i++)
            {
                for (var j = 0; j < grid.Height; j++)
                {
                    var newDot = (GameObject) Instantiate(grid.CellObject, new Vector3(grid.OffsetX + i*grid.Padding, grid.OffsetZ + j*grid.Padding, 0), Quaternion.identity);
                    newDot.transform.parent = grid.transform;
                }
            }
        }

        private void DeleteChildern()
        {
            var grid = (Grid) target;
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