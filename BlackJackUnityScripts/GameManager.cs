using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum HandType
{ 
    Player,
    Dealer,
    SplitOne,
    SplitTwo
}

public class GameManager : MonoBehaviour
{
    public DeckManager CardManager;
    public GameObject[] ChipButtons;
    public GameObject SplitDecisionPanel;
    public GameObject PlayerWinsPanel;
    public GameObject DealerWinsPanel;
    public GameObject HitOrStandDecisionPanel;
    public TextMeshProUGUI PlayerHandTotal;
    public TextMeshProUGUI DealerHandTotal;
    public TextMeshProUGUI SplitHandOneTotal;
    public TextMeshProUGUI SplitHandTwoTotal;
    public TextMeshProUGUI DealerWinsPanelText;
    public TextMeshProUGUI PlayerWinsPanelText;
    public TextMeshProUGUI MoneyText;
    public Button DoubleButton;
    public Button ExitButton;
    public Button ExitButtonS;
    public Button DealButton;
    public Button HitButtonS;
    public Button StandButtonS;
    public Button HitButton;
    public Button StandButton;
    public Button YesSplit;
    public Button NoSplit;
    public Button BetTen;
    public Button BetTwentyFive;
    public Button BetFifty;
    public Button BetOneHundred;
    public Button SelectedChipButton;
    public GameObject CardPrefab;
    public RectTransform PlayerHandTotalText;
    public RectTransform DealerHandTotalText;
    public RectTransform SHOTotalText;
    public RectTransform SHTTotalText;
    public Transform PlayerCardParent;
    public Transform DealerCardParent;
    public Transform SplitHandOneParent;
    public Transform SplitHandTwoParent;
    public Transform CardDeck;
    public Canvas MainCanvas;
    public CardSpriteManager SpriteManager;

    public int PlayerHand = 0;
    public int ButtonIndex;
    public int Card;
    public int CardsInHand = 0;
    public int DealerHand = 0;
    public int SplitHand = 0;
    public int SelectedChipValue;
    public int NumSplitHands;
    public int DealerCardsInHand;
    public Card DealerCardOne;
    public Card DealerCardTwo;
    public double BetAmount = 0;
    public double Money;
    public bool SplitBool;
    public bool DoubleBool;
    public bool IsAnimatingCard = false;
    public bool IsCardImageRunning = false;
    public bool DealerHandTextBox = false;
    public bool PlayerHandTextBox = false;
    private List<Card> SplitHandOne = new List<Card>();
    private List<Card> SplitHandTwo = new List<Card>();
    private List<Card> PlayerHandList = new List<Card>();
    private int ActiveSplitHandIndex = 1;
    public Card CardOne;
    public Card TempCard;
    public float VerticalOffset = 50f;
    public float BaseX = -326f;
    public float AdjustmentPerCard = 30f;
    public float YPos = -372f;
    void ResetGameState()
    {
        DealerHand = 0;
        PlayerHand = 0;
        CardsInHand = 0;

        UpdatePlayerTotal(PlayerHand);
        UpdateDealerTotal(DealerHand);

        BetAmount = 0;
        SelectedChipValue = 0;
        SelectedChipButton = null;
        ChipsOn();
    }

    void ExitLoop()
    {
        HitOrStandDecisionPanel.SetActive(true);
    }

    void Exit()
    {

        Application.Quit();

    }
    void Start()
    {
        Debug.Log("Starting Game!");

        PlayerHand = 0;
        DealerHand = 0;
        Money = 500.00;


        DealButton.onClick.AddListener(() => StartCoroutine(Deal()));
        HitButton.onClick.AddListener(Hit);
        StandButton.onClick.AddListener(Stand);
        HitButtonS.onClick.AddListener(Hit);
        StandButtonS.onClick.AddListener(Stand);
        ExitButtonS.onClick.AddListener(ExitLoop);
        ExitButton.onClick.AddListener(Exit);
        DoubleButton.onClick.AddListener(Double);
        YesSplit.onClick.AddListener(SplitYes);
        NoSplit.onClick.AddListener(SplitNo);
        BetTen.onClick.AddListener(() => CheckBet(10));
        BetTwentyFive.onClick.AddListener(() => CheckBet(25));
        BetFifty.onClick.AddListener(() => CheckBet(50));
        BetOneHundred.onClick.AddListener(() => CheckBet(100));
        PlayerWinsPanel.SetActive(false);
        DealerWinsPanel.SetActive(false);
        PlayerHandTotal.enabled = false;
        DealerHandTotal.enabled = false;


        UpdatePlayerTotal(PlayerHand);
        UpdateDealerTotal(DealerHand);
        UpdateMoneyTotal(Money);

        for (int i = 0; i < ChipButtons.Length; i++)
        {
            Transform highlight = ChipButtons[i].transform.Find("HighlightImage");
            highlight.gameObject.SetActive(false);
        }
    }


