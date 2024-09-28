using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicGame : MonoBehaviour 
{

    // Variables related to questions
    public List<Question> questions = new List<Question>();  // Store all question
    public int min_Qs = 8;  // Min number to start
    private int Qs_count = 0;

    // Game variables
    private int score = 0;
    private int total_Qs_completed = 0;
    private int asked_Qs = 0;

    // UI Elements
    public Text questionText;  // Text object to display the question
    public Text[] answerButtons;  // Array of buttons
    public Text QsCountText;  // Display total questions count
    public Button startButton;  // Start game button
    public Text endScreenText;  // Text to display score on end screen

    // User input fields for adding questions
    public InputField inputQuestionField; 
    public InputField inputAnswerField;
    public Button addButton;

    // Game state
    private bool[] askedQuestions;
    private bool gameStarted = false; // Game status

    void Start()
    {
        // Disable start until minimum questions are added
        startButton.interactable = false;
        Qs_count = questions.Count;
        askedQuestions = new bool[Qs_count];

        // Update the question count text initially
        QsCountText.text = "Questions Added: " + Qs_count;

        // Add listener to add button for adding questions
        addButton.onClick.AddListener(AddQuestion);
    }

    // Function to add a question to the list
    public void AddQuestion()
    {
        string qText = inputQuestionField.text;  // Get text from question input field
        string aText = inputAnswerField.text;    // Get text from answer input field

        // Validate input (make sure fields are not empty)
        if (string.IsNullOrEmpty(qText) || string.IsNullOrEmpty(aText))
        {
            Debug.Log("Question or Answer field is empty.");
            return;
        }

        // Add the question to the list
        questions.Add(new Question(qText, aText));
        Qs_count++;

        // Clear input fields after adding the question
        inputQuestionField.text = "";
        inputAnswerField.text = "";

        // Update question count display
        QsCountText.text = "Questions Added: " + Qs_count;

        // Enable start button if minimum questions are met
        if (Qs_count >= min_Qs)
        {
            startButton.interactable = true;
        }
    }

    // Function to start the game
    public void StartGame()
    {
        gameStarted = true;
        askedQuestions = new bool[Qs_count];  // Reset asked questions tracking
        total_Qs_completed = 0;
        score = 0;
        asked_Qs = 0;

        // Load the first question
        AskQuestion();
    }

    // Function to ask a random question
    private void AskQuestion()
    {
        if (asked_Qs >= Qs_count)
        {
            EndGame();
            return;
        }

        // Randomly pick a question that hasn't been asked yet
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, Qs_count);
        }
        while (askedQuestions[randomIndex]);  // Ensure the question hasn't been asked yet

        // Mark the question as asked
        askedQuestions[randomIndex] = true;
        asked_Qs++;

        // Display the selected question
        Question selectedQuestion = questions[randomIndex];
        questionText.text = selectedQuestion.qText;

        // Randomly generate incorrect answers from other questions
        List<string> answerOptions = new List<string>();
        answerOptions.Add(selectedQuestion.aText);  // Add correct answer

        while (answerOptions.Count < 4)  // Ensure we have 4 options (1 correct + 3 wrong)
        {
            int wrongIndex = Random.Range(0, Qs_count);

            if (wrongIndex != randomIndex && !answerOptions.Contains(questions[wrongIndex].aText))
            {
                answerOptions.Add(questions[wrongIndex].aText);  // Add unique incorrect answers
            }
        }

        // Shuffle the answer options
        for (int i = 0; i < 4; i++)
        {
            int rand = Random.Range(0, answerOptions.Count);
            answerButtons[i].text = answerOptions[rand];  // Assign the text to answer buttons
            answerOptions.RemoveAt(rand);  // Remove from the list after assigning
        }
    }

    // Function to handle answer selection
    public void SelectAnswer(int buttonIndex)
    {
        string selectedAnswer = answerButtons[buttonIndex].text;
        string correctAnswer = questionText.text;  // The correct answer is still displayed in the question text

        total_Qs_completed++;

        // Check if the selected answer is correct
        if (selectedAnswer == correctAnswer)
        {
            score++;
        }

        // Ask the next question
        AskQuestion();
    }

    // Function to end the game and display the end screen
    private void EndGame()
    {
        float percentageScore = (float)score / total_Qs_completed * 100f;
        string resultMessage;

        if (percentageScore >= 75f)
        {
            resultMessage = "BIG BRAIN!";
        }
        else if (percentageScore >= 50f)
        {
            resultMessage = "MEH, JUST MEH";
        }
        else
        {
            resultMessage = "GET A HOLD OF YOURSELF! *cries in failure*";
        }

        // Display the end screen with the final score
        endScreenText.text = "Score: " + score + "/" + total_Qs_completed + "\n" + resultMessage + "\nGAME OVER";
    }
}

