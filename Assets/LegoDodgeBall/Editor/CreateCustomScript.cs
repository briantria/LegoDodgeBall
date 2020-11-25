using UnityEditor;

// template samples: https://4experience.co/how-to-create-useful-unity-script-templates/

/*
    TODO:
        - Test Scripts
        - Scriptable Objects
        - Native C# Class
*/

public class CreateCustomScript
{
    #region Paths

    private const string kMenuHierarchy = "Assets/Create/Custom Script/";
    private const string kScriptTemplateFolder = "Assets/LegoDodgeBall/Editor/CustomScriptTemplates/";

    #endregion

    #region FileNames

    private const string kCustomMonoBehaviorTemplatePath = "MonoBehaviourTemplate.cs.txt";
    private const string kCustomInterfaceTemplatePath = "InterfaceTemplate.cs.txt";
    private const string kCustomNativeScriptTemplatePath = "NativeC#ScriptTemplate.cs.txt";

    #endregion

    #region Methods

    [MenuItem(itemName: kMenuHierarchy + "MonoBehaviour", isValidateFunction: false, priority: 51)]
    public static void CreateCustomMonoBehaviour()
    {
        string path = kScriptTemplateFolder + kCustomMonoBehaviorTemplatePath;
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(path, "NewMonoBehaviour.cs");
    }

    [MenuItem(itemName: kMenuHierarchy + "Interface", isValidateFunction: false, priority: 51)]
    public static void CreateCustomInterface()
    {
        string path = kScriptTemplateFolder + kCustomInterfaceTemplatePath;
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(path, "IInterface.cs");
    }

    [MenuItem(itemName: kMenuHierarchy + "Native C# Script", isValidateFunction: false, priority: 51)]
    public static void CreateCustomNativeScript()
    {
        string path = kScriptTemplateFolder + kCustomNativeScriptTemplatePath;
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(path, "NewClass.cs");
    }

    #endregion
}
