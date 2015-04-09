using UnityEngine;

namespace Assets.Scripts
{
    public class GUILocationHelper
    {
        public enum Point
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight,
            Center,
            TopCenter
        }

        public Vector2 GuiOffset;
        public Vector2 Offset;
        public Vector2 Offset2;
        public float OriginalHeigth = 1280f;
        public float OriginalWidth = 800f;
        public Point PointLocation = Point.TopLeft;
        public Point PointLocation2 = Point.TopRight;

        public void UpdateLocation()
        {
            switch (PointLocation)
            {
                case Point.TopLeft:
                    Offset = new Vector2(0, 0);
                    break;
                case Point.TopRight:
                    Offset = new Vector2(OriginalWidth, 0);
                    break;
                case Point.BottomLeft:
                    Offset = new Vector2(0, OriginalHeigth);
                    break;
                case Point.BottomRight:
                    Offset = new Vector2(OriginalWidth, OriginalHeigth);
                    break;
                case Point.Center:
                    Offset = new Vector2(OriginalWidth/2f, OriginalHeigth/2f);
                    break;
                case Point.TopCenter:
                    Offset = new Vector2(OriginalWidth/2f, 0);
                    break;
            }

            switch (PointLocation2)
            {
                case Point.TopLeft:
                    Offset2 = new Vector2(0, 0);
                    break;
                case Point.TopRight:
                    Offset2 = new Vector2(OriginalWidth, 0);
                    break;
                case Point.BottomLeft:
                    Offset2 = new Vector2(0, OriginalHeigth);
                    break;
                case Point.BottomRight:
                    Offset2 = new Vector2(OriginalWidth, OriginalHeigth);
                    break;
                case Point.Center:
                    Offset2 = new Vector2(OriginalWidth / 2f, OriginalHeigth / 2f);
                    break;
                case Point.TopCenter:
                    Offset2 = new Vector2(OriginalWidth / 2f, 0);
                    break;
            }


            GuiOffset.x = Screen.width/OriginalWidth;
            GuiOffset.y = Screen.height/OriginalHeigth;
        }
    }
}