using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Generic save system that you can put a class into to save to text
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

    //This method saves the file name as "save_" + next incremented number of saveo needed to not overwrite saves, 
    public static void Save(string saveString)
    {
        
        int saveNumber = 1;
        while (File.Exists(SAVE_FOLDER + "save_" + saveNumber + ".sav"))
        {
            saveNumber++;
        }
        
     
        File.WriteAllText(SAVE_FOLDER + "save_" + saveNumber + ".sav", saveString);

    }

    //This Save function lets you choose the save name
        //this is placed as a delegate to buttons populated in the save menu
    public static void Save(string saveString, string saveName)
    {
        File.WriteAllText(SAVE_FOLDER + saveName, saveString);
    }


    //Returns a string[] of names of save files in the save directory
        //Used to populate Save and Load menus
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
    
    //This method loads the last saved file in the save folder.
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

    }

    //This method loads the specific file name, this is placed as a delegate to buttons populated in the load menu 
    public static string Load(string fileName)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
        FileInfo[] loadFiles = directoryInfo.GetFiles("*.sav");
        FileInfo loadFile = null;
        foreach (FileInfo fileInfo in loadFiles)
        {
            if(fileInfo.Name == fileName)
            {
                loadFile = fileInfo;
            }
        }

        if (loadFile != null)
        {
            string saveString = File.ReadAllText(loadFile.FullName);
            return saveString;
        }
        else
        {
            return null;
        }

    }

    //Method saves the highscores as a separate file for each scene. Implemented after getting a new HS.
    public static void SaveHighScores(string saveHighScoreString)
    {
        Debug.Log("Saving High Scores...");

        File.WriteAllText(SAVE_FOLDER + "HighScores_" + SceneManagerScript.instance.sceneName + ".txt", saveHighScoreString);
        
    }

    //Loads the highscores from the relevant HS file. Used in Menu manager to populate the HS screen.
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
