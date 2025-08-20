using UnityEngine;



public class Card: MonoBehaviour
{
    public string Suit;
    public string Rank;
    public int Value;
    public Sprite TempCardSprite;

    public void SetCard(string CardSuit, string CardRank, int CardValue, Sprite CardSprite)
    {
        Suit = CardSuit;
        Rank = CardRank;
        Value = CardValue;
        TempCardSprite = CardSprite;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}

   

