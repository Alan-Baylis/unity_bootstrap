using UnityEngine;
using UnityEditor;

using System;
using System.Collections.Generic;

[CustomEditor(typeof(HeadsetManager))]
public class HeadsetManagerEditor : Editor
{
    ///////////////////////////////////////////////////////////////////////////
    //
    // Editor Overrides
    //

    protected HeadsetManager instance;
    public override void OnInspectorGUI()
    {
        this.instance = (HeadsetManager)this.target;

        HeadsetSelectionGUI();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(this.target);
        }
    }
    
    //
    ///////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////
    //
    // GUI
    //
    
    protected int   activeHeadsetIndex = -1;
    protected void  HeadsetSelectionGUI()
    {
        int newIndex;

        // do we have an active Headset?
        if (activeHeadsetIndex < 0)
        {
            activeHeadsetIndex = GetHeadsetIndex();
            if (activeHeadsetIndex < 0 && prefabs.Length > 0)
            {
                activeHeadsetIndex = 0;
                AddHeadset();
            }
        }

        newIndex = EditorGUILayout.Popup("Active Headset", activeHeadsetIndex, this.prefabNames);
        if (newIndex != activeHeadsetIndex)
        {
            this.activeHeadsetIndex = newIndex;
            ClearHeadsets();
            AddHeadset();
        }
    }  
    
    //
    ///////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////
    //
    // Headset Manager
    //
    
    protected void ClearHeadsets()
    {
        foreach (Transform child in this.instance.transform)
        {
            GameObject.DestroyImmediate(child.gameObject);
        }
    }

    protected void AddHeadset()
    {
        GameObject headsetPrefab;
        GameObject headset;

        headsetPrefab = prefabs[activeHeadsetIndex];

        headset = Instantiate(headsetPrefab);
        headset.transform.parent = this.instance.transform;
    }

    protected int GetHeadsetIndex()
    {
        int index;
        int howMany;
        string name;

        index   = -1;
        howMany = this.instance.transform.childCount;
        if (howMany > 0)
        {
            name    = this.instance.transform.GetChild(0).gameObject.name;
            name    = name.Substring(0, name.Length - 7);
            index   = Array.FindIndex<string>(this.prefabNames, x => x == name);
        }

        return index;
    }
    
    //
    ///////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////
    //
    // Load Headsets
    //
    
    protected string    externalPrefabPath = "Headsets/";
    protected string[]  _prefabNames;
    public string[]     prefabNames
    {
        get
        {
            // not cached?
            if (this._prefabNames == null)
            {
                this._prefabNames = LoadPrefabNames();
            }

            return this._prefabNames;
        }
    }

    protected string[] LoadPrefabNames()
    {
        GameObject prefab;
        string[] result;
        int howMany;
        int i;

        howMany = this.prefabs.Length;
        result = new string[howMany];

        for(i = 0; howMany > i; i++)
        {
            prefab = this.prefabs[i];
            result[i] = prefab.name;
        }

        return result;
    }

    protected GameObject[] _prefabs;
    public GameObject[] prefabs
    {
        get
        {
            return LoadExternalPrefabs();
        }
    }

    protected GameObject[] LoadExternalPrefabs()
    {
        return System.Array.ConvertAll(Resources.LoadAll(
            this.externalPrefabPath, typeof(GameObject)
        ), x => (GameObject) x);
    }
    
    //
    ///////////////////////////////////////////////////////////////////////////
}
