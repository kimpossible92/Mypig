using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IEngine
{
    string MaxSpeed
    {
        get;
    }
}
internal class FastEngine : IEngine
{
    public string MaxSpeed
    {
        get
        {
            return "Object";
        }
    }
}

internal class SlowEngine : IEngine
{
    public string MaxSpeed
    {
        get
        {
            return "Wall";
        }
    }
}
public class BuildModeSelector : MonoBehaviour
{
    public ShowBuildPosition _showBuildPosition;
    public CreateWall _createWall;
    public GameObject buildpositionIndicator;
    BuildSelector _buildSelector;
    private IEngine engine;
    SlowEngine slow; FastEngine fast;
    public string MaximumSpeed
    {
        get
        {
            return this.engine.MaxSpeed;
        }
    }
    public void setEngine(IEngine engine)
    {
        this.engine = engine;
    }
    void Start()
    {
        _buildSelector = FindObjectOfType<BuildSelector>();
    }

    // Update is called once per frame
    public void setBuildmode(string buildmode)
    {

        if (buildmode == "Wall")
        {
            buildpositionIndicator.SetActive(true);

            _showBuildPosition.enabled = true;
            _createWall.enabled = true;
            _buildSelector.HidePanel();
        }
        else
        {
            buildpositionIndicator.SetActive(false);
            _showBuildPosition.enabled = false;
            _createWall.enabled = false;
        }
    }
}
