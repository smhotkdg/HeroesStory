using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GR_InfiniCoin;

public class GRIC_Configurator : MonoBehaviour
{
    [HideInInspector] public InfiniName namingSystem = ICConfig.namingSystem;
    [HideInInspector] public bool verbose = ICConfig.verbose;

    [HideInInspector] public int printPrecision = ICConfig.printPrecision;
    [HideInInspector] public int repeatingNameCap = ICConfig.repeatingNameCap;

    [HideInInspector] public List<string> leading = new List<string>(ICConfig.leading);
    [HideInInspector] public List<long> overridenKeys = new List<long>();
    [HideInInspector] public List<string> overridenNames = new List<string>();

    // Start is called before the first frame update
    void Awake()
    {
        ICConfig.namingSystem = namingSystem;
        ICConfig.verbose = verbose;

        ICConfig.SetPrintPrecision(printPrecision);
        ICConfig.SetRepeatingNameCap(repeatingNameCap);

        ICConfig.SetLeadingNames(leading);

        
        Dictionary<long, string> overridenNames = new Dictionary<long, string>();
        for(int i = 0; i < overridenKeys.Count; i++)
        {
            if(overridenNames.ContainsKey(overridenKeys[i]))
                overridenNames[overridenKeys[i]] = this.overridenNames[i];
            else
                overridenNames.Add(overridenKeys[i], this.overridenNames[i]);
        }

        ICConfig.OverrideNames(overridenNames);
    }

    public void Refresh()
    {
        namingSystem = ICConfig.namingSystem;
        verbose = ICConfig.verbose;

        printPrecision = ICConfig.printPrecision;
        repeatingNameCap = ICConfig.repeatingNameCap;

        leading = new List<string>(ICConfig.leading);

        
        foreach (KeyValuePair<long, string> overridenName in ICConfig.overriddenNames)
        {
            overridenKeys.Add(overridenName.Key);
            overridenNames.Add(overridenName.Value);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
