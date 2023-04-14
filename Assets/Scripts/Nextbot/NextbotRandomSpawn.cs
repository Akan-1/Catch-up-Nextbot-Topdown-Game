using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NextbotRandomSpawn : MonoBehaviour
{
    private void Start()
    {
        List<NextbotController> nextbotsList = Resources.LoadAll<NextbotController>("Nextbots").ToList();

        Instantiate(nextbotsList[Random.Range(0, nextbotsList.Count)],
            GameObject.FindGameObjectWithTag("Spawnpoint").transform.position, Quaternion.identity);
    }
}
