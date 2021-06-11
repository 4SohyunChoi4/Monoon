using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class Shop : MonoBehaviour

{
    [Header("UI")]
    public Button buyButton; 
    public int index;
    public RectTransform uiGroup;
    public Text talkText;
    public string[] talkData;

    public GameObject CheckBuyButton;
    public Text ItemName;
    public Text ItemPrice;
    public Text NoondungText;
    private Camera camera;
    private bool activeShopButton;
    public GameObject ShopButton;

    [Header("Inventory")]
    public Item Noondung;
    public int myNoondung;
    private int price;
    public InventoryObjects inventory;
    public InventorySlot _slot;

    [Header("Item List")]
    public GameObject[] itemObj;
    public int[] itemPrice;
    public Transform[] itemPos;

 
    void Awake()
    {
        camera = Camera.main;
        activeShopButton = false;
    }

    private void Start() {
        inventory.Load();
         ComputeMyNoondung();
        buyButton.onClick.AddListener(Buy);
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (!EventSystem.current.IsPointerOverGameObject()) // UI ��ġ �� ���� ����
                {
                    if (hit.collider.tag == "Shop") //building��� tag�� ���� ��ü�� Ŭ���ϸ�
                    {
                        activeShopButton = true;
                    }
                }
                    else if(hit.collider.tag == "road")
                    {
                        activeShopButton = false;
                    }
            }
        }
        if (activeShopButton) { ShopButton.SetActive(true);}
        else if(!activeShopButton)
        { ShopButton.SetActive(false); }
    
    }
    private void LateUpdate() {
       // ComputeMyNoondung();
        myNoondung = _slot.amount;
        NoondungText.text = myNoondung.ToString();
    }
    
    public void ComputeMyNoondung()
    {
        Noondung.Id = 24;
        _slot = inventory.FindItemInventory(Noondung);
        myNoondung = _slot.amount;
        Debug.Log(myNoondung);
    }
    

    // Update is called once per frame

    
    public void checkBuyActive(int _index)
    {   
        index = _index;
        price = itemPrice[index];
        ItemName.text = itemObj[index].name;
        ItemPrice.text = price.ToString();
        Debug.Log(itemObj[index].name);
        CheckBuyButton.SetActive(true);
    }

    public void checkBuyFalse()
    {
        CheckBuyButton.SetActive(false);
    }
    public void Buy()
    {
        if(price > _slot.amount) { //���� ���ڸ� ��
            CheckBuyButton.SetActive(false);
            StopCoroutine(Talk(1));
            StartCoroutine(Talk(1));//������ �ʵ���
            return;
        }
        else
        {
            CheckBuyButton.SetActive(false);
            
            //������ ����
            _slot.amount -= price;
           
            StopCoroutine(Talk(2));
            StartCoroutine(Talk(2));//������ �ʵ���

            
            int posIndex = Random.Range(0,3);
            //���� ���� ������Ʈ �����
            Vector3 ranVec = Vector3.right * Random.Range(-1, 1) + Vector3.forward * Random.Range(-1,1);
            Instantiate(itemObj[index], new Vector3(Random.Range(-0.5f, 3.9f),0.9f,Random.Range(8.5f, 15f)), Quaternion.identity);
            
            Debug.Log(itemObj[index].name);
           
        }
    }

    IEnumerator Talk(int index)
    {
        talkText.text = talkData[index];
        yield return new WaitForSeconds(2f);
        talkText.text = talkData[0];
    }
     public void EnterShop()
    { 
        uiGroup.anchoredPosition = new Vector3(0,0,0);
    }
    public void Exit()
    {   
        uiGroup.anchoredPosition = Vector3.down * 1800;  
    }

    public void ExitShop()
    {
        SceneManager.LoadScene("UI Scene");
    }
}
