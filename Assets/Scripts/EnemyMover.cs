using UnityEngine;

public class EnemyMover : MonoBehaviour
{

    private void Update()
    {
        if (GameManager.instance != null && GameManager.instance.isGameOver)
        {
            return;
        }

        float currentSpeed = GameManager.instance != null ? GameManager.instance.GetCurrentEnemySpeed() : 2f;
        transform.Translate(Vector3.down * currentSpeed * Time.deltaTime);

        if (transform.position.y < ScreenBounds.bottom - 1f)
        {
            Destroy(gameObject);
        }
    }
}