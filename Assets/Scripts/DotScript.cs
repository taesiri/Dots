using UnityEngine;

namespace Assets.Scripts
{
    public class DotScript : MonoBehaviour
    {
        public virtual void Start()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Colorize()
        {
            renderer.material.color = Color.red;
        }
    }
}