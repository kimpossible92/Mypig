using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Assets.Scripts
{
    [System.Serializable]
    public class Predakon
    {
        public List<string> crds = new List<string>();
        public List<string> crds2 = new List<string>();
        public List<int> numbercrds = new List<int>();
        public List<PlayingCard> GetCards = new List<PlayingCard>();
        protected enum deistvie { allin, x2stavka, x1stavka, check, pass }
        protected deistvie strategy = deistvie.pass;
        public int op1;
        public int cardsumm = 0;
        public int setstrategy()
        {
            op1 = Random.Range(0, (int)deistvie.pass + 1);
            return op1;
        }
        public int summallcards()
        {
            cardsumm = 0;
            foreach (var sm in GetCards)
            {
                if(sm.r1!="K"&& sm.r1 != "J"&&sm.r1 != "K"&&sm.r1 != "Q"&& sm.r1 != "A")
                {
                    cardsumm += int.Parse(sm.r1);
                }
                else
                {
                    if (sm.r1 != "K") { cardsumm += 14; }
                    if (sm.r1 != "J") { cardsumm += 12; }
                    if (sm.r1 != "Q") { cardsumm += 13; }
                    if (sm.r1 != "A") { cardsumm += 15; }
                }
            }
            return cardsumm;
        }
        public bool fullhouse1()
        {
            int streetcounter = 0;
            foreach (var cd in GetCards)
            {
                for (int c = 0; c < GetCards.Count; c++)
                {
                    if (cd.r2 != GetCards[c].r2 &&
                        cd.r1 == GetCards[c].r1)
                    {
                        streetcounter++;
                        if (streetcounter == 3)
                        {
                            return true;
                        }
                        //return true;
                    }
                }
            }
            return false;
        }
        public int findNum()
        {
            int min = numbercrds.Min();
            int lscrd = 0;
            for (int ncrd = 0; ncrd < 5; ncrd++)
            {
                if (numbercrds[ncrd] != min && numbercrds.Contains(min + ncrd))
                {
                    lscrd++;
                }
            }
            return lscrd;
        }
        public bool fullhouse2()
        {
            int streetcounter = 0;
            foreach (var cd in GetCards)
            {
                for (int c = 0; c < GetCards.Count; c++)
                {
                    if (cd.r2 != GetCards[c].r2 &&
                        cd.r1 == GetCards[c].r1)
                    {
                        streetcounter++;
                        if (streetcounter == 2)
                        {
                            return true;
                        }
                        //return true;
                    }
                }
            }
            return false;
        }
        public int contains1()
        {
            int streetcounter = 0;
            foreach (var cd in crds)
            {
                if (crds.Contains(cd))
                {
                    streetcounter++;
                }
            }
            return streetcounter;
        }
        public bool streetflash()
        {
            int streetcounter = 0;
            foreach (var cd in GetCards)
            {
                for (int c = 0; c < GetCards.Count; c++)
                {
                    if (cd.r2 != GetCards[c].r2 && cd.cardparse() == GetCards[c].cardparse() + 1)
                    {
                        streetcounter++;
                        if (streetcounter == 5)
                        {

                        }
                    }
                }
            }
            return false;
        }
        public int cardparse(string r1)
        {
            if (r1 != "A" && r1 != "K" && r1 != "Q" && r1 != "J")
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
        public bool kare()
        {
            int streetcounter = 0;
            foreach (var cd in GetCards)
            {
                for (int c = 0; c < GetCards.Count; c++)
                {
                    if (cd.r2 != GetCards[c].r2 &&
                        cd.r1 == GetCards[c].r1)
                    {
                        streetcounter++;
                        if (streetcounter == 4)
                        {
                            return true;
                        }
                        //return true;
                    }
                }
            }
            return false;
        }

        public bool flashroyal()
        {
            if (GetCards.Contains(GetCard("Hearts", "A")) &&
                        GetCards.Contains(GetCard("Hearts", "K")) &&
                        GetCards.Contains(GetCard("Hearts", "Q")) &&
                        GetCards.Contains(GetCard("Hearts", "J")) &&
                        GetCards.Contains(GetCard("Hearts", "10"))
                        )
            {
                return true;
            }
            if (GetCards.Contains(GetCard("Spades", "A")) &&
                        GetCards.Contains(GetCard("Spades", "K")) &&
                        GetCards.Contains(GetCard("Spades", "Q")) &&
                        GetCards.Contains(GetCard("Spades", "J")) &&
                        GetCards.Contains(GetCard("Spades", "10"))
                        )
            {
                return true;
            }
            if (GetCards.Contains(GetCard("Clubs", "A")) &&
                        GetCards.Contains(GetCard("Clubs", "K")) &&
                        GetCards.Contains(GetCard("Clubs", "Q")) &&
                        GetCards.Contains(GetCard("Clubs", "J")) &&
                        GetCards.Contains(GetCard("Clubs", "10"))
                        )
            {
                return true;
            }
            if (GetCards.Contains(GetCard("Diamonds", "A")) &&
                        GetCards.Contains(GetCard("Diamonds", "K")) &&
                        GetCards.Contains(GetCard("Diamonds", "Q")) &&
                        GetCards.Contains(GetCard("Diamonds", "J")) &&
                        GetCards.Contains(GetCard("Diamonds", "10"))
                        )
            {
                return true;
            }
            return false;
        }
        PlayingCard GetCard(string er1, string er2)
        {
            PlayingCard playingCard = new PlayingCard();
            playingCard.r1 = er1;
            playingCard.r2 = er2;
            return playingCard;
        }
    }
    public class colodCrds : MonoBehaviour
    {

        protected Predakon[] Enemies = new Predakon[3];
        [SerializeField] Button CButton;
        [SerializeField] Button checkbtn;
        [SerializeField] Canvas GetCanvas;
        [SerializeField] List<string> crds = new List<string>();
        [SerializeField]
        List<string> crds2 = new List<string>();
        [SerializeField] List<int> numbercrds = new List<int>();
        [SerializeField] private List<PlayingCard> GetCards = new List<PlayingCard>();
        [SerializeField] string[] vsuit = { "Spades", "Hearts", "Clubs", "Diamonds" };
        [SerializeField] int[] xpoints = { -330, -70, 230,-70 };
        [SerializeField] int[] ypoints = { 0, -112, 0, 110 };
        protected int[] strat = new int[3];
        [SerializeField] Text[] statusPlayers = new Text[3];
        public struct cardpiece
        {
            string cardName;
            int cardtype;
            int cardNumber;
            Sprite sprite;
        };
        public bool isContains(PlayingCard card1)
        {
            if (GetCards.Contains(card1))
            {
                return true;
            }
            return false;
        }
        public void set3Text()
        {
            List<int> vsc = new List<int>();
            for (int j = 0; j < Enemies.Length; j++)
            {
                vsc.Add(Enemies[j].summallcards());
                if (strat[j] == 0) { statusPlayers[j].text = "allin"; }
                if (strat[j] == 1) { statusPlayers[j].text = "x2stavka"; }
                if (strat[j] == 2) { statusPlayers[j].text = "x1stavka"; }
                if (strat[j] == 3) {
                    strat[j] = Random.Range(0, 3);
                    if (strat[j] == 0) { statusPlayers[j].text = "allin"; }
                    if (strat[j] == 1) { statusPlayers[j].text = "x2stavka"; }
                    if (strat[j] == 2) { statusPlayers[j].text = "x1stavka"; }
                }
                if (strat[j] == 4) { statusPlayers[j].text = "pass"; }
            }
            for (int j = 0; j < Enemies.Length; j++)
            {
                if (vsc.Max() == Enemies[j].summallcards()&&Enemies[j].setstrategy() != 4)
                {
                    if (Enemies[j].summallcards() < summallcards()) { FindObjectOfType<StatusText>().setStatus(" Кыайыы!!!"); }
                    else { statusPlayers[j].text += "Winn!!!"; }
                }
            }
        }
        public void openPlayersCards(int set)
        {
            if (set == 0) { }
            if (set == 1) { }
            if (set == 2) { }
            if (set == 3) { }
            if (set == 4) { }
            for(int k = 0;k < strat.Length;k++)
            {
                foreach(var f in Enemies[k].GetCards)
                {
                    FindObjectOfType<DeckOfPlayingCards>().SetCard2(f.gameObject);
                }
                if (strat[k] == 0) { }
                if (strat[k] == 1) { }
                if (strat[k] == 2) { }
                if (strat[k] == 3) { }
                if (strat[k] == 4) { }
            }
        }
        public int summallcards()
        {
            int cardsumm = 0;
            foreach (var sm in GetCards)
            {
                if (sm.r1 != "K" && sm.r1 != "J" && sm.r1 != "K" && sm.r1 != "Q" && sm.r1 != "A")
                {
                    cardsumm += int.Parse(sm.r1);
                }
                else
                {
                    if (sm.r1 != "K") { cardsumm += 14; }
                    if (sm.r1 != "J") { cardsumm += 12; }
                    if (sm.r1 != "Q") { cardsumm += 13; }
                    if (sm.r1 != "A") { cardsumm += 15; }
                }
            }
            return cardsumm;
        }

        public void openCards()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < Enemies.Length; j++)
                {
                    GameObject go1 = Instantiate(CButton.gameObject, GetCanvas.transform);
                    FindObjectOfType<DeckOfPlayingCards>().SetCard1(go1.gameObject, i);
                    if (j == 0) { setpoint(go1, 0, 0, i); }
                    else { setpoint(go1, j+1, j+1, i); }
                    if (i == 0)
                    {
                        Enemies[j] = new Predakon();
                    }
                    Enemies[j].GetCards.Add(go1.GetComponent<PlayingCard>());
                    if (Enemies[j].crds2.Contains(go1.GetComponent<PlayingCard>().r1)) { Enemies[j].crds.Add(go1.GetComponent<PlayingCard>().r1); }
                    Enemies[j].crds2.Add(go1.GetComponent<PlayingCard>().r1);
                    Enemies[j].numbercrds.Add(cardparse(go1.GetComponent<PlayingCard>().r1));
                    if (i == 4)
                    {
                        int strateg = Enemies[0].setstrategy();
                        strat[j] = strateg;
                    }
                }
            }
        }
        public void setpoint(GameObject @object,int x,int y,int c1)
        {
            @object.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(xpoints[x]+(c1*30), ypoints[y], 0);
        }
        public void reconfigure()
        {
            FindObjectOfType<StatusText>().ClearStatus();
            foreach (var crd in GetCards)
            {
                Destroy(crd.gameObject);
            }
            GetCards.Clear(); 
            crds.Clear();
            crds2.Clear(); 
            numbercrds.Clear();
            foreach(var enem in Enemies)
            {
                foreach (var crd in enem.GetCards)
                {
                    Destroy(crd.gameObject);
                }
                enem.GetCards.Clear();
                enem.crds.Clear();
                enem.crds2.Clear();
                enem.numbercrds.Clear();
            }
            SetList();openCards();
        }
        [SerializeField] private List<cardpiece> Cardpieces;
        private void SetList()
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject go1 = Instantiate(CButton.gameObject, GetCanvas.transform);
                FindObjectOfType<DeckOfPlayingCards>().SetCard(go1.gameObject, i);
                //if (isContains(go1.GetComponent<PlayingCard>())) { FindObjectOfType<DeckOfPlayingCards>().SetCard(go1.gameObject, i); }
                go1.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-70 + (i * 30), -112, 0);
                GetCards.Add(go1.GetComponent<PlayingCard>());
                if (crds2.Contains(go1.GetComponent<PlayingCard>().r1)) { crds.Add(go1.GetComponent<PlayingCard>().r1); }
                crds2.Add(go1.GetComponent<PlayingCard>().r1);
                numbercrds.Add(cardparse(go1.GetComponent<PlayingCard>().r1));
            }
            if (flashroyal())
            {
                FindObjectOfType<StatusText>().setStatus(" флэш рояль");
                print("флэш рояль");
            }
            if (kare() && crds[0] == crds[1])
            {
                FindObjectOfType<StatusText>().setStatus(" каре");
                print("каре");
            }
            if (streetflash())
            {
                FindObjectOfType<StatusText>().setStatus(" стрит флэш");
                print("стрит флэш");
            }
            if (fullhouse1() && fullhouse2()&& contains1() == 3)
            {
                FindObjectOfType<StatusText>().setStatus(" фул хаус");
                print("фул хаус");
            }
            if (fullhouse1()&&crds[0]==crds[1])
            {
                FindObjectOfType<StatusText>().setStatus(" 3 карты");
                print("3 карты");
            }
            if (fullhouse2()&&contains1() == 2 && crds[0] != crds[1])
            {
                FindObjectOfType<StatusText>().setStatus(" две пары"); print("две пары");
            }
            if (fullhouse2() && contains1() == 1) { FindObjectOfType<StatusText>().setStatus(" пара"); print("пара"); }
            if (findNum() == 5)
            {
                foreach (var crd1 in GetCards)
                {
                    FindObjectOfType<StatusText>().setStatus(crd1.r1);
                }
            }
            print(contains1());
        }
        bool fullhouse1()
        {
            int streetcounter = 0;
            foreach (var cd in GetCards)
            {
                for (int c = 0; c < GetCards.Count; c++)
                {
                    if (cd.r2 != GetCards[c].r2 &&
                        cd.r1 == GetCards[c].r1)
                    {
                        streetcounter++;
                        if (streetcounter == 3)
                        {
                            return true;
                        }
                        //return true;
                    }
                }
            }
            return false;
        }
        int findNum()
        {
            int min = numbercrds.Min();
            int lscrd = 0;
            for(int ncrd=0;ncrd<5;ncrd++)
            {
                if(numbercrds[ncrd] != min && numbercrds.Contains(min + ncrd))
                {
                    lscrd++;
                }
            }
            return lscrd;
        }
        bool fullhouse2()
        {
            int streetcounter = 0;
            foreach (var cd in GetCards)
            {
                for (int c = 0; c < GetCards.Count; c++)
                {
                    if (cd.r2 != GetCards[c].r2 &&
                        cd.r1 == GetCards[c].r1)
                    {
                        streetcounter++;
                        if (streetcounter == 2)
                        {
                            return true;
                        }
                        //return true;
                    }
                }
            }
            return false;
        }
        int contains1()
        {
            int streetcounter = 0;
            foreach (var cd in crds)
            {
                if (crds.Contains(cd))
                {
                    streetcounter++;
                }
            }
            return streetcounter;
        }
        bool streetflash()
        {
            int streetcounter = 0;
            foreach (var cd in GetCards)
            {
                for (int c = 0; c < GetCards.Count; c++)
                {
                    if(cd.r2 != GetCards[c].r2&&cd.cardparse()==GetCards[c].cardparse()+1)
                    {
                        streetcounter++;
                        if (streetcounter == 5)
                        {

                        }
                    }
                } 
            }
            return false;
        }
        public int cardparse(string r1)
        {
            if (r1 != "A" && r1 != "K" && r1 != "Q" && r1 != "J")
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
        bool kare()
        {
            int streetcounter = 0;
            foreach (var cd in GetCards)
            {
                for (int c = 0; c < GetCards.Count; c++)
                {
                    if (cd.r2 != GetCards[c].r2 &&
                        cd.r1 == GetCards[c].r1)
                    {
                        streetcounter++;
                        if (streetcounter == 4)
                        {
                            return true;
                        }
                        //return true;
                    }
                }
            }
            return false;
        }

        bool flashroyal()
        {
            if (GetCards.Contains(GetCard("Hearts", "A")) &&
                        GetCards.Contains(GetCard("Hearts", "K")) &&
                        GetCards.Contains(GetCard("Hearts", "Q")) &&
                        GetCards.Contains(GetCard("Hearts", "J")) &&
                        GetCards.Contains(GetCard("Hearts", "10"))
                        )
            {
                return true;
            }
            if (GetCards.Contains(GetCard("Spades", "A")) &&
                        GetCards.Contains(GetCard("Spades", "K")) &&
                        GetCards.Contains(GetCard("Spades", "Q")) &&
                        GetCards.Contains(GetCard("Spades", "J")) &&
                        GetCards.Contains(GetCard("Spades", "10"))
                        )
            {
                return true;
            }
            if (GetCards.Contains(GetCard("Clubs", "A")) &&
                        GetCards.Contains(GetCard("Clubs", "K")) &&
                        GetCards.Contains(GetCard("Clubs", "Q")) &&
                        GetCards.Contains(GetCard("Clubs", "J")) &&
                        GetCards.Contains(GetCard("Clubs", "10"))
                        )
            {
                return true;
            }
            if (GetCards.Contains(GetCard("Diamonds", "A")) &&
                        GetCards.Contains(GetCard("Diamonds", "K")) &&
                        GetCards.Contains(GetCard("Diamonds", "Q")) &&
                        GetCards.Contains(GetCard("Diamonds", "J")) &&
                        GetCards.Contains(GetCard("Diamonds", "10"))
                        )
            {
                return true;
            }
            return false;
        }
        PlayingCard GetCard(string er1,string er2)
        {
            PlayingCard playingCard = new PlayingCard();
            playingCard.r1 = er1;
            playingCard.r2 = er2;
            return playingCard;
        }
        private void ShowPlayer()
        {
            foreach (cardpiece card in Cardpieces)
            {
                GameObject goButton = Instantiate(CButton.gameObject);
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            SetList();openCards();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}