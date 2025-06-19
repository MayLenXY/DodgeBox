using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    private Coroutine spawnCoroutine;

    private const int MAX_CONSECUTIVE_SPAWNS = 3;
    private int consecutiveLeftSpawns = 0;
    private int consecutiveRightSpawns = 0;

    private void Start()
    {
        if (GameManager.instance != null)
        {
            spawnCoroutine = StartCoroutine(SpawnEnemyCoroutine());
        }
    }

    IEnumerator SpawnEnemyCoroutine()
    {
        yield return new WaitForSeconds(1f);

        while (!GameManager.instance.isGameOver)
        {
            SpawnEnemyControlled();
            yield return new WaitForSeconds(GameManager.instance.GetCurrentSpawnInterval());
        }
    }

    void SpawnEnemyControlled()
    {
        if (GameManager.instance == null || GameManager.instance.isGameOver)
        {
            return;
        }

        float spawnX;
        bool spawnOnLeft;

        if (consecutiveLeftSpawns >= MAX_CONSECUTIVE_SPAWNS)
        {
            spawnOnLeft = false;
        }

        else if (consecutiveRightSpawns >= MAX_CONSECUTIVE_SPAWNS)
        {
            spawnOnLeft = true;
        }

        else
        {
            spawnOnLeft = Random.value < 0.5f;
        }

        if (spawnOnLeft)
        {
            spawnX = ScreenBounds.left + 0.5f;
            consecutiveLeftSpawns++;
            consecutiveRightSpawns = 0;
        }
        else
        {
            spawnX = ScreenBounds.right - 0.5f;
            consecutiveRightSpawns++;
            consecutiveLeftSpawns = 0;
        }

        float spawnY = ScreenBounds.top + 1f;

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
        CancelInvoke();
    }
}