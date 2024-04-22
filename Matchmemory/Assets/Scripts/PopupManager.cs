using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private GameObject popupObject = null;

    [SerializeField] private TextMeshProUGUI feedbackTextComponent = null;

    //private Animation animation = null;
    //[SerializeField] private AnimationClip clip = null;
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //animator = popupObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowFeedback(string feedback)
    {
        popupObject.SetActive(true);

        switch (feedback)
        {
            case "submiterror":
                feedbackTextComponent.text = "Fields Cannot Be Empty";                
                break;
            case "loaddata":
                feedbackTextComponent.text = "No saved data";
                break;
            case "initcomment":
                feedbackTextComponent.text = "Maximum allowed rows and colums is 6 x 5";
                break;
                default:
                break;
        }

        StartCoroutine(AnimatePopup());
    }

    private IEnumerator AnimatePopup()
    {
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        //yield return new WaitUntil(() => animation.isPlaying == false);
        yield return new WaitForSecondsRealtime(animationLength);
        popupObject.SetActive(false);
    }
}
