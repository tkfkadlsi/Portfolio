using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

public class CardCanvas : BaseCanvas, IOpenClosePanel
{
    [SerializeField] private List<CardPanel> _cardPanels = new List<CardPanel>();

    enum ESliders
    {
        TimeSlider
    }

    private Slider _timeSlider;
    private bool _isSelect = true;

    protected override void Init()
    {
        base.Init();

        SetEnable(false);

        Bind<Slider>(typeof(ESliders));

        _timeSlider = Get<Slider>((int)ESliders.TimeSlider);

        _timeSlider.maxValue = 10f;
        _timeSlider.value = 10f;
    }

    public void ClosePanel()
    {
        for (int i = 0; i < _cardPanels.Count; i++)
        {
            RectTransform rect = _cardPanels[i].GetComponent<RectTransform>();

            rect.DOScale(0f, 0.5f);
        }

        CanvasDisable().Forget();
    }

    private async UniTask CanvasDisable()
    {
        await UniTask.Delay(500);
        SetEnable(false);
    }

    public async void OpenPanel()
    {
        SetEnable(true);
        CardShuffle();

        int duplicateCount = 0;

        for(int i = 0; i < _cardPanels.Count;)
        {

            Card card = Managers.Instance.Data.CardList[i + duplicateCount];
            if (card.MaxCount != 0 && card.MaxCount <= Managers.Instance.Game.GetCardCount(card.Type))
            {
                duplicateCount++;
                continue;
            }

            RectTransform rect = _cardPanels[i].GetComponent<RectTransform>();

            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, 1000);
            rect.localScale = Vector3.one;
            await CardMove(rect, 0);

            _cardPanels[i].SetCard(card);

            i++;
        }

        _selectTime = 10f;
        _timeSlider.value = 10f;
        _isSelect = false;
    }

    private async UniTask CardMove(RectTransform rect, int endYPos)
    {
        rect.DOAnchorPosY(endYPos, 0.5f);
        await UniTask.Delay(200);
    }

    private float _selectTime = 0f;

    private void Update()
    {
        if(_isSelect == false)
        {
            _selectTime -= Time.deltaTime;
            _timeSlider.value = _selectTime;

            if(_selectTime < 0f)
            {
                _isSelect = true;

                int rand = Random.Range(0, 3);
                _cardPanels[rand].AutoSelect();
            }
        }
    }

    private void CardShuffle()
    {
        List<Card> cards = Managers.Instance.Data.CardList;
        Card tempCard;
        int swap1;
        int swap2;


        for(int i = 0; i < 100; i++)
        {
            swap1 = Random.Range(0, cards.Count);
            swap2 = Random.Range(0, cards.Count);
            
            tempCard = cards[swap1];
            cards[swap1] = cards[swap2];
            cards[swap2] = tempCard;
        }
    }

    public async UniTask CardSelect(CardPanel panel)
    {
        _isSelect = true;

        for(int i = 0; i < _cardPanels.Count; i++)
        {
            if(panel == _cardPanels[i]) continue;

            _cardPanels[i].GetComponent<RectTransform>().DOScale(0f, 0.5f);
        }

        await UniTask.Delay(250);

        ClosePanel();
    }
}
