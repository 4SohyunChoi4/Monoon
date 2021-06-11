using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapManager : MonoBehaviour
{
    private GameObject MapPanel;
    private GameObject MapSubPanel;
    //private GameObject MapSubPanel2;

    private GameObject RoadPanel;
    private GameObject HoneytipPanel;
    private GameObject Building;
    private GameObject HoneytipList;
    private GameObject HoneytipListPanel;
    private GameObject MapSubSubPanel;

    private Text BuildingNameText;
    private Text MapSubPanelText;
    private Text HoneytipNameText;
    private Text HoneytipListText;

    private List<string> BuildingList = new List<string>();

    private GameObject Player;

    /*private InputField StartPoint;
    private InputField Destination;
    private GameObject StartMark;
    private GameObject DestinationMark;
    private Transform t1;
    private Transform t2;*/

    private int pos;
    void Start()
    {
        MapPanel = GameObject.Find("Canvas").transform.Find("MapPanel").gameObject;
        /*StartPoint = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("출발지").GetComponent<InputField>();
        Destination = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("도착지").GetComponent<InputField>();
        StartMark = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("출발포인트").gameObject;
        DestinationMark = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("도착포인트").gameObject;*/

        MapSubPanel = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("MapSubPanel").gameObject;
        MapSubPanelText = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("MapSubPanel").transform.Find("MapSubPanelText").GetComponent<Text>();
        BuildingNameText = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("MapSubPanel2").transform.Find("BuildingNameText").GetComponent<Text>();
        HoneytipListText = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("MapSubPanel").transform.Find("MapSubSubPanel").transform.Find("MapSubSubPanelText").GetComponent<Text>();

        MapSubSubPanel = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("MapSubPanel").transform.Find("MapSubSubPanel").gameObject;
        pos = 0;

        BuildingList.Add("명신관");
        BuildingList.Add("새힘관");
        BuildingList.Add("순헌관");
        BuildingList.Add("수련교수회관");
        BuildingList.Add("진리관");
        BuildingList.Add("학생회관");
        BuildingList.Add("행정관");
        BuildingList.Add("프라임관");
        BuildingList.Add("도서관");
        BuildingList.Add("과학관");
        BuildingList.Add("백주년기념관");
        BuildingList.Add("약학대학");
        BuildingList.Add("음악대학");
        BuildingList.Add("미술대학");

        Player = GameObject.Find("Noonsong (1)").gameObject;

        /*NonBuildingList.Add("행파교수회관");
        NonBuildingList.Add("르네상스");
        NonBuildingList.Add("사회교육관");
        NonBuildingList.Add("꽃산달");*/
    }

    // Update is called once per frame
    void Update()
    {
        /*if (StartPoint.text != null && Destination != null)
        {
            if (GameObject.Find(StartPoint.text) && GameObject.Find(Destination.text))
            {
                t1 = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find(StartPoint.text).GetComponent<Transform>();
                t2 = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find(Destination.text).GetComponent<Transform>();
                StartMark.SetActive(true);
                DestinationMark.SetActive(true);
                StartMark.transform.position = t1.position;
                DestinationMark.transform.position = t2.position;
                //Debug.Log(t1.position + "/" + t2.position);
            }
            else
            {
                StartMark.SetActive(false);
                DestinationMark.SetActive(false);
            }
        }
        else
        {
            StartMark.SetActive(false);
            DestinationMark.SetActive(false);
        }*/
        if (Building == null) BuildingNameText.text = "<color=red>" + "건물을 선택해주세요!" + "</color>"; // Building이 선택되지 않은 상태라면

    }
    
    public void ClickMapbutton()
    {
        MapPanel.SetActive(true);
    }

    public void ClickExitbutton()
    {
        MapPanel.SetActive(false);
    }

    public void ClickMapSubPanelExitbutton()
    {
        MapSubPanel.SetActive(false);
    }

    public void ClickMapSubSubPanelExitbutton()
    {
        MapSubSubPanel.SetActive(false);
        // 사진이 여러 장 있는 꿀팁의 경우 다음에 켰을 때 처음 사진부터 뜨게 함.
        int childSize = HoneytipListPanel.transform.childCount;
        for (int i = 0; i < childSize; i++)
        {
            HoneytipListPanel.transform.GetChild(i).gameObject.SetActive(false);
        }
        HoneytipListPanel.transform.GetChild(0).gameObject.SetActive(true);

        // 사진이 여러 장 있는 꿀팁의 현재 위치를 가르킨 pos 정보 초기화
        pos = 0;
    }

    public void ClickBuilding()
    {
        //MapSubPanel2.SetActive(true); 
        Building = EventSystem.current.currentSelectedGameObject;
        BuildingNameText.text = "현재 선택 장소 : " + Building.name;
    }

    public void Teleport()
    {

        if (Building.name.Equals("순헌관"))
        {
            MapPanel.SetActive(false);
            Player.transform.position = new Vector3(127, 15, 207);
        }
        else if (Building.name.Equals("진리관"))
        {
            MapPanel.SetActive(false);
            Player.transform.position = new Vector3(84, 17, 201);
        }
        else if (Building.name.Equals("명신관"))
        {
            MapPanel.SetActive(false);
            Player.transform.position = new Vector3(80, 16, 190);
        }
        else if (Building.name.Equals("새힘관"))
        {
            MapPanel.SetActive(false);
            Player.transform.position = new Vector3(91, 14, 171);
        }
        else if (Building.name.Equals("학생회관"))
        {
            MapPanel.SetActive(false);
            Player.transform.position = new Vector3(133, 10, 162);
        }
        else if (Building.name.Equals("행정관"))
        {
            MapPanel.SetActive(false);
            Player.transform.position = new Vector3(125, 10, 157);
        }
        else if (Building.name.Equals("프라임관"))
        {
            MapPanel.SetActive(false);
            Player.transform.position = new Vector3(130, 9, 134);
        }
        else if (Building.name.Equals("약학대학"))
        {
            MapPanel.SetActive(false);
            Player.transform.position = new Vector3(113, 17, 80);
        }
        else if (Building.name.Equals("음악대학"))
        {
            MapPanel.SetActive(false);
            Player.transform.position = new Vector3(100, 17, 98);
        }
        else if (Building.name.Equals("미술대학"))
        {
            MapPanel.SetActive(false);
            Player.transform.position = new Vector3(121, 15, 95);
        }
        else if (Building.name.Equals("백주년기념관"))
        {
            MapPanel.SetActive(false);
            Player.transform.position = new Vector3(152, 13, 78);
        }
        else if (Building.name.Equals("도서관"))
        {
            MapPanel.SetActive(false);
            Player.transform.position = new Vector3(190, 9, 97);
        }
        else if (Building.name.Equals("과학관"))
        {
            MapPanel.SetActive(false);
            Player.transform.position = new Vector3(184, 8, 116);
        }
        else  // 르네상스, 사회교육관, 꽃산달 텔레포트 제외
        {
            BuildingNameText.text = "<color=red>" + "텔레포트 미지원 구역입니다." + "\n" + "</color>" + "현재 선택 장소 : " + Building.name;
        }
        
    }

    public void ClickNext()
    {
        //HoneytipListPanel = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("MapSubPanel").transform.Find("MapSubSubPanel").transform.Find("Contents").transform.Find("1. 명신관").transform.Find(HoneytipListPanel.name).gameObject;
        int childSize = HoneytipListPanel.transform.childCount;
        //Debug.Log(childSize + "/" + pos);
        if (pos < childSize-1)
        {
            HoneytipListPanel.transform.GetChild(pos).gameObject.SetActive(false);
            pos += 1;
            HoneytipListPanel.transform.GetChild(pos).gameObject.SetActive(true);
        }

    }

    public void ClickPrev()
    {
        int childSize = HoneytipListPanel.transform.childCount;
        if (pos > 0)
        {
            HoneytipListPanel.transform.GetChild(pos).gameObject.SetActive(false);
            pos -= 1;
            HoneytipListPanel.transform.GetChild(pos).gameObject.SetActive(true);
        }
    }

    public void ClickHoneytip()
    {
        HoneytipListPanel = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("MapSubPanel").transform.Find("MapSubSubPanel").gameObject;
        HoneytipListPanel.SetActive(false);
        // 기존 패널들 전부 비활성화
        foreach (string name in BuildingList)
        {
            HoneytipPanel = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("MapSubPanel").transform.Find(name).gameObject;
            HoneytipPanel.SetActive(false);
            //Debug.Log(name + "비활성화!");
        }

        if (Building == null) BuildingNameText.text = "<color=red>" + "건물을 선택해주세요!" + "</color>"; // Building이 선택되지 않은 상태라면
        else // Building이 정상적으로 클릭 된 상태라면
        {
            if (Building.name.Equals("행파교수회관") || Building.name.Equals("르네상스") || Building.name.Equals("사회교육관") || Building.name.Equals("꽃산달")) // 클릭한 구역의 꿀팁이 없는 경우
            {
                BuildingNameText.text = "<color=red>" + "꿀팁 미지원 구역입니다." + "\n" + "</color>" + "현재 선택 장소 : " + Building.name;
            }

            else // 클릭한 구역의 꿀팁이 있는 경우
            {
                MapSubPanel.SetActive(true);
                MapSubPanelText.text = Building.name + "의 꿀팁";

                HoneytipPanel = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("MapSubPanel").transform.Find(Building.name).gameObject;
                HoneytipPanel.SetActive(true);
            }
        }
    }

    public void ClickHoneytipList()
    {
        // 기존 패널들 전부 비활성화
        foreach(string name in BuildingList)
        {
            HoneytipListPanel = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("MapSubPanel").transform.Find("MapSubSubPanel").transform.Find("Contents").transform.Find(name).gameObject;
            HoneytipListPanel.SetActive(false);
        }

        HoneytipListPanel = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("MapSubPanel").transform.Find("MapSubSubPanel").gameObject;
        HoneytipListPanel.SetActive(true);

        HoneytipList = EventSystem.current.currentSelectedGameObject;
        HoneytipListText.text = HoneytipList.name;

        // 건물 내 패널들 모두 비활성화
        HoneytipListPanel = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("MapSubPanel").transform.Find("MapSubSubPanel").transform.Find("Contents").transform.Find(Building.name).gameObject;
        int childSize = HoneytipListPanel.transform.childCount;
        for (int i = 0; i < childSize; i++)
        {
            HoneytipListPanel.transform.GetChild(i).gameObject.SetActive(false);
        }
        // 이후 해당하는 개별 패널만 활성화
        HoneytipListPanel.SetActive(true);
        HoneytipListPanel = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("MapSubPanel").transform.Find("MapSubSubPanel").transform.Find("Contents").transform.Find(Building.name).transform.Find(HoneytipList.name).gameObject;
        HoneytipListPanel.SetActive(true);
    }
}
