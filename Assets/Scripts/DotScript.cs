using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class DotScript : MonoBehaviour
    {
        private Color _baseColor;
        private bool _detected;
        private bool _inPattern;
        public float FlashTime = 0.5f;
        public Vector2 GridIndex;
        public Color HighlightedColor = Color.red;

        public bool InPattern
        {
            get { return _inPattern; }
            set
            {
                _inPattern = value;
                Colorize();
            }
        }

        public bool Detected
        {
            get { return _detected; }
            set
            {
                _detected = value;
                renderer.material.color = Color.green;
            }
        }

        public void Awake()
        {
            _baseColor = renderer.material.color;
        }

        public virtual void Colorize()
        {
            StartCoroutine(ChangeColor(FlashTime));
        }

        public void Update()
        {
        }

        private IEnumerator ChangeColor(float waitTime)
        {
            renderer.material.color = HighlightedColor;

            yield return new WaitForSeconds(waitTime);

            renderer.material.color = _baseColor;
        }

        public void Reset()
        {
            InPattern = false;
            Detected = false;
            renderer.material.color = _baseColor;
        }
    }
}