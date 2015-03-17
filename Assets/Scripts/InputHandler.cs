using UnityEngine;

namespace Assets.Scripts
{
    public class InputHandler : MonoBehaviour
    {
        public int Counter = 1;
        public void Update()
        {
            if (Input.touchCount == 0)
            {
                Counter = 1;
            }
            if (Input.touchCount > 0)
            {
                
                var worldRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                RaycastHit rcHit;

                if (Physics.Raycast(worldRay, out rcHit))
                {
                    if (rcHit.collider.gameObject)
                    {
                        var squareScript = rcHit.collider.gameObject.GetComponent<SquareScript>();
                        squareScript.Colorize();

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
                        var squareScript = rcHit.collider.gameObject.GetComponent<SquareScript>();
                        squareScript.Colorize();
                    }
                }
            }
        }
    }
}