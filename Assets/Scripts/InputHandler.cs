using UnityEngine;

namespace Assets.Scripts
{
    public class InputHandler : MonoBehaviour
    {
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
                    }
                }
            }
        }
    }
}