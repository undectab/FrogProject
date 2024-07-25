using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public enum Spawn1 { None, Frog, Fruit, FruitMaster, Arrow };
    public enum Spawn1Property { None, Blue, Green, Red, Yellow };
    public enum Spawn1RotationY { Left, Right, Up, Down };
    public enum Spawn2 { None, Frog, Fruit, FruitMaster, Arrow };
    public enum Spawn2Property { None, Blue, Green, Red, Yellow };
    public enum Spawn2RotationY { Left, Right, Up, Down };
    public enum Spawn3 { None, Frog, Fruit, FruitMaster, Arrow };
    public enum Spawn3Property { None, Blue, Green, Red, Yellow };
    public enum Spawn3RotationY { Left, Right, Up, Down };
    [SerializeField] private int columnIndex;
    [SerializeField] private int rowIndex;
    public Spawn1 spawn1;
    public Spawn1Property spawn1Property;
    public Spawn1RotationY spawn1RotationY;
    public Spawn2 spawn2;
    public Spawn2Property spawn2Property;
    public Spawn2RotationY spawn2RotationY;
    public Spawn3 spawn3;
    public Spawn3Property spawn3Property;
    public Spawn3RotationY spawn3RotationY;
    [SerializeField] private GameObject frogPrefab, fruitPrefab, arrowPrefab, cellPrefab;
    private Transform spawnerTransform;
    [SerializeField] private string frogColor;
    public int spawn1Rotation, spawn2Rotation, spawn3Rotation;
    public int SpawnIndex;
    private GameObject cellObj1, cellObj2, cellObj3;
    private UIController _UIController;
    


    private void Awake()
    {
        _UIController = GameObject.Find("Manager").GetComponent<UIController>();
        spawnerTransform = transform;
        spawn1Rotation = Spawn1RotationCalc();
        spawn2Rotation = Spawn2RotationCalc();
        spawn3Rotation = Spawn3RotationCalc();
        SpawnerIndexCalc();
        name = "Spawner " + columnIndex.ToString() + "x" + rowIndex.ToString();
    }
    public int Spawn1RotationCalc()
    {
        switch (spawn1RotationY)
        {
            case Spawn1RotationY.Up:
                return 180;
            case Spawn1RotationY.Down:
                return 0;
            case Spawn1RotationY.Left:
                return 90;
            case Spawn1RotationY.Right:
                return 270;
            default:
                return 0;
        }

    }
    public int Spawn2RotationCalc()
    {
        switch (spawn2RotationY)
        {
            case Spawn2RotationY.Up:
                return 180;
            case Spawn2RotationY.Down:
                return 0;
            case Spawn2RotationY.Left:
                return 90;
            case Spawn2RotationY.Right:
                return 270;
            default:
                return 0;
        }

    }
    public int Spawn3RotationCalc()
    {
        switch (spawn3RotationY)
        {
            case Spawn3RotationY.Up:
                return 180;
            case Spawn3RotationY.Down:
                return 0;
            case Spawn3RotationY.Left:
                return 90;
            case Spawn3RotationY.Right:
                return 270;
            default:
                return 0;
        }

    }
    public void SpawnerIndexCalc()
    {
        if (spawn1 != Spawn1.None)
        {
            SpawnIndex++;
        }
        if (spawn2 != Spawn2.None)
        {
            SpawnIndex++;
        }
        if (spawn3 != Spawn3.None)
        {
            SpawnIndex++;
        }
        if (SpawnIndex == 0)
            CellSpawner(1);
        for (int i = 1; i <= SpawnIndex; i++)
        {
            CellSpawner(i);
            print(i);
        }
        if (spawn1 == Spawn1.Frog)
            _UIController.FrogCountController("add");
        if (spawn2 == Spawn2.Frog)
            _UIController.FrogCountController("add");
        if (spawn3 == Spawn3.Frog)
            _UIController.FrogCountController("add");
        FirstRun();
    }
    void FirstRun()
    {
        switch (SpawnIndex)
        {
            case 1:
                Spawn(spawn1.ToString(), spawn1Property.ToString(), spawn1Rotation, SpawnIndex, cellObj1);
                break;
            case 2:
                Spawn(spawn2.ToString(), spawn2Property.ToString(), spawn2Rotation, SpawnIndex, cellObj2);
                break;
            case 3:
                Spawn(spawn3.ToString(), spawn3Property.ToString(), spawn3Rotation, SpawnIndex, cellObj3);
                break;
        }
    }
    public void SubtractFrog()
    {
        _UIController.FrogCountController("subtract");
    }
    public void SpawnStarter()
    {
        switch (SpawnIndex)
        {
            case 1:
                Spawn(spawn1.ToString(), spawn1Property.ToString(), spawn1Rotation, SpawnIndex, cellObj1);
                break;
            case 2:
                Spawn(spawn2.ToString(), spawn2Property.ToString(), spawn2Rotation, SpawnIndex, cellObj2);
                break;
            case 3:
                Spawn(spawn3.ToString(), spawn3Property.ToString(), spawn3Rotation, SpawnIndex, cellObj3);
                break;
        }
        
        
        
    }
    void CellSpawner(int _index)
    {
        if (_index == 1)
        {
            cellObj1 = Instantiate(cellPrefab, new Vector3(spawnerTransform.position.x, 0.46f + (1 * 0.09f), spawnerTransform.position.z), Quaternion.identity);
            cellObj1.GetComponent<Cell>().cellColorName = spawn1Property.ToString();
            cellObj1.GetComponent<Cell>().spawnerGameObject = this.gameObject;
            cellObj1.GetComponent<Cell>().cellIndex = 1;
            cellObj1.transform.SetParent(transform);
        }
        if (_index == 2)
        {
            cellObj2 = Instantiate(cellPrefab, new Vector3(spawnerTransform.position.x, 0.46f + (2 * 0.09f), spawnerTransform.position.z), Quaternion.identity);
            cellObj2.GetComponent<Cell>().cellColorName = spawn2Property.ToString();
            cellObj2.GetComponent<Cell>().spawnerGameObject = this.gameObject;
            cellObj2.GetComponent<Cell>().cellIndex = 2;
            cellObj2.transform.SetParent(transform);
        }
        if (_index == 3)
        {
            cellObj3 = Instantiate(cellPrefab, new Vector3(spawnerTransform.position.x, 0.46f + (3 * 0.09f), spawnerTransform.position.z), Quaternion.identity);
            cellObj3.GetComponent<Cell>().cellColorName = spawn3Property.ToString();
            cellObj3.GetComponent<Cell>().spawnerGameObject = this.gameObject;
            cellObj3.GetComponent<Cell>().cellIndex = 3;
            cellObj3.transform.SetParent(transform);
        }
    }
    void Spawn(string _objName, string _colorName, float _rotationY, int _spawnIndex, GameObject _cell)
    {
        switch (_objName)
        {
            case "Frog":
                GameObject frog = Instantiate(frogPrefab, new Vector3(spawnerTransform.position.x, 0.59f + (_spawnIndex * 0.09f), spawnerTransform.position.z), frogPrefab.transform.rotation);
                frog.transform.GetChild(0).GetComponent<FrogTongue>().frogColorName = _colorName;
                frog.transform.rotation = Quaternion.Euler(0, _rotationY, 0);
                frog.transform.GetChild(1).GetComponent<Frogs>().cellGameObject = _cell;
                frog.transform.GetChild(1).GetComponent<Frogs>().frogColorName = _colorName;
                frog.transform.SetParent(transform);
                break;
            case "FruitMaster":
            case "Fruit":
                GameObject fruit = Instantiate(fruitPrefab, new Vector3(spawnerTransform.position.x, 0.74f + (_spawnIndex * 0.09f), spawnerTransform.position.z), fruitPrefab.transform.rotation);
                fruit.transform.SetParent(transform);
                fruit.GetComponent<Fruit>().fruitColorName = _colorName;
                fruit.GetComponent<Fruit>().cellGameObject = _cell;

                if (_objName == "FruitMaster")
                    fruit.GetComponent<Fruit>().furitRole = "Master";
                break;
            case "Arrow":
                GameObject arrow = Instantiate(arrowPrefab, new Vector3(spawnerTransform.position.x, 0.5f + (_spawnIndex * 0.09f), spawnerTransform.position.z), arrowPrefab.transform.rotation);
                arrow.transform.SetParent(transform);
                arrow.transform.rotation = Quaternion.Euler(90, _rotationY, 0);
                arrow.GetComponent<Arrow>().arrowColor = _colorName;
                arrow.GetComponent<Arrow>().cellGameObject = _cell;
                break;
        }

    }
}
