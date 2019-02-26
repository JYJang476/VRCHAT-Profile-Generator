
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

public class script : EditorWindow {

    GameObject SSField;
    Texture SSTex;
    GameObject SSCamera;
    
    [MenuItem("JJY/Profile Generator")]
    static void Init()
    {
        
        script window = (script)GetWindow(typeof(script), false, "Generator");
        window.minSize = new Vector2(350, 110);
        window.maxSize = new Vector2(350, 110);       
        window.Show();
    }
    private void OnGUI()
    {
        try
        {
            if (Selection.activeObject != null)
            {
                

                if (Selection.activeGameObject.GetComponent<MeshFilter>().mesh.name.IndexOf("Quad Instance") > -1)
                {
                    Debug.Log("quad ok");
                    GameObject SelGameobj = Selection.activeGameObject;
                    SSField = SelGameobj;
                    SSTex = SelGameobj.GetComponent<Renderer>().material.mainTexture;
                }
            }
        } catch (Exception e)
        {
            Debug.Log(e.Message);
        }

        if(GameObject.Find("VRCCam")) SSCamera = GameObject.Find("VRCCam");       
        
        SSCamera = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Camera: ", "Camera"), SSCamera, typeof(GameObject), true);

        SSTex = (Texture)EditorGUILayout.ObjectField(new GUIContent("Image: ", "Image"), SSTex, typeof(Texture), true);
        if (SSTex != null && SSField != null) 
        {
            if (SSField.GetComponent<Renderer>().material.mainTexture != SSTex && SSField == Selection.activeGameObject) 
            {
                SSField.GetComponent<Renderer>().material.mainTexture = SSTex;
                SSField.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Texture");
            }

            Vector3 CheckVector = new Vector3(SSField.transform.position.x + 0.001f, SSField.transform.position.y, SSField.transform.position.z - 0.88f);

            if (SSCamera != null)
            {
                if (SSCamera.transform.position != CheckVector || SSCamera.transform.rotation.y != 0.0f)
                {
                    SSCamera.transform.SetPositionAndRotation(CheckVector, Quaternion.Euler(0.0f, 0.0f, 0.0f));
                }
            }

        }        
        if (GUILayout.Button("Generate Profile"))
        {

            SSField = GameObject.CreatePrimitive(PrimitiveType.Quad);
            if (SSField != null)
            {
                SSField.name = "Profile";
                
                SSField.transform.SetPositionAndRotation(new Vector3(1f, 1f, 1f), new Quaternion(0f, 0f, 0f, 0f));
                SSField.transform.localScale = new Vector3(1.35f, 1f, 1f);  
                
                SSField.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Texture");
                SSField.GetComponent<Renderer>().material.mainTexture = SSTex;

                Selection.activeGameObject = SSField;
                

                if (SSCamera != null) SSCamera.transform.SetPositionAndRotation(new Vector3(1.001f, 0.12f, 0.99f), Quaternion.Euler(0.0f, 0.0f, 0.0f));

            }


        }


    }
}
