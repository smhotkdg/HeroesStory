using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GR_InfiniCoin;

[CustomEditor(typeof(GRIC_Configurator))]
public class GRIC_ConfiguratorEditor : Editor
{
    GRIC_Configurator configurator;

    bool verbose;
    InfiniName suffixType;
    int printPrecision;
    int rNameCap;

    List<string> leadT;
    List<long> oKeysT;
    List<string> oNameT;

    private static GUIContent verboseGUI = new GUIContent("Verbose Mode", "Verbose mode toggles the warning messages.\n\nThe warnings include information such as disregarded operations due to large power of 1000 differences, or configuration warnings regarding limits.");
    private static GUIContent suffixGUI = new GUIContent("Naming System", "Naming system defines the algorithm for generating names for large powers of 1000. Play around with the preview above to see how numbers look in this current configuration.");
    private static GUIContent prtPrecGUI = new GUIContent("Print Precision", "Print precision defines the number of decimal places when printed.");
    private static GUIContent rNameCapGUI = new GUIContent("Repeating Name Cap", "Repeating name cap defines the maximum number of letters to be used in the name. Beyond this number, the name will be shown in the form of XN where X is the letter and N is the number of letters, like scientific notation.");

    private static string leadingInfo = "Leading names are the names for the initial, human readable powers of 1000. The generated names start after the last leading name.";
    private static string overridenInfo = "Overriden names replace the class generated names. If you want to override a particular name, you can add it below. For overriding the entire system, use the ICConfig's SetOverridenNames() method instead.";

    private long ppow = 0;
    private double pval = 0;

    InfiniCoin prev = new InfiniCoin();

    private void Awake()
    {
        configurator = target as GRIC_Configurator;

        Undo.undoRedoPerformed += UndoCallback;
    }

