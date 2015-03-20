using System;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    public class SquareScript : MonoBehaviour
    {
        private static readonly Random RandomGenerator = new Random(DateTime.Now.Millisecond);
        public static int GlobalCounter;
        private bool _alreadyHit;
        private Color _baseColor;
        public bool ChangeColorToEmphasize;
        public AnimationCurve FadeCurve;
        public int FadeInSpeed = 4;
        public int FadeOutSpeed = 4;
        public Vector2 Index;
        public bool InPattern = false;
        public bool ResetColorToBase;
        public Color SelectionColor = Color.cyan;
        public Color TargetColor;
        public int PatternIndex { get; set; }

        public void Start()
        {
            SetupColor();
        }

        public void Update()
        {
            if (ChangeColorToEmphasize)
            {
                ColorAnimator();
            }
        }

        private void ColorAnimator()
        {
            renderer.material.color = Color.Lerp(renderer.material.color, TargetColor, Time.deltaTime*FadeOutSpeed);

            if (renderer.material.color == TargetColor)
            {
                ChangeColorToEmphasize = false;
            }
        }

        private void SetupColor()
        {
            var d = RandomGenerator.Next(125, 255);
            _baseColor = new Color(d/255f, d/255f, d/255f);
            renderer.material.color = _baseColor;
        }

        public void GotHit()
        {
            if (!_alreadyHit)
            {
                if (GlobalCounter + 1 == PatternIndex)
                {
                }


                _alreadyHit = true;
                Colorize();
                GlobalCounter++;
            }
        }

        public void Colorize()
        {
            ChangeColorToEmphasize = true;
            ResetColorToBase = false;
        }

        public void Reset()
        {
            _alreadyHit = false;
            ChangeColorToEmphasize = false;
            ResetColorToBase = true;
        }
    }
}