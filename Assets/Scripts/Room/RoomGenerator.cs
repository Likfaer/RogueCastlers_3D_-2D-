using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class RoomGenerator : MonoBehaviour
{
    public int xMin, xMax, yMin, yMax;
    
    private Vector2 size;
    public Vector2 offset;
    
    //room-objects
    public GameObject wall, floor, corner;

    //teleports
    public GameObject ShopButton, nextRoomButton;

    //overlay
    EnemySpawner spawner;
    private int countEnemies;
    private int countRooms;

    public Text EnemiesCountText;
    public Text RoomsCountText;

    void Start()
    {
        //Debug.Log(PlayerPrefs.GetInt("RoomsCount"));
        countRooms = PlayerPrefs.GetInt("RoomsCount");
        //room-size
        setRoomSize();

        //UI_Overlay

        EnemiesCountText = GameObject.Find("UI_Overlay/EnemyPanel/EnemiesText").GetComponent<Text>();
        EnemiesCountText.text = "";

       
        RoomsCountText = GameObject.Find("UI_Overlay/EnemyPanel/RoomsText").GetComponent<Text>();
        //RoomsCountText.text = "Room 0";
        getRoomCountUI();
        
        //room-collision
        //floor
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Instantiate(floor,new Vector3((transform.position.x + i) * offset.x, (transform.position.y + j) * offset.y, 0),Quaternion.identity, transform);
            }
        }
        Walls();

        //Enemies
        spawner = GetComponent<EnemySpawner>();

        countEnemies = Mathf.RoundToInt((size.x * size.y + (countRooms * size.x *size.y / 10)) / 3.6f);
        //countEnemies = 2;

        //Debug.Log("enemies in RoomGen: " + countEnemies);
        spawner.StartSpawn(countEnemies, size.x, size.y);

        //Teleports
        nextRoomButton = Instantiate(nextRoomButton, new Vector3(size.x * 0.25f , size.y * 0.25f + 0.5f , 0), Quaternion.identity, transform);
        ShopButton = Instantiate(ShopButton, new Vector3(size.x * 0.25f + 0.5f, size.y * 0.25f + 0.5f, 0), Quaternion.identity, transform);
        //nextroom должен запускать свой старт для включения панельки с таймером перехода
        nextRoomButton.SetActive(false);
        ShopButton.SetActive(false);
    }
    void Walls()
    {
        // Top+Bottom Walls
        GameObject wallH = wall;
        wallH.GetComponent<Renderer>().sharedMaterial.SetTextureScale("_MainTex", new Vector2(size.x, 1));

        wallH.transform.localScale = new Vector3((size.x / 2), 0.1f, 1);

        Instantiate(wallH, new Vector3((0.25f * size.x), transform.position.y - 0.55f, 0), Quaternion.identity, transform);
        Instantiate(wallH, new Vector3((0.25f * size.x), transform.position.y * size.y + 0.05f, 0), Quaternion.identity, transform);

        // Left+Right Walls
        GameObject wallV = wallH;
        wallV.transform.localScale = new Vector3((size.y / 2), 0.1f , 1);
        wallV.GetComponent<Renderer>().sharedMaterial.SetTextureScale("_MainTex", new Vector2(size.y, 1));

        Instantiate(wallV, new Vector3(transform.position.x - 0.55f, (0.25f * size.y), 0), Quaternion.Euler(0f, 0f, 90f), transform);
        Instantiate(wallV, new Vector3(transform.position.x * size.x + 0.05f, (0.25f * size.y), 0), Quaternion.Euler(0f, 0f, 90f), transform);

        // Corners

        Instantiate(corner, new Vector3(-0.050f, -0.050f, 0), Quaternion.identity, transform);
        Instantiate(corner, new Vector3((0.5f * size.x) + 0.050f, -0.050f, 0), Quaternion.identity, transform);
        Instantiate(corner, new Vector3((0.5f * size.x) + 0.050f, (0.5f * size.y) + 0.050f, 0), Quaternion.identity, transform);
        Instantiate(corner, new Vector3(-0.050f, (0.5f * size.y) + 0.050f, 0), Quaternion.identity, transform);
    }
    public void setRoomSize()
    {
        size.x = Random.Range(xMin, xMax);
        if (size.x % 2 == 0)
        {
            size.x--;
        }
        size.y = Random.Range(yMin, yMax);
        if (size.y % 2 == 0)
        {
            size.y--;
        }
    }
    public Vector2 getRoomSize()
    {
        return size;
    }
    protected void getEnemiesCountUI()
    {
        EnemiesCountText.text = spawner.getEnemiesNow() + " / " + countEnemies;
    }
    protected void getRoomCountUI()
    {
        
        RoomsCountText.text = "Room " + countRooms;
    }
    // Update is called once per frame
    void Update()
    {
        getEnemiesCountUI();
        if (!spawner.IsCoroutineRunning() && spawner.getEnemiesNow() == 0)
        {
            nextRoomButton.SetActive(true);
            ShopButton.SetActive(true);
            EnemiesCountText.text = "Finished!";
        }
        else
        {

        }
    }
}
