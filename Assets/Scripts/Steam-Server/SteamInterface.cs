using UnityEngine;
using UnityEngine.UI;
using Steamworks;
using System.Threading.Tasks;
using System;

public class SteamInterface : SteamManager
{
    [SerializeField] private RawImage SteamUserPicture;
    [SerializeField] private Text SteamUserName;

    async  void Start()
    {
        if (SteamClient.IsValid)
        {
            //Debug.Log("Steam is valid");
            SteamUserName.text = SteamClient.Name.ToString();
            SteamUserName.color = Color.green;
            string steamid = SteamClient.SteamId.ToString();

            var img = await SteamFriends.GetLargeAvatarAsync(SteamClient.SteamId);
            SteamUserPicture.texture = GetTextureFromImage(img.Value);
        }
        else
        {
            //Debug.Log("Steam not valid");
            SteamUserName.text = "Unavaliable";
            SteamUserName.color = Color.red;
        }
    }
    public static Texture2D GetTextureFromImage(Steamworks.Data.Image image)
    {
        Texture2D texture = new Texture2D((int)image.Width, (int)image.Height);
        for (int x = 0; x < image.Width; x++)
        {
            for (int y = 0; y < image.Height; y++)
            {
                var p = image.GetPixel(x, y);
                texture.SetPixel(x, (int)image.Height - y, new Color(p.r / 255.0f, p.g / 255.0f, p.b / 255.0f, p.a / 255.0f));
            }
        }
        texture.Apply();
        return texture;
    }
}
