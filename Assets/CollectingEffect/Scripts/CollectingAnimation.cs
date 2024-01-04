﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GR_InfiniCoin;

public class CollectingAnimation : MonoBehaviour {

	public enum PLAY_SOUND_MODE { NONE, AT_BEGINNING, AT_THE_END }
    public enum EXPANSION_MODE { UPWARD, EXPLOSION }

    public enum GetCollectionType
    {
        gold,
        gem,
        non
    }
    public GetCollectionType getCollectionType = GetCollectionType.non;
    // Factor to adujst upper translation during animation
    [Tooltip("Factor to adujst upper translation during animation")]
	public float _moveUpFactor = 8.0f;
	// Factor to adujst horizontal translation during animation
	[Tooltip("Factor to adujst horizontal translation during animation")]
	public float _moveHorizontalFactor = 3.0f;
	// Factor to adjust scaling of the item while approching destination
	[Tooltip("Factor to adjust scaling of the item while approching destination")]
	public float _scaleDiminutionFactor = 2.0f;
    // Duration of the expansion animation in seconds
    [Tooltip("Duration of the expansion animation in seconds")]
    public float _expansionDuration = 1.0f;
    // Parameter to adujst animation speed
    [Tooltip("Parameter to adujst animation speed")]
	public float _animationSpeed = 2.0f;
	// Reference to the image component of the object
	[Tooltip("Reference to the image component of the object")]
	public Image _image;

	// Flag telling if animation is running or not for this item
	[HideInInspector]
	public bool _animationRunning = false;

	// Reference to the collecting Effect Controller
	private CollectingEffectController _collectinEffectController;
	// The transform component of the currency displayer
	private Transform _itemDisplayerTransform;
	// Reference to the ItemDisplayer component, showing the amount of item collected
	private ItemDisplayer _itemDisplayer;
	// Defines when to play the collecting sound
	private PLAY_SOUND_MODE _playSoundMode;
    // Defines the expansion mode
    private EXPANSION_MODE _expansionMode;

