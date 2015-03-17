using System;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    public class SquareScript : MonoBehaviour
    {
        private static readonly Random RandomGenerator = new Random(DateTime.Now.Millisecond);
        private Color _baseColor;
        public bool ChangeColorToEmphasize;
        public Color EmphasizeColor = Color.red;
        public AnimationCurve FadeCurve;
        public int FadeInSpeed = 4;
        public int FadeOutSpeed = 4;
        public Vector2 Index;
        public bool InPattern = false;
        public bool ResetColorToBase;
        public int PatternIndex { get; set; }

        public void Awake()
        {
        }

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
            if (ResetColorToBase)
            {
                ResetColor();
            }
        }

        private void ResetColor()
        {
            renderer.material.color = Color.Lerp(renderer.material.color, _baseColor, Time.deltaTime*FadeInSpeed);
            if (renderer.material.color == _baseColor)
            {
                ResetColorToBase = false;
            }
        }

        private void ColorAnimator()
        {
            renderer.material.color = Color.Lerp(renderer.material.color, EmphasizeColor, Time.deltaTime*FadeOutSpeed);


            if (renderer.material.color == EmphasizeColor)
            {
                ChangeColorToEmphasize = false;
                ResetColorToBase = true;
            }
        }

        private void SetupColor()
        {
            var d = RandomGenerator.Next(125, 255);
            _baseColor = new Color(d/255f, d/255f, d/255f);
            renderer.material.color = _baseColor;
        }

        public void Colorize()
        {
            Debug.Log("There");
            ChangeColorToEmphasize = true;
            ResetColorToBase = false;
        }
    }
}