    private IEnumerator CardImage(Card CardValue, bool IsPlayer, Transform Parent)
    {


        GameObject Card = Instantiate(CardPrefab, CardDeck.position, Quaternion.identity, MainCanvas.transform);

        RectTransform rect = Card.GetComponent<RectTransform>();

        float WidthRatio = Screen.width / 1920f;
        float HeightRatio = Screen.height / 1080f;
        float ScaleFactor = Mathf.Min(WidthRatio, HeightRatio);

        rect.sizeDelta = new Vector2(106f, 160f) * ScaleFactor;

        Debug.Log($"Instantiated card {Card.name} parent: {Card.transform.parent.name} localPos: {Card.transform.localPosition}");


        Sprite FrontSprite = SpriteManager.GetSpriteValue(CardValue.Rank, CardValue.Suit);
        Sprite BackSprite = SpriteManager.GetBackSprite();

        CardAnimations display = Card.GetComponent<CardAnimations>();
        display.SetCardSprite(FrontSprite, BackSprite);

        yield return StartCoroutine(AnimateCard(Card, Parent));

        IsCardImageRunning = false;
       
    }

    private IEnumerator AnimateCard(GameObject Card, Transform TargetParent, float Duration = 0.5f)
    {

        Debug.Log("Final parent assignment to: " + TargetParent.name);



        Card.GetComponent<CardAnimations>().ShowBack();

        RectTransform CardRect = Card.GetComponent<RectTransform>();

        Vector3 Start = CardDeck.position;
        Vector3 End = TargetParent.position;

        Quaternion StartRotation = Quaternion.Euler(0f, 0f, 0f);
        Quaternion EndRotation = Quaternion.Euler(0f, 90f, 0f);

        float ElapsedTime = 0f;

        float FlipDuration = Duration / 2f;



        while (ElapsedTime < FlipDuration)
        {

            float T = ElapsedTime / FlipDuration;

            CardRect.position = Vector3.Lerp(Start, End, T / 2f);
            CardRect.localRotation = Quaternion.Slerp(StartRotation, EndRotation, T);

            ElapsedTime += Time.deltaTime;
            yield return null;
        }

        Card.GetComponent<CardAnimations>().RevealFront();

        ElapsedTime = 0f;

        while (ElapsedTime < FlipDuration)
        {

            float T = ElapsedTime / FlipDuration;

            CardRect.position = Vector3.Lerp(Start, End, 0.5f + T / 2f);
            CardRect.localRotation = Quaternion.Slerp(EndRotation, StartRotation, T);

            ElapsedTime += Time.deltaTime;
            yield return null;
        }


        CardRect.position = End;
        Card.transform.SetParent(TargetParent, true);

        



    }

    private IEnumerator DealCardsSequentially(List<Card> cards, Transform parent)
    {
        foreach (Card card in cards)
        {
            yield return CardImage(card, true, parent);
            yield return new WaitForSeconds(0.2f);

        }

    }

    public void ChipsOn()
    {
        BetTen.interactable = true;
        BetTwentyFive.interactable = true;
        BetFifty.interactable = true;
        BetOneHundred.interactable = true;
    }

    public void ChipsOff()
    {
        BetTen.interactable = false;
        BetTwentyFive.interactable = false;
        BetFifty.interactable = false;
        BetOneHundred.interactable = false;
    }

    public void CheckBet(int ChipValue)
    {
        if (SelectedChipButton != null)
        {
            SelectedChipButton.image.color = Color.white;
        }

        SelectedChipValue = ChipValue;
        BetAmount = ChipValue;

        if (ChipValue == 10)
        {
            SelectedChipButton = BetTen;
            ButtonIndex = 0;
        }
        else if (ChipValue == 25)
        {
            SelectedChipButton = BetTwentyFive;
            ButtonIndex = 1;
        }
        else if (ChipValue == 50)
        {
            SelectedChipButton = BetFifty;
            ButtonIndex = 2;    
        }
        else if (ChipValue == 100)
        { 
            SelectedChipButton = BetOneHundred;
            ButtonIndex = 3;
        }

        SelectChip(ButtonIndex);
    }

