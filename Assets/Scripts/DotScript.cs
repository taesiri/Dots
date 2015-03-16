using UnityEngine;

namespace Assets.Scripts
{
    public class DotScript : MonoBehaviour
    {
        public AnimationCurve FadeCurve;
        public Vector2 Index;

        public virtual void Start()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Colorize()
        {
            Debug.Log("HERE");
            renderer.material.color = Color.red;
        }
    }
}