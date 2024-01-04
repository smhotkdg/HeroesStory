using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GR_InfiniCoin;
public class CollectingEffectController : MonoBehaviour {

    
	// Play collecting sound at begining of the animation or at the end
	[Tooltip("Play collecting sound at begining of the animation or at the end ?")]
	public CollectingAnimation.PLAY_SOUND_MODE _playSoundMode;
    [Tooltip("Define the expansion animation mode")]
    public CollectingAnimation.EXPANSION_MODE _expansionMode = CollectingAnimation.EXPANSION_MODE.UPWARD;
    // The emission rate in seconds
    [Tooltip("The emission rate in seconds")]
	public float _emissionRate = 0.2f;
	// The tranform component of the item displayer
	[Tooltip("The tranform component of the item displayer")]
	public Transform _itemDisplayer;
	// The position where to pop the items
	[Tooltip("The position where to pop the items")]
	public Transform _popPosition;
	// The prefab of the items to instanciate
	[Tooltip("The prefab of the items to instanciate")]
	public GameObject _itemPrefab;
	// Instance of this class
	[HideInInspector]
	public static CollectingEffectController _instance;

    List<GameObject> PopUPlIst = new List<GameObject>();
	// This is a list of instanciated _itemPrefab 
	public List<CollectingAnimation> _itemList = new List<CollectingAnimation>();
	// Reference to the AudioSource component
	private AudioSource _audioSource;

	void Awake() {
		// Setting instance
		_instance = this;
		_audioSource = GetComponent<AudioSource> ();
        for(int i =0; i< 70; i++)
        {
            CollectingAnimation animation = null;
            GameObject go = Instantiate(_itemPrefab) as GameObject;
            animation = go.GetComponent<CollectingAnimation>();
            go.transform.SetParent(_popPosition);
            go.transform.localScale = new Vector3(1, 1, 1);
            go.SetActive(false);
            _itemList.Add(animation);
        }
        iIndex = 0;

    }

	// Collect some items with animation
	public void CollectItem(int quantity,InfiniCoin cost) {
		StartCoroutine (PopItems(quantity,cost));
	}

	// Collect some items with animation at a fixed position
	public void CollectItemAtPosition(int quantity, Vector3 position,InfiniCoin cost) {
		// Set the position
		_popPosition.position = position;
		StartCoroutine (PopItems(quantity,cost));
	}

    // Here we pop all the necessary items
    public int iIndex;
	IEnumerator PopItems(int quantity,InfiniCoin cost) {
		WaitForSeconds delay = new WaitForSeconds (_emissionRate);
		for (int i = 0; i < quantity; i++) {
			CollectingAnimation animation = null;
			if(iIndex >= _itemList.Count) {
                iIndex = 0;                                                
            }
            animation = _itemList[iIndex];
            _itemList[iIndex].gameObject.SetActive(true);
                  

            iIndex++;

            // Initialize object
            animation.Initialize(_itemDisplayer, _popPosition, Vector3.zero, Vector3.one, _playSoundMode, _expansionMode, this,cost);
			// Start animation
			animation.StartAnimation();
			yield return delay;
		}
	}

	// Play the collecting sound
	public void PlayCollectingSound() {
		//_audioSource.Play ();
	}
}
