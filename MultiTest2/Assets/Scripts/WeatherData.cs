using System;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

public class WeatherData : MonoBehaviour
{//온도(몇도인지), 이미지 필요함
    public Text WeatherText;
    private float timer;
    public float minutesBetweenUpdate = 60*60*60;
    private string cityID = "1835848"; //서울 지억번호
    private string API_key = "28343fd6de7357fb11e65e338795a68c";
    string iconNumb = "";
    public Image weatherImg;
    private Sprite weatherSprite;
    //30cbc0572f7218a81d8d7056865ae959
    private void Start()
    {
        Debug.Log("weatherData 시작");
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
/*        using (UnityWebRequest tempImg = UnityWebRequest.Get("http://openweathermap.org/img/wn/+" + iconNumb + "@2x.png"))
        {
            yield return tempImg.SendWebRequest();
            if (tempImg.isNetworkError || tempImg.isHttpError)
            {
                Debug.LogError(tempImg.error);
                yield break;
            }
            else
            {
                Debug.Log("성공");
                weatherImg.sprite = weatherSprite;
            }
        }
        */
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
        Debug.Log("Temp: " + weather.Celsius());
        Debug.Log(weather.weatherId);
        Debug.Log(weather.main);
        Debug.Log(weather.description);
        Debug.Log(weather.icon);

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