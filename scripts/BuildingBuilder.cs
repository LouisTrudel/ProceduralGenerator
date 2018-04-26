using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingBuilder : MonoBehaviour {
    [SerializeField] buildingDrawer _buildingDrawer;   
    List<Vector3Int> _intersections;
    List<Vector3Int> _roomCenters;
    List<Vector3Int> _doors;   
    int[,] _map;
    void Start() {
        Build(Vector3Int.zero, 5, 5, 4);
    }  
    void Update() {
        if (Input.GetKeyDown("p"))
            BuildRandom();
    }
    
    void Build(Vector3Int position, int width, int height, int roomSize)
    {
        //will build a int map of values : (1 = wall, 0 =floor) and then draw it!
        //try it without the removeWalls and/or MakeDoors functions to see it step by step.
        //this builder only works with even roomSizes.
        roomSize *= 2; 
        int cols = width * roomSize + 1;
        int rows = height * roomSize + 1;
        Init(cols,rows);      
        BuildFrame(position, cols, rows, roomSize);
        RemoveWalls(roomSize);
        MakeDoors(roomSize);
        _buildingDrawer.Draw(_map,position);
    }
    private void Init(int cols, int rows)
    {
        _roomCenters = new List<Vector3Int>();
        _intersections = new List<Vector3Int>();
        _map = new int[cols, rows];
    }
    void BuildFrame(Vector3Int position, int cols, int rows, int roomSize)
    {
        print("building fram for cols :" + cols + " rows : " + rows);
        for (int x = 0; x < cols; x++)
            for (int y = 0; y < rows; y++)
            {
                var pos = new Vector3Int(x, y, 0) + position;
                if (x == 0 || x == cols - 1 || y == 0 || y == rows - 1)//is a border wall
                    _map[pos.x, pos.y] = 1;

                else if (x % roomSize == 0 || y % roomSize == 0)// is a roomWall
                    setRoomWall(pos, roomSize, x, y);

                else 
                    setFloor(pos, roomSize, x, y);

            }
    }
    void setRoomWall(Vector3Int pos,int roomSize,int x, int y)
    {
        _map[pos.x, pos.y] = 1;
        if (x % roomSize == 0 && y % roomSize == 0)
            _intersections.Add(pos);
    }
    void setFloor(Vector3Int pos, int roomSize, int x, int y)
    {
        _map[pos.x, pos.y] = 0;
        if (x % (roomSize / 2) == 0 && y % (roomSize / 2) == 0)
            _roomCenters.Add(pos);
    }
    void RemoveWalls(int roomSize)
    {
        foreach (Vector3Int v in _intersections)
        {
            var dir = RandomDirection();
            for (int i = 1; i < roomSize; i++)
            {
                var pos = new Vector3Int(v.x + i * dir.x, v.y + i * dir.y, 0);
                _map[pos.x, pos.y] = 0;
            }
        }
    }
    void MakeDoors(int roomSize)
    {
        foreach (Vector3Int v in _roomCenters)
        {
            var closeBy = CloseByRoomCenter(roomSize, v);
            var dir = closeBy - v;            
            var spot = new Vector3Int(v.x + dir.x / 2, v.y + dir.y / 2, 0);
            _map[spot.x, spot.y] = 0;      
        }       
    }
    Vector3Int CloseByRoomCenter(int roomSize, Vector3Int startPoint)
    {
        var choices = new List<Vector3Int>();
        foreach(Vector3Int v in _roomCenters)
            if ((v - startPoint).magnitude == roomSize)
                choices.Add(v);
        int index = Random.Range(0, choices.Count);
        return choices[index];
    }              
    Vector3Int RandomDirection()
    {
        var dir = Vector3Int.zero;
        int rand = Random.Range(0, 100);
        bool horizontal = rand >= 50 ? true : false;

        if (horizontal)
            dir.x = rand >= 75 ? 1 : -1;
        else
            dir.y = rand >= 25 ? 1 : -1;
        return dir;
    }
    void BuildRandom()
    {
        int width = Random.Range(2, 6);
        int height = Random.Range(2, 6);
        int roomSize = Random.Range(1, 7);
        Build(Vector3Int.zero, width, height, roomSize);
    }
 
}