    public override void OnInspectorGUI()
    {
        leadT = new List<string>(configurator.leading);
        oKeysT = new List<long>(configurator.overridenKeys);
        oNameT = new List<string>(configurator.overridenNames);

        base.OnInspectorGUI();

        EditorGUILayout.LabelField("Preview", Styles.BoldLabel());
        EditorGUI.BeginChangeCheck();
        pval = EditorGUILayout.DoubleField("Base", pval);
        ppow = EditorGUILayout.LongField("Power", ppow);

        if (EditorGUI.EndChangeCheck())
        {
            prev.Set1000Power(ppow);
            prev.SetBaseValue(pval);

            pval = prev.baseValue;
            ppow = prev.kPower;
        }
        EditorGUILayout.LabelField("Preview: " + prev);

        EditorGUI.BeginChangeCheck();
        verbose = EditorGUILayout.Toggle(verboseGUI, configurator.verbose);

        suffixType = (InfiniName)EditorGUILayout.EnumPopup(suffixGUI, configurator.namingSystem);
        printPrecision = EditorGUILayout.IntSlider(prtPrecGUI, configurator.printPrecision, 0, 9);
        rNameCap = EditorGUILayout.IntSlider(rNameCapGUI, configurator.repeatingNameCap, 1, 9);

        LeadingSuffixes();
        OverridenNames();

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RegisterCompleteObjectUndo(configurator, "config");

            configurator.verbose = verbose;
            configurator.namingSystem = suffixType;
            configurator.printPrecision = printPrecision;
            configurator.repeatingNameCap = rNameCap;

            configurator.leading = new List<string>(leadT);
            configurator.overridenKeys = new List<long>(oKeysT);
            configurator.overridenNames = new List<string>(oNameT);

            ICConfig.namingSystem = suffixType;
            ICConfig.verbose = verbose;

            ICConfig.SetPrintPrecision(printPrecision);
            ICConfig.SetRepeatingNameCap(rNameCap);

            ICConfig.SetLeadingNames(configurator.leading.ToArray());

            Dictionary<long, string> dict = new Dictionary<long, string>();
            for (int i = 0; i < configurator.overridenKeys.Count; i++)
            {
                if (dict.ContainsKey(configurator.overridenKeys[i]))
                    dict[configurator.overridenKeys[i]] = configurator.overridenNames[i];
                else
                    dict.Add(configurator.overridenKeys[i], configurator.overridenNames[i]);
            }
            ICConfig.OverrideNames(dict);
        }
    }

    private void LeadingSuffixes()
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Leading Suffixes", Styles.BoldLabel());
        if (verbose) EditorGUILayout.LabelField(leadingInfo, Styles.VerboseLabel());
        EditorGUIUtility.labelWidth = 1;

        for (int i = 0; i < configurator.leading.Count; i++)
        {
            if (leadT.Count <= i)
                continue;

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("1000 ^ " + i);
            leadT[i] = EditorGUILayout.TextField(configurator.leading[i]);
            if (GUILayout.Button("x", Styles.SlimButton()))
            {
                DeleteLeadingSuffix(i);
            }
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Add Leading Suffix"))
        {
            leadT.Add("");
        }
    }

    private void OverridenNames()
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Overriden Names", Styles.BoldLabel());
        if (verbose) EditorGUILayout.LabelField(overridenInfo, Styles.VerboseLabel());
        int count = Mathf.Min(oKeysT.Count, oNameT.Count);
        EditorGUIUtility.labelWidth = 1;

        for (int i = 0; i < count; i++)
        {
            if (oKeysT.Count <= i || oNameT.Count <= i)
                continue;

            EditorGUILayout.LabelField("Default: " + ICConfig.Name(oKeysT[i], false));
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Power of 1000");
            oKeysT[i] = EditorGUILayout.LongField(oKeysT[i], Styles.SuffixField());
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Custom Name");
            oNameT[i] = EditorGUILayout.TextField(oNameT[i], Styles.SuffixField());
            EditorGUILayout.EndVertical();

            if (GUILayout.Button("x", Styles.PaddedButton()))
            {
                DeleteCustomSuffix(i);
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }

        if (GUILayout.Button("Add Suffix Override"))
        {
            oKeysT.Add(0);
            oNameT.Add("");
        }
    }

    private void DeleteCustomSuffix(int i)
    {
        oKeysT.RemoveAt(i);
        oNameT.RemoveAt(i);
    }

    private void DeleteLeadingSuffix(int i)
    {
        leadT.RemoveAt(i);
    }

    private void UndoCallback()
    {
        leadT = new List<string>(configurator.leading);
        oKeysT = new List<long>(configurator.overridenKeys);
        oNameT = new List<string>(configurator.overridenNames);
    }

    private static class Styles
    {
        private static GUIStyle slimButton = null;
        private static GUIStyle paddedButton = null;
        private static GUIStyle suffixField = null;
        private static GUIStyle boldLabel = null;
        private static GUIStyle verboseLabel = null;

        public static GUIStyle PaddedButton()
        {
            if (paddedButton == null)
            {
                paddedButton = new GUIStyle(GUI.skin.button);
                paddedButton.margin = new RectOffset(10, 10, 10, 10);
                paddedButton.fixedWidth = 30;
                paddedButton.fixedHeight = 30;
                paddedButton.alignment = TextAnchor.MiddleCenter;
                paddedButton.fontStyle = FontStyle.Bold;
            }

            return paddedButton;
        }

        public static GUIStyle SlimButton()
        {
            if (slimButton == null)
            {
                slimButton = new GUIStyle(GUI.skin.button);
                slimButton.fixedWidth = 18;
                slimButton.fixedHeight = 18;
                slimButton.alignment = TextAnchor.MiddleCenter;
                slimButton.fontStyle = FontStyle.Bold;
            }

            return slimButton;
        }

        public static GUIStyle SuffixField()
        {
            if (suffixField == null)
            {
                suffixField = new GUIStyle(GUI.skin.textField);
                suffixField.fixedWidth = 75;
            }

            return suffixField;
        }

        public static GUIStyle BoldLabel()
        {
            if (boldLabel == null)
            {
                boldLabel = new GUIStyle(GUI.skin.label);
                boldLabel.fontStyle = FontStyle.Bold;
                boldLabel.fontSize += 1;
            }

            return boldLabel;
        }


        public static GUIStyle VerboseLabel()
        {
            if (verboseLabel == null)
            {
                verboseLabel = new GUIStyle(GUI.skin.label);
                verboseLabel.wordWrap = true;
                verboseLabel.fontSize -= 1;
                verboseLabel.margin = new RectOffset(0, 0, 0, 10);
            }

            return verboseLabel;
        }
    }
}
