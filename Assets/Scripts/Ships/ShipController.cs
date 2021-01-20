using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Ships
{
    public class ShipController : MonoBehaviour
    {
        public bool flyable;
        public bool playerShip = false;
        public int sizeMin = 6;
        public int sizeMax = 20;
        public int rooms = 12;
        public int currentRooms = 0;
        public float sideRoomChance = 0.25f;
        public bool symmetric = true;
        public Vector3Int pointer;
        private List<Tile> tilePalette = new List<Tile>();
        private List<ShipRoom> shipRooms = new List<ShipRoom>();
        private Tilemap bgMap;
        private Tilemap colMap;

        // Start is called before the first frame update
        void Start()
        {
            //build a new ship for the player
            pointer = new Vector3Int(0, 0, 0);
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void BuildShip(GameObject ship, bool playerShip)
        {
            //ship constraints:
            //ship should have at least one external door
            //each room should be connected by at least one set of doors
            //each room overlaps by at least one tile
            //room 0 should always be a cockpit
            //ships should be symmetric horizontally (if we're building a room on the west side, also build on east)
            //room sizes should only include odd number of tiles (to ensure there is a "middle" row)
            //doors should align on vertical and horizontal centers

            //background tilemap defines size, but does not collide with player
            GameObject backgroundMap = new GameObject();
            backgroundMap.name = "BackgroundMap";
            backgroundMap.layer = 8;
            backgroundMap.AddComponent<Tilemap>();
            backgroundMap.AddComponent<TilemapRenderer>().sortingLayerName = "Ship";
            //parent ship to tilemap
            backgroundMap.transform.parent = ship.transform;
            //set tilemap size for editing (need to resize afterwards)
            backgroundMap.GetComponent<Tilemap>().size = new Vector3Int(100, 100, 0);
        
        
            //add new tileMap for collision layer
            GameObject collisionMap = new GameObject();
            collisionMap.name = "CollisionMap";
            collisionMap.AddComponent<Tilemap>();
            collisionMap.AddComponent<TilemapRenderer>();
            collisionMap.AddComponent<TilemapCollider2D>();
            collisionMap.layer = 8;
            //child it to playerShip
            collisionMap.transform.parent = ship.transform;
            backgroundMap.GetComponent<Tilemap>().size = new Vector3Int(100, 100, 0);


            //build rooms with background tiles
            tilePalette.Add((Tile)Resources.Load("ShipTiles/FloorTile"));
            tilePalette.Add((Tile)Resources.Load("ShipTiles/WallTile"));
            tilePalette.Add((Tile)Resources.Load("ShipTiles/DoorTile"));
            bgMap = backgroundMap.GetComponent<Tilemap>();
            colMap = collisionMap.GetComponent<Tilemap>();
            BuildRooms(backgroundMap.GetComponent<Tilemap>(), collisionMap.GetComponent<Tilemap>());


        }
    
    
    

        private void BuildBackground(Vector3Int bgPointer, int xSize, int ySize)
        {
            //set start point to always be bottom left of fill 
            int thisXSize = xSize;
            int thisYSize = ySize;
            int startX = bgPointer.x;
            int endX = bgPointer.x + thisXSize;
            int startY = bgPointer.y;
            int endY = bgPointer.y + thisYSize;
        
            if (startX > endX)
            {
                int temp = startX;
                startX = endX;
                endX = temp;
            }
            if (startY > endY)
            {
                int temp = startY;
                startY = endY;
                endY = temp;
            }
        
            bgMap.BoxFill(new Vector3Int(startX,startY,0), tilePalette[0],startX, startY, endX, endY);
        }

        void BuildDoors(Vector3Int door1, Vector3Int door2)
        {
        
        }
    

        void BuildWalls(Vector3Int wallPointer, int[] size)
        {
            int xSize = size[0];
            int ySize = size[1];
            int curDoors = 0;
            bool[] doors = new bool[4];
            bool topDoor = false;
            bool botDoor = false;
            bool lDoor = false;
            bool rDoor = false;
            for (int x = 0; x < xSize; x++)
            {

            
                Vector3Int top = new Vector3Int(wallPointer.x + x, wallPointer.y + ySize - 1, 0);
                Vector3Int bottom = new Vector3Int(wallPointer.x + x, wallPointer.y, 0);
                bool topTile = colMap.HasTile(top);
                bool botTile = colMap.HasTile(bottom);
                int mid1 = xSize / 2 - 1;
                int mid2 = xSize / 2;
                if (!topTile)
                {
                    colMap.SetTile(top, tilePalette[1]); 
                }
                else
                {
                    if (x == mid1 || x == mid2)
                    {
                        colMap.SetTile(top, null);
                    } 
                }

                if (!botTile)
                {
                    colMap.SetTile(bottom, tilePalette[1]);
                }
                else
                {
                    if (x == mid1 || x == mid2)
                    {
                        colMap.SetTile(bottom, null);
                    }
                }
            }

            for (int y = 0; y < ySize; y++)
            {

                Vector3Int left = new Vector3Int(wallPointer.x, wallPointer.y + y, 0);
                Vector3Int right = new Vector3Int(wallPointer.x + xSize - 1, wallPointer.y + y, 0);
                int mid1 = ySize / 2 - 1;
                int mid2 = ySize / 2;
            
                if (!colMap.HasTile(left))
                {
                    colMap.SetTile(left, tilePalette[1]); 
                }
                else
                {
                    if (y == mid1 || y == mid2)
                    {
                        colMap.SetTile(left, null);
                    } 
                }

                if (!colMap.HasTile(right))
                {
                    colMap.SetTile(right, tilePalette[1]);
                }
                else
                {
                    if (y == mid1 || y == mid2)
                    {
                        colMap.SetTile(right, null);
                    } 
                }
            }
        
        
        }
    
    
        //TODO
        void BuildObjects()
        {
        
        }

    

        private void AddRoom(Vector3Int startPoint, int[] size)
        {
            //draw background layer
            BuildBackground(startPoint, size[0] -1 , size[1]  - 1);
            //draw walls
            BuildWalls(startPoint, size);
            BuildObjects();
            ShipRoom room = new ShipRoom(size, startPoint);
            shipRooms.Add(room);
            currentRooms++;
        }

        private void BuildRooms(Tilemap backgroundMap, Tilemap collisionMap)
        {
            Vector3Int pointer = new Vector3Int(0, 0, 0);
            int[] size = GetRoomSize();

            int shipLength = Random.Range(rooms / 2, (rooms / 3 * 2) + 1);
        
            //ensure shipLength is even so side rooms are symmetric
            if ((rooms - shipLength) % 2 != 0 && symmetric)
            {
                shipLength++;
            }
        
            //draw length of ship
            int sideRooms = 0;
            for (int i = 0; i < shipLength; i++)
            {
                int[] lastSize = size;
                //get new size
                size = GetRoomSize();
                //make new pointer
                pointer = new Vector3Int(pointer.x + lastSize[0] / 2 - size[0] / 2, pointer.y - size[1] + 1, 0);
                AddRoom(pointer, size);
            
                //chance for left/right rooms off of main
                float sideChance = Random.Range(0, 1);
                if (sideChance <= sideRoomChance && sideRooms < 1)
                {
                    if (symmetric)
                    {
                        lastSize = size;
                        size = GetRoomSize();
                        Vector3Int sidePointer = new Vector3Int(pointer.x - size[0], pointer.y + lastSize[1]/2 - size[1] / 2, 0);
                        AddRoom(sidePointer, size);
                        sidePointer.x = (pointer.x + size[0]);
                        AddRoom(sidePointer, size);
                        sideRooms++;
                    }
                    else
                    {
                        //pick a side
                    }
                }

            }

            collisionMap.ResizeBounds();
            collisionMap.CompressBounds();
        }
    
        private int[] GetRoomSize()
        {
            //setting x and y room size
            int[] sizes = new int[2];
            for (int i = 0; i < sizes.Length; i++)
            {
                int size = Random.Range(sizeMin, sizeMax);
                //ensuring our room sizes are even
                if (size % 2 != 0)
                {
                    size -= 1;
                }
            
                sizes[i] = size;
            }
            Debug.Log("size x: " + sizes[0] + " size y: " + sizes[1]);
            return sizes;
        }
    }
}
