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
    public GameObject wallVert, wallHor, floor, corner;

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

        //countEnemies = Mathf.RoundToInt(size.x * size.y / 2.6f);
        countEnemies = 2;

        //Debug.Log("enemies in RoomGen: " + countEnemies);
        spawner.StartSpawn(countEnemies, size.x, size.y);

        //Teleports
        nextRoomButton = Instantiate(nextRoomButton, new Vector3(size.x * 0.25f , size.y * 0.25f + 0.5f , 0), Quaternion.identity, transform);
        ShopButton = Instantiate(ShopButton, new Vector3(size.x * 0.25f + 0.5f, size.y * 0.25f + 0.5f, 0), Quaternion.identity, transform);
        //nextroom ������ ��������� ���� ����� ��� ��������� �������� � �������� ��������
        nextRoomButton.SetActive(false);
        ShopButton.SetActive(false);
    }
    void Walls()
    {
        // Left+Right Walls
        wallVert.transform.localScale = new Vector3(0.1f, (size.y / 2), 1);
        Renderer prefabRendererV = wallVert.GetComponent<Renderer>();
        Material prefabMaterialV = prefabRendererV.sharedMaterial;
        prefabMaterialV.SetTextureScale("_MainTex", new Vector2(1, size.x));
        Instantiate(wallVert, new Vector3(transform.position.x - 0.55f, (0.25f * size.y), 0), Quaternion.identity, transform);
        Instantiate(wallVert, new Vector3(transform.position.x * size.x + 0.05f, (0.25f * size.y), 0), Quaternion.identity, transform);

        // Top+Bottom Walls
        wallHor.transform.localScale = new Vector3((size.x / 2), 0.1f, 1);
        Renderer prefabRendererH = wallHor.GetComponent<Renderer>();
        Material prefabMaterialH = prefabRendererH.sharedMaterial;
        prefabMaterialH.SetTextureScale("_MainTex", new Vector2(size.y, 1));
        Instantiate(wallHor, new Vector3((0.25f * size.x), transform.position.y - 0.55f, 0), Quaternion.identity, transform);
        Instantiate(wallHor, new Vector3((0.25f * size.x), transform.position.y * size.y + 0.05f, 0), Quaternion.identity, transform);

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