    public void SelectChip(int index)
    {

        for (int i = 0; i < ChipButtons.Length; i++)
        { 
            Transform highlight = ChipButtons[i].transform.Find("HighlightImage");
            if (highlight != null)
            { 
                highlight.gameObject.SetActive(i == index);
                Debug.Log($"Highlight for chip {i} active: {i == index}");
            }
        }

    }

    public void Dealer()
    {
        DealerHand = 0;
        UpdateDealerTotal(DealerHand);
        DealerHandTotal.enabled = true;
        DealerCardOne = CardManager.DrawCard();
        DealerCardsInHand++;

        if (DealerCardOne.Value == 11 || DealerCardOne.Value == 1)
        {

            DealerCardOne.Value = AceLogic(DealerHand);

        }

        DealerHand += DealerCardOne.Value;

        
        DealerCardTwo = CardManager.DrawCard();
        DealerCardsInHand++;

        if (DealerCardTwo.Value == 11 || DealerCardTwo.Value == 1)
        {

            DealerCardTwo.Value = AceLogic(DealerHand);

        }

        
        StartCoroutine(CardImage(DealerCardOne, false, DealerCardParent));
        Debug.Log("Dealer was dealt a: " + DealerCardOne.Value);
        DealerHandTotal.text = DealerCardOne.Value.ToString();
        Debug.Log("Dealer second card is unknown.");
        DealerHand = DealerCardOne.Value + DealerCardTwo.Value;


        if (DealerHand == 21 && PlayerHand != 21)
        {
            DisableAllButtons();
            Debug.Log("Dealer second card is: " + DealerCardTwo);
            UpdateDealerTotal(DealerHand);
            StartCoroutine(CardImage(DealerCardTwo, false, DealerCardParent));
            Debug.Log("Dealer hit a BlackJack! Player Loses...");
            DealerWinsPanelText.text = "Dealer hit a BlackJack! Player Loses..." + " Place your bet below to play another hand!";
            DealerWinsPanel.SetActive(true);
            ChipsOn();
            DealButton.interactable = true;



        }
        else if (PlayerHand == 21 && DealerHand != 21)
        {

            Debug.Log("Dealer second card is: " + DealerCardTwo);
            Debug.Log("Player hit a BlackJack! Player Wins!!!");
            DealButton.interactable = true;
            ChipsOn();

            if (DoubleBool)
            {
                DisableAllButtons();
                Money += BetAmount * 4;

                PlayerWinsPanelText.text = $"Player doubled down and won {BetAmount * 4}!!!" + " Place your bet below to play another hand!";
                PlayerWinsPanel.SetActive(true);

                UpdateMoneyTotal(Money);
                ChipsOn();
                DealButton.interactable = true;

            }
            else
            {
                DisableAllButtons();
                Money += BetAmount * 2.5;
                UpdateMoneyTotal(Money);
                PlayerWinsPanelText.text = $"Player hit a BlackJack! Player Wins {BetAmount * 2.5}!!!" + " Place your bet below to play another hand!";
                PlayerWinsPanel.SetActive(true);
                UpdateDealerTotal(DealerHand);
                ChipsOn();
                DealButton.interactable = true;

            }
        }
        else if (DealerHand == 21 && PlayerHand == 21)
        {
            DisableAllButtons();
            Debug.Log("Dealer second card was a: " + DealerCardTwo.Value);
            StartCoroutine(CardImage(DealerCardTwo, false, DealerCardParent));
            Debug.Log("Both Player and Dealer hit a BlackJack. It's a push!!!");
            Money += BetAmount;
            UpdateMoneyTotal(Money);
            PlayerWinsPanelText.text = "Both Player and Dealer hit a BlackJack. It's a push!!!" + " Place your bet below to play another hand!";
            PlayerWinsPanel.SetActive(true);
            ChipsOn();
            DealButton.interactable = true;


        }

        


    }

