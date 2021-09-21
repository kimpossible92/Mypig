using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSelector : MonoBehaviour
{

    public GameObject buildPanel;
    public BuildSystem buildSystem;
    public bool showPanel = true;
    PiUIMan piUi;
    private PiUI normalMenu;
    cameraController _cameraController;

    private void Start()
    {
        _cameraController = FindObjectOfType<cameraController>();

        piUi = FindObjectOfType<PiUIMan>();
        normalMenu = piUi.GetPiUIOf("Normal Menu");
        //piUi.ChangeMenuState("Normal Menu", new Vector2(Screen.width / 2f, Screen.height / 2f));
    }
    public void StartBuild(GameObject go)
    {
        buildSystem.NewBuild(go);
        showPanel = !showPanel;
        piUi.ChangeMenuState("Normal Menu", new Vector2(Screen.width / 2f, Screen.height / 2f));
        _cameraController.enabled = !_cameraController.enabled;
    }
    public void HidePanel()
    {
        piUi.ChangeMenuState("Normal Menu", new Vector2(Screen.width / 2f, Screen.height / 2f));
        _cameraController.enabled = !_cameraController.enabled;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && showPanel)
        {
            piUi.ChangeMenuState("Normal Menu", new Vector2(Screen.width / 2f, Screen.height / 2f));
            _cameraController.enabled = !_cameraController.enabled;
        }
    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(15, 45, 160, 50), "Tab"))
        {
            piUi.ChangeMenuState("Normal Menu", new Vector2(Screen.width / 2f, Screen.height / 2f));
            _cameraController.enabled = !_cameraController.enabled;
        }
    }
}
