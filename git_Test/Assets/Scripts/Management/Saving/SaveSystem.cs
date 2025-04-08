using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem 
{
    private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";
    
   
    
    
    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void Save(string saveString)
    {
        
        int saveNumber = 1;
        while (File.Exists(SAVE_FOLDER + "save_" + saveNumber + ".sav"))
        {
            saveNumber++;
        }
        
        Debug.Log("Saving Game. Save Number is: " + saveNumber);
        File.WriteAllText(SAVE_FOLDER + "save_" + saveNumber + ".sav", saveString);
        Debug.Log("File Saved to: " + SAVE_FOLDER + "save_" + saveNumber + ".sav");
    }
    public static void Save(string saveString, string saveName)
    {

       
        Debug.Log("Saving Game. Save Name is: " + saveName);
        File.WriteAllText(SAVE_FOLDER + saveName, saveString);
        Debug.Log("File Saved to: " + SAVE_FOLDER + saveName);
    }

    public static string[] ListOfSaveFiles()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
        FileInfo[] saveFiles = directoryInfo.GetFiles("*.sav");
        
        string[] saveFileString = new string[saveFiles.Length];
        for(int i = 0; i < saveFileString.Length; i++)
        {
            saveFileString[i] = saveFiles[i].Name.ToString();
          
        }


        return saveFileString;
    }
    public static string Load()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
        FileInfo[] saveFiles = directoryInfo.GetFiles("*.sav");
        FileInfo mostRecentFile = null;
        foreach (FileInfo fileInfo in saveFiles)
        {
            if (mostRecentFile == null)
            {
                mostRecentFile = fileInfo;
            }
            else
            {
                if (fileInfo.LastWriteTime > mostRecentFile.LastWriteTime && fileInfo.Name != "HighScores.txt")
                {
                    mostRecentFile = fileInfo;
                    Debug.Log("file info is: " + fileInfo);
                }

            }
        }

        if(mostRecentFile != null)
        {
            string saveString = File.ReadAllText(mostRecentFile.FullName);
            return saveString;
        }
        else
        {
            return null;
        }

        /*if (File.Exists(SAVE_FOLDER + "save.txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "save.txt");
            return saveString;
        }
        else
        {
            return null;
        }
        */
    }

    

    public static void SaveHighScores(string saveHighScoreString)
    {
        Debug.Log("Saving High Scores...");

        File.WriteAllText(SAVE_FOLDER + "HighScores_" + SceneManagerScript.instance.sceneName + ".txt", saveHighScoreString);
        Debug.Log("File Saved to: " + SAVE_FOLDER + "HighScores_" + SceneManagerScript.instance.sceneName + ".txt");
    }

    public static string LoadHighScores()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
        FileInfo[] saveFiles = directoryInfo.GetFiles("HighScores_" + SceneManagerScript.instance.sceneName + ".txt");
        FileInfo mostRecentFile = null;
        foreach (FileInfo fileInfo in saveFiles)
        {
            if (mostRecentFile == null)
            {
                mostRecentFile = fileInfo;
            }
            else
            {
                if (fileInfo.LastWriteTime > mostRecentFile.LastWriteTime)
                {
                    mostRecentFile = fileInfo;

                }

            }
        }

        if (mostRecentFile != null)
        {
            string saveString = File.ReadAllText(mostRecentFile.FullName);
            //string replacementString = saveString.Replace("\n", "");

            //Debug.Log("Here is the string:" + saveString);
            //Debug.Log("Here is the replacement string:" + replacementString);
            //return replacementString;
            return saveString;
        }
        else
        {
            return null;
        }
    }


}