    public IEnumerator DealerLoop()
    {
        int SHOTotal = CalculateHandTotal(SplitHandOne);
        int SHTTotal = CalculateHandTotal(SplitHandTwo);
        

        HitButton.interactable = false;
        DealButton.interactable = false;

        while (DealerHand < 17)
        {
            yield return new WaitForSeconds(1f);

            Card NewCard = CardManager.DrawCard();

            StartCoroutine(CardImage(NewCard, false, DealerCardParent));

            if (NewCard.Value == 11 || NewCard.Value == 1)
            {
                Debug.Log($"Dealer card number {DealerCardsInHand} is an Ace! Dealer is deciding if they want 1 or 11.");

                NewCard.Value = AceLogic(DealerHand);
            }

            Debug.Log($"Dealer card number {DealerCardsInHand} is a: " + NewCard);
            DealerCardsInHand++;
            DealerHand += NewCard.Value;
            UpdateDealerTotal(DealerHand);
            
            UpdateHandTotalPosition(DealerHandTotalText, HandType.Dealer, DealerCardsInHand);


        }

        
        UpdateHandTotalPosition(DealerHandTotalText, HandType.Dealer, DealerCardsInHand);

        if (SplitBool)
        {

            string result = "";

            if (SHOTotal > 21)
            {

                result += "First hand busts.\n";

            }
            else if (DealerHand > 21 || SHOTotal > DealerHand)
            {

                if (DoubleBool)
                {
                    Money += BetAmount * 4;

                    result += $"Player doubled down on the first hand and won {BetAmount * 4}!!!";

                    UpdateMoneyTotal(Money);
                }
                else
                {
                    Money += BetAmount * 2;
                    result += $"First hand wins {BetAmount * 2}!\n";
                }
            }
            else if (SHOTotal < DealerHand)
            {

                result += "First hand loses.\n";

            }
            else
            {
                Money += BetAmount;
                result += "First hand pushes.\n";
            }

            if (SHTTotal > 21)
            {

                result += "Second hand busts.\n";

            }
            else if (DealerHand > 21 || SHTTotal > DealerHand)
            {

                if (DoubleBool)
                {
                    Money += BetAmount * 4;

                    result += $"Player doubled down on the second hand and won {BetAmount * 4}!!!";

                    UpdateMoneyTotal(Money);
                }
                else
                {
                    Money += BetAmount * 2;
                    result += $"Second hand wins {BetAmount * 2}!\n";
                }
            }
            else if (SHTTotal < DealerHand)
            {
                result += "Second hand loses.\n";
            }
            else
            {
                Money += BetAmount;
                result += "Second hand pushes.\n";
              
            }

            
            UpdateDealerTotal(DealerHand);
            
            UpdateMoneyTotal(Money);
            UpdateHandTotalPosition(DealerHandTotalText, HandType.Dealer, DealerCardsInHand);
            DisableAllButtons();
            PlayerWinsPanelText.text = result + " Place your bet below to play another hand!";
            PlayerWinsPanel.SetActive(true);
            ChipsOn();

        }



        if (!SplitBool)
        {

            if (DealerHand > 21)
            {

                if (DoubleBool)
                {
                    DisableAllButtons();
                    Money += BetAmount * 4;
                    UpdateMoneyTotal(Money);
                    PlayerWinsPanelText.text = $"Dealer busts! Player doubled down and won {BetAmount * 4}!!!" + " Place your bet below to play another hand!";
                    PlayerWinsPanel.SetActive(true);
                    ChipsOn();
                }
                else
                {
                    DisableAllButtons();
                    Debug.Log("Dealer busts! Player Wins!!!");
                    Money += BetAmount * 2;
                    UpdateMoneyTotal(Money);
                    PlayerWinsPanelText.text = $"Dealer busts! Player Wins {BetAmount * 2}!!!" + " Place your bet below to play another hand!";
                    PlayerWinsPanel.SetActive(true);
                    ChipsOn();
                }
            }

            else if (PlayerHand > 21)
            {
                DisableAllButtons();
                Debug.Log("Player busts! Dealer wins...");
                DealerWinsPanelText.text = "Player busts, dealer wins..." + " Place your bet below to play another hand!";
                DealerWinsPanel.SetActive(true);
                ChipsOn();

            }

            else if (DealerHand < PlayerHand)
            {

                if (DoubleBool)
                {
                    DisableAllButtons();
                    Money += BetAmount * 4;
                    UpdateMoneyTotal(Money);
                    PlayerWinsPanelText.text = $"Dealer hand less than Player hand! Player doubled down and won {BetAmount * 4}!!!" + " Place your bet below to play another hand!";
                    PlayerWinsPanel.SetActive(true);
                    ChipsOn();
                }
                else
                {
                    DisableAllButtons();
                    Debug.Log("Player hand is greater than Dealer! Player Wins!!!");
                    Money += BetAmount * 2;
                    UpdateMoneyTotal(Money);
                    PlayerWinsPanelText.text = $"Player hand is greater than Dealer! Player Wins {BetAmount * 2}!!!" + " Place your bet below to play another hand!";
                    PlayerWinsPanel.SetActive(true);
                    ChipsOn();
                }
            }
            else if (DealerHand > PlayerHand)
            {
                DisableAllButtons();
                Debug.Log("Dealer hand is greater than Player! Dealer Wins...");
                DealerWinsPanelText.text = "Dealer hand is greater than Player! Dealer Wins..." + " Place your bet below to play another hand!";
                DealerWinsPanel.SetActive(true);
                ChipsOn();

            }
            else if (DealerHand == PlayerHand)
            {
                DisableAllButtons();
                Debug.Log("Dealer and Player hands tie! It's a push!!!");
                Money += BetAmount;
                UpdateMoneyTotal(Money);
                PlayerWinsPanelText.text = "Dealer and Player hands tie! It's a push!!!" + " Place your bet below to play another hand!";
                PlayerWinsPanel.SetActive(true);
                ChipsOn();
            }
        }

        
        SplitBool = false;
        DealButton.interactable = true;
        
    }

