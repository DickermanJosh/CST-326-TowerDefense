using System.Runtime.CompilerServices;
using UnityEngine;
using System.Collections;
using TMPro;
public class WaveSpawner : MonoBehaviour
{
    public GameManager manager;
    public static int enemiesAlive = 0;
    public TextMeshProUGUI waveCountdownText;
    public Wave[] waves;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    private int waveIndex  = 0;
    void Update()
    {
        if (enemiesAlive > 0)
        {
            return;
        }
        
        if (waveIndex == waves.Length)
        {
            manager.WinLevel();
            // disable script
            enabled = false;
        }
        
        if (countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }
    
        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountdownText.text = string.Format("{0:00.00}",countdown);
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;
        Wave wave = waves[waveIndex];
        enemiesAlive = wave.count;
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveIndex++;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
