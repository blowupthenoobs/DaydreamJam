using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance;
    [SerializeField] TMP_Text scoreTracker;
    public LayerMask playerLayer;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] GameObject bloodDrop;
    [SerializeField] GameObject player;
    [SerializeField] PlayerID playerID;

    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    [SerializeField] List<GameObject> entitySpawns = new List<GameObject>();
    [SerializeField] float currentCooldown;
    [SerializeField] float cooldownTime;
    [SerializeField] float spawnArea;

    [SerializeField] GameObject normalUI;
    [SerializeField] GameObject deathMenu;
    [SerializeField] TMP_Text deathScore;

    public float score;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        EnemyScript.playerLayer = playerLayer;
        EnemyScript.wallLayer = wallLayer;
        EnemyScript.bloodDrop = bloodDrop;
        EnemyScript.target = player;
    }

    void Update()
    {
        SpawnEnemys();
        UpdateScore(Time.deltaTime * .5f);
    }

    private void SpawnEnemys()
    {
        if (currentCooldown >= cooldownTime)
        {
            int spawns = Random.Range(0, entitySpawns.Count);

            List<GameObject> tempSpawnList = new List<GameObject>(entitySpawns);
            for (int i = 0; i < spawns; i++)
            {
                int index = Random.Range(1, tempSpawnList.Count);
                Instantiate(enemies[Random.Range(0, enemies.Count)], GetRandomPositionWithinRadius(tempSpawnList[index].transform.position, spawnArea), transform.rotation);
                tempSpawnList.RemoveAt(index);
            }
            currentCooldown = 0;
        }
        else
            currentCooldown += Time.deltaTime;
    }

    private Vector2 GetRandomPositionWithinRadius(Vector2 center, float radius)
    {
        // Generate a random angle between 0 and 360 degrees
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

        // Generate a random distance within the radius
        float distance = Random.Range(0f, radius);

        // Calculate the x and y offset using the angle and distance
        float x = Mathf.Cos(angle) * distance;
        float y = Mathf.Sin(angle) * distance;

        Vector2 randomPosition = new Vector2(center.x + x, center.y + y);

        while (Physics2D.Raycast(center, (randomPosition - center).normalized, distance, wallLayer))
        {
            angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

            distance = Random.Range(0f, radius);

            x = Mathf.Cos(angle) * distance;
            y = Mathf.Sin(angle) * distance;

            // Calculate the final position by adding the offset to the center position
            randomPosition = new Vector2(center.x + x, center.y + y);
        }


        return randomPosition;
    }

    public void UpdateScore(float scoreIncrease)
    {
        score += scoreIncrease;

        scoreTracker.text = ((int)score).ToString();
        deathScore.text = ((int)score).ToString();
    }

    public void PlayerDeath()
    {
        deathMenu.SetActive(true);
        normalUI.SetActive(false);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        ClearActions();
        SceneManager.LoadScene("SampleScene");
    }

    private void ClearActions()
    {
        playerID.Attack = null;
        playerID.Dash = null;
        playerID.Shoot = null;
    }
}