    public IEnumerator Deal()
    {
        EnableAllButtons();

        if (BetAmount == 0)
        {
            Debug.Log("No bet selected! Please choose a chip.");
            yield break;
        }

        DealButton.interactable = false;

        Money -= BetAmount;
        UpdateMoneyTotal(Money);
        ChipsOff();
        if (SelectedChipButton != null)
        {
            SelectedChipButton.image.color = Color.white;
            SelectedChipButton = null;
        }

        ClearPlayerCards();
        ClearDealerCards();
        ClearSplitHandCards();
        SplitHandOne.Clear();
        SplitHandTwo.Clear();
        PlayerHandList.Clear();
        SplitBool = false;
        DoubleBool = false;
        ActiveSplitHandIndex = 1;

        PlayerWinsPanel.SetActive(false);
        DealerWinsPanel.SetActive(false);
        SplitDecisionPanel.SetActive(false);
        HitOrStandDecisionPanel.SetActive(false);
        PlayerHandTotal.enabled = false;
        SplitHandOneTotal.enabled = false;
        SplitHandTwoTotal.enabled = false;
        DealerHandTotal.enabled = false;
        PlayerHandTextBox = false;
        DealerHandTextBox = false;
        DealerHand = 0;
        DealerCardsInHand = 0;
        CardsInHand = 0;
        PlayerHand = 0;

        Vector2 DealerTotalAnchoredPosition = DealerHandTotalText.anchoredPosition;
        DealerTotalAnchoredPosition.x = -1104;
        DealerHandTotalText.anchoredPosition = DealerTotalAnchoredPosition;

        Vector2 SplitHandOneTotalAnchoredPosition = SHOTotalText.anchoredPosition;
        SplitHandOneTotalAnchoredPosition.x = -896;
        SHOTotalText.anchoredPosition = SplitHandOneTotalAnchoredPosition;

        Vector2 SplitHandTwoTotalAnchoredPosition = SHTTotalText.anchoredPosition;
        SplitHandTwoTotalAnchoredPosition.x = -538;
        SHTTotalText.anchoredPosition = SplitHandTwoTotalAnchoredPosition;

        UpdatePlayerTotal(PlayerHand);
        UpdateSplitHandOneTotal(PlayerHand);
        UpdateSplitHandTwoTotal(PlayerHand);
        UpdateDealerTotal(DealerHand);
        

        Debug.Log("Checking Cards In Deck Count...");
        CardManager.CardCheck();

        Card firstCard = CardManager.DrawCard();
        Card secondCard = CardManager.DrawCard();

        PlayerHandList.Add(firstCard);
        UpdatePlayerTotal(PlayerHand);
        
        PlayerHandList.Add(secondCard);
        PlayerHand = CalculateHandTotal(PlayerHandList);
        UpdatePlayerTotal(PlayerHand);
        
        CardsInHand = 2;

        yield return StartCoroutine(DealCardsSequentially(new List<Card> { firstCard, secondCard }, PlayerCardParent));

        if (firstCard.Value == secondCard.Value)
        {
            CardOne = firstCard;
            TempCard = secondCard;
            SplitDecisionPanel.SetActive(true);
            yield break;
        }

        
        UpdateHandTotalPosition(PlayerHandTotalText, HandType.Player, CardsInHand);
        PlayerHandTextBox = false; 
        PlayerHandTotal.enabled = true;
        Dealer();
    }

