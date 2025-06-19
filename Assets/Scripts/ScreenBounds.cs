using UnityEngine;

public class ScreenBounds : MonoBehaviour
{
    public static float left;
    public static float right;
    public static float top;
    public static float bottom;

    private void Awake()
    {
        Camera camera = Camera.main;

        Vector2 bottomLeft = camera.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 topRight = camera.ViewportToWorldPoint(new Vector2(1, 1));

        left = bottomLeft.x;
        right = topRight.x;
        bottom = bottomLeft.y;
        top = topRight.y;
    }
}
