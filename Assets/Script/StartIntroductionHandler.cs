using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartIntroductionHandler : MonoBehaviour
{
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button startButton;
    private int _currentPage;

    // creates an accessible component in the editor, which allows to add as many Text pages to the Introduction as one would like
    // press "+" in the according component in the editor to add a new field for text input for each Page
    [TextArea(15, 20)]
    [SerializeField] private string[] textpageList;     

    // text field of the UI element, which is used to show the currently selected question
    [SerializeField] private TextMeshProUGUI textDisplay;

    // check if there has been entered a question via the input field in the editor
    // if so, show the first question on the questionnaire UI, else initialize the question array with an empty string
    // UI always starts displaying page 0
    void Start()
    {
        // set up first page
        _currentPage = 0;
        if (textpageList.Length <= 0)
        {
            textpageList = new string[1];
            textpageList[0] = "";
        }
        textDisplay.text = textpageList[0];

        // set up buttons
        MaybeEnableStartButton();
        previousButton.interactable = false;
        if (textpageList.Length <= 1)
        {
            nextButton.interactable = false;
        }
        else
        {
            nextButton.interactable = true;
        }
    }

    // if on the last Page enable StartButton
    public void MaybeEnableStartButton()
    {
        if (_currentPage+1 == textpageList.Length)
        {
            startButton.interactable = true;
        } else
        {
            startButton.interactable = false;
        }
    }

    // if the "next" button is clicked, the UI will show the next question in line
    public void ShowNextPage()
    {
        // increase page number
        _currentPage++;

        // if page number is out of bounds, reset to max page number
        if (_currentPage >= textpageList.Length)
        {
            _currentPage = textpageList.Length - 1;
        }

        // display question according to page number
        textDisplay.text = textpageList[_currentPage];
        
        previousButton.interactable = true;

        // disable "next" button if UI currently shows the last page
        if (_currentPage == textpageList.Length - 1)
        {
            nextButton.interactable = false;
        }

        MaybeEnableStartButton();
    }

    // if the "previous" button is clicked, the UI will show the previous question
    public void ShowPrevPage()
    {
        // decrease page number
        _currentPage--;

        // if page number is out ouf bounds, reset to min page number
        if (_currentPage < 0)
        {
            _currentPage = 0;
        }

        // display question according to page number
        textDisplay.text = textpageList[_currentPage];

        // disable "previous" button if there is no previous page
        if (_currentPage == 0)
        {
            previousButton.interactable = false;
        }

        // disable "confirm" button if UI currently does not show the last page
        if (_currentPage < textpageList.Length - 1)
        {
            nextButton.interactable = true;
            startButton.interactable = false;
        }

        MaybeEnableStartButton();
    }

}