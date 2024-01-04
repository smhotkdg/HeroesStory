using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GR_InfiniCoin;
public class unlockSlotPanel : MonoBehaviour
{
    enum costType
    {
        gold,
        gem
    }
    
    public Image CostType;
    public Text CostText;
    public Button BuyButton;
    public int SelectHeroPos = -1;
    private void Start()
    {
        
    }
    private void OnEnable()
    {
        CheckCost();
    }
    int costIndex = -1;
    void CheckCost()
    {
        
        costIndex = -1;
      
        
        for(int i =0; i< GameManager.Instance.heroPos.Length;i++)
        {
            if(GameManager.Instance.heroPos[i] ==-2)
            {
                costIndex++;
            }
        }        
        CostText.text = UiManager.Instance.SetCost(GameManager.Instance.GetSlotCost(costIndex));
        if(costIndex == 0 || costIndex ==1)
        {
            UiManager.Instance.SetIcon(CostType, UiManager.iconType.gem);
          
        }
        else
        {
            UiManager.Instance.SetIcon(CostType, UiManager.iconType.gold);
        }
    }
    private void FixedUpdate()
    {
        if (costIndex == 0 || costIndex == 1)
        {
            if (GameManager.Instance.TotalGem >= GameManager.Instance.GetSlotCost(costIndex))
            {
                BuyButton.image.color = UiManager.Instance.enableButtonColor;
                CostText.color = UiManager.Instance.enableButtonColor;
                CostType.color = UiManager.Instance.enableButtonColor;
            }
            else
            {
                BuyButton.image.color = UiManager.Instance.disableButtonColor;
                CostText.color = UiManager.Instance.disableButtonColor;
                CostType.color = UiManager.Instance.disableButtonColor;
            }

        }
        else
        {
            if (GameManager.Instance.TotalGold >= GameManager.Instance.GetSlotCost(costIndex))
            {
                BuyButton.image.color = UiManager.Instance.enableButtonColor;
                CostText.color = UiManager.Instance.enableButtonColor;
                CostType.color = UiManager.Instance.enableButtonColor;
            }
            else
            {
                BuyButton.image.color = UiManager.Instance.disableButtonColor;
                CostText.color = UiManager.Instance.disableButtonColor;
                CostType.color = UiManager.Instance.disableButtonColor;
            }
        }
    
    }

    public void BuySlot()
    {
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
        if (costIndex == -1)
        {
            return;
        }
        if (costIndex == 0 || costIndex == 1)
        {
            if(GameManager.Instance.TotalGem >= GameManager.Instance.GetSlotCost(costIndex))
            {
                GameManager.Instance.heroPos[SelectHeroPos] = -1;
                GameManager.Instance.TotalGem -= GameManager.Instance.GetSlotCost(costIndex);
                GameManager.Instance.Save(GameManager.saveType.TotalGem);
                //UiManager.Instance.setgem
                UiManager.Instance.SetGemText();
                GameManager.Instance.setSelectPosNormal();
                gameObject.SetActive(false);
            }
            else
            {
                UiManager.Instance.SetNotification(UiManager.NotificationType.gem);
            }
        }
        else
        {
            if(GameManager.Instance.TotalGold >= GameManager.Instance.GetSlotCost(costIndex))
            {
                GameManager.Instance.heroPos[SelectHeroPos] = -1;
                GameManager.Instance.TotalGold -= GameManager.Instance.GetSlotCost(costIndex);
                UiManager.Instance.SetGoldText();
                GameManager.Instance.setSelectPosNormal();
                gameObject.SetActive(false);
                GameManager.Instance.Save(GameManager.saveType.TotalGold);
            }

            else
            {
                UiManager.Instance.SetNotification(UiManager.NotificationType.gold);
            }            
        }
    }
}
