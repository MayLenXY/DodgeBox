using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float targetX;
    private bool moving = false;
    private Vector2 touchStart;
    public float minSwipe = 50f;

    private void Start()
    {
        var col = GetComponent<Collider2D>();
        float offsetY = col ? col.bounds.extents.y : 0;
        transform.position = new Vector2(0f, ScreenBounds.bottom + offsetY);
        targetX = transform.position.x;
    }


    private void Update()
    {
        if (GameManager.instance?.isGameOver == true) return;

        // Клавиатура
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) SetTarget(true);
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) SetTarget(false);

        // Свайп
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) touchStart = touch.position;
            else if (touch.phase == TouchPhase.Ended)
            {
                float deltaX = touch.position.x - touchStart.x;
                if (Mathf.Abs(deltaX) > minSwipe)
                    SetTarget(deltaX < 0);
            }
        }

        // Движение
        if (moving)
        {
            float speed = GameManager.instance?.GetCurrentPlayerSpeed() ?? 15f;
            float newX = Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime);
            transform.position = new Vector2(newX, transform.position.y);
            if (Mathf.Abs(transform.position.x - targetX) < 0.01f) moving = false;
        }
    }

    private void SetTarget(bool left)
    {
        var col = GetComponent<Collider2D>();
        float offsetX = col ? col.bounds.extents.x : 0;
        targetX = left ? ScreenBounds.left + offsetX : ScreenBounds.right - offsetX;
        moving = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            GameManager.instance.GameOver();
    }
}
