using Ships;
using UnityEngine;
using UnityEngine.Tilemaps;


public class ShipBuilder : MonoBehaviour
{
    public Tile shipTile;
    // Start is called before the first frame update
    void Start()
    {
        //build a player ship by default
        BuildShip(true);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void BuildShip(bool playerShip = false)
    {
        //build new ship game object
        GameObject Ship = new GameObject();
        if (playerShip)
        {
            Ship.name = "PlayerShip";  
        }
        else
        {
            Ship.name = "EnemyShip";
        }
        
        //set to ship layer
        Ship.layer = 8;
        //this makes a grid component for the ship and changes the value of cell size
        Ship.AddComponent<Grid>().cellSize = new Vector3(1, 1, 0);
        Ship.AddComponent<ShipController>();
        
        // call shipController to build ship
        Ship.GetComponent<ShipController>().BuildShip(Ship, playerShip);
    }
}
