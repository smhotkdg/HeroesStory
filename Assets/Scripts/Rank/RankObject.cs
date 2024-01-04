using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RankObject : MonoBehaviour
{
 
    public List<GameObject> HerosImageList;
    public List<Text> GradeList;
    public List<Text> NameList;

    public Text RankTitleText;
    public Text NicknameText;
    public enum RankType
    {
        top,
        normal        
    }
    public RankType rankType = RankType.top;
    public void setData(int index)
    {
        if(rankType == RankType.top)
        {
            if(GameManager.Instance.rankerDatas[index].UserID!="")
            {
                for(int i =0; i< 10;i++)
                {
                    if(GameManager.Instance.rankerDatas[index].HeroPos[i] <0)
                    {
                        HerosImageList[i].SetActive(false);
                        GradeList[i].gameObject.SetActive(false);
                        NameList[i].gameObject.SetActive(false);
                    }
                    else
                    {
                        HerosImageList[i].SetActive(true);
                        GradeList[i].gameObject.SetActive(true);
                        NameList[i].gameObject.SetActive(true);
                        UiManager.Instance.SetHeroIcon_UI(HerosImageList[i].GetComponent<Image>(), GameManager.Instance.rankerDatas[index].HeroPos[i]);
                        UiManager.Instance.SetHeroName_UI(NameList[i], GameManager.Instance.rankerDatas[index].HeroPos[i]);
                        UiManager.Instance.SetTier_UIText(GradeList[i], GameManager.Instance.rankerDatas[index].HeroPos[i]);
                    }
                }
                RankTitleText.text = "<color=yellow>RANK. " + GameManager.Instance.rankerDatas[index].rank + "</color>" + " (Stage. " + GameManager.Instance.rankerDatas[index].Stage + ")";
                NicknameText.text = GameManager.Instance.rankerDatas[index].nickname;

            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    if (GameManager.Instance.rankerDatas[index].HeroPos[i] < 0)
                    {
                        HerosImageList[i].SetActive(false);
                        GradeList[i].gameObject.SetActive(false);
                        NameList[i].gameObject.SetActive(false);
                    }                 
                }
                RankTitleText.text = "갱신 필요";
                NicknameText.text = "갱신 필요";
            }

            
        }
        else
        {
            if (GameManager.Instance.rankerDatas[index].UserID != "")
            {
                RankTitleText.text = "<color=yellow>RANK. " + GameManager.Instance.rankerDatas[index].rank+"</color>" + " (Stage. " + GameManager.Instance.rankerDatas[index].Stage + ")";
                NicknameText.text = GameManager.Instance.rankerDatas[index].nickname;
            }
            else
            {
                RankTitleText.text = "갱신 필요";
                NicknameText.text = "갱신 필요";
            }
        }
    }
  
}
