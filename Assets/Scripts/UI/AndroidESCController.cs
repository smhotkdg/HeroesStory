using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AndroidESCController : MonoBehaviour
{
    public Button ExitButton;
    public PanelType panelType = PanelType.normal;
    public enum PanelType
    {
        normal,
        bottom
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        UiManager.Instance.AddOpenPanel(gameObject);
    }
    private void OnDisable()
    {
        UiManager.Instance.OpenPanelRemove();
    }
    public void Cancel()
    {
        if (ExitButton == null)
            return;
        ExitButton.onClick.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
