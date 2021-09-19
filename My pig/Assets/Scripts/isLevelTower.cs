using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static PiUI;
using Assets.Scripts.Radial_Menu;
[System.Serializable]
public class isLevelTower : MonoBehaviour
{
    public bool lvl = false;
    public towerData[] towerDatas;
    public Vector3 Martposition;
    internal bool equalSlices;
    [Tooltip("The style this piUI opens.")]
    [SerializeField]
    public TransitionType openTransition;
    [Tooltip("The style this piUI closes.")]
    [SerializeField]
    public TransitionType closeTransition;
    [Tooltip("Default pi Slice to instantiate suggested to be a child gameobject to make adjusting values easier.")]
    public isLvl piCut;
    public bool dynamicallyScaleToResolution;
    [Tooltip("All slices will have an outline.")]
    public bool outline;
    [Tooltip("Adjust this to match the inner radius of the piCut Sprite.")]
    public float innerRadius;
    [Tooltip("Adjust this to match the outer radius of the piCut Sprite.")]
    public float outerRadius;
    [HideInInspector]
    public float scaleModifier = 1;
    [HideInInspector]
    public bool interactable = false;
    [SerializeField]
    [Tooltip("Resolution to match.")]
    private Vector2 defaultResolution;
    [Tooltip("How fast this piUI opens/closes.")]
    [SerializeField]
    float transitionSpeed;
    [Header("Color Settings")]
    [Space(10)]

    [Tooltip("All slices will share a color.")]
    public bool syncColors;
    [HideInInspector]
    public bool openedMenu;
    private Vector2 menuPosition;
    [HideInInspector]
    public bool worldSpace;
    [Tooltip("The synced non-highlighted color.")]
    [SerializeField]
    private Color syncNormal;
    public readonly List<isLvl> piList = new List<isLvl>();
    [SerializeField]
    [Tooltip("The synced highlighted color.")]
    private Color syncSelected;
    [SerializeField]
    [HideInInspector]
    private float[] angleList;
    [HideInInspector]
    [Range(1, 30)]
    public int sliceCount;
    [Tooltip("Color of outline.")]
    [SerializeField]
    private Color outlineColor;
    [Tooltip("The synced disabled color.")]
    [SerializeField]
    private Color syncDisabled;
    [HideInInspector]
    public bool overMenu;
    internal bool fade;
    internal float textVerticalOffset;
    internal float iconDistance;
    [HideInInspector]
    public bool joystickButton;
    [HideInInspector]
    public Vector2 joystickInput;
    [Tooltip("How the slices scale on hover")]
    [Range(1, 3)]
    public float hoverScale;
    [Tooltip("Enable Controller Support")]
    public bool useController;
    private void Awake()
    {
        if (transform.parent.GetComponent<Canvas>().renderMode == RenderMode.WorldSpace)
        {
            worldSpace = true;
        }
        else
        {
            worldSpace = false;
        }
        if (dynamicallyScaleToResolution)
        {
            if (Screen.width > Screen.height)
            {
                scaleModifier = Screen.height / defaultResolution.y;
            }
            else
            {
                scaleModifier = Screen.width / defaultResolution.x;
            }
        }
        innerRadius *= scaleModifier;
        outerRadius *= scaleModifier;
        GenerateLevel(new Vector2(-1000, -1000));
    }

    internal void CloseMenu()
    {
        openedMenu = false;
    }
    public void OpenMenu(Vector2 screenPos)
    {
        menuPosition = screenPos;
        openedMenu = true;
        foreach (isLvl pi in piList)
        {
            pi.gameObject.SetActive(true);
        }
        if (piList.Count == 0)
        {
            GenerateLevel(screenPos);
        }
        else
        {
            ResetPiRotation();
        }
        Vector2 tempPos = menuPosition;
        switch (openTransition)
        {
            case TransitionType.Scale:
                transform.localScale *= 0;
                break;

        }
    }
    private void ResetPiRotation()
    {
        float lastRot = 0;
        for (int i = 0; i < piList.Count; i++)
        {

            float fillPercentage = (1f / sliceCount);
            float angle = fillPercentage * 360;
            if (!equalSlices)
            {
                angle = towerDatas[i].angle;
            }
            int rot = Mathf.Clamp((int)(angle + lastRot), 0, 359);
            Vector3 rotVec = new Vector3(0, 0, rot);
            piList[i].transform.rotation = Quaternion.Euler(rotVec);
            lastRot += angle;
        }
    }

