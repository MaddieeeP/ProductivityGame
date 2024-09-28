[System.Serializable]
public class Question
{
    private string _qText;
    private string _aText;

    public string qText { get { return _qText; } }
    public string aText { get { return _aText; } }

    public Question(string questionText, string answerText)
    {
        _qText = questionText;
        _aText = answerText;
    }
}