    Rigidbody2D rigidbody2;
    Vector2 pos;
    bool bStartMouseCheck;
    private void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        bStartMouseCheck = false;
        if (getCollectionType == GetCollectionType.gold)
        {
            DealyTime = 1.5f;
        }
        else
        {
            DealyTime = 1;            
        }
    }
    float DealyTime;
    
    public void Click()
    {       
        if(bStartMouseCheck ==true)
        {            
            DealyTime = 0;
            StartCoroutine(MoveStart());
        }
       
    }

    private void Update()
    {
        if (pos.y - 100 > transform.localPosition.y)
        {
            if (getCollectionType == GetCollectionType.gold)
            {
                rigidbody2.constraints = RigidbodyConstraints2D.FreezeAll;
                rigidbody2.gravityScale = 0;
            }
        }    
    }
    InfiniCoin myCost = new InfiniCoin();
    // Initialize this item
    public void Initialize(Transform destination, Transform parent, Vector3 localPosition, Vector3 localScale, PLAY_SOUND_MODE playSoundMode, EXPANSION_MODE expansionMode, CollectingEffectController collectingEffectController
        ,InfiniCoin cost) {
        myCost = cost;
        if (getCollectionType == GetCollectionType.gold)
        {
            rigidbody2 = GetComponent<Rigidbody2D>();
            rigidbody2.constraints = RigidbodyConstraints2D.None;
            rigidbody2.gravityScale = 1f;
        }                    
        _itemDisplayerTransform = destination;
		_itemDisplayer = _itemDisplayerTransform.GetComponent<ItemDisplayer> ();
		transform.SetParent(parent);
		transform.localPosition = localPosition;
        pos = transform.localPosition;
        transform.localScale = localScale;
		_playSoundMode = playSoundMode;
        _expansionMode = expansionMode;
        _collectinEffectController = collectingEffectController;
        bStartMouseCheck = false;      
    }

	// Start the animation for this item
	public void StartAnimation() {
		_animationRunning = true;
		_image.enabled = true;
        if (getCollectionType == GetCollectionType.gold)
        {
            DealyTime = 1.5f;
        }
        else
        {
            DealyTime = 1;
        }
        StartCoroutine ("CollectAnimation");
	}

	// Main loop during animation
	IEnumerator CollectAnimation() {
		float t = 0;
		float speed = 1.0f;

		// Playing sound at beginning of the animation if relevant
		if (_playSoundMode == PLAY_SOUND_MODE.AT_BEGINNING) {			
			_collectinEffectController.PlayCollectingSound ();
		}

        // 1st step : Move up animation
        Vector3 direction;
        if (_expansionMode == EXPANSION_MODE.UPWARD)
        {
            direction = new Vector3((Random.value - 0.5f) * _moveHorizontalFactor, _moveUpFactor, 0.0f);
        } else
        {
            //direction = new Vector3((Random.value - 0.5f) * _moveHorizontalFactor, (Random.value - 0.5f) * _moveUpFactor, 0.0f);
            direction = new Vector3((Random.value-0.5f) * _moveHorizontalFactor, (Random.value) * _moveUpFactor, 0.0f);
        }

        while (t < _expansionDuration)
        {
            t += Time.deltaTime * _animationSpeed;
            if (_expansionMode == EXPANSION_MODE.UPWARD)
            {
                transform.position += Vector3.Scale(direction, new Vector3(1, speed, 1));
            }
            else
            {
                transform.position += Vector3.Scale(direction, new Vector3(speed, speed, 1));
            }
            speed = Mathf.Exp(-4 * t / _expansionDuration);
            if (getCollectionType == GetCollectionType.gold)
            {
                if (rigidbody2.gravityScale == 0)
                    break;
            }
            yield return new WaitForFixedUpdate();
        }
        bStartMouseCheck = true;
        if (getCollectionType == GetCollectionType.gold)
        {
            yield return new WaitForSeconds(2);
        }            
		if(DealyTime !=0)
        {
            if (getCollectionType == GetCollectionType.gold)
            {
                UiManager.Instance.SetTextPool(this.gameObject, myCost, UiTargetManager.TextType.gold,false,null);
            }
            // 2nd step : Move to destination
            t = 0;
            Vector3 pos = transform.position;
            Vector3 scale = transform.localScale / _scaleDiminutionFactor;
            while (t < 1.0f)
            {
                t += Time.deltaTime * _animationSpeed;
                transform.position = Vector3.Lerp(pos, _itemDisplayerTransform.position, t);
                transform.localScale = Vector3.Lerp(transform.localScale, scale, t);
                yield return new WaitForFixedUpdate();
            }

            // Playing sound at the end of the animation if relevant
            if (_playSoundMode == PLAY_SOUND_MODE.AT_THE_END)
            {
                _collectinEffectController.PlayCollectingSound();
            }

            // Adding the gem
            //_itemDisplayer.AddItem (1);
            //baseClicker.Instance.TotalGold += myCost;
            if (getCollectionType == GetCollectionType.gold)
            {
                if (GameManager.Instance.startTrans == true)
                {
                    myCost = 0;
                }
                GameManager.Instance.TotalGold += myCost;
                //GameManager.Instance.Save(GameManager.saveType.TotalGold);
                UiManager.Instance.SetGoldText();
            }
            
            //UiManager.Instance.CheckNewHero();
            _animationRunning = false;
            // Hide this item until next reuse
            _image.enabled = false;
            yield return null;
        }
        if (getCollectionType == GetCollectionType.gold)
        {
            bStartMouseCheck = false;
            rigidbody2.gravityScale = 1f;
        }

    }
    IEnumerator MoveStart()
    {
        // 2nd step : Move to destination
        if (getCollectionType == GetCollectionType.gold)
        {
            UiManager.Instance.SetTextPool(this.gameObject, myCost, UiTargetManager.TextType.gold,false,null);
        }
        float t = 0;
        t = 0;
        Vector3 pos = transform.position;
        Vector3 scale = transform.localScale / _scaleDiminutionFactor;
        while (t < 1.0f)
        {
            t += Time.deltaTime * _animationSpeed;
            transform.position = Vector3.Lerp(pos, _itemDisplayerTransform.position, t);
            transform.localScale = Vector3.Lerp(transform.localScale, scale, t);
            yield return new WaitForFixedUpdate();
        }

        // Playing sound at the end of the animation if relevant
        if (_playSoundMode == PLAY_SOUND_MODE.AT_THE_END)
        {
            _collectinEffectController.PlayCollectingSound();
        }

        // Adding the gem
        //_itemDisplayer.AddItem (1);
        if(getCollectionType == GetCollectionType.gold)
        {
            if(GameManager.Instance.startTrans ==true)
            {
                myCost = 0;
            }
            GameManager.Instance.TotalGold += myCost;
            //GameManager.Instance.Save(GameManager.saveType.TotalGold);
            UiManager.Instance.SetGoldText();
            bStartMouseCheck = false;
            rigidbody2.gravityScale = 1f;

        }
        
        //UiManager.Instance.CheckNewHero();
        _animationRunning = false;
        // Hide this item until next reuse
        _image.enabled = false;
        
        
        yield return null;
    }
}
