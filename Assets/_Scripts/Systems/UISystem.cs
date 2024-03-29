using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;
 

public partial class UISystem : SystemBase
{
     Transform _canvas= null;
     TextMeshProUGUI _incomeText = null;
    int buildCostFactory=0;
    int buildCostOilRig=0;
    float buildTimeFactory=0;
    float buildTimeOilRig=0;
     int buildCostTank = 0;
      float buildTimeTank = 0;
      Transform startButton = null;
    protected override void OnStartRunning()
    {
        
        //
        Entities.
            WithoutBurst().
            ForEach(
                (in UIComponent ui) =>
                {
                    _canvas = GameObject.Instantiate(ui.CanvasPrefab).transform;
                }).Run();

        _incomeText = _canvas.Find("TxtIncome").GetComponent<TextMeshProUGUI>();
         //
         startButton = _canvas.Find("BtnStart");
         Entities.
             WithoutBurst().
             ForEach(
                 (ref SettingsComponent settings) =>
                 {
                     buildCostFactory=  settings.CostOfFactoryBuild;
                     buildCostOilRig= settings.CostOfOilRigBuild;
                     buildTimeFactory = settings.DurationOfFactoryBuild;
                     buildTimeOilRig = settings.DurationOfOilRigBuild;
                     buildCostTank = settings.CostOfTankBuild;
                     buildTimeTank = settings.DurationOfTankBuild;
                 }).Run();
    }

    protected override void OnUpdate()
    {
        long playerIncome = 0;
        Entities.
            WithoutBurst().
            ForEach(
            (ref IncomeComponent income) =>
            {
                playerIncome = income.IncomePlayer;
                _incomeText.text = income.IncomePlayer.ToString().PadLeft(6, '0');
            }).Run();
        
        
        Entities.
            WithoutBurst()
            .ForEach(
            (in BuildingInfoComponent info) =>
            {
                TextMeshProUGUI text;
                for (int i = 0; i < _canvas.childCount; i++)
                {
                    if (info.BuildingType=="PlayerFactory" && _canvas.GetChild(i).name== "ImgFactory" +(info.Index + 1))
                    {
                     text = _canvas.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>();
                     if (info.Built)
                     {
                         text.text = buildCostTank.ToString();
                         text.color = playerIncome < buildCostTank ? Color.red : Color.white;
                         
                     }
                     else
                     {
                         text.text = buildCostFactory.ToString();
                         text.color = playerIncome < buildCostFactory ? Color.red : Color.white;
                     }
                 
                     

                    }
                    else if (info.BuildingType=="PlayerOilRig"&& _canvas.GetChild(i).name== "ImgOilRig" +(info.Index + 1))
                    {
                            text = _canvas.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>();
                            text.text = buildCostOilRig.ToString();
                            text.color = playerIncome < buildCostOilRig ? Color.red : Color.white;

                    }
                    
                }
                //
                //
                if (info.BuildingType == "PlayerFactory" || info.BuildingType == "PlayerOilRig")
                {
                    Transform img;
                    float value = 0;
                    if (info.BuildingType == "PlayerFactory")
                    {
                        img = _canvas.Find("ImgFactory" + (info.Index + 1));
                        value = (info.TimeToFinishProduction - DateTime.Now.Ticks) / 10000000f;

                        if (!info.Built)
                        {
                            // factory
                            value = (buildTimeFactory - value) / buildTimeFactory;

                            if (!info.Built && info.IsProducing)
                            {
                                if (info.TimeToFinishProduction <= DateTime.Now.Ticks)
                                {
                                    // finisged
                                    img.GetComponent<Image>().sprite = info.TankSprite;
                                }
                            }
                        } else
                        {
                            // tank 
                            value = (buildTimeTank - value) / buildTimeTank;
                        }
                    }
                    else
                    {
                        img = _canvas.Find("ImgOilRig" + (info.Index + 1));
                        value = (info.TimeToFinishProduction - DateTime.Now.Ticks) / 10000000f;
                        value = (buildTimeOilRig - value) / buildTimeOilRig;
                    }

                    if (info.IsProducing)
                    {
                        // 
                        img.GetChild(0).gameObject.SetActive(true);
                        img.GetComponent<Image>().color = Color.white;

                        Slider slider = img.GetChild(0).GetComponent<Slider>();
                        slider.value = value;
                    } else
                    {
                        img.GetChild(0).gameObject.SetActive(false);
                    }
                }
            }).Run();
        
        //marche
        bool marchButtonEnabled = false;
        Entities.ForEach(
            (ref BattleFieldComponent battleFieldComponent) =>
            {
                marchButtonEnabled = battleFieldComponent.NumPlayerTanks >= 100;
            }).Run();

        startButton.GetComponent<CanvasGroup>().alpha = marchButtonEnabled ? 1 : 0.5f;

        // winner
        int playerTanks = 0;
        int aiTanks = 0;
        bool gameStarted = false;

        Entities.
            WithoutBurst().
            ForEach(
                (ref BattleFieldComponent component) =>
                {
                    playerTanks = component.NumPlayerTanks;
                    aiTanks = component.NumAITanks;
                    gameStarted = component.BattleStarted;
                }).Run();

        if (gameStarted && playerTanks <= 0)
        {
            // lose
            _canvas.Find("TxtDefeat").GetComponent<TextMeshProUGUI>().enabled = true;
        }

        if (gameStarted && aiTanks <= 0)
        {
            // win
            _canvas.Find("TxtVictory").GetComponent<TextMeshProUGUI>().enabled = true;
        }
    }
}
