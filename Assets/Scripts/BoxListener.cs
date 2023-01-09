using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxListener : MonoBehaviour
{
    private GameObject _boxObjective;
    [SerializeField] private GameObject boxesParent;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] List<GameObject> eachbox;

    
    
    public GameObject BoxObjective
    {
        get => _boxObjective;
        set => _boxObjective = value;
    }

    public void CompareBoxes(GameObject box)
    {
        int a = _boxObjective.GetComponent<BoxStats>().getBoxNumber;
        int b = box.GetComponent<BoxStats>().getBoxNumber;
        if (a == b)
        {
            Destroy(_boxObjective);
            _boxObjective = null;
            Debug.Log("CORRECT");
            RespawnBoxes();
            SelectObjective();
        }
        else
        {
            Debug.Log("NOT CORRECT");

        }

        SpawnObjective();
    }

    private void RespawnBoxes()
    {
        Vector3 room = boxesParent.transform.position;
        int rex = 0;
        foreach (var box in eachbox)
        {
            box.transform.position = new Vector3(room.x, room.y, room.z + rex);
            rex += 1;
        }
    }

    public void SelectObjective()
    {
        int selected = Random.Range(0, eachbox.Count);
        _boxObjective = eachbox[selected];
        eachbox.Remove(_boxObjective);
        SpawnObjective();
    }

    private void SpawnObjective()
    {
        _boxObjective.transform.position = spawnPoint.transform.position;
    }
}
