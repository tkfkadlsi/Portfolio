using System.Collections.Generic;

[System.Serializable]
public class Card
{
    public CardType Type;
    public string DisplayName;
    public string Description;
    public int MaxCount;

    public void SelectCard()
    {
        Managers.Instance.Game.AddCard(Type);
    }
}