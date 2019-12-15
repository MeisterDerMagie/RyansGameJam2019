/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 30/03/2017
/// Author: Chloroplast Games
/// Website: http://www.chloroplastgames.com
/// Programmers: Ferran Aguiló, Adan Baró
/// Description: Tool that allows sort comfortably the sorting layers and order in layer of the elements from de current scene.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditorInternal;
using UnityEngine;
using UnityEditor;

// Local Namespace
namespace Assets.CGF.Editor.Tools
{

    /// \english
    /// <summary>
    /// Sorting layer data
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Información de la sorting layer.
    /// </summary>
    /// \endspanish
    public class SortingLayerInfo
    {

        public int layerID;

        public string name = string.Empty;

        public List<Renderer> rendererList = new List<Renderer>();

        public ReorderableList _reorderableRendererList;

        public int enumIndex = 0;

    }

    /// \english
    /// <summary>
    /// Tool that allows sort comfortably the sorting layers and order in layer of the elements from de current scene.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Herramienta que permite ordenar de una forma cómoda la sorting layer y el order in layer de los elementos de la escena actual.
    /// </summary>
    /// \endspanish
    public class CGFSortingLayerManagerTool : EditorWindow
    {

        #region Public Variables

            public Renderer currentSelectedRenderer;

            public List<SortingLayerInfo> _layerList = new List<SortingLayerInfo>();

            public ReorderableList _reorderableLayerList;

            public Vector2 _scrollPosition = Vector2.zero;

            public string[] sortingLayerNames;

            public List<Renderer> currentRenderers = new List<Renderer>();

            public string[] currentLayerList;

            public bool _layerListSorted = true;

            public List<String> _layerNames = new List<String>();

            public List<String> _layerLabelNames = new List<String>();

            public int currentLayerSelected;

        #endregion


        #region Private Variables

            private PropertyInfo sortingLayersProperty;

            private System.Type internalEditorUtilityType;

            private SerializedObject tagManager;

            private SerializedProperty sortingLayersProp;

            private SerializedProperty currentSortingLayer;

        #endregion


        #region Main Methods

            [MenuItem("Window/Chloroplast Games Framework/Sorting Layer Manager Tool")]
            private static void ShowWindow()
            {
                EditorWindow window = EditorWindow.GetWindow(typeof(CGFSortingLayerManagerTool), false, "Sorting Layer Manager Tool", true);

                window.minSize = new Vector2(570, 400);
            }

            private void OnEnable()
            {

                tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);

                sortingLayersProp = tagManager.FindProperty("m_SortingLayers");

                CreateLayerNames();

                internalEditorUtilityType = typeof(InternalEditorUtility);

                sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames",
                    BindingFlags.Static | BindingFlags.NonPublic);

                sortingLayerNames = (string[])sortingLayersProperty.GetValue(null, new object[0]);

                _layerList.Clear();

                for (int i = 0; i < sortingLayerNames.Length; i++)
                {
                    SortingLayerInfo info = new SortingLayerInfo();

                    info.name = sortingLayerNames[i];

                    info.layerID = sortingLayersProp.GetArrayElementAtIndex(i).FindPropertyRelative("uniqueID").intValue;

                    _layerList.Add(info);

                }

                foreach (SortingLayerInfo sortingLayerInfo in _layerList)
                {

                    sortingLayerInfo.rendererList = new List<Renderer>();

                    foreach (Renderer sceneRenderer in FindObjectsOfType<Renderer>())
                    {

                        if (sceneRenderer.GetType().Name != "MeshRenderer" || sceneRenderer.GetComponent<TextMesh>() != null)
                        {

                            if (sceneRenderer.sortingLayerID == sortingLayerInfo.layerID)
                            {

                                sortingLayerInfo.rendererList.Add(sceneRenderer);

                                sceneRenderer.sortingOrder = sortingLayerInfo.rendererList.IndexOf(sceneRenderer);

                            }

                        }
                        
                    }

                    sortingLayerInfo._reorderableRendererList = new ReorderableList(sortingLayerInfo.rendererList,
                        typeof(Renderer), true, true, false, false);

                    _reorderableLayerList = new ReorderableList(_layerList, typeof(String), true, true, true, true);

                }

