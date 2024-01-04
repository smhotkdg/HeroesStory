/*
 * GR InfiniCoin - Improbable Idle
 *
 * To put everything into practicle use, let's make an idle game.
 * Or at least a part of it that makes use of the InfiniCoin.
 *
 * Improbable Idle is an incremental game. The lowest tier asset
 * generates income, while further assets generate the asset that precedes it.
 *
 * This way, the more you unlock higher tiers, the faster the lower tiers will
 * increase.
 *
 * To add to the acceleration, each asset has a possibility of generating bonus
 * yield, which is 256 times the regular amount.
 *
 * The update button each asset has reduces the production time and increase the
 * chance of a bonus yield. As a result, the game starts slow and shoots up to
 * astronomical numbers very fast.
 *
 * [Weirdflex]
 * As a second thought, the number of atoms in the observable universe is estimated
 * to be between 10^78 and 10^82, so astronomical is perhaps an understatement.
 * [Weirdflex]
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GR_InfiniCoin;

public class GRIC_ImprobableIdleEngine : MonoBehaviour
{
    [SerializeField] Text scoreLabel = default;

    public InfiniCoin budget { get; private set; }
    public static GRIC_ImprobableIdleEngine engine;
    [SerializeField] Button pModeSwitch = default;
    private Text pModeText;

    public PurchaseMode pmode { get; private set; } = PurchaseMode.one;
    int pmodeCount;

    private GRIC_RandomAssetButton[] buttons;

    // Start is called before the first frame update
    void Awake()
    {
        if (engine != null && engine != this)
            Destroy(this);
        else
            engine = this;

        /*
         * reset the configuration to its defaults.
         * not necessary, but in case this scene was loaded
         * after other demo scenes, the following line
         * resets the changes.
         */
        ICConfig.Reset();

        ICConfig.SetPrintPrecision(2);
        ICConfig.namingSystem = InfiniName.base26Capital;
        ICConfig.SetLeadingNames("", "K", "M", "G");
        //The game is configured to to have 2 decimal places,
        //the naming system is set as base26, in capital letters,
        //and the leading names are set to K, M, and G for thousand
        //million and billion respectively.

        ICConfig.OverrideName(42, "*");
        //Finally, to drive the reference home, let's override the
        //name for 1000^42, 1000 to the power of answer to life, the universe
        //and everything, as '*', which is incidentally the symbol to all
        //and ascii equivalent of 42.

        buttons = FindObjectsOfType<GRIC_RandomAssetButton>();

        budget = 0;
        //Game starts with 0 budget

        UpdateScoreLabel();

        pModeSwitch.onClick.AddListener(CyclePMode);
        pModeText = pModeSwitch.GetComponentInChildren<Text>();
        pmodeCount = System.Enum.GetNames(typeof(PurchaseMode)).Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateScoreLabel()
    {
        scoreLabel.text = budget + " Improbable Liras";
    }

    /*
     * UpdateScore class takes two parameters. an InfiniCoin object and
     * the flag whether to subtract or add the value.
     *
     * When executed, the function below adds or subtracts the value,
     * and updates the affordability of each asset.
     * (More on that at the asset class)
     */
    public void UpdateScore(InfiniCoin ic = null, bool subtract = false)
    {
        if(ic != null)
        {
            if (subtract) budget -= ic;
            else budget += ic;
        }

        UpdateScoreLabel();

        foreach (GRIC_RandomAssetButton btn in buttons)
            btn.UpdateAffordability();
    }

    /*
     * CyclePMode function cycles through the purchase modes.
     * The game offers you the following purchase modes:
     *
     * Buy one by one
     * Buy up to 10
     * Buy up to 1000
     * Buy 10% of available
     * Buy 50% of available
     * Buy 100% of available
     */

    private void CyclePMode()
    {
        pmode = (PurchaseMode)((pmode.GetHashCode() + 1) % pmodeCount);
        if (pmode == PurchaseMode.one) pModeText.text = "x1";
        else if (pmode == PurchaseMode.ten) pModeText.text = "x10";
        else if (pmode == PurchaseMode.thousand) pModeText.text = "x1000";
        else if (pmode == PurchaseMode.percent10) pModeText.text = "10%";
        else if (pmode == PurchaseMode.percent50) pModeText.text = "50%";
        else if (pmode == PurchaseMode.all) pModeText.text = "100%";
        else pModeText.text = "x1";

        foreach (GRIC_RandomAssetButton btn in buttons)
            btn.UpdateAffordability();
    }

    public void Exit() => _ = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Demo_Main");
}

public enum PurchaseMode
{
    one = 0,
    ten = 1,
    thousand = 2,
    percent10 = 3,
    percent50 = 4,
    all = 5
}

/*
 * Rest of the functionality is in the GRIC_RandomAssetButton
 */