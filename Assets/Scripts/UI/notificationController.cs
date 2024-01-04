using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class notificationController : MonoBehaviour
{
    public enum NotificationType
    {
        heroList,
        buff,
        achivement,
        levelup,
        raid,
        expedition,
        transcendence,
        altar,
        shop,
        place,
        craft,
        inventory,
        collection
    };

    GameManager gameManager;
    public List<GameObject> NotificationList;

    private void Start()
    {
        gameManager = GameManager.Instance;
        StartCoroutine(CheckNotificaitonRoutine());
    }
    IEnumerator CheckNotificaitonRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.5f);
            CheckNotification();
        }
    }
    bool getExpeditionCheck()
    {
        for(int i =0; i< gameManager.timerCotroller.ExpedtionTime.Count; i++)
        {
            if(gameManager.timerCotroller.ExpedtionTime[i] <=0 || gameManager.heroExpeditionComplete[i] >-1)
            {
                return true;
            }            
        }
        return false;
    }
    bool getCheckCraft()
    {
        for(int i =0; i< gameManager.Scrolls.Count; i++)
        {
            if(gameManager.Scrolls[i].isGet ==true && gameManager.Scrolls[i].count >=1)
            {
                int tier = gameManager.Scrolls[i].itemTier;
                int mtier = tier * 25;
                int aTier = tier * 10;

                if(gameManager.materialCount >= 50+mtier && gameManager.TotalAltarCoin >= 25+aTier)
                {
                    return true;
                }
            }
        }
        return false;
    }
    bool levelUpCheck()
    {
        for(int i =0; i< gameManager.heroPos.Length; i++)
        {
            if(gameManager.heroPos[i] >-1)
            {
                if (gameManager.TotalGold >= gameManager.GetHeroCost(gameManager.heroPos[i], 1))
                {
                    return true;
                }
            }
        }
        if(gameManager.TotalGold>= 5 * SystemManager.mathPow(1.07f, GameManager.Instance.clicker.level+1))
        {
            return true;
        }
        return false;
    }
    void CheckNotification()
    {
        if(gameManager !=null)
        {
            if(gameManager.IsNewCollection ==true)
            {
                NotificationList[(int)NotificationType.collection].SetActive(true);
            }
            else
            {
                NotificationList[(int)NotificationType.collection].SetActive(false);
            }
            int costIndex =-1;
            for (int i = 0; i < GameManager.Instance.heroPos.Length; i++)
            {
                if (GameManager.Instance.heroPos[i] == -2)
                {
                    costIndex++;
                }
            }
            if (costIndex == -1)
            {
                NotificationList[(int)NotificationType.place].SetActive(false);
            }
            else
            {
                if (costIndex == 0 || costIndex == 1)
                {
                    if (gameManager.TotalGem >= GameManager.Instance.GetSlotCost(costIndex))
                    {
                        NotificationList[(int)NotificationType.place].SetActive(true);
                    }
                    else
                    {
                        NotificationList[(int)NotificationType.place].SetActive(false);
                    }

                }
                else
                {
                    if (gameManager.TotalGold >= GameManager.Instance.GetSlotCost(costIndex))
                    {
                        NotificationList[(int)NotificationType.place].SetActive(true);
                    }
                    else
                    {
                        NotificationList[(int)NotificationType.place].SetActive(false);
                    }
                }
            }

            //new item Check
            if (gameManager.isNewItem == true)
            {
                if (NotificationList[(int)NotificationType.inventory].activeSelf == false)
                {
                    NotificationList[(int)NotificationType.inventory].SetActive(true);
                }
            }
            else
            {
                if (NotificationList[(int)NotificationType.inventory].activeSelf == true)
                {
                    NotificationList[(int)NotificationType.inventory].SetActive(false);
                }
            }

            //Craft Check
            if(getCheckCraft() ==true)
            {
                if (NotificationList[(int)NotificationType.craft].activeSelf == false)
                {
                    NotificationList[(int)NotificationType.craft].SetActive(true);
                }
            }
            else
            {
                if (NotificationList[(int)NotificationType.craft].activeSelf == true)
                {
                    NotificationList[(int)NotificationType.craft].SetActive(false);
                }
            }        

            //if(gameManager.)
            //new hero Check
            if (gameManager.isNewHero == true)
            {
                if (NotificationList[(int)NotificationType.heroList].activeSelf == false)
                {
                    NotificationList[(int)NotificationType.heroList].SetActive(true);
                }
            }
            else
            {
                if (NotificationList[(int)NotificationType.heroList].activeSelf == true)
                {
                    NotificationList[(int)NotificationType.heroList].SetActive(false);
                }
            }

            //buff Check//
            if (gameManager.timerCotroller.bStart_attackBuff == false)
            {
                if (NotificationList[(int)NotificationType.buff].activeSelf == false)
                {
                    NotificationList[(int)NotificationType.buff].SetActive(true);
                }
            }
            else
            {
                if (NotificationList[(int)NotificationType.buff].activeSelf == true)
                {
                    NotificationList[(int)NotificationType.buff].SetActive(false);
                }
            }

            //achivement Check
            if(GameManager.Instance.isAchivement() == true)
            {
                if (NotificationList[(int)NotificationType.achivement].activeSelf == false)
                {
                    NotificationList[(int)NotificationType.achivement].SetActive(true);
                }              
            }
            else
            {
                if (NotificationList[(int)NotificationType.achivement].activeSelf == true)
                {
                    NotificationList[(int)NotificationType.achivement].SetActive(false);
                }
            }
            // levelup Check
            if (levelUpCheck() == true)
            {
                if (NotificationList[(int)NotificationType.levelup].activeSelf == false)
                {
                    NotificationList[(int)NotificationType.levelup].SetActive(true);
                }
            }
            else
            {
                if (NotificationList[(int)NotificationType.levelup].activeSelf == true)
                {
                    NotificationList[(int)NotificationType.levelup].SetActive(false);
                }
            }

            // Raid check
            if(GameManager.Instance.raidTicketCount >0)
            {
                if (NotificationList[(int)NotificationType.raid].activeSelf == false)
                {
                    NotificationList[(int)NotificationType.raid].SetActive(true);
                }
            }
            else
            {
                if (NotificationList[(int)NotificationType.raid].activeSelf == true)
                {
                    NotificationList[(int)NotificationType.raid].SetActive(false);
                }
            }
            //expedition check
            int expIndex = -1;
            for(int i =0; i< GameManager.Instance.heroExpeditionPos.Length; i++)
            {
                if(GameManager.Instance.heroExpeditionPos[i] == -2)
                {
                    expIndex = i;
                    break;
                }
            }
            bool bExp = false;
            if (expIndex != 6 && expIndex != 7)
            {
                if(gameManager.TotalGold >= gameManager.GetExpCost(expIndex))
                {
                    bExp = true;
                }
            }
            else
            {
                if (gameManager.TotalGem >= gameManager.GetExpCost(expIndex))
                {
                    bExp = true;
                }
            }


            if (getExpeditionCheck() ==true || bExp ==true)
            {
                if (NotificationList[(int)NotificationType.expedition].activeSelf == false)
                {
                    NotificationList[(int)NotificationType.expedition].SetActive(true);
                }
            }
            else
            {
                if (NotificationList[(int)NotificationType.expedition].activeSelf == true)
                {
                    NotificationList[(int)NotificationType.expedition].SetActive(false);
                }
            }
            // transcendence check
            if (gameManager.timerCotroller.bTrasnTicketTime == false)
            {
                if (NotificationList[(int)NotificationType.transcendence].activeSelf == false)
                {
                    NotificationList[(int)NotificationType.transcendence].SetActive(true);
                }
            }
            else
            {
                if (NotificationList[(int)NotificationType.transcendence].activeSelf == true)
                {
                    NotificationList[(int)NotificationType.transcendence].SetActive(false);
                }
            }

            // altar check
            if (gameManager.TotalAltarCoin >0)
            {
                if (NotificationList[(int)NotificationType.altar].activeSelf == false)
                {
                    NotificationList[(int)NotificationType.altar].SetActive(true);
                }
            }
            else
            {
                if (NotificationList[(int)NotificationType.altar].activeSelf == true)
                {
                    NotificationList[(int)NotificationType.altar].SetActive(false);
                }
            }

            //shop check
            if (gameManager.timerCotroller.bStart_normal ==false || gameManager.timerCotroller.bStart_Speacial ==false || gameManager.timerCotroller.bAdsGoldTime ==false)
            {
                if(NotificationList[(int)NotificationType.shop].activeSelf ==false)
                {
                    NotificationList[(int)NotificationType.shop].SetActive(true);
                }                
            }
            else
            {
                if (NotificationList[(int)NotificationType.shop].activeSelf == true)
                {
                    NotificationList[(int)NotificationType.shop].SetActive(false);
                }                    
            }
        }
    }
}
