﻿using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameLogic : MonoBehaviour
    {
        public GameObject DotPrefab;
        public SquareScript[,] Dots;
        public int Height = 10;
        public float HindInterval = .25f;
        public float OffsetX = 0;
        public float OffsetZ = 0;
        public PatternQueue PQ;
        public int Width = 10;

        public void Start()
        {
            InitializeGrid(transform);
        }

        public void InitializeGrid(Transform parentTransform)
        {
            Dots = new SquareScript[Width, Height];


            if (DotPrefab == null)
            {
                Debug.LogError("Dot prefab not found");
            }
            else
            {
                for (var i = 0; i < Width; i++)
                {
                    for (var j = 0; j < Height; j++)
                    {
                        var newDot = (GameObject) Instantiate(DotPrefab, new Vector3(OffsetX + i*1.2f, OffsetZ + j*1.2f, 0), Quaternion.identity);
                        newDot.transform.parent = parentTransform;
                        newDot.name = string.Format("Dot ({0},{1})", i, j);
                        var dotScript = newDot.GetComponent<SquareScript>();


                        Dots[i, j] = dotScript;
                    }
                }
            }

            PQ = new PatternQueue(10) {EmphasizeColor = Color.green};

            PQ.Enqueue(Dots[1, 1]);
            PQ.Enqueue(Dots[2, 2]);
            PQ.Enqueue(Dots[3, 3]);
            PQ.Enqueue(Dots[4, 4]);
            PQ.Enqueue(Dots[5, 5]);
            PQ.Enqueue(Dots[6, 6]);
            PQ.Enqueue(Dots[6, 5]);
            PQ.Enqueue(Dots[6, 4]);


            ShowHint();
        }

        public void ShowHint()
        {
            StartCoroutine(ColorHint());
        }

        private IEnumerator ColorHint()
        {
            yield return new WaitForSeconds(HindInterval);
            while (PQ.Count > 0)
            {
                var item = PQ.Dequeue();
                item.FadeInSpeed = 2;
                item.FadeOutSpeed = 6;
                item.Colorize();

                yield return new WaitForSeconds(HindInterval);
            }
        }

       
    }
}