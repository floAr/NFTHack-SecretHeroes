using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    public enum Location
    {
        MAIN,
        MARKET,
        BAR,
        ARENA
    }

    public CameraController MainCam;
    public FadeCamera MainCamFade;

    public Transform ResetTransform;

    public Coroutine RunningTransition;

    // MARKET
    public ClickableObject Market;
    public DrawManager DrawHall;

    //BAR
    public ClickableObject Bar;
    public SelectionRooster Selection;

    //ARENA
    public ClickableObject Arena;
    public BattleMaster ArenaMaster;

    public TMPro.TMP_Text CTA;
    public Button MenuButton;
    public Button MintButton;
    public Button SendButton;

    public Location CurrentLocation = Location.MAIN;

    [ContextMenu("Market Transition")]
    public void TransitionIntoMarket()
    {
        if (CurrentLocation == Location.MARKET)
            return;
        StartCoroutine(MarketTransition());
    }

    public IEnumerator MarketTransition()
    {
        if (CurrentLocation != Location.MAIN)
            yield return StartCoroutine(ResetTransition());
        CurrentLocation = Location.MARKET;
        MainCam.LerpToTransform(Market.ObjectCamera.transform.position, Market.ObjectCamera.transform.rotation.eulerAngles);
        yield return new WaitForSeconds(MainCam.LerpTime - MainCamFade.Duration * 0.95f);
        MainCamFade.FadeOut();
        yield return new WaitForSeconds(MainCamFade.Duration);
        MainCam.transform.position = DrawHall.DrawCamera.transform.position;
        MainCam.transform.rotation = DrawHall.DrawCamera.transform.rotation;
        MainCamFade.FadeIn();

        CTA.text = "Reveal Heroes";
        CTA.gameObject.SetActive(true);
        MenuButton.gameObject.SetActive(true);
        MintButton.gameObject.SetActive(false);
        SendButton.gameObject.SetActive(false);
        yield return true;
    }


    [ContextMenu("Bar Transition")]
    public void TransitionIntoBar()
    {
        if (CurrentLocation == Location.BAR)
            return;
        StartCoroutine(BarTransition());
    }

    public IEnumerator BarTransition()
    {
        if (CurrentLocation != Location.MAIN)
            yield return StartCoroutine(ResetTransition());
        CurrentLocation = Location.BAR;
        MainCam.LerpToTransform(Bar.ObjectCamera.transform.position, Bar.ObjectCamera.transform.rotation.eulerAngles);
        yield return new WaitForSeconds(MainCam.LerpTime - MainCamFade.Duration * 0.95f);
        MainCamFade.FadeOut();
        yield return new WaitForSeconds(MainCamFade.Duration);
        MainCam.transform.position = Selection.SectionCamera.transform.position;
        MainCam.transform.rotation = Selection.SectionCamera.transform.rotation;
        MainCamFade.FadeIn();

        CTA.text = "Reveal Heroes";
        CTA.gameObject.SetActive(false);
        MenuButton.gameObject.SetActive(true);
        MintButton.gameObject.SetActive(false);
        SendButton.gameObject.SetActive(false);

        MainCam.GetComponent<CameraController>().enabled = false;
        Selection.canvas.gameObject.SetActive(true);
        Selection.thumbnailCreator.SetActive(true);

        yield return true;
    }


    [ContextMenu("Arena Transition")]
    public void TransitionIntoArena()
    {
        if (CurrentLocation == Location.ARENA)
            return;
        StartCoroutine(ArenaTransition());
    }

    public IEnumerator ArenaTransition()
    {
        if (CurrentLocation != Location.MAIN)
            yield return StartCoroutine(ResetTransition());
        CurrentLocation = Location.ARENA;
        MainCam.LerpToTransform(Arena.ObjectCamera.transform.position, Arena.ObjectCamera.transform.rotation.eulerAngles);
        yield return new WaitForSeconds(MainCam.LerpTime - MainCamFade.Duration * 0.95f);
        MainCamFade.FadeOut();
        yield return new WaitForSeconds(MainCamFade.Duration);
        MainCam.transform.position = ArenaMaster.BattleCamera.transform.position;
        MainCam.transform.rotation = ArenaMaster.BattleCamera.transform.rotation;
        MainCamFade.FadeIn();
        CTA.text = "Prepare for Battle";
        CTA.gameObject.SetActive(true);
        MenuButton.gameObject.SetActive(true);
        MintButton.gameObject.SetActive(false);
        SendButton.gameObject.SetActive(false);
        yield return true;
    }





    [ContextMenu("Reset")]
    public void ResetTransitions()
    {
        if (CurrentLocation == Location.MAIN)
            return;
        CurrentLocation = Location.MAIN;
        StartCoroutine(ResetTransition());
    }

    public IEnumerator ResetTransition()
    {
        MainCamFade.FadeOut();
        yield return new WaitForSeconds(MainCamFade.Duration);
        MainCam.transform.position = ResetTransform.position;
        MainCam.transform.rotation = ResetTransform.rotation;
        MainCamFade.FadeIn();
        CurrentLocation = Location.MAIN;

        CTA.text = "Reveal Heroes";
        CTA.gameObject.SetActive(false);
        MenuButton.gameObject.SetActive(false);
        MintButton.gameObject.SetActive(false);
        SendButton.gameObject.SetActive(false);

        Selection.canvas.gameObject.SetActive(false);
        Selection.thumbnailCreator.SetActive(false);
        MainCam.GetComponent<CameraController>().enabled = true;

        yield return true;
    }
}
