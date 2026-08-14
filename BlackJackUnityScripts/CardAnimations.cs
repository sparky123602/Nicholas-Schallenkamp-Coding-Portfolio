using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class CardAnimations : MonoBehaviour
{
    public Image CardImage;
    public Sprite BackSprite;

    public Sprite FrontSprite;

    public void SetCardSprite(Sprite Front, Sprite Back)
    {
        FrontSprite = Front;
        BackSprite = Back;
        CardImage.sprite = BackSprite;
    }

    public void CardValue(Sprite CardSprite)
    { 
        CardImage.sprite = CardSprite;
    }

    public void RevealFront()
    { 
        CardImage.sprite = FrontSprite;
    }

    public void ShowBack()
    { 
        CardImage.sprite = BackSprite;
    }
}