                currentSelectedRenderer = null;

                foreach (Renderer renderer in FindObjectsOfType<Renderer>())
                {

                    if (!(renderer is MeshRenderer) || renderer.GetComponent<TextMesh>() != null)
                    {

                        currentRenderers.Add(renderer);

                    }
                    
                }

            }

            private void OnGUI()
            {

                tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
                
                sortingLayersProp = tagManager.FindProperty("m_SortingLayers");

                sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);

                sortingLayerNames = (string[])sortingLayersProperty.GetValue(null, new object[0]);

                for (int i = 0; i < sortingLayerNames.Length; i++)
                {
                    if (_layerList.Count == sortingLayerNames.Length)
                    {

                        if (_layerList[i].layerID !=
                            sortingLayersProp.GetArrayElementAtIndex(i).FindPropertyRelative("uniqueID").intValue)
                        {

                            _layerListSorted = true;

                            currentSelectedRenderer = null;

                            break;

                        }

                    }

                }

                if (sortingLayerNames.Length == 1 && _layerList.Count != sortingLayerNames.Length)
                {

                    CreateLayerList();

                    CreateLayerNames();

                }

                if (_layerList.Count != sortingLayerNames.Length || _layerListSorted)
                {
                    
                    _reorderableLayerList = new ReorderableList(_layerList, typeof(String), true, true, true, true);

                    tagManager.ApplyModifiedProperties();

                    sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames",
                        BindingFlags.Static | BindingFlags.NonPublic);

                    sortingLayerNames = (string[])sortingLayersProperty.GetValue(null, new object[0]);

                    CreateLayerNames();
                    
                    foreach (Renderer renderer in FindObjectsOfType<Renderer>())
                    {

                        if (renderer.gameObject.activeSelf == true)
                        {

                            renderer.gameObject.SetActive(false);

                            renderer.gameObject.SetActive(true);

                        }
                        
                    }

                    _layerListSorted = false;

                }

                if (currentRenderers.Count != FindObjectsOfType<Renderer>().Length)
                {
                    
                    if (currentRenderers.Count < FindObjectsOfType<Renderer>().Length)
                    {

                        foreach (Renderer renderer in FindObjectsOfType<Renderer>())
                        {
                            if (!currentRenderers.Contains(renderer))
                            {
                                
                                currentRenderers.Add(renderer);

                                if (renderer.GetType().Name != "MeshRenderer" || renderer.GetComponent<TextMesh>() != null)
                                {

                                    foreach (SortingLayerInfo layerInfo in _layerList)
                                    {

                                        if (renderer.sortingLayerID == layerInfo.layerID)
                                        {

                                            layerInfo.rendererList.Add(renderer);

                                            layerInfo._reorderableRendererList = new ReorderableList(layerInfo.rendererList,
                                                typeof(Renderer), true, true, false, false);
                                            
                                        }

                                    }
                                    
                                }

                            }
                            
                        }
                        
                    }
                    else
                    {

                        foreach (Renderer renderer in currentRenderers)
                        {

                            if (!FindObjectsOfType<Renderer>().Contains(renderer))
                            {

                                foreach (SortingLayerInfo layerInfo in _layerList)
                                {

                                    for (int i = 0; i < layerInfo.rendererList.Count; i++)
                                    {

                                        if (layerInfo.rendererList[i] == renderer)
                                        {

                                            layerInfo.rendererList.RemoveAt(i);

                                            layerInfo._reorderableRendererList = new ReorderableList(
                                                layerInfo.rendererList, typeof(Renderer), true, true, false, false);

                                        }

                                    }

                                }

                            }

                        }

                        currentRenderers.Clear();

                        foreach (Renderer renderer in FindObjectsOfType<Renderer>())
                        {

                            currentRenderers.Add(renderer);

                        }

                    }

                    _reorderableLayerList = new ReorderableList(_layerList, typeof(String), true, true, true, true);

                    tagManager.ApplyModifiedProperties();

                    sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames",
                        BindingFlags.Static | BindingFlags.NonPublic);

                    sortingLayerNames = (string[])sortingLayersProperty.GetValue(null, new object[0]);

                    CreateLayerNames();

                    _layerListSorted = false;

                }

                GUILayout.BeginArea(new Rect(new Vector2(10, 20), new Vector2(position.width - 10, position.height - 10)));

                _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, false, true);

                _reorderableLayerList.drawHeaderCallback = (Rect rect) =>
                {

                    Rect namePosition = new Rect(new Vector2(rect.x, rect.y), new Vector2(rect.width / 2, rect.height));

                    EditorGUI.LabelField(namePosition, "Sorting Layers");

                };

                _reorderableLayerList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                {

                    Rect namePosition = new Rect(new Vector2(rect.x, rect.y), new Vector2(rect.width, rect.height));

                    Rect textAreaPosition = new Rect(new Vector2(namePosition.width / 4, rect.y),
                        new Vector2(rect.width - namePosition.width / 4, rect.height - 2));

                    EditorGUI.LabelField(namePosition, "Layer");

                    if (sortingLayersProp.GetArrayElementAtIndex(index).FindPropertyRelative("name").stringValue !=
                        "Default")
                    {

                        string layerName = GUI.TextField(textAreaPosition,
                            sortingLayersProp.GetArrayElementAtIndex(index).FindPropertyRelative("name").stringValue);

                        sortingLayersProp.GetArrayElementAtIndex(index).FindPropertyRelative("name").stringValue = layerName;

                        _layerList[index].name = layerName;

                        _layerLabelNames[index] = layerName;

                        _layerNames[index] = layerName;

                    }
                    else
                    {

                        EditorGUI.LabelField(textAreaPosition, "Default");

                    }

                };

                _reorderableLayerList.onAddCallback = (ReorderableList list) =>
                {

                    CreateNewLayer();

                };

                _reorderableLayerList.onRemoveCallback = (ReorderableList List) =>
                {

                    if (
                        sortingLayersProp.GetArrayElementAtIndex(currentLayerSelected)
                            .FindPropertyRelative("name")
                            .stringValue != "Default")
                    {

                        DeleteLayer();

                    }

                    currentSelectedRenderer = null;

                    currentLayerSelected = 0;

                };

                _reorderableLayerList.onSelectCallback = (ReorderableList list) =>
                {

                    currentLayerSelected = _reorderableLayerList.index;

                };

                _reorderableLayerList.DoLayoutList();

                GUILayout.Space(40);

                for (int i = 0; i < sortingLayersProp.arraySize; i++)
                {

                    if (sortingLayersProp.GetArrayElementAtIndex(i).FindPropertyRelative("name").stringValue !=_layerList[i].name ||
                        sortingLayersProp.GetArrayElementAtIndex(i).FindPropertyRelative("uniqueID").intValue != _layerList[i].layerID || 
                        sortingLayersProp.arraySize != _layerList.Count)
                    {

                        for (int dstLayer = 0; dstLayer < _layerList.Count; dstLayer++)
                        {

                            if (sortingLayersProp.GetArrayElementAtIndex(i).FindPropertyRelative("uniqueID").intValue ==
                                _layerList[dstLayer].layerID)
                            {

                                sortingLayersProp.MoveArrayElement(i, dstLayer);

                            }

                        }

                        _layerListSorted = true;

                        currentSelectedRenderer = null;

                    }
                    
                    

                }
                
                tagManager.ApplyModifiedProperties();
                
                tagManager.Update();

                
                
                foreach (SortingLayerInfo sortingLayerInfo in _layerList)
                {

                    EditorGUILayout.Space();

                    sortingLayerInfo._reorderableRendererList.drawHeaderCallback = (Rect rect) =>
                    {
                        
                        Rect namePosition = new Rect(new Vector2(rect.x, rect.y), new Vector2(rect.width / 4, rect.height));

                        EditorGUI.LabelField(namePosition, sortingLayerInfo.name);

                        Rect orderPosition = new Rect(new Vector2(namePosition.width, rect.y), new Vector2(100, rect.height));

                        EditorGUI.LabelField(orderPosition, "Order In Layer");

                        Rect rendererTyperPosition = new Rect(
                            new Vector2(namePosition.width + orderPosition.width, rect.y), new Vector2(100, rect.height));

                        EditorGUI.LabelField(rendererTyperPosition, "Renderer Type");

                        Rect buttonMovePosition =
                            new Rect(new Vector2(rect.width / 2 + (rect.width / 4) + (rect.width / 8), rect.y),
                                new Vector2(rect.width / 8, rect.height));

                        Rect popupPosition =
                            new Rect(
                                new Vector2(rendererTyperPosition.width + orderPosition.width + namePosition.width, rect.y),
                                new Vector2(
                                    (rect.width - buttonMovePosition.width) -
                                    (rendererTyperPosition.width + orderPosition.width + namePosition.width), rect.height));

                        sortingLayerInfo.enumIndex = EditorGUI.Popup(popupPosition, sortingLayerInfo.enumIndex,
                            _layerLabelNames.ToArray());

                        bool currentSelectedOnList = false;

                        foreach (Renderer tempsRenderer in sortingLayerInfo.rendererList)
                        {

                            if (currentSelectedRenderer != null && tempsRenderer != null)
                            {

                                if (currentSelectedRenderer.name == tempsRenderer.name)
                                {

                                    currentSelectedOnList = true;

                                    break;

                                }

                            }

                        }

                        if (currentSelectedRenderer == null ||
                            sortingLayerInfo.layerID ==
                            sortingLayersProp.GetArrayElementAtIndex(sortingLayerInfo.enumIndex)
                                .FindPropertyRelative("uniqueID")
                                .intValue || sortingLayerInfo.rendererList.Count <= 0 || !currentSelectedOnList)
                        {

                            GUI.enabled = false;

                            var moveButton = GUI.Button(buttonMovePosition, "Move");

                            if (moveButton)
                            {
                                foreach (Renderer renderer in sortingLayerInfo.rendererList)
                                {

                                    if (currentSelectedRenderer != null && currentSelectedRenderer.name == renderer.name)
                                    {

                                        sortingLayerInfo.rendererList.RemoveAt(
                                            sortingLayerInfo._reorderableRendererList.index);

                                        currentSelectedRenderer.sortingLayerID =
                                            sortingLayersProp.GetArrayElementAtIndex(sortingLayerInfo.enumIndex)
                                                .FindPropertyRelative("uniqueID")
                                                .intValue;

                                        _layerList[sortingLayerInfo.enumIndex].rendererList.Add(currentSelectedRenderer);

                                        currentSelectedRenderer = null;

                                        _layerList[sortingLayerInfo.enumIndex]._reorderableRendererList =
                                            new ReorderableList(_layerList[sortingLayerInfo.enumIndex].rendererList,
                                                typeof(Renderer), true, true, false, false);

                                        break;

                                    }

                                }

                            }

                            GUI.enabled = true;

                        }
                        else
                        {

                            var moveButton = GUI.Button(buttonMovePosition, "Move");

                            if (moveButton)
                            {

                                foreach (Renderer renderer in sortingLayerInfo.rendererList)
                                {

                                    if (currentSelectedRenderer != null && currentSelectedRenderer.name == renderer.name)
                                    {

                                        sortingLayerInfo.rendererList.RemoveAt(
                                            sortingLayerInfo._reorderableRendererList.index);

                                        currentSelectedRenderer.sortingLayerID =
                                            sortingLayersProp.GetArrayElementAtIndex(sortingLayerInfo.enumIndex)
                                                .FindPropertyRelative("uniqueID")
                                                .intValue;

                                        _layerList[sortingLayerInfo.enumIndex].rendererList.Add(currentSelectedRenderer);

                                        currentSelectedRenderer = null;

                                        _layerList[sortingLayerInfo.enumIndex]._reorderableRendererList =
                                            new ReorderableList(_layerList[sortingLayerInfo.enumIndex].rendererList,
                                                typeof(Renderer), true, true, false, false);

                                        break;

                                    }

                                }

                            }

                        }

                    };

                    sortingLayerInfo._reorderableRendererList.onSelectCallback = (ReorderableList list) =>
                    {

                        currentSelectedRenderer =
                            sortingLayerInfo.rendererList[sortingLayerInfo._reorderableRendererList.index];

                    };

                    sortingLayerInfo._reorderableRendererList.drawElementCallback =
                        (Rect rect, int index, bool isActive, bool isFocused) =>
                        {

                            Renderer renderer = sortingLayerInfo.rendererList[index];

                            if (renderer != null)
                            {

                                EditorGUILayout.BeginHorizontal();

                                Rect namePosition = new Rect(new Vector2(rect.x, rect.y), new Vector2(rect.width / 4, rect.height));

                                EditorGUI.LabelField(namePosition, renderer.name);

                                Rect sortingOrderPosition = new Rect(new Vector2(10 + namePosition.width, rect.y),
                                    new Vector2(100, rect.height));

                                EditorGUI.LabelField(sortingOrderPosition, renderer.sortingOrder.ToString());

                                Rect rendererTypePosition =
                                    new Rect(new Vector2(10 + namePosition.width + sortingOrderPosition.width, rect.y),
                                        new Vector2(100, rect.height));

                                EditorGUI.LabelField(rendererTypePosition, renderer.GetType().Name);

                                Rect buttonPosition =
                                    new Rect(new Vector2(13 + (rect.width / 2 + (rect.width / 4) + (rect.width / 8)), rect.y),
                                        new Vector2(rect.width / 8, rect.height - 2));

                                var selectButton = GUI.Button(buttonPosition, "Select");

                                EditorGUILayout.EndHorizontal();

                                if (selectButton)
                                {
                                    
                                    Selection.activeGameObject = renderer.gameObject;

                                }

                            }
                            

                        };
                    
                    foreach (Renderer rendererOnList in sortingLayerInfo.rendererList)
                    {

                        if (rendererOnList != null)
                        {

                            rendererOnList.sortingOrder = sortingLayerInfo.rendererList.IndexOf(rendererOnList);

                        }
                        
                   
                    }

                    sortingLayerInfo._reorderableRendererList.DoLayoutList();

                }

                GUILayout.EndScrollView();

                GUILayout.EndArea();

            }
        
        #endregion

        
        #region Utility Methods
      
            private void CreateLayerList()
            {
                
                _layerList.Clear();

                for (int i = 0; i < sortingLayerNames.Length; i++)
                {

                    SortingLayerInfo info = new SortingLayerInfo();

                    info.name = sortingLayerNames[i];

                    info.layerID = sortingLayersProp.GetArrayElementAtIndex(i).FindPropertyRelative("uniqueID").intValue;

                    _layerList.Add(info);

                }

                foreach (SortingLayerInfo sortingLayerInfo in _layerList)
                {

                    sortingLayerInfo.rendererList = new List<Renderer>();

                    foreach (Renderer sceneRenderer in FindObjectsOfType<Renderer>())
                    {

                        if (sceneRenderer.sortingLayerID == sortingLayerInfo.layerID)
                        {

                            sortingLayerInfo.rendererList.Add(sceneRenderer);

                            sceneRenderer.sortingOrder = sortingLayerInfo.rendererList.IndexOf(sceneRenderer);                            
                        }

                    }

                    sortingLayerInfo._reorderableRendererList = new ReorderableList(sortingLayerInfo.rendererList,
                        typeof(Renderer), true, true, false, false);

                    _reorderableLayerList = new ReorderableList(_layerList, typeof(String), true, true, true, true);

                }

                currentSelectedRenderer = null;

                currentRenderers.Clear();

                foreach (Renderer renderer in FindObjectsOfType<Renderer>())
                {

                    currentRenderers.Add(renderer);

                }

            }

            private void CreateLayerNames()
            {

                _layerNames.Clear();

                _layerLabelNames.Clear();

                for (int i = 0; i < sortingLayersProp.arraySize; i++)
                {

                    string sortingLayerName =
                        sortingLayersProp.GetArrayElementAtIndex(i).FindPropertyRelative("name").stringValue;

                    string sortingLayerLabelName = sortingLayerName;

                    _layerLabelNames.Add(sortingLayerLabelName);

                    _layerNames.Add(sortingLayerName);

                }

            }

            private void CreateNewLayer()
            {

                int newID = 0;

                string newLayerName = "";

                bool foundID = false;

                sortingLayersProp.InsertArrayElementAtIndex(sortingLayersProp.arraySize);

                for (int i = 0; i < sortingLayersProp.arraySize; i++)
                {

                    for (int j = 0; j < sortingLayersProp.arraySize; j++)
                    {

                        if (i == sortingLayersProp.GetArrayElementAtIndex(j).FindPropertyRelative("uniqueID").intValue)
                        {

                            foundID = false;

                            break;

                        }

                        else
                        {

                            foundID = true;

                        }

                    }

                    if (foundID)
                    {

                        newID = i;

                        newLayerName = "New Layer (" + newID + ")";

                        break;

                    }

                }

                sortingLayersProp.GetArrayElementAtIndex(sortingLayersProp.arraySize - 1)
                    .FindPropertyRelative("name")
                    .stringValue = newLayerName;

                sortingLayersProp.GetArrayElementAtIndex(sortingLayersProp.arraySize - 1)
                    .FindPropertyRelative("uniqueID")
                    .intValue = newID;

                tagManager.ApplyModifiedProperties();

                sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames",
                    BindingFlags.Static | BindingFlags.NonPublic);

                sortingLayerNames = (string[])sortingLayersProperty.GetValue(null, new object[0]);

                SortingLayerInfo newLayer = new SortingLayerInfo();

                newLayer.name = newLayerName;

                newLayer.rendererList = new List<Renderer>();

                newLayer.layerID = newID;

                newLayer._reorderableRendererList = new ReorderableList(newLayer.rendererList, typeof(Renderer), true, true,
                    false, false);

                _layerList.Add(newLayer);

                _layerNames.Add(newLayerName);

                _layerLabelNames.Add(newLayerName);

                _reorderableLayerList = new ReorderableList(_layerList, typeof(String), true, true, true, true);

                currentLayerSelected = 0;

                foreach (SortingLayerInfo layerinfo in _layerList)
                {

                    layerinfo.enumIndex = 0;

                }

            }

            private void DeleteLayer()
            {

                for (int i = 0; i < _layerList[currentLayerSelected].rendererList.Count; i++)
                {

                    foreach (SortingLayerInfo layerinfo in _layerList)
                    {

                        if (layerinfo.layerID == 0)
                        {

                            Renderer newRenderer = _layerList[currentLayerSelected].rendererList[i];

                            layerinfo.rendererList.Add(newRenderer);

                            newRenderer.sortingLayerID = layerinfo.layerID;

                            newRenderer.sortingLayerName = layerinfo.name;

                            layerinfo._reorderableRendererList = new ReorderableList(layerinfo.rendererList,
                                typeof(Renderer), true, true, false, false);

                        }

                    }

                }

                foreach (SortingLayerInfo layerinfo in _layerList)
                {

                    layerinfo.enumIndex = 0;

                }

                _layerLabelNames.Clear();

                sortingLayersProp.DeleteArrayElementAtIndex(currentLayerSelected);

                _layerList.RemoveAt(currentLayerSelected);

                sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames",
                    BindingFlags.Static | BindingFlags.NonPublic);

                sortingLayerNames = (string[])sortingLayersProperty.GetValue(null, new object[0]);

                tagManager.ApplyModifiedProperties();

                for (int i = 0; i < sortingLayersProp.arraySize; i++)
                {

                    string sortingLayerName =
                        sortingLayersProp.GetArrayElementAtIndex(i).FindPropertyRelative("name").stringValue;

                    string sortingLayerLabelName = sortingLayerName;

                    _layerLabelNames.Add(sortingLayerLabelName);

                    _layerNames.Add(sortingLayerName);

                }

                _reorderableLayerList = new ReorderableList(_layerList, typeof(String), true, true, true, true);

            }

        #endregion


        #region Utility Events

        #endregion
    
    }

}