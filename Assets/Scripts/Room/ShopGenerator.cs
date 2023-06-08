using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ShopGenerator : MonoBehaviour
{
    public int xMin, xMax, yMin, yMax;

    private Vector2 size;
    public Vector2 offset;

    //room-objects
    public GameObject wall, floor, corner;

    //teleports
    public GameObject nextRoomButton;

    // buffs
    public List<GameObject> lootItems = new List<GameObject>();


    //overlay
    private int countRooms;

    public Text EnemiesCountText;
    public Text RoomsCountText;

    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("RoomsCount", 0);
    }
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
                Instantiate(floor, new Vector3((transform.position.x + i) * offset.x, (transform.position.y + j) * offset.y, 0), Quaternion.identity, transform);
            }
        }
        Walls();
        // Items

        RandomShop();

        //Teleports
        nextRoomButton = Instantiate(nextRoomButton, new Vector3(0.25f, size.y * 0.25f + 0.5f, 0), Quaternion.identity, transform);
        //nextroom должен запускать свой старт для включения панельки с таймером перехода
        nextRoomButton.SetActive(true);
    }

    public void RandomShop()
    {
        List<GameObject> possibleItems = new List<GameObject>();
        int qut = 0;
        while (possibleItems.Count - 1 < size.x)
        {
            qut++;
            AddItem();
            //Debug.Log((possibleItems.Count - 1) + " < " + size.x + " ?");
            if (qut > 50)
                break;
        }
        int itemscount = 1;
        foreach (GameObject item in possibleItems)
        {
            if (size.x <= itemscount)
                break;
            GameObject lootGameObject = Instantiate(item, new Vector3((transform.position.x + itemscount) * offset.x, (transform.position.y + 1) * offset.y, 0), Quaternion.identity, transform);
            itemscount++;
        }
        void AddItem()
        {
            foreach (GameObject item in lootItems)
            {
                float randomnow = Random.Range(1, 100);
                //Debug.Log("trying add " + item.transform.GetComponent<SpriteRenderer>().sprite.name + " with chance " + item.GetComponent<Loot>().dropChance  + " >= " + randomnow + " ?");

                if (possibleItems.Count(itemObj => itemObj == item) < 2 && item.GetComponent<Loot>().dropChance >= randomnow)
                {
                    possibleItems.Add(item);
                }
            }
        }
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
        wallV.transform.localScale = new Vector3((size.y / 2), 0.1f, 1);
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
