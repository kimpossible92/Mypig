using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class previewObject : MonoBehaviour
{

    private List<GameObject> obj = new List<GameObject>();

    public Material goodMat;
    public Material badMat;

    public GameObject prefab;

    public MeshRenderer myRend;
    private bool canBuild = false;
    public bool snapToGrid;
    public float cost;
    private Stats stats;
    public Text control;

    private void OnEnable()
    {
        control = GameObject.Find("Controls").GetComponent<Text>();
        control.text = "LMB to place object, RMB to cancel, R to rotate.";
    }
    private void OnDisable()
    {
        control.text = "";
    }

    private void Start()
    {
        if (myRend == null)
        {
            myRend = GetComponent<MeshRenderer>();
        }
        stats = FindObjectOfType<Stats>();
        Observable.EveryUpdate().Select(_ => obj.Count == 0).Subscribe(x => { SetColor(x); });
    }
    void removeObj(GameObject GO)
    {
        obj.Remove(GO);
    }
    void addobj(GameObject GO)
    {
        obj.Add(GO);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ground"))
        {
            //obj.Add(other.gameObject);
            addobj(other.gameObject);
        }
        Observable.EveryUpdate().Select(_ => obj.Count == 0).Subscribe(x => { SetColor(x); });
    }


    private void OnTriggerExit(Collider other)
    {

        if (!other.CompareTag("Ground"))
        {
            //obj.Remove(other.gameObject);
            removeObj(other.gameObject);
        }
        Observable.EveryUpdate().Select(_ => obj.Count == 0).Subscribe(x => { SetColor(x); });
    }

    void SetColor(bool l1)
    {
        if (l1) {
            if (myRend != null)
            {
                myRend.material = goodMat;
            }
            canBuild = true;
        }
        else {
            if (myRend != null)
            {
                myRend.material = badMat;
            }
            canBuild = false;
        }
    }
    public void Build()
    {

        if (stats.Money >= cost)
        {
            stats.Money -= cost;
            UIMan.instance.UpdateValues();
            var pref = Instantiate(prefab, transform.position, transform.rotation);
            print(pref.transform.position);
            Destroy(gameObject);
        }
        else { Debug.Log("no money left"); }

    }

    public bool CanBuild()
    {
        return canBuild;
    }


}
