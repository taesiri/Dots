using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class DotScript : MonoBehaviour
    {
        private bool _detected;
        private bool _inPattern;
        public Vector2 GridIndex;
        public Transform SecondLayer;

        public bool InPattern
        {
            get { return _inPattern; }
            set
            {
                _inPattern = value;
                ChangeColor();
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

        public void ExposeCell()
        {
            if (InPattern)
            {
                renderer.material.color = GameLogic.Instance.HighlightedColor;
            }
        }

        public void ChangeColor()
        {
            StartCoroutine(ChangeColor(GameLogic.Instance.FlashTime));
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
            renderer.material.color = GameLogic.Instance.HighlightedColor;

            yield return new WaitForSeconds(waitTime);

            renderer.material.color = GameLogic.Instance.BaseColor;
        }

        public void Reset()
        {
            InPattern = false;
            Detected = false;
            renderer.material.color = GameLogic.Instance.BaseColor;
            SecondLayer.localScale = new Vector3(0, 0, 0.1f);
        }
    }
}