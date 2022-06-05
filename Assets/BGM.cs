 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.SceneManagement;
 
 public class BGM : MonoBehaviour
 {
     [Tooltip("A unique string identifier for this object, must be shared across scenes to work correctly")]
     public string instanceName = "MusicPlayer";
 
     private void Start()
     {
         DontDestroyOnLoad(this.gameObject);
 
         // subscribe to the scene load callback
         SceneManager.sceneLoaded += OnSceneLoaded;
     }
 
     void OnSceneLoaded(Scene scene, LoadSceneMode mode)
     {
         // delete any potential duplicates that might be in the scene already, keeping only this one 
         CheckForDuplicateInstances();
     }
 
     void CheckForDuplicateInstances()
     {
         // cache all objects containing this component
         BGM[] collection = FindObjectsOfType<BGM>();
 
         // iterate through the objects with this component, deleting those with matching identifiers
         foreach (BGM obj in collection)
         {
             if(obj != this) // avoid deleting the object running this check
             {
                 if (obj.instanceName == instanceName)
                 {
                     DestroyImmediate(obj.gameObject);
                 }
             }
         }
     }
 }
