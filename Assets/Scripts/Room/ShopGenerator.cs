using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ShopGenerator : MonoBehaviour
{
    public int xMin, xMax, yMin, yMax;

    private Vector2 size;
    public Vector2 offset;

    //room-objects
    public GameObject wallVert, wallHor, floor, corner;

    //teleports
    public GameObject nextRoomButton;

    // buffs
    public List<GameObject> lootItems = new List<GameObject>();


    //overlay
    private int countRooms;

    private GameObject UI_Overlay;
    private Transform UItoPanel;
    private GameObject EnemyPanel;
    private Transform PaneltoTextEnemy;
    private Transform PaneltoTextRoom;
    public Text EnemiesCountText;
    public Text RoomsCountText;

    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("RoomsCount", 0);
    }
    void Start()
    {
        countRooms = PlayerPrefs.GetInt("RoomsCount");
        //room-size
        setRoomSize();

        //UI_Overlay

        UI_Overlay = GameObject.Find("UI_Overlay");
        UItoPanel = UI_Overlay.transform.Find("EnemyPanel");
        EnemyPanel = UItoPanel.gameObject;

        PaneltoTextEnemy = EnemyPanel.transform.Find("EnemiesText");
        EnemiesCountText = PaneltoTextEnemy.GetComponent<Text>();
        EnemiesCountText.text = "";

        PaneltoTextRoom = EnemyPanel.transform.Find("RoomsText");
        RoomsCountText = PaneltoTextRoom.GetComponent<Text>();
        //RoomsCountText.text = "Room 0";
        getRoomCountUI();

        //room-collision
        //floor
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Instantiate(floor, new Vector3((transform.position.x + i) * offset.x, (transform.position.y + j) * offset.y, 0), Quaternion.identity, transform);
            }
        }
        Walls();
        // Items
        List<GameObject> possibleItems = new List<GameObject>();
        foreach (GameObject item in lootItems)
        {
            float randomnow = Random.Range(1, 100);
            Debug.Log(randomnow + " >= " + item.GetComponent<Loot>().dropChance + " ?");
            if (randomnow >= item.GetComponent<Loot>().dropChance)
            {
                possibleItems.Add(item);
            }
        }
        if (possibleItems.Count == 0) { Debug.Log("анлак.."); }
        else
        {
            int itemscount = 0;
            foreach (GameObject item in possibleItems)
            {
                if (size.x < itemscount)
                    break;
                GameObject lootGameObject = Instantiate(item, new Vector3((transform.position.x + itemscount + 1) * offset.x, (transform.position.y + 1) * offset.y, 0), Quaternion.identity, transform);
                itemscount++;
            }
        }

        //Teleports
        nextRoomButton = Instantiate(nextRoomButton, new Vector3(0.25f, size.y * 0.25f + 0.5f, 0), Quaternion.identity, transform);
        //nextroom должен запускать свой старт для включения панельки с таймером перехода
        nextRoomButton.SetActive(true);
    }

    public void RandomShop()
    {
        // speed,atkspeed, speed,health

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
    protected void getRoomCountUI()
    {
        RoomsCountText.text = "Room " + countRooms;
    }
    public Vector2 getRoomSize()
    {
        return size;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