    public void HitOnDouble()
    {

        Card DoubleCard = CardManager.DrawCard();
        CardsInHand++;

        if (DoubleCard.Value == 11 || DoubleCard.Value == 1)
        {
            DoubleCard.Value = AceLogic(PlayerHand);
        }

        if (SplitBool && ActiveSplitHandIndex == 1)
        {
            SplitHandOne.Add(DoubleCard);
            PlayerHand = CalculateHandTotal(SplitHandOne);
            UpdateSplitHandOneTotal(PlayerHand);
            StartCoroutine(CardImage(DoubleCard, true, SplitHandOneParent));
            UpdateHandTotalPosition(SHOTotalText, HandType.SplitOne, SplitHandOne.Count);
        }

        else if (SplitBool && ActiveSplitHandIndex == 2)
        {
            SplitHandTwo.Add(DoubleCard);
            PlayerHand = CalculateHandTotal(SplitHandTwo);
            UpdateSplitHandTwoTotal(PlayerHand);
            StartCoroutine(CardImage(DoubleCard, true, SplitHandTwoParent));
            UpdateHandTotalPosition(SHTTotalText, HandType.SplitTwo, SplitHandTwo.Count);
        }

        else 
        {
            PlayerHand = CalculateHandTotal(PlayerHandList);
            UpdatePlayerTotal(PlayerHand);
            StartCoroutine(CardImage(DoubleCard, true, PlayerCardParent));
            PlayerHand += DoubleCard.Value;
            
            UpdateHandTotalPosition(PlayerHandTotalText, HandType.Player, CardsInHand);
        }

        UpdatePlayerTotal(PlayerHand);


        if (PlayerHand > 21)
        {

            if (SplitBool)
            {
                if (ActiveSplitHandIndex == 1)
                {
                    UpdateHandTotalPosition(SHOTotalText, HandType.SplitOne, SplitHandOne.Count);
                    StartCoroutine(AdvanceToNextSplitHand());
                }
                else
                {
                    UpdateHandTotalPosition(SHTTotalText, HandType.SplitTwo, SplitHandTwo.Count);
                    Stand();
                }
            }
            else
            {
                DisableAllButtons();
                Debug.Log("Player busts! Player Loses...");
                DealerWinsPanelText.text = "Player busts! Player Loses..." + " Choose your bet below to play another hand!";
                DealerWinsPanel.SetActive(true);
                ChipsOn();
                DealButton.interactable = true;
                UpdateHandTotalPosition(PlayerHandTotalText, HandType.Player, CardsInHand);

            }


            return;

        }
        else if (PlayerHand == 21)
        { 
            DisableAllButtons();
            Debug.Log("Player hit 21, automatically stand.");
        }

        UpdateHandTotalPosition(DealerHandTotalText, HandType.Dealer, DealerCardsInHand);
        Stand();

    }

    public void Hit()
    {

        Card CardR = CardManager.DrawCard();
        CardsInHand++;

        if (CardsInHand > 2)
        {
            DoubleButton.interactable = false;
        }

        if (SplitBool)
        {
            if (ActiveSplitHandIndex == 1)
            {
                SplitHandOne.Add(CardR);
                PlayerHand = CalculateHandTotal(SplitHandOne);

                if (CardR.Value == 1 || CardR.Value == 11)
                {
                    CardR.Value = AceLogic(PlayerHand);
                }

                
                PlayerHand = CalculateHandTotal(SplitHandOne);
                StartCoroutine(CardImage(CardR, true, SplitHandOneParent));
                UpdateSplitHandOneTotal(PlayerHand);
                UpdateHandTotalPosition(SHOTotalText, HandType.SplitOne, SplitHandOne.Count);
            }
            else
            {
                SplitHandTwo.Add(CardR);
                PlayerHand = CalculateHandTotal(SplitHandTwo);

                if (CardR.Value == 1 || CardR.Value == 11)
                {
                    CardR.Value = AceLogic(PlayerHand);
                }

                PlayerHand = CalculateHandTotal(SplitHandTwo);
                StartCoroutine(CardImage(CardR, true, SplitHandTwoParent));
                UpdateSplitHandTwoTotal(PlayerHand);
                UpdateHandTotalPosition(SHTTotalText, HandType.SplitTwo, SplitHandTwo.Count);
            }

            UpdatePlayerTotal(PlayerHand);

            if (PlayerHand > 21)
            {
                if (ActiveSplitHandIndex == 1)
                {
                    StartCoroutine(AdvanceToNextSplitHand());
                }
                else
                {
                    Stand();
                }
             }
            else if (PlayerHand == 21)
            {
                Stand();
            }

            return; 
        }


        if (!SplitBool)
        {

            if (CardR.Value == 11 || CardR.Value == 1)
            {

                Debug.Log("Player was dealt an Ace! Deciding best value...");

                CardR.Value = AceLogic(PlayerHand);


            }

            PlayerHandList.Add(CardR);


            PlayerHand = CalculateHandTotal(PlayerHandList);
            StartCoroutine(CardImage(CardR, true, PlayerCardParent));
            Debug.Log("Player requested a card and got: " + CardR);
            UpdatePlayerTotal(PlayerHand);

        }



        if (PlayerHand > 21)
        {
            if (SplitBool)
            {
                if (ActiveSplitHandIndex == 1)
                {
                    StartCoroutine(AdvanceToNextSplitHand());
                }
                else if (ActiveSplitHandIndex == 2)
                {
                    Stand();
                }
            }
            else
            {
                DisableAllButtons();
                Debug.Log("Player busts! Player Loses...");
                DealerWinsPanelText.text = "Player busts! Player Loses..." + " Choose bet below to play another hand!!!";
                DealerWinsPanel.SetActive(true);
                ChipsOn();
                DealButton.interactable = true;
            }
        }
        else if (PlayerHand == 21)
        {
            Stand();
        }

        
        UpdateHandTotalPosition(PlayerHandTotalText, HandType.Player, CardsInHand);

    }

