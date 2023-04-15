using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskItemGenerate : MonoBehaviour
{
    [SerializeField] private TaskItem _item;
    private List<Transform> _points;

    public void Generate(int count)
    {
        _points = GetComponentsInChildren<Transform>().ToList();
        _points.RemoveAt(0);
        for (int i = 0; i < count; i++)
        {
            int randomPoint = Random.Range(0, _points.Count);
            Instantiate(_item, _points[randomPoint].position, Quaternion.identity);
            _points.RemoveAt(randomPoint);
        }
    }

}
