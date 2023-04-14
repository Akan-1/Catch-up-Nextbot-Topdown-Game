using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NextbotAutoAddToPicker : MonoBehaviour
{
    [SerializeField] private NextbotPickerUI _itemPrefab;
    [SerializeField] private Transform _spawnTransform;
    
    void Start()
    {
        List<NextbotController> nextbotsList = Resources.LoadAll<NextbotController>("Nextbots").ToList();
        foreach (var nextbot in nextbotsList)
        {
            _itemPrefab.nextbotController = nextbot;
            
            Instantiate(_itemPrefab, _spawnTransform);
        }
    }
}
