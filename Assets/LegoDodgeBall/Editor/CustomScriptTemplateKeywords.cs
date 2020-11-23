using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

/*  source:
 *      - https://gist.github.com/nicoplv/3e65b261e6d1693db38fea2b67813c8d
 *      - https://forum.unity.com/threads/c-script-template-how-to-make-custom-changes.273191/
 **/
internal sealed class CustomScriptTemplateKeywords : UnityEditor.AssetModificationProcessor
{
    #region Keywords

    private const string kNameSpaceKeyword = "#NAMESPACE#";
    private const string kCreationDateKeyword = "#CREATIONDATE#";
    private const string kAuthorKeyword = "#AUTHOR#";

    #endregion

    #region Constant Values

    private const string kAuthor = "Brian Tria";
    private const string kCreationDateFormat = "dd/MM/yyyy HH:mm:ss";
    private const string kNameSpace = "LegoDodgeBall";

    #endregion

    private static char[] spliters = new char[] { '/', '\\', '.' };
    private static List<string> wordsToDelete = new List<string>() { "Extensions", "Scripts", "Editor" };

    public static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", "");
        int index = path.LastIndexOf(".");
        if (index < 0)
        {
            return;
        }

        string file = path.Substring(index);
        if (file != ".cs" && file != ".js")
        {
            return;
        }

        // List<string> namespaces = path.Split(spliters).ToList<string>();
        // namespaces = namespaces.GetRange(1, namespaces.Count - 3);
        // namespaces = namespaces.Except(wordsToDelete).ToList<string>();

        // string namespaceString = "TLM";

        // for (int i = 0; i < namespaces.Count; i++)
        // {
        //     if (i == 0)
        //     {
        //         namespaceString = "";
        //     }

        //     namespaceString += namespaces[i];

        //     if (i < namespaces.Count - 1)
        //     {
        //         namespaceString += ".";
        //     }
        // }

        index = Application.dataPath.LastIndexOf("Assets");
        path = Application.dataPath.Substring(0, index) + path;
        if (!System.IO.File.Exists(path))
        {
            return;
        }

        string fileContent = System.IO.File.ReadAllText(path);
        fileContent = fileContent.Replace(kNameSpaceKeyword, kNameSpace);
        fileContent = fileContent.Replace(kCreationDateKeyword, System.DateTime.Now.ToString(kCreationDateFormat) + "");
        fileContent = fileContent.Replace(kAuthorKeyword, kAuthor);
        //fileContent = fileContent.Replace("#SMARTDEVELOPERS#", PlayerSettings.companyName);

        System.IO.File.WriteAllText(path, fileContent);
        AssetDatabase.Refresh();
    }
}
