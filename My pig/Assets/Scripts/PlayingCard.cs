using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayingCard : MonoBehaviour
    {
        private string suit;
        private string rank;
        public string r1, r2;
        [HideInInspector] public int r3=0;
        [SerializeField] UnityEngine.UI.Text text1;
        [SerializeField] UnityEngine.UI.Text text2;
        [SerializeField] public UnityEngine.UI.Image image;
        private Sprite sprite;
        [SerializeField] bool isScene;
        [SerializeField] string[] vsuit = { "Spades", "Hearts", "Clubs", "Diamonds" };
        private void Start()
        {
            if (isScene)
            {
                FindObjectOfType<DeckOfPlayingCards>().SetCard(gameObject, 0);
            }
        }
        private void Update()
        {
            
        }

        public string getSuit(string suitCounter)
        {
            string newSuit = null;
            switch (suitCounter)
            {
                case ("S"):
                    r3 = 0;
                    newSuit = "Spades";//лопаты
                    break;
                case ("H"):
                    r3 = 1;
                    newSuit = "Hearts";//сердце
                    break;
                case ("C"):
                    r3 = 2;
                    newSuit = "Clubs";//трефы
                    break;
                case ("D"):
                    r3 = 3;
                    newSuit = "Diamonds";//рубины
                    break;
            }
            suit = newSuit;
            return suit;
        }
        public int cardparse()
        {
            if(r1!="A"&& r1 != "K"&& r1 != "Q"&& r1 != "J")
            {
                return int.Parse(r1);
            }
            else
            {
                if (r1 != "A")
                {
                    return 1;
                }
                if (r1 != "J")
                {
                    return 11;
                }
                if (r1 != "Q")
                {
                    return 12;
                }
                if (r1 != "K")
                {
                    return 13;
                }
                return 1;
            }
        }
        public string getValue(int valueCounter)
        {
            string newValue = null;
            switch (valueCounter)
            {
                case (1):
                    newValue = "A";
                    break;
                case (2):
                    newValue = "2";
                    break;
                case (3):
                    newValue = "3";
                    break;
                case (4):
                    newValue = "4";
                    break;
                case (5):
                    newValue = "5";
                    break;
                case (6):
                    newValue = "6";
                    break;
                case (7):
                    newValue = "7";
                    break;
                case (8):
                    newValue = "8";
                    break;
                case (9):
                    newValue = "9";
                    break;
                case (10):
                    newValue = "10";
                    break;
                case (11):
                    newValue = "J";
                    break;
                case (12):
                    newValue = "Q";
                    break;
                case (13):
                    newValue = "K";
                    break;
            }
            rank = newValue;
            return rank;
        }
        public string finalValue(string passedSuit, int passedValue)
        {
            string newSuit = getSuit(passedSuit);
            string newValue = getValue(passedValue);
            r1 = newValue;
            r2 = newSuit;
            return newValue + " of " + newSuit;
        }
    }
}