using System;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

public class WeatherData : MonoBehaviour
{//�µ�(�����), �̹��� �ʿ���
    public Text WeatherText;
    private float timer;
    public float minutesBetweenUpdate = 60 * 60 * 60;
    private string cityID = "1835848"; //���� �����ȣ
    private string API_key = "28343fd6de7357fb11e65e338795a68c";
    string iconNumb = "";
    public RawImage weatherImg;
    private Sprite weatherSprite;
    //30cbc0572f7218a81d8d7056865ae959
    private void Start()
    {
        weatherImg.texture = Texture2D.blackTexture;
        GetWeatherData();
    }
    /*void Update()
    {
        if (timer <= 0)
        {
            GetWeatherData();
            timer = minutesBetweenUpdate * 60;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
    */
    public void GetWeatherData()
    {
        StartCoroutine(GetCoroutine());
    }
    IEnumerator GetCoroutine()
    {
        //UnityWebRequest www = new UnityWebRequest("http://api.openweathermap.org/data/2.5/forecast?id=" + cityID + "&appid=" + API_key)
        //{
        //  downloadHandler = new DownloadHandlerBuffer()
        // };
        using (UnityWebRequest www = UnityWebRequest.Get("http://api.openweathermap.org/data/2.5/weather?q=Seoul&appid=" + API_key))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
                yield break;
            }
            else
            {
                ParseJson(www.downloadHandler.text);
            }
        }
        using (UnityWebRequest iconwww = UnityWebRequestTexture.GetTexture("http://openweathermap.org/img/wn/" + iconNumb + "@2x.png"))
        {
            yield return iconwww.SendWebRequest();
            if (iconwww.isNetworkError || iconwww.isHttpError)
            {
                Debug.LogError(iconwww.error);
                yield break;
            }
            else
            {
                weatherImg.texture = DownloadHandlerTexture.GetContent(iconwww);
                weatherImg.texture.filterMode = FilterMode.Point;
            }

        }
    }

    WeatherStatus ParseJson (string json)
    {
        WeatherStatus weather = new WeatherStatus();
        try
        {
            dynamic obj = JObject.Parse(json);
            weather.weatherId = obj.weather[0].id;
            weather.main = obj.weather[0].main;
            weather.description = obj.weather[0].description;
            weather.temperature = obj.main.temp;
            weather.pressure = obj.main.pressure;
            weather.windSpeed = obj.wind.speed;
            weather.icon = obj.weather[0].icon;
            iconNumb = weather.icon;
        }
        catch(Exception e)
        {
            Debug.Log(e.StackTrace);
        }
        WeatherText.text = weather.Celsius()+ "˚C";

        return weather;
    }

    public class WeatherStatus
    {
        public int weatherId;
        public string main;
        public string description;
        public string imgUrl;
        public float temperature;
        public float pressure;
        public float windSpeed;
        public string icon;

        public float Celsius()
        {
            return (int)(temperature - 273.15f);
        }
    }
}