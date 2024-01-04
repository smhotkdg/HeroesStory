/*
 * GR InfiniCoin - Improbable Idle - The Asset Button
 *
 * Continuing from the Idle Engine, the class below defines the asset button.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using GR_InfiniCoin;

[RequireComponent(typeof(CanvasGroup))]
public class GRIC_RandomAssetButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] GameObject bar = default;
    [SerializeField] RectTransform barInnerRT = default;
    [SerializeField] Text nameLabel = default;
    [SerializeField] Text ilpsLabel = default;
    [SerializeField] Text totalCostLabel = default;
    [SerializeField] Text levelInfoLabel = default;
    [SerializeField] Text upgradeCostLabel = default;
    [SerializeField] Button upgrade = default;
    [Space]
    public string assetName;
    [SerializeField] float cycleTime = default;
    [Space]
    /*
     * The following InfiniCoin objects define the costs and production
     * baseCost defines the price of one asset, which is in IL.
     * assetCost defines the price of one asset, which is in other assets, if any
     * upgradeCost is the price of upgrading, which is in IL and increases per each upgrade
     * baseIncome is the per cycle yield.
     */
    [SerializeField] InfiniCoin baseCost = default;
    [SerializeField] InfiniCoin assetCost = default;
    [SerializeField] InfiniCoin upgradeCost = default;
    [SerializeField] InfiniCoin baseIncome = default;
    [Space]
    /*
     * If target is null, then the asset yields money.
     * If the target is assigned another asset, the yield is
     * added to the target asset's count.
     */
    [SerializeField] GRIC_RandomAssetButton target = default;
    [SerializeField] bool unlocked = false;
    [SerializeField] CanvasGroup cg = default;

    float t, tp;
    float barH, barS, barV;
    Image barImage;
    float cycleCutoff = 0.5f;

    InfiniCoin count = new InfiniCoin(1);
    //current number of assets
    bool affordable = false;
    InfiniCoin purchaseCount = new InfiniCoin(1);
    //defines the number of assets for the following purchase

    int upgradeLevel = 0;
    float upgradeMultiplier = 1.1f;
    float upgradeTotal = 1f;
    int bonusRate = 0;
    CanvasGroup upgradeButtonCG;

    Coroutine zoomCR = null;
    float zoomFactor = 1f;
    RectTransform rt;

    // Start is called before the first frame update
    void Start()
    {
        t = tp = 0;
        rt = GetComponent<RectTransform>();
        upgradeButtonCG = upgrade.GetComponent<CanvasGroup>();

        UpdateAffordability();

        if(unlocked)
        {
            Unlock();
        }
        else
        {
            bar.SetActive(false);
        }

        UpdateUpgradeButton();
        upgrade.onClick.AddListener(Upgrade);

        barImage = barInnerRT.GetComponent<Image>();
        Color barColor = barImage.color;
        Color.RGBToHSV(barColor, out barH, out barS, out barV);
    }

    /*
     * To make the purchases happen as long as the player is pressing
     * on the asset button, and to keep it coming as long as there is
     * enough funds, "IncrementCount" function is invoked repeatedly.
     * It keeps trying 10 times per second to make a purchase.
     */
    public void OnPointerDown(PointerEventData eventData)
    {
        if (unlocked)
            InvokeRepeating("IncrementCount", 0f, 0.1f);
        else
            Unlock();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CancelInvoke("IncrementCount");
    }

    private void Upgrade()
    {
        /*
         * if the asset is unlocked and the upgradeCost is less than or equal to
         * the current available budget, the asset is upgraded as follows:
         */
        if(unlocked && upgradeCost <= GRIC_ImprobableIdleEngine.engine.budget)
        {
            upgradeLevel += 1;
            GRIC_ImprobableIdleEngine.engine.UpdateScore(upgradeCost, true);
            //subtract the upgradeCost from the budget

            upgradeTotal *= upgradeMultiplier;
            //multiply the upgrade with the upgrade multiplier

            cycleTime /= upgradeMultiplier;
            //reduce the production cycle by the upgrade multiplier

            upgradeCost *= upgradeMultiplier;
            //increase the upgrade cost for the next upgrade

            if(bonusRate < 100) bonusRate++;
            //increase the bonus rate percentage by one, if the current
            //rate is below 100.

            if (cycleTime <= cycleCutoff)
                barInnerRT.localScale = Vector3.one;
            //after the cycle becomes shorter than half a second, the
            //loading bar animation is removed because it doesn't make
            //visual sense and also for very small numbers, it is better
            //to calculate the value in bulk and apply it in defined intervals
            //after the cutoff point, it becomes a shining full bar.
            //to make sure the bar is rendered as intended, the scale
            //is set to one if the cycle time is below the cutoff.

            UpdateUpgradeButton();
        }
    }

    private void UpdateUpgradeButton()
    {
        upgradeCostLabel.text = "Upgrade\n(" + upgradeCost + ")";
        levelInfoLabel.text = "speed x" + upgradeTotal.ToString("N2") + "\n";
        levelInfoLabel.text += "bonus " + bonusRate + "%";
    }

    // Update is called once per frame
    void Update()
    {
        if(unlocked)
            Tick();
    }

    //when unlocked
    //activate the production bar
    //set it as unlocked
    //update the labels.
    public void Unlock()
    {
        if(affordable)
        {
            bar.SetActive(true);
            unlocked = true;
            UpdateAssetState();
        }
    }

    private void Tick()
    {
        tp = t;
        //tp is the game time at the previous frame.

        InfiniCoin yield = baseIncome * count;
        //yield is the total production amount, which is the base income
        //times the number of assets.

        if (cycleTime < cycleCutoff)
        {
            //if the production time is below the threshold, then animate
            //the bar as a full, shining bar. And, multiply the yield
            //with the ratio of actual cycle time to the threshold.

            t = (t + Time.deltaTime) % cycleCutoff;
            barImage.color = Color.HSVToRGB(barH, barS, barV + (Mathf.Sin(Time.time * 3) + 1f) / 5);
            yield *= cycleCutoff / cycleTime;
        }
        else
        {
            //else, animate the bar in the form of a loading bar.

            t = (t + Time.deltaTime) % cycleTime;
            barInnerRT.localScale = new Vector3(t / cycleTime, 1, 1);
        }

        //the variable t keeps record of time, but as a modulus of the cycle time.
        //this way, we can compare the current value to the previous value to
        //detect the end of cycle: if t is lesser than tp, it cycled back and
        //it can yield.
        if (tp > t)
        {
            //generate a random number between 0 and 100 and
            //compare it to the bonus percentage. if the number
            //is lesser than the ratio, then generate bonus yield.
            //i.e. multiply the yield by 256, and scale the asset button
            //up as a visual cue of the bonus.
            bool bonus = Random.Range(0f, 100f) < bonusRate;
            if (bonus)
            {
                yield *= 256;
                zoomFactor = 1.1f;

                if (zoomCR == null)
                    zoomCR = StartCoroutine(ZoomBack());
            }

            //if the target variable is null, than the yield is added
            //to the total budget.
            //if it isn't, then it means this is a higher tier asset,
            //and the yield is added to the count of the target asset.
            if (target == null)
                GRIC_ImprobableIdleEngine.engine.UpdateScore(yield);
            else
                target.AddAssets(yield);
        }
    }

    //Coroutine that handles the zoom out part of the bonus animation
    IEnumerator ZoomBack()
    {
        do
        {
            zoomFactor *= 0.99f;
            rt.localScale = new Vector3(zoomFactor, zoomFactor, zoomFactor);
            yield return null;
        }while(zoomFactor > 1.001f);

        rt.localScale = Vector3.one;
        zoomCR = null;
        yield return null;
    }

    //this function adds assets by increasing the "count" variable.
    public void AddAssets(InfiniCoin assetCount)
    {
        count += assetCount;
        UpdateAssetState();
    }

    //Update Asset State function updates all the labels, as well as the next purchase amount
    void UpdateAssetState()
    {
        if (unlocked)
        {
            nameLabel.text = count + " " + assetName + (count <= 1 ? "" : "s") + " (lvl. " + upgradeLevel + ")";
            //if the count is greater than 1, then do not pluralize the name of the asset, and also print out the uprade level next to the name.

            //if the cycle time is below the cutoff, then display the yield as per seconds.
            //else, display the total yield per cycle.
            if(cycleTime < cycleCutoff)
            {
                //if the target is null, display the yield as improbable liras per second
                //else, display the yield as target asset per second.
                if (target == null)
                    ilpsLabel.text = (baseIncome * count * cycleCutoff / cycleTime) + " ILPS";
                else
                    ilpsLabel.text = (baseIncome * count * cycleCutoff / cycleTime) + " " + target.assetName + "s PS";
            }
            else
            {
                //if the target is null, display the yield as improbable liras per cycle
                //else, display the yield as target asset per cycle.
                if (target == null)
                    ilpsLabel.text = (baseIncome * count) + " IL per " + cycleTime.ToString("N2") + " second" + (cycleTime == 1 ? "." : "s.");
                else
                    ilpsLabel.text = (baseIncome * count) + " " + target.assetName + "s per " + cycleTime.ToString("N2") + " second" + (cycleTime == 1 ? "." : "s.");
            }

            //if the asset is affordable, then update the purchase count, based on the purchase mode
            if (affordable)
            {
                if (GRIC_ImprobableIdleEngine.engine.pmode == PurchaseMode.one)
                {
                    PurchaseCountByCap(1);
                }
                else if (GRIC_ImprobableIdleEngine.engine.pmode == PurchaseMode.ten)
                {
                    PurchaseCountByCap(10);
                }
                else if (GRIC_ImprobableIdleEngine.engine.pmode == PurchaseMode.thousand)
                {
                    PurchaseCountByCap(1000);
                }
                else if (GRIC_ImprobableIdleEngine.engine.pmode == PurchaseMode.percent10)
                {
                    PurchaseCountByPercent(10);
                }
                else if (GRIC_ImprobableIdleEngine.engine.pmode == PurchaseMode.percent50)
                {
                    PurchaseCountByPercent(50);
                }
                else if (GRIC_ImprobableIdleEngine.engine.pmode == PurchaseMode.all)
                {
                    PurchaseCountByPercent(100);
                }
            }
            else
            {
                totalCostLabel.text = "Need more resources to buy.";
            }
        }
        else
        {
            //if the asset is still locked, but finally affordable, notify the
            //player by displaying the message "ready" next to the asset name
            //
            //else, display the message that it's locked.
            if (affordable)
            {
                nameLabel.text = assetName + " (Ready)";
                totalCostLabel.text = "Unlock for " + baseCost;
                totalCostLabel.text += target ? " and " + assetCost + " " + target.name + "s." : ".";
            }
            else
            {
                nameLabel.text = assetName + " (Locked)";
                totalCostLabel.text = "Need " + baseCost + (target ? " and " + assetCost + " " + target.name + "s" : "") + " to unlock.";
            }
        }
    }

    //PurchaseCountByCap function takes the upper limit for the purchase,
    //and returns the lesser of the cap and the currently available.
    private void PurchaseCountByCap(int cap)
    {
        //this function is called only if the asset is available for purchase
        //so it is a given that at least one is available.
        if(cap == 1)
        {
            purchaseCount = new InfiniCoin(1);
        }
        else
        {
            purchaseCount = GRIC_ImprobableIdleEngine.engine.budget / baseCost;
            //divide the budget by the baseCost
            purchaseCount = InfiniCoin.Floor(purchaseCount);
            //floor the baseValue to find maximum available
            purchaseCount = InfiniCoin.Min(purchaseCount, cap);
            //Take the minimum of the result above and the cap.

            //if target is not null, then the asset also has an asset cost.
            //do the same for the asset cost and also find the minimum.
            if (target != null)
            {
                InfiniCoin availableByAsset = target.count / assetCost;
                availableByAsset = InfiniCoin.Floor(availableByAsset);
                purchaseCount = InfiniCoin.Min(purchaseCount, availableByAsset);
            }

            //notice that there is a floor function even though InfiniCoin is a whole
            //number class. This is due to the fact that InfiniCoin is recording the
            //number as an approximation, where floating numbers are necessary for
            //higher powers of 1000. So, Flooring works upto a trillion, after which
            //the integer value is lost and the flooring is irrelevant.
        }

        totalCostLabel.text = "Buy " + purchaseCount + " " + assetName + " for " + baseCost * purchaseCount;

        if (target != null)
            totalCostLabel.text += " & " + (assetCost * purchaseCount) + " " + target.assetName + "(s.)";
    }

    //PurchaseCountByPercent function calculates the amount for purchase
    //based on the percentage given.
    private void PurchaseCountByPercent(float percent)
    {
        float p = 100 / percent;

        purchaseCount = GRIC_ImprobableIdleEngine.engine.budget / baseCost / p;
        purchaseCount = InfiniCoin.Floor(purchaseCount);
        //calculate the purchase count based on the percentage and floor the
        //value.

        //if target is not null, then the asset also has an asset cost.
        //do the same for the asset cost and also find the minimum.
        if (target != null)
        {
            InfiniCoin availableByAsset = target.count / assetCost / p;
            availableByAsset = InfiniCoin.Floor(availableByAsset);
            purchaseCount = InfiniCoin.Min(purchaseCount, availableByAsset);
        }

        //Finally, just like with the purchase by cap but in reverse order,
        //compare the result to 1, and take the maximum, as, again, this
        //function is only called if the asset is available, so at least one
        //can be bought.
        purchaseCount = InfiniCoin.Max(purchaseCount, 1);

        totalCostLabel.text = "Buy " + purchaseCount + " " + assetName + " for " + baseCost * purchaseCount +" IL";

        if (target != null)
            totalCostLabel.text += " & " + (assetCost * purchaseCount) + " " + target.assetName + "(s.)";
    }

    //Update whether or not the asset is available for purchase by comparing
    //the base cost to the total budget and if necessary, the asset cost to the
    //target asset count.
    public void UpdateAffordability()
    {
        affordable = GRIC_ImprobableIdleEngine.engine.budget >= baseCost && (target == null || target.count >= assetCost);
        cg.alpha = affordable ? 1f : 0.5f;

        UpdateAssetState();
        upgradeButtonCG.alpha = (GRIC_ImprobableIdleEngine.engine.budget >= upgradeCost && unlocked) ? 1f : 0.5f;
        //also check if the budget is higher than the upgrade cost and update the update button accordingly.
    }

    //The function that is invoked repeatedly.
    void IncrementCount()
    {
        //As long as the asset is affordable:
        if (affordable)
        {
            //increase the count by purchase count
            count += purchaseCount;

            //if the target is not null, reduce asset cost times the purchase count amount from it
            if(target) target.SpendAsset(assetCost * purchaseCount);

            //reduce the budget by base cost times the purchase count.
            GRIC_ImprobableIdleEngine.engine.UpdateScore(baseCost * purchaseCount, true);

            //update the affordability
            UpdateAffordability();
        }
    }


    //reduce "amount" amount of assets from the count and update the affordability.
    public void SpendAsset(InfiniCoin amount)
    {
        count -= amount;
        UpdateAssetState();
        UpdateAffordability();
    }
}