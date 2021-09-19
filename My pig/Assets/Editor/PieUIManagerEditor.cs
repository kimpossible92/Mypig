using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(PiUIMan))]
public class PiUIManagerEditor : Editor
{

    PiUIMan myTarget;

    private void OnEnable()
    {
        myTarget = (PiUIMan)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector( );
        PiUI[] piMenus = myTarget.GetComponentsInChildren<PiUI>( );
        string[] piMenuNames = new string[myTarget.nameMenu.Length];

        if (piMenus.Length != myTarget.nameMenu.Length)
        {
            for (int i = 0; i < myTarget.nameMenu.Length; i++)
            {
                piMenuNames[i] = myTarget.nameMenu[i].name;
            }
            myTarget.nameMenu = new PiUIMan.NameMenuPair[piMenus.Length];
            for (int j = 0; j < piMenus.Length; j++)
            {
                myTarget.nameMenu[j] = new PiUIMan.NameMenuPair( );
                myTarget.nameMenu[j].name = piMenus[j].name;
                myTarget.nameMenu[j].menu = piMenus[j];
            }
        }
    }
}