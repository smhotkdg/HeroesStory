using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GR_InfiniCoin;
public class MapStageClearSrc : MonoBehaviour
{
    public List<Sprite> MonsterList;
    public Image MonsterImage;
    public List<Image> GetSomethingSprite;
    public List<Text> GetSomethingText;

    public GameObject TouchText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DIsableObject()
    {
        if(TouchText.activeSelf ==true)
        {
            UiManager.Instance.GatchaPanel.SetActive(true);
            UiManager.Instance.GatchaPanel.GetComponent<GatchaManager>().gatchaType = GatchaManager.GatchaType.Speacial;
            this.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    int stageIndex;
    public void SetData(int index)
    {
        stageIndex = index-1;
        InfiniCoin coin = GameManager.Instance.GetShopCoin();  

        InfiniCoin Coin_3 = coin * 300000 * 5;
        GetSomethingText[1].text = UiManager.Instance.SetCost(Coin_3);
        GameManager.Instance.TotalGold += Coin_3;
        GameManager.Instance.Save(GameManager.saveType.TotalGold);
        switch (stageIndex)
        {
            case 0:
                MonsterImage.sprite = MonsterList[0];
                GetSomethingText[0].text = "x 500";
                GameManager.Instance.TotalAltarCoin += 500;
                GameManager.Instance.Save(GameManager.saveType.TotalAltarCoin);

                break;
            case 1:
                MonsterImage.sprite = MonsterList[1];
                GetSomethingText[0].text = "x 1000";
                GameManager.Instance.TotalAltarCoin += 1000;
                GameManager.Instance.Save(GameManager.saveType.TotalAltarCoin);
                break;
            case 2:
                MonsterImage.sprite = MonsterList[2];
                GetSomethingText[0].text = "x 1500";
                GameManager.Instance.TotalAltarCoin += 1500;
                GameManager.Instance.Save(GameManager.saveType.TotalAltarCoin);
                break;
            case 3:
                MonsterImage.sprite = MonsterList[3];
                GetSomethingText[0].text = "x 2000";
                GameManager.Instance.TotalAltarCoin += 2000;
                GameManager.Instance.Save(GameManager.saveType.TotalAltarCoin);
                break;
            case 4:
                MonsterImage.sprite = MonsterList[4];
                GetSomethingText[0].text = "x 2500";
                GameManager.Instance.TotalAltarCoin += 2500;
                GameManager.Instance.Save(GameManager.saveType.TotalAltarCoin);
                break;
        }

        
    }
}
