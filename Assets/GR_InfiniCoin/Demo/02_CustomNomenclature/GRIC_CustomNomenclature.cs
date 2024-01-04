/*
 * GR InfiniCoin - Custom Nomenclature
 *
 * ICConfig class can override multiple number names. If given
 * in consecutive order, it is possible to create your own naming
 * system.
 */

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using GR_InfiniCoin;

public class GRIC_CustomNomenclature : MonoBehaviour
{
    [SerializeField] Text prevNumber = default;
    [SerializeField] Text prevName = default;

    InfiniCoin ic;

    void Start()
    {
        /*
         * reset the configuration to its defaults.
         * not necessary, but in case this scene was loaded
         * after other demo scenes, the following line
         * resets the changes.
         */
        ICConfig.Reset();

        ic = 2; //this will be a binary demo.

        ICConfig.SetPrintPrecision(2);
        ICConfig.namingSystem = InfiniName.base26Capital;

        /*
         * We already saw what IC looks like when printed, so let's jump
         * right to the business and start creating a new set of custom names
         * after making tiny adjustments above.
         */

        Dictionary<long, string> byteNaming = new Dictionary<long, string>();

        byteNaming.Add(0, "Byte(s)");
        byteNaming.Add(1, "Kilobyte(s)");
        byteNaming.Add(2, "Megabyte(s)");
        byteNaming.Add(3, "Gigabyte(s)");
        byteNaming.Add(4, "Terabyte(s)");
        byteNaming.Add(5, "Petabyte(s)");
        byteNaming.Add(6, "Exabyte(s)");
        byteNaming.Add(7, "Zettabyte(s)");
        byteNaming.Add(8, "Yottabyte(s)");
        byteNaming.Add(9, "Brontobyte(s)");
        byteNaming.Add(10, "Manybyte(s)");
        byteNaming.Add(11, "Strangebyte(s)");
        byteNaming.Add(12, "Gazorpabyte(s)");
        byteNaming.Add(13, "Gerrybyte(s)");
        byteNaming.Add(14, "Rectanglebyte(s)");

        ICConfig.RemoveLeadingNames();
        ICConfig.OverrideNames(byteNaming);

        /*
         * Here, we created a dictionary of key and name pairs. keys are
         * consecutive, and names follow conventional byte naming until it doesn't.
         * Still, by stretching it with gibberish, we name the first 15 powers
         * of 1000 manually.
         *
         * Also, notice that we removed the leading names. The leading names
         * take precedence over the default naming system and the overridings.
         */

        Debug.Log("Overridden upto 15:");
        for (int i = 0; i < 20; i++)
        {
            Debug.Log("1000^" + i + " = " + ic);
            ic *= 1024;
        }

        /*
         * As you'll see, since we overriden the powers of 1000 up to 1000^15,
         * beginning with ^15, it defaults back to the chosen naming system.
         * So the more you add, the longer you'll keep using the custom names.
         * It is also useful if you know the maximum value your game allows, so
         * that you know up to what you need to override.
         */

        /*
         * Let's do something DARING.
         *
         * ICConfig has two static functions, one for converting names to powers of
         * 1000 and one for converting powers of 1000 to names. Both used by the
         * InfiniCoin class.
         *
         * Our naming ends at 15, and it looks weird after that. Let's use the
         * Name function to create more byte looking names (at this point I'm
         * improvising with this demo).
         */

        Dictionary<long, string> moreBytes = new Dictionary<long, string>();

        for(int i = 15; i < 100; i++)
        {
            moreBytes.Add(i, ICConfig.Name(i) + "byte(s)");
        }

        ICConfig.OverrideNames(moreBytes);

        /*
         * We generated names based on the naming system, appended them with
         * the word "byte(s)", and added these new names back to the config
         * as the additional overridden names.
         *
         * Now the custom names go upto 100^100.
         */

        ic.Set1000Power(95);

        Debug.Log("Overridden upto 100:");
        for (int i = 0; i < 10; i++)
        {
            Debug.Log("1000^" + ic.kPower + " = " + ic);
            ic *= 1024;
        }

        GRIC_Configurator configurator = FindObjectOfType<GRIC_Configurator>();
        if(configurator != null) configurator.Refresh();
        /*
         * Let's leave this configurator component here for one last time
         * so that we see the list of overriden names.
         *
         * Once again, the configurator component is only a helper with a
         * graphical interface, so use ICConfig itself, as seen above,
         * for configuration.
         *
         * Say goodbye to the configurator because this is the last time
         * we see it.
         */

        ic = 2;
        InvokeRepeating("UpdatePreview", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void UpdatePreview()
    {
        ic *= 1024;
        string[] name = ic.ToSplitString();

        prevNumber.text = name[0];
        prevName.text = name[1];
    }

    public void Exit() => _ = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Demo_Main");
}