using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
[SerializeField] List<GameObject> locations = new List<GameObject>();

    [SerializeField] Transform playerTransform;     // char konumu
    [SerializeField] GameObject shield;

    float levelLength = 106.3f; // zemin uzunluğu, verilmiş değer

    int count=5;
    void Start()
    {
        Instantiate(locations[0], transform.forward, transform.rotation);

        for(int i=0; i<count;i++)
        {
            CreateLocation();
        }

        GenerateObject();
    }

    void Update()
    {
        if (playerTransform.position.z > levelLength - 106.3f*count)
        {
            CreateLocation();
        }
    }

    private void CreateLocation()
    {
        Instantiate(locations[Random.Range(0, locations.Count)], transform.forward * levelLength, transform.rotation);

        levelLength += 106.3f;  // levelLength = levelLength + 106.3f;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void GenerateObject()
    {
        float distance = Random.Range(100, 200);
        //Instantiate(shield, playerTransform.position + new Vector3(0, 3, distance), transform.rotation);
        Instantiate(shield, playerTransform.position + new Vector3(0, 3, distance), Quaternion.Euler(0, 180, 0));
        
        Invoke("GenerateObject",Random.Range(10,20));

    }
}
