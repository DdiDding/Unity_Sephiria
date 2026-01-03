using GameFramework.FileSystem;
using GameFramework;
using System.Data;
using UnityEngine;
using UnityGameFramework.Runtime;
using Unity.VisualScripting.FullSerializer;

public class NewMonoBehaviourScript : EntityLogic
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IFileSystemManager fileSystemManager =
    GameFrameworkEntry.GetModule<IFileSystemManager>();

        IFileSystem fs = fileSystemManager.GetFileSystem("Main.fs");

        //byte[] bytes = fs.ReadFile();
    }

    // Update is called once per frame
    void Update()
    {
        int a = 3;
    }
}
