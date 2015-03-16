using System;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    public class SquareScript : DotScript
    {
        private static readonly Random RandomGenerator = new Random(DateTime.Now.Millisecond);
        private Color _baseColor;
        private Color _secondColor;
        public bool ChangeColorToEmphasize;
        public Color EmphasizeColor = Color.red;
        public int FadeTime = 4;
        public bool ResetColorToBase;

        public void Awake()
        {
            _secondColor = EmphasizeColor;
        }

        public override void Start()
        {
            SetupColor();
        }

        public override void Update()
        {
            base.Update();

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
            renderer.material.color = Color.Lerp(renderer.material.color, _baseColor, Time.deltaTime*FadeTime);
            if (renderer.material.color == _baseColor)
            {
                ResetColorToBase = false;
            }
        }

        private void ColorAnimator()
        {
            renderer.material.color = Color.Lerp(renderer.material.color, EmphasizeColor, Time.deltaTime*FadeTime);


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

        public override void Colorize()
        {
            Debug.Log("There");
            ChangeColorToEmphasize = true;
            ResetColorToBase = false;
        }
    }
}