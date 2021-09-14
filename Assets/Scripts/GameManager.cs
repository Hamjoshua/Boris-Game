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
    public Transform treePrefab;

    public List<Transform> enemies;
    public List<Transform> trees;

    public Transform spawnPlatform;
    public Transform player;

    public int treeQuantity;

    public Text houseHealthText;
    public Text playerHealthText;

    public int lenOfWave;
    private int spawnedEnemies;

    [SerializeField] private bool isLose;
    private (float, float)[][] enemySpawnAreas;
    private object[] treesSpawnAreas;
    private (float, float)[] currentDirectionOfEnemySpawn;

    private bool waveIsOver;
    
    void Start()
    {        
        float middle = spawnPlatform.localScale.x / 2;
        float indent = middle - (middle / 100 * 10);
        Debug.Log(indent);
        (float, float)[] zapad = new (float, float)[] { (-50f, -indent), (50f, -50f) };
        (float, float)[] sever = new (float, float)[] { (-50f, 50f), (50f, indent) };
        (float, float)[] vostok = new (float, float)[] { (50f, indent), (50f, -50f) };
        (float, float)[] ug = new (float, float)[] { (-50f, 50f), (-50f, -indent) };
        enemySpawnAreas = new (float, float)[][] { zapad, sever, vostok, ug };
    }

    void Update()
    {
        removeDiedEnemies();
        removeDiedTrees();
        DrawToCanvas();

        if (isLose)
        {
            SceneManager.LoadScene("Level1");            
        }
        else
        {
            ManageWave();            
            trees = spawnPrefabs(treePrefab, 5, trees);
        }

        getEnd();
    }

    void getEnd()
    {
        Creature houseCreature = playerHouse.GetComponent<Creature>();
        if(houseCreature.died)
        {
            isLose = true;            
        }        
    }

    void ManageWave()
    {
        if(enemies.Count == 0 && !waveIsOver)
        {
            spawnedEnemies = 0;
            waveIsOver = true;
            lenOfWave += 1;
            currentDirectionOfEnemySpawn = enemySpawnAreas[Random.Range(0, enemySpawnAreas.Length)];            
        }
        else
        {
            waveIsOver = false;
        }

        if(spawnedEnemies < lenOfWave)
        {
            spawnEnemy();
        }
    }

    List<Transform> spawnPrefabs(Transform prefab, int limitOfList, List<Transform> prefabList)
    {        
        if(limitOfList > prefabList.Count)
        {            
            for (int i = 0; i < limitOfList - prefabList.Count; i++)
            {
                Vector3 randPos = getRandomSpawnPos(prefab);
                if(checkForObstacles(randPos, prefab))
                {
                    Transform newObject = Instantiate(prefab, randPos, Quaternion.identity);
                    prefabList.Add(newObject);
                    if(prefab == enemyPrefab)
                    {
                        newObject.GetComponent<ZombieScript>().player = getRandomPurpose();
                        spawnedEnemies++;
                    }                    
                }
                else
                {
                    Debug.Log("Try to spawn");                    
                }
            }
        }
        return prefabList;
    }
    
    void spawnEnemy()
    {
        enemies = spawnPrefabs(enemyPrefab, lenOfWave, enemies);
    }


    Vector3 getRandomSpawnPos(Transform prefab)
    {
        float x, z;
        float y = (spawnPlatform.localScale.y / 2) + spawnPlatform.position.y;
        if (prefab == enemyPrefab)
        {
            (float, float) randomRangeX = currentDirectionOfEnemySpawn[0];
            (float, float) randomRangeZ = currentDirectionOfEnemySpawn[1];
            x = Random.Range(randomRangeX.Item1, randomRangeX.Item2);
            z = Random.Range(randomRangeZ.Item1, randomRangeZ.Item2);
        }
        else 
        {
            float scalex = spawnPlatform.localScale.x - (spawnPlatform.localScale.x / 100 * 20);
            float scalez = spawnPlatform.localScale.z - (spawnPlatform.localScale.y / 100 * 20);

            x = Random.Range((-scalex / 2), (scalex / 2));
            z = Random.Range((-scalez / 2), (scalez / 2));
        }        
        
        return new Vector3(x, y, z);
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
    void removeDiedTrees()
    {
        List<Transform> filtered_trees = new List<Transform>();
        foreach (Transform tree in trees)
        {
            if (tree.GetComponent<Creature>().died)
            {
                treeQuantity++;
                Destroy(tree.gameObject);
            }
            else
            {
                filtered_trees.Add(tree);
            }
        }
        trees = filtered_trees;
    }
}