    public void Stand()
    {


        if (SplitBool)
        {
            if (ActiveSplitHandIndex == 1)
            {
                StartCoroutine(AdvanceToNextSplitHand());
                return;
            }
            else
            {
                DealerHand += DealerCardTwo.Value;
                UpdateDealerTotal(DealerHand);
                Debug.Log("Player stands on second wplit with:" + PlayerHand);
                StartCoroutine(CardImage(DealerCardTwo, false, DealerCardParent));
                StartCoroutine(DealerLoop());
                DealerHandTotal.enabled = true;
                return;
            }
        }

        if (!SplitBool && DealerCardsInHand == 0 && PlayerHand < 21)
        {
            Dealer();
        }


        DisableAllButtons();

        UpdateDealerTotal(DealerHand);

        Debug.Log("Player stands with hand total: " + PlayerHand);

        Debug.Log("Dealer second card was a: " + DealerCardTwo.Value);

        StartCoroutine(CardImage(DealerCardTwo, false, DealerCardParent));

        UpdateHandTotalPosition(DealerHandTotalText, HandType.Dealer, DealerCardsInHand);

        StartCoroutine(DealerLoop());

    }

    public void Double()
    {

        DoubleBool = true;

        Money -= BetAmount;

        UpdateMoneyTotal(Money);

        Debug.Log("Player has Doubled Down!!!");

        Debug.Log("Double bool is: " + DoubleBool);

        if (DealerCardTwo == null)
        {
            Dealer(); 
        }


        HitOnDouble();
    }

    private void UpdatePlayerTotal(int PlayerHand)
    {

        PlayerHandTotal.text = PlayerHand.ToString();

    }

    private void UpdateSplitHandOneTotal(int PlayerHand)
    {

        SplitHandOneTotal.text = PlayerHand.ToString();

    }

    private void UpdateSplitHandTwoTotal(int PlayerHand)
    { 
    
        SplitHandTwoTotal.text = PlayerHand.ToString();

    }
    private void UpdateDealerTotal(int DealerHand)
    {

        DealerHandTotal.text =  DealerHand.ToString();

    }

    private void UpdateMoneyTotal(double Money)
    {

        MoneyText.text = $"Money: ${Money}!!!";

    }

    public int AceLogic(int CurrentHandTotal)
    {
        if (CurrentHandTotal + 11 > 21)
        {
            return 1;
        }
        else
        {
            return 11;
        }

    }

    public int CalculateHandTotal(List<Card> Hand)
    {
        int AceCount = 0;

        int Total = 0;

       

        foreach (Card card in Hand)
        { 
            Total += card.Value;

            if(card.Value == 1 || card.Value == 11)
            { 
                AceCount++; 
            } 
        }


        while (Total > 21 && AceCount > 0)
        {

            if (Total == 21)
            {
                return Total;
            }

            Total -= 10;
            AceCount--;
        }

        return Total;
        

    }

    public void StandBy()
    {

        Debug.Log("Would you like to hit or stand?");

        HitButton.onClick.RemoveListener(Hit);
        StandButton.onClick.RemoveListener(Stand);

        HitButton.onClick.AddListener(Hit);
        StandButton.onClick.AddListener(Stand);

    }

    private void ClearPlayerCards()
    {
        foreach (Transform child in PlayerCardParent)
        {
            Destroy(child.gameObject);
        }       
    }

    private void ClearDealerCards()
    {
        foreach (Transform child in DealerCardParent)
        {
            Destroy(child.gameObject);
        }
    }

