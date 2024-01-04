using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RankManager : MonoBehaviour
{
    public Text NicknameText;
    public Text RankText;

    public List<GameObject> Top10List;
    public List<GameObject> Bottom20List;
    public Text RenewTimeText;
    public Button RenewButton;
    public GameObject LoadingUI;
    // Start is called before the first frame update
    void Start()
    {
        PlayNanooManager.Instance.RankRenewCallbackHandler += Instance_RankRenewCallbackHandler;
        PlayNanooManager.Instance.RankRenewFailCallbackHandler += Instance_RankRenewFailCallbackHandler;

        PlayNanooManager.Instance.PersonalRankRenewCallbackHandler += Instance_PersonalRankRenewCallbackHandler;

        PlayNanooManager.Instance.PersonalRankRenewFailCallbackHandler += Instance_PersonalRankRenewFailCallbackHandler;
    }

    private void Instance_PersonalRankRenewFailCallbackHandler()
    {
        LoadingUI.SetActive(false);
        UiManager.Instance.SetNotification(UiManager.NotificationType.RankFail);
    }

    private void Instance_PersonalRankRenewCallbackHandler()
    {
        SetRankDataPersonal();
    }

    private void Instance_RankRenewFailCallbackHandler()
    {        
        LoadingUI.SetActive(false);
        UiManager.Instance.SetNotification(UiManager.NotificationType.RankFail);
    }

    private void Instance_RankRenewCallbackHandler()
    {
        SetRankData();
        StartCoroutine(renewRoutine());
        
    }
    IEnumerator renewRoutine()
    {
        yield return new WaitForSeconds(3f);
        LoadingUI.SetActive(false);
        UiManager.Instance.SetNotification(UiManager.NotificationType.RankSucess);
    }

    private void FixedUpdate()
    {
        if(GameManager.Instance.rankRenewTime <=0)
        {
            RenewButton.image.color = UiManager.Instance.enableButtonColor;
            RenewTimeText.text = "갱신 가능";
        }
        else
        {
            RenewButton.image.color = UiManager.Instance.disableButtonColor;
            RenewTimeText.text = GameManager.Instance.getTime(GameManager.Instance.rankRenewTime)+ " 후 갱신 가능";
        }
    }
    private void OnEnable()
    {
        //PlayNanooManager.Instance.SaveHeroesData();
        SetRankData();
        SetRankDataPersonal();
    }
    public void Test()
    {
        if (GameManager.Instance.strNickName == "Empty")
        {
            UiManager.Instance.SetNotification(UiManager.NotificationType.MakeNickname);
            return;
        }
        if (GameManager.Instance.rankRenewTime>0)
        {
            return;
        }
        if(GameManager.Instance.MaxLevel_rank <5)
        {
            UiManager.Instance.SetNotification(UiManager.NotificationType.needRank5);
            return;
        }
        LoadingUI.SetActive(true);

        PlayNanooManager.Instance.CheckRank();

        GameManager.Instance.rankRenewTime = (1800)+1;
        //PlayNanooManager.Instance.SaveHeroesData();
        GameManager.Instance.Save(GameManager.saveType.RankTime);
        
        
        //PlayNanooManager.Instance.GetSaveHeroData();
    }
    void SetRankDataPersonal()
    {
        NicknameText.text = GameManager.Instance.strNickName;
        RankText.text = "RANK. " + GameManager.Instance.MaxLevel_rank_Nanoo + " (Stage. " + GameManager.Instance.MaxLevel_rank + ")";
        GameManager.Instance.Save(GameManager.saveType.MaxrankNanoo);
    }
    void SetRankData()
    {        
        for (int i=0; i< Top10List.Count; i++)
        {
            Top10List[i].GetComponent<RankObject>().setData(i);
        }
        for (int i = 0; i < Bottom20List.Count; i++)
        {
            Bottom20List[i].GetComponent<RankObject>().setData(10+i);
        }

        GameManager.Instance.Save(GameManager.saveType.RankData);
    }
}
