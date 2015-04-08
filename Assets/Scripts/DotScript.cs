using UnityEngine;

namespace Assets.Scripts
{
    public class DotScript : MonoBehaviour
    {
        public bool InPattern = false;

        public Vector2 GridIndex;
        public virtual void Colorize()
        {
            renderer.material.color = Color.red;
        }
    }
}