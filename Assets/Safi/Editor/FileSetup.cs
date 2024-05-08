using UnityEditor;
using UnityEngine;


public class FileSetup : Editor {

    string module = "Main Menu";

    void Awake() {
       // NA_Editor.GetLogo();
    }
    public override void OnInspectorGUI() {

        //NA_Editor.DefineGUIStyle(module);

        EditorGUILayout.BeginVertical("box");
        DrawDefaultInspector();
        EditorGUILayout.EndHorizontal();
    }
}
public class ResetSaveData{
	[MenuItem("Window/SafiKhan Framework/Reset Save Data %#r")]
	private static void ResetSave (){				
		Reset ();
	}
	[MenuItem("Window/SafiKhan Framework/Open Save File %#o")]
	private static void OpenSave (){
		Application.OpenURL (Application.persistentDataPath);
	}
	public static void Reset(){
		DataSaveLoad.DeleteProgress();
		EditorUtility.DisplayDialog("SafiKhan Framework",
			"Save data reset successfull !", 
			"Ok");
	}
}