using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class DotScript : MonoBehaviour
    {
        private Color _baseColor;
        public bool _detected;
        private bool _inPattern;
        public float FlashTime = 0.5f;
        public Vector2 GridIndex;
        public Color HighlightedColor = Color.red;
        public Transform SecondLayer;

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
            if (Detected)
            {
                SecondLayer.localScale = new Vector3(Mathf.Lerp(SecondLayer.localScale.x, 1, Time.deltaTime*10f), Mathf.Lerp(SecondLayer.localScale.y, 1, Time.deltaTime*10f), 0.1f);
            }
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
            SecondLayer.localScale = new Vector3(0, 0, 0.1f);
        }
    }
}