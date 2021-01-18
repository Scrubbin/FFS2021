using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRoom

{
    public int[] size;

    public Vector3Int position;

    public List<GameObject> RoomComponents;
    
    public ShipRoom(int[] size, Vector3Int position)
    {
        this.size = size;
        this.position = position;  
    }

    void AddShipComponent(GameObject component, Vector3Int position)
    {
        
    }
}
