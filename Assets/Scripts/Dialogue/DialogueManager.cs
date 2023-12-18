using System;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour 
{ 
    [SerializeField] private TextMeshProUGUI _textbox;
    [SerializeField] private Image _DialogueBox;
    [SerializeField] private Image _AvatarBox;

    private int _page = 0;
    private int _page_number = 0;
    private List<Page> _text = new List<Page>();
    private List<int> _text_lengt = new List<int>();
    private int _draw_char = 0;
    private int _text_speed = 1;

    private List<Vector2> _textbox_x_offset = new List<Vector2>();
    private List<Vector2> _avater_x_offset = new List<Vector2>();

    private bool _is_space_pressed;
    private bool _is_dialogue_done = true;

    private Vector2 _TextboxPositionMiddle = new Vector2(0 , -59);
    private Vector2 _TextboxPositionLeft = new Vector2(-32 , -59);
    private Vector2 _TextboxPositionRight = new Vector2(28 , -59);

    private Vector2 _AvatarPositionLeft = new Vector2(-99, -59);
    private Vector2 _AvatarPositionRight = new Vector2(96, -59);

    ~DialogueManager() 
    {
        EventBus.StopListening<bool>(EventBusEvents.EventName.SPACE_KEY, OnDialogueSkip);
    }

    public void Awake()
    {
        EventBus.StartListening<bool>(EventBusEvents.EventName.F_KEY, Test);
    }

    public void Update() {
        if (!_is_dialogue_done) {
            UpdateDialogue();
        }
    }

    public void Test(bool is_space_pressed)
    {
        if (is_space_pressed)
        {
            Dialogue d = new Dialogue();
            d.AddDialogue("Tesst je weet wel blood for the blood god", "Vin", DialoguePosition.MIDDLE);
            d.AddDialogue("Dit is pagina ttwee je weet wel tesessting", "Vin", DialoguePosition.RIGHT);
            LoadDialogue(d);
            _is_dialogue_done = false;
        }
    }

    public void LoadDialogue(Dialogue dialogue)
    {
        EventBus.StartListening<bool>(EventBusEvents.EventName.SPACE_KEY, OnDialogueSkip);

        _text = dialogue.GetDialogue();

        Debug.Log("Leading dialogue");

        _page = 0;

        _is_dialogue_done = false;

        InitializeDialogueParameters(dialogue);
    }

    private void InitializeDialogueParameters(Dialogue dialogue)
    {
        _page_number = dialogue.GetDialogue().Count;
        _textbox_x_offset.Clear();
        _avater_x_offset.Clear();

        for (int i = 0; i < _page_number; i++)
        {
            _text_lengt.Add(_text[i].GetSentence().Length);

            UpdateTextboxAndAvatarPositions(_text[i]);
        }
    }

    private void UpdateTextboxAndAvatarPositions(Page page)
    {
        switch (page.GetPosition())
        {
            case DialoguePosition.LEFT:
                _textbox_x_offset.Add(_TextboxPositionLeft);
                _avater_x_offset.Add(_AvatarPositionRight);
                break;

            case DialoguePosition.MIDDLE:
                _textbox_x_offset.Add(_TextboxPositionMiddle);
                _avater_x_offset.Add(new Vector2(0, -56));
                break;

            case DialoguePosition.RIGHT:
                _textbox_x_offset.Add(_TextboxPositionRight);
                _avater_x_offset.Add(_AvatarPositionLeft);
                break;
        }
    }

    public void UpdateDialogue()
    {
        _DialogueBox.rectTransform.anchoredPosition = _textbox_x_offset[_page];
        _AvatarBox.rectTransform.anchoredPosition = _avater_x_offset[_page];

        _draw_char += _text_speed;
        _draw_char = Math.Clamp(_draw_char, 0, _text_lengt[_page]);

        if (_is_space_pressed)
        {
            if (_draw_char >= _text_lengt[_page])
            {
                if (_page < _page_number - 1)
                {
                    _page++;
                    _draw_char = 0;
                }
                else {
                    _is_dialogue_done = true;
                }
            }
            else 
            {
                _draw_char = _text_lengt[_page];
            }

            _is_space_pressed = false;
        }

        string _draw_text = _text[_page].GetSentence().Substring(0, _draw_char);
        _textbox.text = _draw_text;
    }  

    public void OnDialogueSkip(bool is_space_pressed) {
        _is_space_pressed = is_space_pressed;
    }
}