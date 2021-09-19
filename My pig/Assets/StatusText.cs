using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusText : MonoBehaviour
{
    [SerializeField] Text statustxt;
    // Start is called before the first frame update
    void Start()
    {
        statustxt = GetComponent<Text>();
    }
    public void setStatus(string txt)
    {
        statustxt.text += txt;
    }
    public void ClearStatus()
    {
        statustxt.text = "";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
