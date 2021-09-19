using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(isLevelTower))]
public class pokemartEditor : Editor
{
    isLevelTower islevel;
    int slicesToAdd, slices2Add;
    System.Action addSlice2;
    System.Action<towerData> removeSlice2;
    float[] lastAngles;
    List<string> itemsNotToDraw = new List<string>();
    List<towerData> isLevelInstances = new List<towerData>();
    private void OnEnable()
    {
        islevel = (isLevelTower)target;
        addSlice2 = AddedSlice2;
        removeSlice2 = removedSlice2;
    }
    public void removedSlice2(towerData removeTower)
    {

    }
    public void AddedSlice2()
    {
        slices2Add++;
    }
    public void towerUpdate(int order)
    {
        float sumBefore = 0;
        for (int i = 0; i <= order; i++)
        {
            sumBefore += islevel.towerDatas[i].angle;
        }
        float remainder = (360 - sumBefore) / (islevel.towerDatas.Length - order - 1);
        for (int i = order + 1; i < islevel.towerDatas.Length; i++)
        {
            islevel.towerDatas[i].angle = remainder;
        }
    }

    public void TowerGui()
    {
        if (lastAngles == null)
        {
            lastAngles = new float[islevel.towerDatas.Length];
        }
        if(GUILayout.Button("Change PiCut State"))
        {
            islevel.piCut.gameObject.SetActive(!islevel.piCut.gameObject.activeInHierarchy);
        }
        if (GUILayout.Button("Sync Colors"))
        {
            islevel.SyncColor();
        }
        if (!islevel.dynamicallyScaleToResolution)
        {
            if (!itemsNotToDraw.Contains("defaultResolution"))
                itemsNotToDraw.Add("defaultResolution");
        }
        else
        {
            itemsNotToDraw.Remove("defaultResolution");
        }
        if (!islevel.outline)
        {
            if (!itemsNotToDraw.Contains("outlineColor"))
                itemsNotToDraw.Add("outlineColor");
        }
        else
        {
            itemsNotToDraw.Remove("outlineColor");
        }
        if (!islevel.syncColors)
        {
            if (!itemsNotToDraw.Contains("syncNormal"))
            {
                itemsNotToDraw.Add("syncNormal");
            }
            if (!itemsNotToDraw.Contains("syncSelected"))
            {
                itemsNotToDraw.Add("syncSelected");
            }
            if (!itemsNotToDraw.Contains("syncDisabled"))
            {
                itemsNotToDraw.Add("syncDisabled");
            }
        }
        else
        {
            itemsNotToDraw.Remove("syncNormal");
            itemsNotToDraw.Remove("syncSelected");
            itemsNotToDraw.Remove("syncDisabled");
        }
        if (itemsNotToDraw.Count == 0)
        {
            EditorUtility.SetDirty(islevel);
        }
        else
        {
            DrawPropertiesExcluding(serializedObject, itemsNotToDraw.ToArray());
            EditorUtility.SetDirty(islevel);
        }
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
        islevel.towerDatas = islevel.towerDatas.ToList().OrderBy(x => x.order).ToArray();
        var sprop = serializedObject.FindProperty("towerDatas");

        for (var i = 0; i < islevel.towerDatas.Length; i++)
        {
            if (!islevel.towerDatas.Contains(islevel.towerDatas[i]))
            {
                islevel.towerDatas[i].OnInspectorGUI(
                    sprop.GetArrayElementAtIndex(i),
                    islevel, addSlice2, removeSlice2,
                    towerUpdate);
            }
            if (i < islevel.towerDatas.Length - 1)
            {
                GUILayout.Space(10);
            }
        }
        if (islevel.towerDatas.Length < 1)
        {
            islevel.towerDatas = new towerData[1];
        }
        if (slices2Add > 0)
        {
            Undo.RecordObject(islevel, "Add Slice2");
            towerData[] towers = new towerData[islevel.towerDatas.Length + slices2Add];
            for (int i = islevel.towerDatas.Length; i < towers.Length; i++)
            {
                towers[i] = new towerData();
                towers[i].SetValues(islevel.towerDatas[islevel.towerDatas.Length - 1]);
                towers[i].order = i;
            }
            for (int j = 0; j < islevel.towerDatas.Length; j++)
            {
                towers[j] = islevel.towerDatas[j];
            }
        }
        else if (isLevelInstances.Count > 0 && islevel.towerDatas.Length > 1)
        {
            Undo.RecordObject(islevel, "Removed Slice2");
            towerData[] tempArray = new towerData[islevel.towerDatas.Length - isLevelInstances.Count];
            int addedSlices = 0;
            for (int i = 0; i < islevel.towerDatas.Length; i++)
            {
                if (!isLevelInstances.Contains(islevel.towerDatas[i]))
                {
                    tempArray[addedSlices] = islevel.towerDatas[i];
                    tempArray[addedSlices].order = addedSlices;
                    addedSlices++;
                }
            }
            for (int j = 0; j < islevel.towerDatas.Length; j++)
            {
                tempArray[j] = islevel.towerDatas[j];
            }
            slices2Add = 0;
            islevel.towerDatas = tempArray;
        }
    }

    public override void OnInspectorGUI()
    {
        TowerGui();
    }
}