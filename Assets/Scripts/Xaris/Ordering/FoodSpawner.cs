using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;

    public List<GameObject> cupcakeObjects = new List<GameObject>();
    public List<GameObject> sandwichObjects = new List<GameObject>();

    public float preparationTime;

    private Dictionary<string, List<GameObject>> poolOfObjects = new Dictionary<string, List<GameObject>>();


    private void Start()
    {
        poolOfObjects.Add("Cupcake", cupcakeObjects);
        poolOfObjects.Add("Sandwich", sandwichObjects);
    }

    public void SpawnAFood(string itemName)
    {
        GameObject foodToSpawn = Instantiate(ItemToSpawn(itemName), spawnPoint.position, Quaternion.identity);

        foodToSpawn.name = itemName;
        
    }

    private GameObject ItemToSpawn(string itemName)
    {
        GameObject objectToSpawn = null;

        if (poolOfObjects.ContainsKey(itemName))
        {
            objectToSpawn = poolOfObjects[itemName][Random.Range(0, poolOfObjects[itemName].Count)];
        }
        else
        {
            Debug.Log("ItemName "+itemName+ " does not exist");
        }

        return objectToSpawn;
    }

    public IEnumerator SpawnFoodWithTimer(string itemName)
    {
        yield return new WaitForSeconds(preparationTime);
        SpawnAFood(itemName);
    } 

    public void SpawnFoodWithTimerButton(string itemName)
    {
        StartCoroutine(SpawnFoodWithTimer(itemName));
    }

        
}
