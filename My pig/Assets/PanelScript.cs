using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelScript : MonoBehaviour
{
    protected AnimalController[] animalController;
    [SerializeField]
    private int positionsCount=1;
    [SerializeField]
    private UnityEngine.UI.Image icon0;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        animalController = FindObjectsOfType<AnimalController>();
        if (positionsCount != animalController.Length)
        {
            positionsCount = animalController.Length;
        }
        foreach(AnimalController animalIcon in animalController)
        {
            if (!animalIcon.isnullicon())
            {
                GameObject image1  = Instantiate(icon0.gameObject,this.transform);
                animalIcon.setiacon(image1.GetComponent<UnityEngine.UI.Image>());
                image1.GetComponent<UnityEngine.UI.Image>().transform.SetParent(this.transform);
            }
        }
    }
}
