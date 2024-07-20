using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class List : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<int> numbers = new List<int>();
    void Start()
    {
        numbers.Add(82);
        numbers.Add(46);
        numbers.Add(-323);
        numbers.Add(2345);
        print(numbers[0]);
       // print(numbers[2345]);
       for (int i = 0; i <=5; i++)
        {
            print(i);       // 0, 1, 2,3,4,5
        }
    }

    // Update is called once per frame
    void Updatse()
    {
        
    }
}
