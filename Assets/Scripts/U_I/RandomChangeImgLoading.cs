using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;


public class RandomChangeImgLoading : MonoBehaviour
{
    public GameObject[] pictures;
    public GameObject[] titles;
    private GameObject cacheLastTitle;
    private int currentPicIndex;
    private bool isChangeTextCompleted;
    private bool isChangePicCompleted;
    private bool isTransitionIn;
    private int currenTitleIndex;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TransitionToAnotherText());
        StartCoroutine(TransitionToAnotherPic());
    }

    IEnumerator TransitionToAnotherText()
    {
        while (!SceneLoadingManager.Instance.isGameReady)
        {
            var nextTitleIndex = GetIndexAnotherText();
            yield return new WaitUntil(() => isTransitionIn);
            StartCoroutine(ChangeText(nextTitleIndex));
            yield return new WaitUntil(()=>isChangeTextCompleted);

            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator TransitionToAnotherPic()
    {
        while (!SceneLoadingManager.Instance.isGameReady)
        {
           
            var nextIndexPic = GetIndexAnotherPicture();

            StartCoroutine(ChangePic(nextIndexPic));
            yield return new WaitUntil(()=>isChangePicCompleted);

            yield return new WaitForSeconds(3.5f);
        }
    }
    IEnumerator ChangeText(int nextTitleIndex)
    {
        isChangeTextCompleted = false;
        //fade out
        titles[currenTitleIndex].GetComponent<TMP_Text>().DOFade(0, 1).SetEase(Ease.Linear);
        yield return new WaitForSeconds(1);
        titles[currenTitleIndex].SetActive(false);
        //fade in
        titles[nextTitleIndex].GetComponent<TMP_Text>().DOFade(0, 0);
        yield return null;
        titles[nextTitleIndex].SetActive(true);
        yield return new WaitForSeconds(0.2f);
        titles[nextTitleIndex].GetComponent<TMP_Text>().DOFade(1, 1).SetEase(Ease.Linear);
        yield return new WaitForSeconds(1);
        isChangeTextCompleted = true;
    }

    IEnumerator ChangePic(int nextIndexPic)
    {
        //di vao 
        isTransitionIn = false;
        isChangePicCompleted = false;
        pictures[nextIndexPic].GetComponent<Image>().DOFade(0, 0);
        yield return null;
        pictures[nextIndexPic].SetActive(true);
        //bat dau fade out
        pictures[currentPicIndex].GetComponent<Image>().DOFade(0, 1).SetEase(Ease.Linear);
        yield return new WaitForSeconds(0.1f);
        //bat dau fade in
        pictures[nextIndexPic].GetComponent<Image>().DOFade(1, 1).SetEase(Ease.Linear);
        yield return new WaitForSeconds(1f);
        isTransitionIn = true;
        pictures[currentPicIndex].SetActive(false);
        isChangePicCompleted = true;
    }

    private int GetIndexAnotherPicture()
    {
        var nextPicIndex = Random.Range(0, 3);
        currentPicIndex = GetCurrentPicIndex();

        while (nextPicIndex == currentPicIndex)
        {
            nextPicIndex = Random.Range(0, 3);
        }

        return nextPicIndex;
    }

    private int GetCurrentPicIndex()
    {
        for (int i = 0; i < pictures.Length; i++)
        {
            if (pictures[i].activeInHierarchy)
            {
                return i;
            }
        }

        return 0;
    }

    private int GetIndexAnotherText()
    {
        var nextTextIndex = Random.Range(0, 5);
        currenTitleIndex = GetCurrentTextIndex();

        while (nextTextIndex == currenTitleIndex)
        {
            nextTextIndex = Random.Range(0, 5);
        }


        return nextTextIndex;
    }
    
    private int GetCurrentTextIndex()
    {
        for (int i = 0; i < titles.Length; i++)
        {
            if (titles[i].activeInHierarchy)
            {
                return i;
            }
        }

        return 0;
    }
}