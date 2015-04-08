using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameLogic : MonoBehaviour
    {
        public static GameLogic Instance;
        public bool IsRecording;
        public Queue<Vector2> PatternQ;

        public void Awake()
        {
            Instance = this;
        }

        public void Start()
        {
            PatternQ = new Queue<Vector2>();
        }

        public void Update()
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


            if (IsRecording)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    var worldRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                    RaycastHit rcHit;

                    if (Physics.Raycast(worldRay, out rcHit))
                    {
                        if (rcHit.collider.gameObject)
                        {
                            var dotScript = rcHit.collider.gameObject.GetComponent<DotScript>();
                            dotScript.Colorize();
                            if (!dotScript.InPattern)
                            {
                                dotScript.InPattern = true;
                                PatternQ.Enqueue(dotScript.GridIndex);
                            }
                        }
                    }
                }
            }
        }

        public void OnGUI()
        {
            if (!IsRecording)
            {
                if (GUI.Button(new Rect(10, 10, 250, 125), "Create Pattern"))
                {
                    IsRecording = true;
                }

             
            }
            else
            {
                if (GUI.Button(new Rect(10, 10, 250, 125), "Stop Recording"))
                {
                    IsRecording = false;
                }
            }
        }
    }
}