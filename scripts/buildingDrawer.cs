using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingDrawer : MonoBehaviour {
    [SerializeField] GameObject _wall;
    [SerializeField] GameObject _floor;
    [SerializeField] GameObject _player;
    GameObject _building;
    public void Draw(int[,] map,Vector3Int offset)
    {
        Init();
        int cols = map.GetLength(0);
        int rows = map.GetLength(1);
        for (int x = 0; x < cols; x++)
            for (int y = 0; y < rows; y++)
            {
                var spot = new Vector3Int(x, y,0)+offset;
                if (map[x, y] == 0)
                    PlacePrefab(_floor, spot);
                else
                    PlacePrefab(_wall, spot);
            }
        _player.transform.position = new Vector3(3, 3, - 2);
    }
    void PlacePrefab(GameObject prefab,Vector3 spot)
    {        
        var go = Instantiate(prefab);
        var pos = new Vector3(spot.x, spot.y, -go.transform.localScale.z);
        go.transform.position = pos;
        go.transform.SetParent(_building.transform);
    }
    private void Init()
    {
        if (_building != null)
            Destroy(_building);
        _building = new GameObject();
        _building.name = "Building";
    }

}
