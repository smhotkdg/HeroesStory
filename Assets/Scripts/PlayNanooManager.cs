using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayNANOO;
using EasyMobile;
public class PlayNanooManager : MonoBehaviour
{    
    private static PlayNanooManager _instance = null;
    Plugin plugin;

    public delegate void CheckCheatEvent(bool bChect);
    public event CheckCheatEvent CheckCheatEventHandler;

    public delegate void useItemEvent();
    public event useItemEvent useItemEventHandler;


    public delegate void RankRenewCallback();
    public event RankRenewCallback RankRenewCallbackHandler;

    public delegate void RankRenewFailCallback();
    public event RankRenewFailCallback RankRenewFailCallbackHandler;


    public delegate void PersonalRankRenewCallback();
    public event PersonalRankRenewCallback PersonalRankRenewCallbackHandler;

    public delegate void PersonalRankRenewFailCallback();
    public event PersonalRankRenewFailCallback PersonalRankRenewFailCallbackHandler;


    public delegate void RankFailHandler();
    public event RankFailHandler RankFailHandlerEvent;

    public static PlayNanooManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("cSingleton PlayNanooManager == null");
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null)
        {
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        //CheckGift();
        
        GetTime();
        CheckUpdate();
    }
    
    private void OnApplicationPause(bool pause)
    {
        if (pause == true)
        {


        }
        else
        {
            GetTime();
        }
    }
    public void CheckUpdate()
    {
        
    }
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = UnityEngine.JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }
        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return UnityEngine.JsonUtility.ToJson(wrapper);
        }
        [SerializeField]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }

    public void SetServerData()
    {
        //plugin = Plugin.GetInstance();
        //plugin.SetUUID(GameManager.Instance.UserID);
        //if (GameManager.Instance.strNickName != "Empty")
        //{
        //    plugin.SetNickname(GameManager.Instance.strNickName);
        //}
        
        //string strJson = JsonHelper.ToJson();
        ////hoitsave_craft.es3
        //plugin.StorageSave("heroesPos", strJson, false, (state, message, rawData, dictionary) => {
        //    if (state.Equals(Configure.PN_API_STATE_SUCCESS))
        //    {
        //        Debug.Log("Success");
        //    }
        //    else
        //    {
        //        Debug.Log("Fail");
        //    }
        //});
    }

    [SerializeField]
    public void SaveHeroesData()
    {
        plugin = Plugin.GetInstance();        
        plugin.SetUUID(GameManager.Instance.UserID);
        if(GameManager.Instance.strNickName != "Empty")
        {
            plugin.SetNickname(GameManager.Instance.strNickName);
        }
        
        string strJson = JsonHelper.ToJson(GameManager.Instance.heroPos);

        plugin.StorageSave("heroesPos", strJson, false, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
                RankFailHandlerEvent?.Invoke();
            }
        });

    }
    void CheckOtherData()
    {
        plugin = Plugin.GetInstance();
        plugin.SetUUID(GameManager.Instance.UserID);

        if (GameManager.Instance.strNickName != "Empty")
        {
            plugin.SetNickname(GameManager.Instance.strNickName);
        }        

        plugin.Ranking("heroesstory-RANK-F084BA46-8EAC9072", 30, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                ArrayList list = (ArrayList)dictionary["list"];
                int index = 0;
                foreach (Dictionary<string, object> rank in list)
                {
                    int rankIndex = index + 1;
                    Debug.Log(rank["score"]);
                    Debug.Log(rank["data"]);
                    GameManager.RankerData ranker = new GameManager.RankerData();
                    ranker.rank = rankIndex.ToString();
                    ranker.UserID = rank["uuid"].ToString();
                    ranker.nickname = rank["nickname"].ToString();
                    ranker.Stage = rank["score"].ToString();
                    herosGetData = JsonHelper.FromJson<int>(rank["data"].ToString());
                    ranker.HeroPos = herosGetData;
                    GameManager.Instance.rankerDatas[index] = ranker;
                    index++;
                }
                RankRenewCallbackHandler?.Invoke();
            }
            else
            {
                Debug.Log("Fail");
                RankRenewFailCallbackHandler?.Invoke();
            }
        });

        plugin.RankingPersonal("heroesstory-RANK-F084BA46-8EAC9072", (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log(dictionary["ranking"]);
                GameManager.Instance.MaxLevel_rank_Nanoo = int.Parse(dictionary["ranking"].ToString());
                Debug.Log(dictionary["total_player"]);
                PersonalRankRenewCallbackHandler?.Invoke();
            }
            else
            {
                Debug.Log("Fail");
                PersonalRankRenewFailCallbackHandler?.Invoke();
            }
        });
    }
    public void CheckRank()
    {
        plugin = Plugin.GetInstance();
        plugin.SetUUID(GameManager.Instance.UserID);

        if (GameManager.Instance.strNickName != "Empty")
        {
            plugin.SetNickname(GameManager.Instance.strNickName);
        }
        string strJson = JsonHelper.ToJson(GameManager.Instance.heroPos);

        plugin.RankingRecord("heroesstory-RANK-F084BA46-8EAC9072", GameManager.Instance.MaxLevel_rank, strJson, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                Debug.Log("Success");
                CheckOtherData();
            }
            else
            {
                Debug.Log("Fail");
                RankRenewFailCallbackHandler?.Invoke();
            }
        });
      
    }
    public int [] herosGetData;
    public void GetSaveHeroData()
    {
        plugin = Plugin.GetInstance();
        plugin.SetUUID(GameManager.Instance.UserID);
        if (GameManager.Instance.strNickName != "Empty")
        {
            plugin.SetNickname(GameManager.Instance.strNickName);
        }

        plugin.StorageLoad("heroesPos", (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                //Debug.Log(dictionary["value"]);

                herosGetData = JsonHelper.FromJson<int>(dictionary["value"].ToString());
                //Debug.Log("<color=red>"+ herosGetData + "</color>");

            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    public void CheckGift(GameObject panel)
    {
        
        plugin = Plugin.GetInstance();
        plugin.SetUUID(GameManager.Instance.UserID);
        plugin.SetLanguage(Configure.PN_LANG_KO);
        if (GameManager.Instance.strNickName != "Empty")
        {
            plugin.SetNickname(GameManager.Instance.strNickName);
        }
        // 실행

        plugin.AccessEvent((state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                if (dictionary.ContainsKey("postbox_count"))
                {
                    if (int.Parse(dictionary["postbox_count"].ToString()) > 0)
                    {
                        panel.SetActive(true);
                    }
                    else
                    {
                        panel.SetActive(false);
                    }

                }
                if (dictionary.ContainsKey("server_timestamp")) Debug.Log(dictionary["server_timestamp"]);
                if (dictionary.ContainsKey("postbox_subscription"))
                {
                    foreach (Dictionary<string, object> subscription in (ArrayList)dictionary["postbox_subscription"])
                    {
                        Debug.Log(subscription["product"]);
                        Debug.Log(subscription["ttl"]);
                    }
                }
            }
            else
            {
                Debug.Log("실패");
            }
        });
        
    }
   
    public void GetMailBox(GameObject panel)
    {

        plugin = Plugin.GetInstance();
        plugin.SetUUID(GameManager.Instance.UserID);
        if (GameManager.Instance.strNickName != "Empty")
        {
            plugin.SetNickname(GameManager.Instance.strNickName);
        }
        // 실행
        plugin.PostboxItem((state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                ArrayList items = (ArrayList)dictionary["item"];
                foreach (Dictionary<string, object> item in items)
                {
                    Debug.Log(item["uid"]);
                    Debug.Log(item["message"]);
                    Debug.Log(item["item_code"]);
                    Debug.Log(item["item_count"]);
                    Debug.Log(item["expire_sec"]);
                    if (int.Parse(item["item_count"].ToString()) > 0)
                    {
                        panel.GetComponent<GiftPanelSrc>().MakeItem(item["item_code"].ToString(), int.Parse(item["item_count"].ToString()), int.Parse(item["expire_sec"].ToString()), item["uid"].ToString());
                    }
                }
            }
            else
            {
                Debug.Log("실패");
            }
        });
    }

    public void UseItem(string itemCode,GameObject item)
    {
        plugin = Plugin.GetInstance();
        plugin.SetUUID(GameManager.Instance.UserID);
        if (GameManager.Instance.strNickName != "Empty")
        {
            plugin.SetNickname(GameManager.Instance.strNickName);
        }
        plugin.SetLanguage(Configure.PN_LANG_KO);

        plugin.PostboxItemUse(itemCode, (state, message, rawData, dictionary) => {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {
                if (item != null)
                {
                    item.GetComponent<GiftItem>().GetReward();
                }
                //useItemEventHandler?.Invoke();
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    public void GetTime()
    {
        plugin = Plugin.GetInstance();
        System.DateTime myTime;
        // 실행
        plugin.ServerTime((state, message, rawData, dictionary) =>
        {
            if (state.Equals(Configure.PN_API_STATE_SUCCESS))
            {                

                int year = int.Parse(dictionary["year"].ToString());
                int month = int.Parse(dictionary["month"].ToString());
                int day = int.Parse(dictionary["day"].ToString());
                int hour = int.Parse(dictionary["hour"].ToString());
                int minute = int.Parse(dictionary["Minute"].ToString());
                int second = int.Parse(dictionary["second"].ToString());

                myTime = new System.DateTime(year, month, day, hour, minute, second);
                //Debug.Log("<color=red>" + myTime + "</color>");
                //Debug.Log("<color=blue>" + System.DateTime.UtcNow + "</color>");

                System.TimeSpan travelTime = System.DateTime.UtcNow - myTime;
                //Debug.Log("travelTime: " + travelTime.TotalSeconds);
                if (travelTime.TotalSeconds > 600)
                {
                    //치팅
                    //GameManager.Instance.bChect = true;
                    //TimerManager.Instance.OldTime = myTime;
                    CheckCheatEventHandler?.Invoke(true);
                }
                else
                {
                    //TimerManager.Instance.CheckRewarad();
                    //TimerManager.Instance.CheckTime();
                    //GameManager.Instance.bChect = false;
                    CheckCheatEventHandler?.Invoke(false);
                 
                    
                }
            }
            else
            {
                CheckCheatEventHandler?.Invoke(true);
            }
        });
    }
}
