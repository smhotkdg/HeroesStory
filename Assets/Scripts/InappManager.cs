using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;

public class InappManager : MonoBehaviour
{
    // Start is called before the first frame update    
    private static InappManager _instance = null;

    public static InappManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("cSingleton InappManager == null");
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            //
            _instance = this;
        }
    }


    void Start()
    {
        InAppPurchasing.InitializePurchasing();
        InitData();
    }
    void InitData()
    {
        if (InAppPurchasing.IsInitialized() == true)
        {
            IAPProduct[] products = InAppPurchasing.GetAllIAPProducts();

            // Print all product names
            foreach (IAPProduct prod in products)
            {

                Debug.Log("Product name: " + prod.Name);
            }
        }
        else
        {
            InAppPurchasing.InitializePurchasing();
        }
    }

    // Subscribe to IAP purchase events
    void OnEnable()
    {
        InAppPurchasing.PurchaseCompleted += PurchaseCompletedHandler;
        InAppPurchasing.PurchaseFailed += PurchaseFailedHandler;

        InAppPurchasing.RestoreCompleted += InAppPurchasing_RestoreCompleted;
        InAppPurchasing.RestoreFailed += InAppPurchasing_RestoreFailed;
    }

    private void InAppPurchasing_RestoreFailed()
    {
        
        //UIManager.Instance.ShowNotification(tryagainstring);
    }

    private void InAppPurchasing_RestoreCompleted()
    {
        
        //UIManager.Instance.ShowNotification(tryagainstring);
    }

    // Unsubscribe when the game object is disabled
    void OnDisable()
    {
        InAppPurchasing.PurchaseCompleted -= PurchaseCompletedHandler;
        InAppPurchasing.PurchaseFailed -= PurchaseFailedHandler;
    }

    // Purchase the sample product
    public void PurchaseSampleProduct()
    {
        // EM_IAPConstants.Sample_Product is the generated name constant of a product named "Sample Product"

    }

    public void PurchaseGem(int index)
    {
        UiManager.Instance.InappProcess.SetActive(true);
        switch (index)
        {
            case 0:
                InAppPurchasing.Purchase(EM_IAPConstants.Product_gem1);
                break;
            case 1:
                InAppPurchasing.Purchase(EM_IAPConstants.Product_gem2);
                break;
            case 2:
                InAppPurchasing.Purchase(EM_IAPConstants.Product_gem3);
                break;
            case 3:
                InAppPurchasing.Purchase(EM_IAPConstants.Product_gem4);
                break;
            case 4:
                InAppPurchasing.Purchase(EM_IAPConstants.Product_gem5);
                break;
            case 5:
                InAppPurchasing.Purchase(EM_IAPConstants.Product_gem6);
                break;
        }
    }
    public void PurchasePack(int index)
    {
        if (GameManager.Instance.Pack1 == true)
            return;
        UiManager.Instance.InappProcess.SetActive(true);
        switch (index)
        {
            case 0:
                InAppPurchasing.Purchase(EM_IAPConstants.Product_pack1);
                break;          
        }
    }

    // Successful purchase handler
    void PurchaseCompletedHandler(IAPProduct product)
    {
        // Compare product name to the generated name constants to determine which product was bought
        Debug.Log("구매완료");

        UiManager.Instance.InappProcess.SetActive(false);
        switch (product.Name)
        {
            case EM_IAPConstants.Product_gem1:                
                UiManager.Instance.SetBuyComplete(80,BuyCompletePanel.buyType.gem);
                break;
            case EM_IAPConstants.Product_gem2:
                UiManager.Instance.SetBuyComplete(500, BuyCompletePanel.buyType.gem);
                break;
            case EM_IAPConstants.Product_gem3:
                UiManager.Instance.SetBuyComplete(1200, BuyCompletePanel.buyType.gem);
                break;
            case EM_IAPConstants.Product_gem4:
                UiManager.Instance.SetBuyComplete(2500, BuyCompletePanel.buyType.gem);
                break;
            case EM_IAPConstants.Product_gem5:
                UiManager.Instance.SetBuyComplete(6500, BuyCompletePanel.buyType.gem);
                break;
            case EM_IAPConstants.Product_gem6:
                UiManager.Instance.SetBuyComplete(14000, BuyCompletePanel.buyType.gem);
                break;
            case EM_IAPConstants.Product_pack1:
                UiManager.Instance.SetBuyComplete(0, BuyCompletePanel.buyType.pack1);
                break;

        }

    }

    // Failed purchase handler
    void PurchaseFailedHandler(IAPProduct product)
    {
        UiManager.Instance.InappProcess.SetActive(false);
        Debug.Log("The purchase of product " + product.Name + " has failed.");
    }
    public void RestorePurchases()
    {
#if UNITY_IOS
        InAppPurchasing.RestorePurchases();
#endif
    }
    void Update()
    {

    }
}
