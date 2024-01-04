/*
 * GR InfiniCoin Basics Demo
 *
 * InfiniCoin is a numeric class for incremental games.
 * By using InfiniCoin, you can keep track of vastly large numbers for any use
 * such as scoring or for ingame currency.
 *
 * InfiniCoin registers the power of 1000 and the base value of the number,
 * and this way the class can represent values up to 999 x 1000 ^ 2 ^ 64.
 *
 * Let's create some numbers and explode them into unreasonable values!
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GR_InfiniCoin;

public class GRIC_Basics : MonoBehaviour
{
    [SerializeField] Text coinLabel = default;

    /*
     * InfiniCoin, being a numeric class, can be initialized like any other number
     * class, such as int or double. Let's create the coins variable.
     */
    [SerializeField] InfiniCoin coin = default;
    [SerializeField] InfiniCoin operand = default;

    [Space]
    [SerializeField] Text opLabel = default;
    char opSign = '+';
    [SerializeField] InputField opBase = default;
    [SerializeField] InputField opKPow = default;
    [SerializeField] Text opPrev = default;
    [SerializeField] Button updateCoin = default;
    /*
     * An InfiniCoin variable can be initialized with an integer, float, double
     * or long. Because InfiniCoin deals with large numbers, it can only register
     * whole, positive numbers. Because of that, even though assignment can be
     * made in any numeric value type, it will be cast to a whole number. Double
     * and float assignments have their own uses, but all in due time.
     *
     * Let's make more numbers.
     */

    [SerializeField] InfiniCoin[] coins = new InfiniCoin[5];

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
        //before we start, let's turn the verbose mode on. More on this later.

        /*
         * Other than directly assigning numbers, an InfiniCoin variable can be
         * Initialized in many other ways. 0th of the infiniCoins array is assigned
         * in the inspector with the very fancy property drawer. So let's jump to
         * the 1st.
         */

        coins[1] = new InfiniCoin(42);
        //here, the variable is initialized with the raw numeric value.

        coins[2] = new InfiniCoin(1.57079632679, 49503);
        /*
         * in this example, the variable is initialized with the base value
         * and the power of 1000. Meaning that, this variable is now
         * equivalent to PI / 2 x 1000 ^ 49503.
         *
         * Why would you use this particular number, I do not know, but there
         * you have it.
         */

        coins[3] = new InfiniCoin(5, "gr");
        /*
         * here, the variable is created by giving the intended number name.
         * if you know what you want it to read, but not exactly know the
         * corresponding power of 1000 to achieve that, you can use this option.
         * here, we create a number that is equal to 5.000 gr, in the name
         * of my good rectangle products.
         */

        coins[4] = new InfiniCoin(coin);
        /*
         * lastly, you can create new infiniCoin instances by cloning an existing
         * one. Here, we take the coin variable, and use it to create the last
         * element in the infiniCoins array.
         */

        /* OPERATIONS */

        //First of all, let's print them out and see how they look.
        foreach (InfiniCoin ic in coins) Debug.Log(ic);

        /*
         * The Debug.Log output will show these numbers with 2 decimal points
         * and with the base26, double digit start naming system. These, and
         * more options can be tweaked by configuring InfiniCoin by using
         * ICConfig class, but, again, more on that later.
         */

        /* IC objects can be added, subtracted, multiplied by and divided by other IC objects. */
        InfiniCoin result = coins[2] / coins[3];
        Debug.Log(coins[2] + " / " + coins[3] + " = " + result);

        /*
         * because the class is inherently a whole number system, addition and subtraction
         * with values below one doesn't make a difference. But, for scenarios
         * such as "increase performence by 20%" or "reduce cost by 99% percent"
         * IC objects can be multiplied and divided by floats and doubles.
         *
         * let's reduce the result from the previous operation by 99.998% (?),
         * until the power of 1000 is below 49300, and see each step.
         */
        while(result.kPower > 49300)
        {
            string rOld = result.ToString();
            result *= 0.00002f;
            Debug.Log(rOld + " * " + 0.00002f + " = " + result);
        }

        Debug.Log("New power of 1000: " + result.kPower);

        /*
         * Like every good thing, InfiniCoin comes with a cost. And that is the loss
         * of information as the power of 1000 increases. Since the expressible numbers
         * are extremely large, the drawback here is that once an IC object is 1000^4 times
         * than another, the operation no longer makes a difference.
         */

        /*
         * As an example, let's take coins[1], which is right now 42 and increment
         * it by one. The result is, as you would expect, 43.
         */

        coins[1] += 1;
        Debug.Log(coins[1]);

        /*
         * Let's spice things up. First, let's increase the power by a million,
         * in a way other than multiplying by 1000,000:
         */

        coins[1].Set1000Power(2);
        /* Now the number is 43 M. Let's add one again. */
        coins[1] += 1;

        Debug.Log(coins[1]);
        /*
         * You'll see that the number just prints 43.00 M.
         * Actually, the value is still there, it's just truncated.
         * If we increase the print precision, it will appear there.
         */

        ICConfig.SetPrintPrecision(6);
        Debug.Log(coins[1]);

        /*
         * Let's increase the power even more, and see what happens
         * when we add one again:
         */

        ICConfig.SetPrintPrecision(2);
        coins[1].SetPowerByName("az");
        coins[1] += 1;

        /*
         * Now the IC object will show a warning sign and notify you
         * that nothing has happened due to the large difference.
         *
         * Since coins[1] is at the order of "az" but the addition is just
         * 1, it is lost and is not expressed.
         *
         * For scenarios where the game may contain an array of IC objects
         * with varying powers of 1000, refer to the sample idle game demo,
         * the demo number 4.
         */


        /*
         * Unlike other numeric classes, ++ and -- works a bit differently
         * in InfiniCoin.
         *
         * Keeping in mind that this class is made for incremental games
         * and the point is to keep track of increasing score with increasing
         * speed, ++ and -- increments work on the base value, and keep increases
         * the value by the power of 1000.
         *
         * Let's give an example on coin[4], which we originally set to 990 M
         * by cloning the coin variable.
         *
         * Let's run a loop on it and inrement the value 20 times and see what happens:
         */

        for (int i = 0; i < 20; i++)
            Debug.Log(coins[4]++);

        /*
         * As you'll see, the final value has become 11 billion. This is because
         * as the value passed the 1 billion mark, the power of 1000 increased
         * by 1 and became 3, also looping the base value back to 1. Now, the cycle
         * starts from scratch for incrementing, but now each step is worth 1000^3
         *
         * -- works the same as ++, as follows:
         */

        for (int i = 0; i < 20; i++)
            Debug.Log(coins[4]--);


        /* COMPARISONS */

        /*
         * IC objects, as you would expect from a numeric class, are comparable.
         * Both between IC objects and against other numeric classes.
         */

        Debug.Log(coins[4] + " > " + 100 + " -> " + (coins[4] > 100));
        Debug.Log(coins[3]+ " == " + new InfiniCoin(5, 178) + " -> " + (coins[3] == new InfiniCoin(5, 178)));
        Debug.Log(coins[3] + ".Equals(" + new InfiniCoin(5, 178) + ") -> " + (coins[3].Equals(new InfiniCoin(5, 178))));

        /*
         * So this wraps up the basics. Play around with the values above,
         * or use the input fields in this scene to try more operations and
         * see how the class behaves.
         */

        coinLabel.text = coin.ToString();

        opBase.onValueChanged.AddListener(UpdateOpBase);
        opKPow.onValueChanged.AddListener(UpdateOpKPow);

        updateCoin.onClick.AddListener(UpdateCoin);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateOpBase(string s)
    {
        double d;
        if(double.TryParse(s, out d))
        {
            operand.SetBaseValue(d);
            opBase.text = operand.baseValue.ToString();
            opKPow.text = operand.kPower.ToString();
            opPrev.text = operand.ToString();
        }
    }

    void UpdateOpKPow(string s)
    {
        long l;
        if (long.TryParse(s, out l))
        {
            operand.Set1000Power(l);
            opBase.text = operand.baseValue.ToString();
            opKPow.text = operand.kPower.ToString();
            opPrev.text = operand.ToString();
        }
    }

    public void ChangeOperation(string c)
    {
        opSign = c[0];
        opLabel.text = "Chosen Operation: " + opSign;
    }

    void UpdateCoin()
    {
        if (opSign == '+')
            coin += operand;
        else if (opSign == '-')
            coin -= operand;
        else if (opSign == '*')
            coin *= operand;
        else if (opSign == '/')
            coin /= operand;

        coinLabel.text = coin.ToString();
    }

    public void Exit() => _ = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Demo_Main");
}
