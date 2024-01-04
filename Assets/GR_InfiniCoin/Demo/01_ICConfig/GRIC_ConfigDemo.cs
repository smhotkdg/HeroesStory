/*
 * GR InfiniCoin - ICConfig Demo
 *
 * ICConfig is a static class for configuring the look and feel
 * of the InfiniCoin objects, when printed.
 *
 * The default values are as follows:
 * - Default naming system is base26
 * - Verbose mode is off
 * - Property drawer's UI are tight
 * - IC objects have two decimal places when printed
 * - Repeating letter naming system is capped at 4 letters.
 * - Has no name overriding
 * - The first powers of 1000 go as: "", "k", "m", "b", "t", where t
 *   corresponds to 1000^4, i.e. a trillion.
 */

using UnityEngine;
using UnityEngine.UI;
using GR_InfiniCoin;

public class GRIC_ConfigDemo : MonoBehaviour
{
    [SerializeField] InputField prevBase = default;
    [SerializeField] InputField prevKPow = default;
    [SerializeField] Text prevLabel = default;

    InfiniCoin preview = 0;

    // Start is called before the first frame update
    void Start()
    {
        /*
         * reset the configuration to its defaults.
         * not necessary, but in case this scene was loaded
         * after other demo scenes, the following line
         * resets the changes.
         */
        ICConfig.Reset();

        ICConfig.verbose = true;
        /*
         * Once again, lets turn the verbose mode on, so that we can see
         * warnings, if any.
         */

        InfiniCoin ic = new InfiniCoin(1.1234567890123456789, 10);

        /*
         * Let's print the number above and see how it looks
         * without changing any options.
         *
         * As mentioned, 2 decimal points, base26.
         */

        Debug.Log(ic);

        /*
         * As you can see, this number has more decimal places
         * to show. So let's do that. Let's increase the print precision.
         * The next Debug.Log will show the number up to 9 decimal points,
         * since that is the cap.
         */

        ICConfig.SetPrintPrecision(20);
        Debug.Log(ic);

        /*
         * As mentioned before, there are a number of namig systems available.
         * 8, to be precise. And they are as follows:
         *
         * repeating:                   aa, bb, cc, ... , aaa, bbb, ccc
         * repeatingCapital:            AA, BB, CC, ... , AAA, BBB, CCC
         * repeatingSingleStart:        a, b, c, ... , aa, bb, cc
         * repeatingCapitalSingleStart: A, B, C, ... , AA, BB, CC
         * base26:                      aa, ab, ac, ... , ba, bb, bc
         * base26Capital:               AA, AB, AC, ... , BA, BB, BC
         * base26SingleStart:           a, b, c, ... , aa, ab, ac
         * base26CapitalSingleStart:    A, B, C, ... , AA, AB, AC
         */

        ICConfig.SetPrintPrecision(2);
        ICConfig.namingSystem = InfiniName.repeatingCapital;

        Debug.Log(ic);

        /*
         * As you can see, the name changed from af to FF.
         * this works, but let's see how it looks when look at the first
         * few powers of 1000.
         */

        for(int i = 0; i < 8; i++)
        {
            ic.Set1000Power(i);
            Debug.Log("K Power: " + i + " - " + ic);
        }

        /*
         * The problem you'll see that, the leading names are miniscule
         * while the naming system is in capital letters.
         *
         * I wish there was a way to set these names...
         */

        /*
         * SetLeadingNames() and RemoveLeadingNames() functions sets the
         * first, human comprehensible values of 1000, after which the
         * actual naming system kicks in.
         * 
         * You can remove the leading names altogether, like this:
         */

        ICConfig.RemoveLeadingNames();

        Debug.Log("--Leading Names Removed--");
        for (int i = 0; i < 8; i++)
        {
            ic.Set1000Power(i);
            Debug.Log("K Power: " + i + " - " + ic);
        }

        /*
         * Or, you can set the names, like this:
         */

        Debug.Log("--Capital Leading Names Added (sans-'T')--");
        ICConfig.SetLeadingNames("", "K", "M", "G");
        for (int i = 0; i < 8; i++)
        {
            ic.Set1000Power(i);
            Debug.Log("K Power: " + i + " - " + ic);
        }

        /*
         * Now that we switched to repeating letters, you might have realized
         * that it can easily get unreasonably long, very easily. So, the
         * repeating name system is capped. After it gets longer that 4 by default,
         * it will be truncated and displayed as [Letter][Expected Length]
         * Let's take a look:
         */

        ic.Set1000Power(150);
        Debug.Log(ic);

        /*
         * Since the repeating name system is capped at 4, you'll see that 1000^150
         * becomes Q7. But that can change. Lets increase it.
         */

        ICConfig.SetRepeatingNameCap(9);
        Debug.Log(ic);
        /*
         * Now that we increased the cap to 9, now it becomes QQQQQQQ.
         */


        /*
         * Since the naming systems are context-free, it is possible that
         * it can generate letter combinations which also happen to be
         * offensive or unwanted words. In order to avoid it, you can
         * override names.
         *
         * Suppose you don't like me. It happens. No hard feelings.
         * And you don't want to see the prefix I use in my classes,
         * "gr". Let's put that in.
         */

        ICConfig.namingSystem = InfiniName.base26;
        ICConfig.OverrideName("gr", "g*");
        ic.SetPowerByName("gr");
        Debug.Log("Overriden Name: " + ic);

        /*
         * Now that "gr" is overriden with "g*", the number above
         * will read 1.12 g*, and not 1.12 gr.
         */

        /*
         * Maybe after seeing this amazing feature, you had a change
         * of heart and no longer want to avoid my prefix. Let's take
         * it back.
         */

        ICConfig.RemoveNameOverride("gr");
        Debug.Log("Default Name: " + ic);

        /*
         * We've seen how to override names by the names. But, it is also
         * possible to override them by the power of 1000. It is useful
         * if there is a specific power you want to override.
         * 
         * Suppose in your game, 1000^1000 is the maximum value you can
         * ever get and you would like to reflect it to the naming
         * convention of your score system. Here, we override 1000^1000
         * name to "MAX"
         *
         * But, keep in mind that this is just the naming. If you were to
         * go beyond 1000^1000, it will default back to the underlying naming
         * system. So, you need manually cap the maximum value.
         */

        ICConfig.OverrideName(1000, "MAX");
        ic.Set(999, 1000);
        Debug.Log(ic);

        /*
         * Finally, maybe you'd like create your own naming system for a
         * custom concept. You can do that by entring multiple values
         * at once.
         *
         * More on that in demo Number 02, Custom Nomenclature.
         */

        GRIC_Configurator configurator = FindObjectOfType<GRIC_Configurator>();
        if(configurator != null) configurator.Refresh();
        /*
         * Configurator component is used to set the ICConfig values.
         * Since we manually set these values, let's reflect these
         * changes to the component, so that you can continue
         * with the values that we left just above.
         *
         * Once again, the configurator component is only a helper with a
         * graphical interface, so use ICConfig itself, as seen above,
         * for configuration.
         */

        prevBase.onValueChanged.AddListener(UpdateOpBase);
        prevKPow.onValueChanged.AddListener(UpdateOpKPow);
    }

    // Update is called once per frame
    void Update()
    {
        prevLabel.text = preview.ToString();
    }


    void UpdateOpBase(string s)
    {
        double d;
        if (double.TryParse(s, out d))
        {
            preview.SetBaseValue(d);
            prevBase.text = preview.baseValue.ToString();
            prevKPow.text = preview.kPower.ToString();
            prevLabel.text = preview.ToString();
        }
    }

    void UpdateOpKPow(string s)
    {
        long l;
        if (long.TryParse(s, out l))
        {
            preview.Set1000Power(l);
            prevBase.text = preview.baseValue.ToString();
            prevKPow.text = preview.kPower.ToString();
            prevLabel.text = preview.ToString();
        }
    }

    public void Exit() => _ = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Demo_Main");
}