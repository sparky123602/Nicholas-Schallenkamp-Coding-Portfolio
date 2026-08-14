using UnityEngine;
using System.Collections.Generic;

public class DeckManager : MonoBehaviour
{
    public List<Card> Deck = new List<Card>();
    public CardSpriteManager SpriteManager;
    public GameObject CardPrefab;
    public int CardsInDeck = 0;

    void Start()
    {
        GenerateDeck();
        ShuffleDeck();
    }

    void GenerateDeck()
    {

        string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
        string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };
        Dictionary<string, int> cardValues = new Dictionary<string, int>()
        {
            { "1", 1}, {"2", 2}, {"3", 3}, {"4", 4}, {"5", 5}, {"6", 6},
            {"7", 7}, {"8", 8}, {"9", 9}, {"10", 10},
            {"Jack", 10}, {"Queen", 10}, {"King", 10}, {"Ace", 11} 
        };

        foreach (string suit in suits)
        {
            foreach (string rank in ranks)
            {
                int value = cardValues[rank];
                Sprite sprite = SpriteManager.GetSpriteValue(rank, suit);

                GameObject cardGO = Instantiate(CardPrefab);
                Card card = cardGO.GetComponent<Card>();

                card.SetCard(suit, rank, value, sprite);
                Deck.Add(card);
                CardsInDeck++;
            }
        }
    }

    void ShuffleDeck()
    {
        for (int i = 0; i < Deck.Count; i++)
        {

            Card temp = Deck[i];
            int randomIndex = Random.Range(i, Deck.Count);
            Deck[i] = Deck[randomIndex];
            Deck[randomIndex] = temp;
        }
    }

    public Card DrawCard()
    {
        CardsInDeck--;

        Debug.Log($"CARDS LEFT IN DECK: {CardsInDeck}");

        if (Deck.Count == 0)
        {
            Debug.Log("Deck is empty!");
            return null;
        }

        Card card = Deck[0];
        Deck.RemoveAt(0);
        return card;
    }

    public void CardCheck()
    {
        if (CardsInDeck < 10)
        {
            Deck.Clear();
            GenerateDeck();
            ShuffleDeck();
            CardsInDeck = Deck.Count;

            Debug.Log("Deck regenerated and shuffled because there were less than 10 cards left.");
        }
    
    }

}





