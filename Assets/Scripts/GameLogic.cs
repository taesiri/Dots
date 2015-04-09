using System;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    public class GameLogic : MonoBehaviour
    {
        public static Random RandomGenerator = new Random(DateTime.Now.Millisecond);
        public static GameLogic Instance;
        private int _numberOfDots;
        private int _remainingDots = -1;
        public GameStatus GameStatus;
        public int Height;
        public int Level = 1;
        public GUISkin MasterSkin;
        private DotScript[] PatternDots;
        public int Score;
        public int Width;

        public void Awake()
        {
            Instance = this;
        }

        public void Start()
        {
            Height = GridBuilder.Instance.Height;
            Width = GridBuilder.Instance.Width;

            _numberOfDots = Height*Width;

            _remainingDots = (int) Math.Pow(2, Level);
            PickRandomDots(_remainingDots);


            //Edit this!
            GameStatus = GameStatus.Playing;
        }

        public void PickRandomDots(int count)
        {
            PatternDots = new DotScript[count];

            var counter = 0;

            while (counter < count)
            {
                var rndDot = RandomGenerator.Next(0, _numberOfDots);

                if (!GridBuilder.Instance.Dots[rndDot].InPattern)
                {
                    GridBuilder.Instance.Dots[rndDot].InPattern = true;

                    PatternDots[counter] = GridBuilder.Instance.Dots[rndDot];
                    counter++;
                }
            }
        }

        public void Update()
        {
            if (GameStatus == GameStatus.Playing)
            {
                if (Input.touchCount > 0)
                {
                    var worldRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                    RaycastHit rcHit;

                    if (Physics.Raycast(worldRay, out rcHit))
                    {
                        if (rcHit.collider.gameObject)
                        {
                            var dotScript = rcHit.collider.gameObject.GetComponent<DotScript>();
                            dotScript.Colorize();
                        }
                    }
                }


                if (Input.GetMouseButtonDown(0))
                {
                    var worldRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                    RaycastHit rcHit;

                    if (Physics.Raycast(worldRay, out rcHit))
                    {
                        if (rcHit.collider.gameObject)
                        {
                            var dotScript = rcHit.collider.gameObject.GetComponent<DotScript>();


                            if (!dotScript.Detected)
                            {
                                if (!dotScript.InPattern)
                                {
                                    Debug.Log("Lose");
                                    GameStatus = GameStatus.GameOver;
                                }
                                else
                                {
                                    dotScript.Detected = true;
                                    Debug.Log("Score Up!");
                                    ScoreUp();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ScoreUp()
        {
            Score += Level;
            _remainingDots--;

            if (_remainingDots == 0)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            ResetGrid();

            Level++;
            _remainingDots = (int) Math.Pow(2, Level);
            PickRandomDots(_remainingDots);
        }


        private void ResetGrid()
        {
            for (int i = 0; i < PatternDots.Length; i++)
            {
                PatternDots[i].Reset();

            }
        }

        public void OnGUI()
        {
            switch (GameStatus)
            {
                case GameStatus.Playing:
                    GUI.Label(new Rect(10, 10, 100, 50), Score.ToString(), MasterSkin.label);
                    GUI.Label(new Rect(10, 60, 100, 50), Level.ToString(), MasterSkin.label);
                    break;

                case GameStatus.GameOver:
                    if (GUI.Button(new Rect(10, 10, 100, 50), "Play Again!", MasterSkin.button))
                    {
                        Application.LoadLevel(0);
                    }
                    break;
            }
        }
    }
}