using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class FileManager
{
	public static string persistPath = "jar:file://" + Application.dataPath + "!/assets";

	private static FileManager instance;

	public static FileManager Instance
	{
		get
		{
			if (FileManager.instance == null)
			{
				FileManager.instance = new FileManager();
			}
			return FileManager.instance;
		}
		set
		{
		}
	}

	private FileManager()
	{
	}

	public static string getAppPath(string fileName)
	{
		return string.Format("{0}/{1}", FileManager.persistPath, fileName);
	}

	public static void writeFile(string fileName, byte[] datas)
	{
		string appPath = FileManager.getAppPath(fileName);
		UnityEngine.Debug.Log(appPath);
		FileInfo fileInfo = new FileInfo(appPath);
		Stream stream = fileInfo.Create();
		stream.Write(datas, 0, datas.Length);
		stream.Close();
		stream.Dispose();
	}

	public static bool isExists(string fileName)
	{
		string appPath = FileManager.getAppPath(fileName);
		UnityEngine.Debug.Log(appPath);
		FileInfo fileInfo = new FileInfo(appPath);
		return fileInfo.Exists;
	}

	public static byte[] readFile(string fileName)
	{
		string appPath = FileManager.getAppPath(fileName);
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileInfo fileInfo = new FileInfo(appPath);
		UnityEngine.Debug.Log(appPath);
		if (fileInfo.Exists)
		{
			UnityEngine.Debug.Log("find file successfully!!");
			using (FileStream fileStream = new FileStream(appPath, FileMode.Open, FileAccess.Read))
			{
				return (byte[])binaryFormatter.Deserialize(fileStream);
			}
		}
		return null;
	}

	public static void DeleteFolder(string dir)
	{
		string[] fileSystemEntries = Directory.GetFileSystemEntries(dir);
		for (int i = 0; i < fileSystemEntries.Length; i++)
		{
			string text = fileSystemEntries[i];
			if (File.Exists(text))
			{
				FileInfo fileInfo = new FileInfo(text);
				if (fileInfo.Attributes.ToString().IndexOf("ReadOnly") != -1)
				{
					fileInfo.Attributes = FileAttributes.Normal;
				}
				File.Delete(text);
			}
			else
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(text);
				if (directoryInfo.GetFiles().Length != 0)
				{
					FileManager.DeleteFolder(directoryInfo.FullName);
				}
				Directory.Delete(text);
			}
		}
	}
}
