using System.Collections.Generic;

public class Dialogue
{
    public void AddDialogue(string page, string speaker, DialoguePosition position)
    {
        _pages.Add(new Page(page, speaker, position));
    }

    public List<Page> GetDialogue()
    {
        return _pages;
    }

    private List<Page> _pages;
}

public class Page
{
    public Page(string sentence, string speaker, DialoguePosition position)
    {
        _sentence = sentence;
        _speaker = speaker;
        _position = position;
    }

    public string GetSpeaker()
    {
        return _speaker;
    }

    public string GetSentence()
    {
        return _sentence;
    }

    public DialoguePosition GetPosition()
    {
        return _position;
    }
    private string _speaker;
    private string _sentence;
    private DialoguePosition _position;
}

public enum DialoguePosition
{
    LEFT,
    MIDDLE,
    RIGHT,
}