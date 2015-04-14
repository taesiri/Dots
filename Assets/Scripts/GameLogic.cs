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
        public static int PlayCount;
        private Matrix4x4 _guiMatrix;
        private float _levelStartTime;
        private int _numberOfDots;
        private DotScript[] _patternDots;
        public int _remainingDots = -1;
        public int adCounter = 5;
        public Sprite BlueDot;
        public AnimationCurve CameraCurve;
        public float FlashTime = 0.75f;
        public GUISkin GameStatSkin;
        public GUISkin TextSkin;
        public GameStatus GameStatus;
        public Sprite GreenDot;
        public int Height;
        public int Level = 1;
        public GUISkin MasterSkin;
        public Sprite OrangeDot;
        public Sprite PurpleDot;
        public Texture2D QuitTexture2D;
        public Sprite RedDot;
        public int Score;
        public int Width;

        public int highscore;

        public void Awake()
        {
            Instance = this;
        }

        public void Start()
        {
            PlayCount++;

            highscore = PlayerPrefs.GetInt("PlayerHighScore");

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
            if (count > 40)
            {
                count = 40;
                _remainingDots = 40;
            }

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


            StartCoroutine(ShowHint());
        }

        private IEnumerator ShowHint()
        {
            GameStatus = GameStatus.DisplayHint;

            for (var i = 0; i < _patternDots.Length; i++)
            {
                _patternDots[i].DisplayHint();
            }

            yield return new WaitForSeconds(0.75f);

            for (var i = 0; i < _patternDots.Length; i++)
            {
                _patternDots[i].ResetColor();
            }


            GameStatus = GameStatus.Playing;
        }

        public IEnumerator InitialSetup()
        {
            yield return new WaitForSeconds(0.75f);
            ////////////////////////////// Level Setup

            Level = 1;
            _remainingDots = 1;
            PickRandomDots(_remainingDots);

            ////////////////////////RequestBanner();
            ////////////////////////RequestInterstitial();
        }

        public void RestartGame()
        {
            PlayCount++;
            _levelStartTime = Time.time;
            Score = 0;

            GameStatus = GameStatus.StartScreen;
            Camera.main.orthographicSize = 0.1f;


            for (var i = 0; i < GridBuilder.Instance.Dots.Length; i++)
            {
                GridBuilder.Instance.Dots[i].Reset();
            }

            adCounter--;

            if (adCounter <= 0)
            {
                // Show Advertisement
            }
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


                            if (!dotScript.Detected)
                            {
                                if (!dotScript.InPattern)
                                {
                                    DoGameOver();
                                    dotScript.GoRed();
                                }
                                else
                                {
                                    dotScript.Detected = true;

                                    ScoreUp();
                                }
                            }
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
                                    DoGameOver();
                                    dotScript.GoRed();
                                }
                                else
                                {
                                    dotScript.Detected = true;

                                    ScoreUp();
                                }
                            }
                        }
                    }
                }
            }
        }

        public void DoGameOver()
        {
            GameStatus = GameStatus.GameOver;

            for (var i = 0; i < _patternDots.Length; i++)
            {
                if (!_patternDots[i].Detected)
                {
                    _patternDots[i].ExposeCell();
                }
            }

            SubmitScoreToServer();

            if (PlayCount%5 == 0)
            {
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
            StartCoroutine(LevelingUp(1.1f));
        }

        private IEnumerator LevelingUp(float waitTime)
        {
            GameStatus = GameStatus.ChangeLevel;
            yield return new WaitForSeconds(waitTime);
            ResetGrid();

            _remainingDots = 2*Level;

            Level++;

            //CheckAchievement();

            PickRandomDots(_remainingDots);
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
                if (GUI.Button(new Rect(15, 15, 48, 48), QuitTexture2D, GUIStyle.none))
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
                    GUI.Label(new Rect(LocationHelper.Offset.x - 300, 290, 600, 75), "Try memorizing the position of Purple Dots", TextSkin.label);
                    break;

                case GameStatus.ChangeLevel:
                    GUI.Label(new Rect(LocationHelper.Offset.x - 125, 200, 250, 75), string.Format("Level Up!"), GameStatSkin.label);
                    break;

                case GameStatus.GameOver:
                    if (GUI.Button(new Rect(LocationHelper.Offset.x - 240, 220, 250, 50), "PLAY AGAIN", MasterSkin.button))
                    {
                        RestartGame();
                    }
                    if (GUI.Button(new Rect(LocationHelper.Offset.x + 20, 220, 250, 50), "LEADERBOARD", MasterSkin.button))
                    {
                        CheckUser();
                    }


                    GUI.Label(new Rect(LocationHelper.Offset.x - 125, 310, 250, 75), string.Format("Score: {0}", Score), GameStatSkin.label);

                    break;

                case GameStatus.DisplayHint:
                    
                    break;
            }

            GUI.matrix = Matrix4x4.identity;
        }

        private void CheckUser()
        {

        }

        private void SubmitScoreToServer()
        {
            CheckUser();

            if (Score > highscore)
            {
                PlayerPrefs.SetInt("PlayerHighScore", Score);
                highscore = Score;
            }

            //SendAchievement();
        }
    }
}