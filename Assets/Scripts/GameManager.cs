using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform canvas;
    public Transform playerHouse;
    public Transform enemyPrefab;
    public List<Transform> enemies;
    public Transform spawnPlatform;
    public Transform player;
    public Text houseHealthText;
    public Text playerHealthText;

    public int lenOfWave;

    private bool isLose;
    private bool isWin;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        removeDiedEnemies();
        DrawToCanvas();

        if (!getEnd())
        {
            spawnEnemies();
        }
        else
        {
            SceneManager.LoadScene("Level1");
        }
    }

    bool getEnd()
    {
        Creature houseCreature = playerHouse.GetComponent<Creature>();
        if(houseCreature.health < 0)
        {
            isLose = true;
            return true;
        }

        if(enemies.Count == 0)
        {
            isWin = true;
            return true;
        }
        return false;
    }

    void spawnEnemies()
    {
        if(lenOfWave != enemies.Count)
        {
            Debug.Log("Try to spawn");
            for (int i = 0; i < lenOfWave - enemies.Count; i++)
            {
                Vector3 enemyPos = getRandomSpawnPos();
                if(checkForObstacles(enemyPos, enemyPrefab))
                {
                    Transform enemy = Instantiate(enemyPrefab, getRandomSpawnPos(), Quaternion.identity);
                    enemies.Add(enemy);
                    enemy.GetComponent<ZombieScript>().player = getRandomPurpose();
                }                
            }
        }        
    }

    Vector3 getRandomSpawnPos()
    {
        float scalex = spawnPlatform.localScale.x - (spawnPlatform.localScale.x / 100 * 20);
        float scalez = spawnPlatform.localScale.z - (spawnPlatform.localScale.y / 100 * 20);

        float x = Random.Range((-scalex / 2), (scalex / 2));
        float z = Random.Range((-scalez / 2), (scalez / 2));

        return new Vector3(x, 0, z);
    }

    void DrawToCanvas()
    {
        Creature houseCreature = playerHouse.GetComponent<Creature>();
        Creature playerCreature = player.GetComponent<Creature>();
        houseHealthText.text = houseCreature.health.ToString();
        playerHealthText.text = playerCreature.health.ToString();
    }

    Transform getRandomPurpose()
    {
        float rdn = Random.value;
        if (rdn > 0.7)
        {
            return player;
        }
        else
        {
            return playerHouse;
        }
    }

    bool checkForObstacles(Vector3 new_pos, Transform obj)
    {
        Collider[] obstales = new Collider[5];
        int numObstaces = Physics.OverlapSphereNonAlloc(new_pos, obj.localScale.x, obstales);        
        return numObstaces == 1;
    }

    void removeDiedEnemies()
    {
        List<Transform> filtered_enemis = new List<Transform>();
        foreach(Transform enemy in enemies)
        {
            if(enemy.GetComponent<Creature>().died)
            {
                Destroy(enemy.gameObject, 5);
            }
            else
            {
                filtered_enemis.Add(enemy);
            }
        }
        enemies = filtered_enemis;
    }
}
