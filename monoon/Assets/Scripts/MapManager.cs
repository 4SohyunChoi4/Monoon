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
        /*StartPoint = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("�����").GetComponent<InputField>();
        Destination = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("������").GetComponent<InputField>();
        StartMark = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("�������Ʈ").gameObject;
        DestinationMark = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("��������Ʈ").gameObject;*/

        MapSubPanel = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("MapSubPanel").gameObject;
        MapSubPanelText = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("MapSubPanel").transform.Find("MapSubPanelText").GetComponent<Text>();
        BuildingNameText = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("MapSubPanel2").transform.Find("BuildingNameText").GetComponent<Text>();
        HoneytipListText = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("MapSubPanel").transform.Find("MapSubSubPanel").transform.Find("MapSubSubPanelText").GetComponent<Text>();

        MapSubSubPanel = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("MapSubPanel").transform.Find("MapSubSubPanel").gameObject;
        pos = 0;

        BuildingList.Add("��Ű�");
        BuildingList.Add("������");
        BuildingList.Add("�����");
        BuildingList.Add("���ñ���ȸ��");
        BuildingList.Add("������");
        BuildingList.Add("�л�ȸ��");
        BuildingList.Add("������");
        BuildingList.Add("�����Ӱ�");
        BuildingList.Add("������");
        BuildingList.Add("���а�");
        BuildingList.Add("���ֳ����");
        BuildingList.Add("���д���");
        BuildingList.Add("���Ǵ���");
        BuildingList.Add("�̼�����");

        Player = GameObject.Find("Noonsong (1)").gameObject;

        /*NonBuildingList.Add("���ı���ȸ��");
        NonBuildingList.Add("���׻�");
        NonBuildingList.Add("��ȸ������");
        NonBuildingList.Add("�ɻ��");*/
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
        if (Building == null) BuildingNameText.text = "<color=red>" + "�ǹ��� �������ּ���!" + "</color>"; // Building�� ���õ��� ���� ���¶��

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
        // ������ ���� �� �ִ� ������ ��� ������ ���� �� ó�� �������� �߰� ��.
        int childSize = HoneytipListPanel.transform.childCount;
        for (int i = 0; i < childSize; i++)
        {
            HoneytipListPanel.transform.GetChild(i).gameObject.SetActive(false);
        }
        HoneytipListPanel.transform.GetChild(0).gameObject.SetActive(true);

        // ������ ���� �� �ִ� ������ ���� ��ġ�� ����Ų pos ���� �ʱ�ȭ
        pos = 0;
    }

    public void ClickBuilding()
    {
        //MapSubPanel2.SetActive(true); 
        Building = EventSystem.current.currentSelectedGameObject;
        BuildingNameText.text = "���� ���� ��� : " + Building.name;
    }

    public void Teleport()
    {

        if (Building.name.Equals("�����"))
        {
            MapPanel.SetActive(false);
            Player.transform.position = new Vector3(127, 15, 207);
        }
        else if (Building.name.Equals("������"))
        {
            MapPanel.SetActive(false);
            Player.transform.position = new Vector3(84, 17, 201);
        }
        else if (Building.name.Equals("��Ű�"))
        {
            MapPanel.SetActive(false);
            Player.transform.position = new Vector3(80, 16, 190);
        }
        else if (Building.name.Equals("������"))
        {
            MapPanel.SetActive(false);
            Player.transform.position = new Vector3(91, 14, 171);
        }
        else if (Building.name.Equals("�л�ȸ��"))
        {
            MapPanel.SetActive(false);
            Player.transform.position = new Vector3(133, 10, 162);
        }
        else if (Building.name.Equals("������"))
        {
            MapPanel.SetActive(false);
            Player.transform.position = new Vector3(125, 10, 157);
        }
        else if (Building.name.Equals("�����Ӱ�"))
        {
            MapPanel.SetActive(false);
            Player.transform.position = new Vector3(130, 9, 134);
        }
        else if (Building.name.Equals("���д���"))
        {
            MapPanel.SetActive(false);
            Player.transform.position = new Vector3(113, 17, 80);
        }
        else if (Building.name.Equals("���Ǵ���"))
        {
            MapPanel.SetActive(false);
            Player.transform.position = new Vector3(100, 17, 98);
        }
        else if (Building.name.Equals("�̼�����"))
        {
            MapPanel.SetActive(false);
            Player.transform.position = new Vector3(121, 15, 95);
        }
        else if (Building.name.Equals("���ֳ����"))
        {
            MapPanel.SetActive(false);
            Player.transform.position = new Vector3(152, 13, 78);
        }
        else if (Building.name.Equals("������"))
        {
            MapPanel.SetActive(false);
            Player.transform.position = new Vector3(190, 9, 97);
        }
        else if (Building.name.Equals("���а�"))
        {
            MapPanel.SetActive(false);
            Player.transform.position = new Vector3(184, 8, 116);
        }
        else  // ���׻�, ��ȸ������, �ɻ�� �ڷ���Ʈ ����
        {
            BuildingNameText.text = "<color=red>" + "�ڷ���Ʈ ������ �����Դϴ�." + "\n" + "</color>" + "���� ���� ��� : " + Building.name;
        }
        
    }

    public void ClickNext()
    {
        //HoneytipListPanel = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("MapSubPanel").transform.Find("MapSubSubPanel").transform.Find("Contents").transform.Find("1. ��Ű�").transform.Find(HoneytipListPanel.name).gameObject;
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
        // ���� �гε� ���� ��Ȱ��ȭ
        foreach (string name in BuildingList)
        {
            HoneytipPanel = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("MapSubPanel").transform.Find(name).gameObject;
            HoneytipPanel.SetActive(false);
            //Debug.Log(name + "��Ȱ��ȭ!");
        }

        if (Building == null) BuildingNameText.text = "<color=red>" + "�ǹ��� �������ּ���!" + "</color>"; // Building�� ���õ��� ���� ���¶��
        else // Building�� ���������� Ŭ�� �� ���¶��
        {
            if (Building.name.Equals("���ı���ȸ��") || Building.name.Equals("���׻�") || Building.name.Equals("��ȸ������") || Building.name.Equals("�ɻ��")) // Ŭ���� ������ ������ ���� ���
            {
                BuildingNameText.text = "<color=red>" + "���� ������ �����Դϴ�." + "\n" + "</color>" + "���� ���� ��� : " + Building.name;
            }

            else // Ŭ���� ������ ������ �ִ� ���
            {
                MapSubPanel.SetActive(true);
                MapSubPanelText.text = Building.name + "�� ����";

                HoneytipPanel = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("MapSubPanel").transform.Find(Building.name).gameObject;
                HoneytipPanel.SetActive(true);
            }
        }
    }

    public void ClickHoneytipList()
    {
        // ���� �гε� ���� ��Ȱ��ȭ
        foreach(string name in BuildingList)
        {
            HoneytipListPanel = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("MapSubPanel").transform.Find("MapSubSubPanel").transform.Find("Contents").transform.Find(name).gameObject;
            HoneytipListPanel.SetActive(false);
        }

        HoneytipListPanel = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("MapSubPanel").transform.Find("MapSubSubPanel").gameObject;
        HoneytipListPanel.SetActive(true);

        HoneytipList = EventSystem.current.currentSelectedGameObject;
        HoneytipListText.text = HoneytipList.name;

        // �ǹ� �� �гε� ��� ��Ȱ��ȭ
        HoneytipListPanel = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("MapSubPanel").transform.Find("MapSubSubPanel").transform.Find("Contents").transform.Find(Building.name).gameObject;
        int childSize = HoneytipListPanel.transform.childCount;
        for (int i = 0; i < childSize; i++)
        {
            HoneytipListPanel.transform.GetChild(i).gameObject.SetActive(false);
        }
        // ���� �ش��ϴ� ���� �гθ� Ȱ��ȭ
        HoneytipListPanel.SetActive(true);
        HoneytipListPanel = GameObject.Find("Canvas").transform.Find("MapPanel").transform.Find("MapSubPanel").transform.Find("MapSubSubPanel").transform.Find("Contents").transform.Find(Building.name).transform.Find(HoneytipList.name).gameObject;
        HoneytipListPanel.SetActive(true);
    }
}
