using UnityEngine;

namespace Assets.Scripts
{
    public class DotScript : MonoBehaviour
    {
        private bool _detected;
        private SpriteRenderer _sprite;
        public Vector2 GridIndex;
        public Transform SecondLayer;
        public bool InPattern { get; set; }

        public bool Detected
        {
            get { return _detected; }
            set
            {
                _detected = value;
                _sprite.sprite = GameLogic.Instance.GreenDot;
            }
        }

        public void Awake()
        {
            _sprite = GetComponent<SpriteRenderer>();
        }

        public void ExposeCell()
        {
            if (InPattern)
            {
                _sprite.sprite = GameLogic.Instance.PurpleDot;
            }
        }

        public void DisplayHint()
        {
            _sprite.sprite = GameLogic.Instance.PurpleDot;
        }

        public void ResetColor()
        {
            _sprite.sprite = GameLogic.Instance.BlueDot;
        }

        public void Update()
        {
            if (Detected)
            {
                SecondLayer.localScale = new Vector3(Mathf.Lerp(SecondLayer.localScale.x, 1, Time.deltaTime*10f), Mathf.Lerp(SecondLayer.localScale.y, 1, Time.deltaTime*10f), 0.1f);
            }
        }

        public void Reset()
        {
            InPattern = false;
            Detected = false;
            _sprite.sprite = GameLogic.Instance.BlueDot;
            SecondLayer.localScale = new Vector3(0, 0, 0.1f);
        }

        public void GoRed()
        {
            _sprite.sprite = GameLogic.Instance.RedDot;
        }
    }
}