    private void ClearSplitHandCards()
    {
        foreach (Transform child in SplitHandOneParent)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in SplitHandTwoParent)
        {
            Destroy(child.gameObject);
        }
    }

    public IEnumerator Split(Card CardOne, Card CardTwo)
    {
        SplitDecisionPanel.SetActive(false);
        SplitHandTwoTotal.enabled = false;
        PlayerHandTotal.enabled = false;
        SplitHandOneTotal.enabled = true;

        SplitBool = true;
        ClearPlayerCards();

        SplitHandOne.Clear();
        SplitHandTwo.Clear();

        SplitHandOne.Add(CardOne);
        SplitHandTwo.Add(CardTwo);

        ActiveSplitHandIndex = 1;

        Card NewCardOne = CardManager.DrawCard();
        SplitHandOne.Add(NewCardOne);
        PlayerHand = CalculateHandTotal(SplitHandOne);

        yield return StartCoroutine(DealCardsSequentially(new List<Card> { CardOne, NewCardOne }, SplitHandOneParent));

        Debug.Log("First Split");
        Debug.Log($"Player bet the bet amount: {BetAmount} on the split hand.");
        Money -= BetAmount;
        UpdateMoneyTotal(Money);
        Debug.Log("Player chose to split and got:" + CardOne + NewCardOne);

        PlayerHand = CalculateHandTotal(SplitHandOne);
        UpdateSplitHandOneTotal(PlayerHand);
        


    }

    public void SplitYes()
    {
        StartCoroutine(Split(CardOne, TempCard));
    }

    public void SplitNo()
    {
        Debug.Log("Split wasn't taken, would you like to hit or stand?");
        SplitDecisionPanel.SetActive(false);
        Dealer();
    }

    public IEnumerator AdvanceToNextSplitHand()
    {
        if (ActiveSplitHandIndex == 1)
        {
            ActiveSplitHandIndex = 2;

            PlayerHandTotal.enabled = false;
            SplitHandTwoTotal.enabled = true;

            Card NewCardTwo = CardManager.DrawCard();

            SplitHandTwo.Add(NewCardTwo);
            PlayerHand = CalculateHandTotal(SplitHandTwo);

            foreach (Card card in SplitHandTwo)
            {
                
                yield return StartCoroutine(DealCardsSequentially(new List<Card> {card}, SplitHandTwoParent));

            }

            Debug.Log("Second Split");
            Debug.Log($"Player bet the bet amount: {BetAmount} on the split hand.");
            Money -= BetAmount;
            UpdateMoneyTotal(Money);
            Debug.Log("Players second split got:" + string.Join(", ", SplitHandTwo));



            PlayerHand = CalculateHandTotal(SplitHandTwo);
            UpdateSplitHandTwoTotal(PlayerHand);

        }


    }

    public void DisableAllButtons()
    {
        DealButton.interactable = false;
        HitButton.interactable = false;
        StandButton.interactable = false;
        DoubleButton.interactable = false;
    }

    public void EnableAllButtons()
    {
        DoubleButton.interactable = true;
        DealButton.interactable = true;
        HitButton.interactable = true;
        StandButton.interactable = true;
    }

    public void UpdateHandTotalPosition(RectTransform HandTextBox, HandType HandType, int CardsInHand)
    {

        Vector2 NewAnchoredPosition = HandTextBox.anchoredPosition;

        Debug.Log("Entered UpdateHandPositionTotal" + HandTextBox);

        switch (HandType)
        {
            case HandType.Player:
                NewAnchoredPosition.x = CardsInHand <= 2 ? -326 : -271;
                Debug.Log($"Updated {HandType} hand textbox to {NewAnchoredPosition.x} based on {CardsInHand} cards.");
                break;

            case HandType.Dealer:
                NewAnchoredPosition.x = CardsInHand <= 2 ? -1086 : -1030;
                Debug.Log($"Updated {HandType} hand textbox to {NewAnchoredPosition.x} based on {CardsInHand} cards.");
                break;

            case HandType.SplitOne:
                NewAnchoredPosition.x = CardsInHand <= 2 ? -900 : -845;
                Debug.Log($"Updated {HandType} hand textbox to {NewAnchoredPosition.x} based on {CardsInHand} cards.");
                break;

            case HandType.SplitTwo:
                NewAnchoredPosition.x = CardsInHand <= 2 ? -538 : -475;
                Debug.Log($"Updated {HandType} hand textbox to {NewAnchoredPosition.x} based on {CardsInHand} cards.");
                break;
        }

        HandTextBox.anchoredPosition = NewAnchoredPosition;
    }

}