using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardPanel : BaseUI
{
    private Card _currentCard;

    enum ETexts
    {
        CardName,
        Description
    }

    enum EButtons
    {
        CardSelectButton
    }

    private TextMeshProUGUI _cardNameText;
    private TextMeshProUGUI _descriptionText;

    private Button _cardSelectButton;

    protected override void Init()
    {
        base.Init();

        Bind<TextMeshProUGUI>(typeof(ETexts));
        Bind<Button>(typeof(EButtons));

        _cardNameText = Get<TextMeshProUGUI>((int)ETexts.CardName);
        _descriptionText = Get<TextMeshProUGUI>((int)ETexts.Description);

        _cardSelectButton = Get<Button>((int)EButtons.CardSelectButton);

        _cardSelectButton.onClick.AddListener(ButtonHandler);
    }

    protected override void Release()
    {
        base.Release();

        _cardSelectButton.onClick.RemoveAllListeners();
    }

    public void SetCard(Card card)
    {
        _cardSelectButton.interactable = true;

        _currentCard = card;

        _cardNameText.text = _currentCard.DisplayName;
        _descriptionText.text = _currentCard.Description;

        _cardSelectButton.gameObject.SetActive(true);
    }

    private void ButtonHandler()
    {
        _cardSelectButton.interactable = false;
        Managers.Instance.UI.GetRootUI().GetCanvas<CardCanvas>().CardSelect(this).Forget();
        _currentCard.SelectCard();
    }

    public void AutoSelect()
    {
        ButtonHandler();
    }
}
