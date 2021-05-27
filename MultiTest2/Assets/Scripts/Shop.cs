using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour

{
    public RectTransform uiGroup;
    public Animator anim;
    public GameObject[] itemObj;
    public int[] itemPrice;
    public Transform[] itemPos;
    public Text talkText;
    public string[] talkData;

    public GameObject CheckBuyButton;
    public Text ItemName;
    public Text ItemPrice;

    public Text myNoondung;
    private int price;

    //Player enterPlayer;

    private void Update() {
        myNoondung.text = $"{MainPlayer.noondung}";
    }
    
    // Start is called before the first frame update
    /*
    public void Enter(Player player)
    {
        enterPlayer = player;
        uiGroup.anchoredPosition = Vector3.zero;
    }
    */
    

    // Update is called once per frame
    public void Exit()
    {   
       // anim.SetTrigger("doHello");
        uiGroup.anchoredPosition = Vector3.down * 1800;   
    }
    
    public void checkBuyActive(int index)
    {
        
        price = itemPrice[index];
        ItemName.text = itemObj[index].name;
        ItemPrice.text = price.ToString();
        CheckBuyButton.SetActive(true);
    }

    public void checkBuyFalse()
    {
        CheckBuyButton.SetActive(false);
    }
    public void Buy()
    {
        //int price = itemPrice[index];
        //ItemName.text = itemObj[index].name;
       // ItemPrice.text = price.ToString();


        if(price > MainPlayer.noondung) { //돈이 모자를 때
            CheckBuyButton.SetActive(false);
            StopCoroutine(Talk(1));
            StartCoroutine(Talk(1));//꼬이지 않도록
            return;
        }
        else
        {
            CheckBuyButton.SetActive(false);
            MainPlayer.noondung -= price;
            StopCoroutine(Talk(2));
            StartCoroutine(Talk(2));//꼬이지 않도록
           
        }
        //Vector3 ranVec = Vector3.right * Random.Range(-3, 3) + Vector3.forward * Random.Range(-3,3);
        
        //실제 게임 오벡트 만들기 
        //Instantiate(itemObj[index], itemPos[index].position + ranVec, itemPos[index].rotation);
    }

    IEnumerator Talk(int index)
    {
        talkText.text = talkData[index];
        yield return new WaitForSeconds(2f);
        talkText.text = talkData[0];
    }

}
