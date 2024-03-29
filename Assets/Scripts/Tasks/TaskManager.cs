using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = System.Random;
using UnityRandom = UnityEngine.Random;

public enum Tasks
{
    KeyCollect = 0,
    Activate = 1
}

public class TaskManager : MonoBehaviour
{
    [SerializeField, Range(0, 25)] private int _maxCount = 5;
    [SerializeField, Range(0, 25)] private int _maxCountObligatory = 5;

    private int _count;
    private int _taskID;
    private Tasks _task;

    [SerializeField] private List<TaskItemGenerate> _taskGameObjects;
    [SerializeField] private List<TaskItemGenerate> _obligatoryObjects;

    [Header("UI")]
    [SerializeField] private List<string> _textList; // Пример: Собери,ключей
    [SerializeField] private TMP_Text _taskText;
    [SerializeField] private List<Sprite> _iconList;
    [SerializeField] private Image _icon;

    [SerializeField] private List<UnityEvent> _onFinish;

    public void SetTask()
    {
        _count = UnityRandom.Range(4, _maxCount);
        Array values = Enum.GetValues(typeof(Tasks));
        Random random = new Random();
        _task = (Tasks)values.GetValue(random.Next(values.Length));
        _taskID = _task.GetHashCode();
        
        _icon.sprite = _iconList[_taskID];
        _icon.SetNativeSize();
        
        string[] text = _textList[_taskID].Split(",");
        _taskText.text = $"{text[0]} {_count} {text[1]}";
        
        _taskGameObjects[_taskID].Generate(_count);
    }

	public void SpawnObligatoryObjects()
	{
        var count = UnityRandom.Range(2, _maxCountObligatory);
	    _obligatoryObjects[UnityRandom.Range(0, _obligatoryObjects.Count)].Generate(count);
	}

	private void ChangeUI()
    {
        string[] text = _textList[_taskID].Split(",");
        _taskText.text = $"{text[0]} {_count} {text[1]}";
    }

    public void Use()
    {
        _count--;
        ChangeUI();
        if (_count == 0)
        {
            _onFinish[_taskID]?.Invoke();
        }
    }
}
