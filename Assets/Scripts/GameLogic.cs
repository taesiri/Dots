using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    public class GameLogic : MonoBehaviour
    {
        public static Random RandomGenerator = new Random(DateTime.Now.Millisecond);
        public static GameLogic Instance;
        public static GUILocationHelper LocationHelper = new GUILocationHelper();
        private Matrix4x4 _guiMatrix;
        private float _levelStartTime;
        private int _numberOfDots;
        private DotScript[] _patternDots;
        private int _remainingDots = -1;
        public AnimationCurve CameraCurve;
        public GUISkin GameStatSkin;
        public GameStatus GameStatus;
        public int Height;
        public int Level;
        public GUISkin MasterSkin;
        public Texture2D QuitTexture2D;
        public int Score;
        public int Width;

        public void Awake()
        {
            Instance = this;
        }

        public void Start()
        {
            _levelStartTime = Time.time;

            ////////////////////////////// GRID Setup
            Height = GridBuilder.Instance.Height;
            Width = GridBuilder.Instance.Width;
            _numberOfDots = Height*Width;
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            ////////////////////////////// GUI Setup
            LocationHelper.PointLocation = GUILocationHelper.Point.Center;
            LocationHelper.UpdateLocation();
            var ratio = LocationHelper.GuiOffset;
            _guiMatrix = Matrix4x4.identity;
            _guiMatrix.SetTRS(new Vector3(1, 1, 1), Quaternion.identity, new Vector3(ratio.x, ratio.y, 1));
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            GameStatus = GameStatus.StartScreen;
            Camera.main.orthographicSize = 0.1f;
        }

        public void PickRandomDots(int count)
        {
            _patternDots = new DotScript[count];

            var counter = 0;

            while (counter < count)
            {
                var rndDot = RandomGenerator.Next(0, _numberOfDots);

                if (!GridBuilder.Instance.Dots[rndDot].InPattern)
                {
                    GridBuilder.Instance.Dots[rndDot].InPattern = true;

                    _patternDots[counter] = GridBuilder.Instance.Dots[rndDot];
                    counter++;
                }
            }
        }

        public IEnumerator InitialSetup()
        {
            yield return new WaitForSeconds(0.75f);
            ////////////////////////////// Level Setup
            _remainingDots = 1;
            PickRandomDots(_remainingDots);

            GameStatus = GameStatus.Playing;
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        public void Update()
        {
            if (GameStatus == GameStatus.StartScreen)
            {
                Camera.main.orthographicSize = CameraCurve.Evaluate(Time.time - _levelStartTime)*5;

                if (Math.Abs(5 - Camera.main.orthographicSize) < .8f)
                {
                    Camera.main.orthographicSize = 5;
                    GameStatus = GameStatus.Initialize;
                    StartCoroutine(InitialSetup());
                }
            }


            else if (GameStatus == GameStatus.Playing)
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
                                    GameStatus = GameStatus.GameOver;
                                }
                                else
                                {
                                    dotScript.Detected = true;
                                    Debug.Log(dotScript.GridIndex.ToString());
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
            if (Level == 0)
                Score++;
            else
            {
                Score += Level;
            }

            _remainingDots--;

            if (_remainingDots == 0)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            StartCoroutine(LevelingUp(1f));
        }

        private IEnumerator LevelingUp(float waitTime)
        {
            GameStatus = GameStatus.ChangeLevel;
            yield return new WaitForSeconds(waitTime);
            ResetGrid();

            Level++;
            _remainingDots = (int) Math.Pow(2, Level);
            PickRandomDots(_remainingDots);


            GameStatus = GameStatus.Playing;
        }

        private void ResetGrid()
        {
            for (var i = 0; i < _patternDots.Length; i++)
            {
                _patternDots[i].Reset();
            }
        }

        public void OnGUI()
        {
            GUI.matrix = _guiMatrix;


            if (GameStatus != GameStatus.StartScreen)
            {
                if (GUI.Button(new Rect(15, 15, 32, 32), QuitTexture2D, GUIStyle.none))
                {
                    Application.Quit();
                }
            }


            if (GameStatus != GameStatus.GameOver)
            {
                GUI.Label(new Rect(LocationHelper.Offset.x - 125, 45, 250, 125), string.Format("DOTs"), MasterSkin.label);
            }
            else
            {
                GUI.Label(new Rect(LocationHelper.Offset.x - 150, 55, 300, 160), string.Format("GAME OVER"), MasterSkin.label);
            }


            switch (GameStatus)
            {
                case GameStatus.Playing:
                    GUI.Label(new Rect(25, 200, 250, 75), string.Format("Level: {0}", Level), GameStatSkin.label);
                    GUI.Label(new Rect(LocationHelper.Offset2.x - 300, 200, 250, 75), string.Format("Score: {0}", Score), GameStatSkin.label);
                    break;

                case GameStatus.ChangeLevel:
                    GUI.Label(new Rect(LocationHelper.Offset.x - 125, 200, 250, 75), string.Format("Level Up!"), GameStatSkin.label);
                    break;

                case GameStatus.GameOver:
                    if (GUI.Button(new Rect(LocationHelper.Offset.x - 240, 220, 250, 50), "PLAY AGAIN", MasterSkin.button))
                    {
                        Application.LoadLevel(0);

                    }
                    if (GUI.Button(new Rect(LocationHelper.Offset.x + 20, 220, 250, 50), "LEADERBOARD", MasterSkin.button))
                    {
                        
                    }


                    GUI.Label(new Rect(LocationHelper.Offset.x - 125, 280, 250, 75), string.Format("Score: {0}", Score), GameStatSkin.label);

                    break;
            }

            GUI.matrix = Matrix4x4.identity;
        }
    }
}