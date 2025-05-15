using System.Collections.Generic;
using UnityEngine;

public class GameRootUI : BaseUI, ISetActiveCanvases
{
    private Dictionary<string, Canvas> CanvasDict = new Dictionary<string, Canvas>();

    public MainCanvas MainCanvas { get; private set; }
    public BuildCanvas BuildingsCanvas { get; private set; }
    public SongCanvas SongCanvas { get; private set; }
    public RewardCanvas RewardCanvas { get; private set; }
    public BloodCanvas BloodCanvas { get; private set; }
    public OptionCanvas OptionCanvas { get; private set; }
    public TowerUpCanvas TowerUpCanvas { get; private set; }

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        MainCanvas = gameObject.FindChild<MainCanvas>("MainCanvas", true);
        BuildingsCanvas = gameObject.FindChild<BuildCanvas>("BuildCanvas", true);
        SongCanvas = gameObject.FindChild<SongCanvas>("SongCanvas", true);
        RewardCanvas = gameObject.FindChild<RewardCanvas>("RewardCanvas", true);
        BloodCanvas = gameObject.FindChild<BloodCanvas>("BloodCanvas", true);
        OptionCanvas = gameObject.FindChild<OptionCanvas>("OptionCanvas", true);
        TowerUpCanvas = gameObject.FindChild<TowerUpCanvas>("TowerUpCanvas", true);

        CanvasDict.Add(MainCanvas.name, MainCanvas.GetComponent<Canvas>());
        CanvasDict.Add(BuildingsCanvas.name, BuildingsCanvas.GetComponent<Canvas>());
        CanvasDict.Add(SongCanvas.name, SongCanvas.GetComponent<Canvas>());
        CanvasDict.Add(RewardCanvas.name, RewardCanvas.GetComponent<Canvas>());
        CanvasDict.Add(BloodCanvas.name, BloodCanvas.GetComponent<Canvas>());
        CanvasDict.Add(OptionCanvas.name, OptionCanvas.GetComponent<Canvas>());
        CanvasDict.Add(TowerUpCanvas.name, TowerUpCanvas.GetComponent<Canvas>());

        SetActiveCanvas("MainCanvas", true);
        SetActiveCanvas("BuildCanvas", false);
        SetActiveCanvas("SongCanvas", false);
        SetActiveCanvas("RewardCanvas", false);
        SetActiveCanvas("BloodCanvas", true);
        SetActiveCanvas("OptionCanvas", false);
        SetActiveCanvas("TowerUpCanvas", false);
        return true;
    }

    public void SetActiveCanvas(string name, bool active)
    {
        CanvasDict[name].gameObject.SetActive(active);
        if(name == TowerUpCanvas.name)
        {
            SetActiveCanvas("MainCanvas", !active);
        }
    }
}
