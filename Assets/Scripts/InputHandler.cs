using UnityEngine;

namespace Assets.Scripts
{
    public class InputHandler : MonoBehaviour
    {
        public void Update()
        {
            if (GameLogic.Instance.IsReady)
            {

                if (Input.touchCount > 0)
                {
                    var worldRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                    RaycastHit rcHit;

                    if (Physics.Raycast(worldRay, out rcHit))
                    {
                        if (rcHit.collider.gameObject)
                        {
                            var squareScript = rcHit.collider.gameObject.GetComponent<SquareScript>();
                            squareScript.GotHit();
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
                            squareScript.GotHit();
                        }
                    }
                }
            }
        }
    }
}