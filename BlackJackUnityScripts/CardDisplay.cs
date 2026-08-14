using UnityEngine;
using System.Collections.Generic;
using System.Linq;



public class CardSpriteManager : MonoBehaviour
{
    [System.Serializable]
    public class CardSpriteEntry
    {
        public string Rank;
        public string Suit;
        public Sprite CardSprite;

    }

    public Sprite BackOfCardSprite;

    public Sprite GetBackSprite()
    {
        return BackOfCardSprite;
    }

    public List<CardSpriteEntry> CardSprites;

    public Sprite GetSpriteValue(string rank, string suit)
    {
        var entry = CardSprites.FirstOrDefault(c => c.Rank == rank && c.Suit == suit);

        if (entry == null)
        {
            Debug.Log("SPRITE NOT FOUND :(((((");
            return BackOfCardSprite;
        }

        return entry.CardSprite;

    }
}
