using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class DeckOfPlayingCards : MonoBehaviour
    {
        PlayingCard[] card = new PlayingCard[52];
        PlayingCard cardTop = new PlayingCard();
        [SerializeField] Sprite[] musts = new Sprite[4];
        [SerializeField] UnityEngine.UI.Button CButton;
        [SerializeField] Canvas GetCanvas;
        [SerializeField] int val1;string val2;
        public void CreateCard(int number)
        {
            cardTop = card[0];
            string randomNumberResult = null;
            string randomNum1 = null;
            string newval = null;
            int randomNumGen1 = (int)(Random.Range(1, 4)) + 1;
            if (randomNumGen1 == 1)
            {
                newval = "H";
            }
            else if (randomNumGen1 == 2)
            {
                newval = "S";
            }
            else if (randomNumGen1 == 3)
            {
                newval = "C";
            }
            else if (randomNumGen1 == 4)
            {
                newval = "D";
            }
            val2 = newval;

            randomNum1 = newval;
            int randomNumGen2 = (int)(Random.Range(1, 13)) + 1;
            int randomNum2 = randomNumGen2;
            PlayingCard finalVal = new PlayingCard();

            //for (int i = 0; i < card.Length; i++)
            //{
            //    card[i] = finalVal;
            //    card[i].finalValue(randomNum1, randomNum2);
            //}
            card[number] = finalVal;
            card[number].finalValue(randomNum1, randomNum2);
            GameObject goButton = Instantiate(CButton.gameObject);
            transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = card[number].r1;
            transform.Find("Text (1)").GetComponent<UnityEngine.UI.Text>().text = card[number].r1;
            transform.Find("Image").GetComponent<UnityEngine.UI.Image>().sprite = musts[randomNumGen1 - 1];
            CardToPosition(goButton, number);
        }
        public void SetCard2(GameObject thecard)
        {
            thecard.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = thecard.GetComponent<PlayingCard>().r1;
            thecard.transform.Find("Text (1)").GetComponent<UnityEngine.UI.Text>().text = thecard.GetComponent<PlayingCard>().r1;
            thecard.GetComponent<PlayingCard>().image.sprite = musts[thecard.GetComponent<PlayingCard>().r3];
        }
        public void SetCard1(GameObject thecard, int number)
        {
            string randomNumberResult = null;
            string randomNum1 = null;
            string newval = null;
            int randomNumGen1 = (int)(Random.Range(1, 4)) + 1;
            if (randomNumGen1 == 1)
            {
                newval = "H";
            }
            else if (randomNumGen1 == 2)
            {
                newval = "S";
            }
            else if (randomNumGen1 == 3)
            {
                newval = "C";
            }
            else if (randomNumGen1 == 4)
            {
                newval = "D";
            }
            randomNum1 = newval;
            int randomNumGen2 = (int)(Random.Range(1, 13)) + 1;
            int randomNum2 = randomNumGen2;
            PlayingCard finalVal = new PlayingCard();
            thecard.GetComponent<PlayingCard>().finalValue(randomNum1, randomNum2);
            //for (int i = 0; i < card.Length; i++)
            //{
            //    card[i] = finalVal;
            //    card[i].finalValue(randomNum1, randomNum2);
            //}

            //card[number] = finalVal;
            //card[number].finalValue(randomNum1, randomNum2);
            //thecard.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = card[number].r1;
            //thecard.transform.Find("Text (1)").GetComponent<UnityEngine.UI.Text>().text = card[number].r1;
            //thecard.GetComponent<PlayingCard>().image.sprite = musts[randomNumGen1 - 1];
        }
        public void SetCard(GameObject thecard, int number)
        {
            string randomNumberResult = null;
            string randomNum1 = null;
            string newval = null;
            int randomNumGen1 = (int)(Random.Range(1, 4)) + 1;
            if (randomNumGen1 == 1)
            {
                newval = "H";
            }
            else if (randomNumGen1 == 2)
            {
                newval = "S";
            }
            else if (randomNumGen1 == 3)
            {
                newval = "C";
            }
            else if (randomNumGen1 == 4)
            {
                newval = "D";
            }
            randomNum1 = newval;
            int randomNumGen2 = (int)(Random.Range(1, 13)) + 1;
            int randomNum2 = randomNumGen2;
            PlayingCard finalVal = new PlayingCard();

            thecard.GetComponent<PlayingCard>().finalValue(randomNum1, randomNum2);
            //for (int i = 0; i < card.Length; i++)
            //{
            //    card[i] = finalVal;
            //    card[i].finalValue(randomNum1, randomNum2);
            //}

            card[number] = finalVal;
            card[number].finalValue(randomNum1, randomNum2);
            thecard.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = card[number].r1;
            thecard.transform.Find("Text (1)").GetComponent<UnityEngine.UI.Text>().text = card[number].r1;
            thecard.GetComponent<PlayingCard>().image.sprite = musts[randomNumGen1 - 1];
        }
        public void CardToPosition(GameObject @object, int num)
        {
            @object.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(num * 92, -55, 0);
        }
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}