    public void UpdateislvlUI()
    {
        foreach (isLvl currentPi in piList)
        {
            if (syncColors)
            {
                SyncColor();
            }
            Image sliceIcon = currentPi.transform.GetChild(0).GetComponent<Image>();
            Text sliceText = currentPi.transform.GetChild(1).GetComponent<Text>();
            sliceIcon.sprite = towerDatas[piList.IndexOf(currentPi)].icon;
            sliceText.text = towerDatas[piList.IndexOf(currentPi)].sliceLabel;
            currentPi.Set2Data(towerDatas[piList.IndexOf(currentPi)], innerRadius, outerRadius, this);
        }

    }
    public void GenerateLevel(Vector2 screenPosition)
    {
        sliceCount = towerDatas.Length;
        if (piList.Count > 1)
        {
            //ClearMenu();
        }
        transform.position = screenPosition;
        float lastRot = 0;
        if (syncColors)
        {
            SyncColor();
        }
        angleList = new float[sliceCount];
        for (int i = 0; i < sliceCount; i++)
        {
            isLvl currentPi = Instantiate(piCut);
            Image currentImage = currentPi.GetComponent<Image>();
            if (outline)
            {
                currentPi.GetComponent<Outline>().effectColor = outlineColor;
            }
            else
            {
                currentPi.GetComponent<Outline>().enabled = outline;

            }
            float fillPercentage = (1f / sliceCount);
            float angle = fillPercentage * 360;
            if (!equalSlices)
            {
                angle = towerDatas[i].angle;
                fillPercentage = towerDatas[i].angle / 360;
            }
            currentImage.fillAmount = fillPercentage;
            angle = (angle + 360) % 360;
            if (angle == 0)
            {
                angle = 360;
            }
            currentPi.transform.SetParent(transform);
            int rot = Mathf.Clamp((int)(angle + lastRot), 0, 360);
            if (rot == 360)
            {
                rot = 0;
            }
            currentPi.transform.rotation = Quaternion.Euler(0, 0, rot);
            lastRot += angle;
            angleList[i] = rot;
            currentPi.gameObject.SetActive(true);
            currentImage.rectTransform.localPosition = Vector2.zero;
            currentPi.Set2Data(towerDatas[i], innerRadius, outerRadius, this);
            piList.Add(currentPi);
        }
        openedMenu = false;
    }

    private void Update()
    {
        overMenu = false;
        if (openedMenu)
        {
            switch (openTransition)
            {
                case TransitionType.Scale:
                    Scale();
                    transform.position = menuPosition;
                    break;

            }
        }
        else if (!openedMenu)
        {
            interactable = false;
            switch (closeTransition)
            {
                case TransitionType.Scale:
                    Scale();
                    transform.position = menuPosition;
                    break;

            }
        }
        foreach (isLvl pi in piList)
        {
            if (pi.gameObject.activeInHierarchy)
            {
                pi.ManualUpdate();
            }
        }
    }
    private void Scale()
    {
        if (openedMenu)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, Vector2.one * scaleModifier, Time.deltaTime * transitionSpeed);
            if (Mathf.Abs((Vector2.one * scaleModifier).sqrMagnitude - transform.localScale.sqrMagnitude) < .05f)
            {
                interactable = true;
            }
        }
        else if (!openedMenu)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, Vector2.zero, Time.deltaTime * transitionSpeed);
            if (transform.localScale.x < .05f)
            {
                transform.localScale = Vector2.zero;
                foreach (isLvl pi in piList)
                {
                    pi.gameObject.SetActive(false);
                }
            }
        }
    }
    public void SyncColor()
    {
        foreach (towerData pi in towerDatas)
        {
            pi.nonHighlightedColor = syncNormal;
            pi.highlightedColor = syncSelected;
            pi.disabledColor = syncDisabled;
        }
        foreach (isLvl pi in piList)
        {
            pi.Set2Data(towerDatas[piList.IndexOf(pi)], innerRadius, outerRadius, this);
        }
    }
}
[System.Serializable]
public class towerData
{
    public string sliceLabel;
    public Sprite icon;
    public bool lvl = false;
    public Vector3 Martposition;
    public GameObject Object;
    public float angle; 
    public int iconSize; 
    public bool isInteractable = true;
    public UnityEvent onHoverEnter;
    public UnityEvent onHoverExit;
    public int order; 
    public bool hoverFunctions;
    public Color nonHighlightedColor;
    public Color highlightedColor;
    public Color disabledColor;
    public UnityEvent onSlicePressed;
    public void SetValues(towerData newData)
    {

    }
    public void OnInspectorGUI(UnityEditor.SerializedProperty sprop, isLevelTower menu, System.Action AddSlice, System.Action<towerData> RemoveSlice, System.Action<int> angleUpdate)
    {
        GUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.BeginHorizontal();
        float angleStart = angle;
        order = Mathf.Clamp(order, 0, menu.towerDatas.Length);
        GUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("+", GUILayout.Width(32)))
            AddSlice.Invoke();
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("-", GUILayout.Width(32)))
            RemoveSlice.Invoke(this);
        GUILayout.EndHorizontal();

        EditorGUI.indentLevel++;
        hoverFunctions = EditorGUILayout.Foldout(hoverFunctions, "Hover Events", true);
        EditorGUI.indentLevel--;
        if (hoverFunctions)
        {
            EditorGUILayout.PropertyField(sprop.FindPropertyRelative("onHoverEnter"));
            EditorGUILayout.PropertyField(sprop.FindPropertyRelative("onHoverExit"));
        }
        EditorGUILayout.PropertyField(sprop.FindPropertyRelative("onSlicePressed"));
        GUILayout.Label(order.ToString(), GUILayout.Width(10));
        GUILayout.EndVertical();
        if (!menu.equalSlices && angle != angleStart)
        {
            angleUpdate.Invoke(order);
        }
    }
}