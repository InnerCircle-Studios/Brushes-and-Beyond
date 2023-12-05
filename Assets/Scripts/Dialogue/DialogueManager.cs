using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private int _textbox_width = 200;
    private int _textbox_height = 64;
    private int _border = 8;
    private int _line_sep = 12;
    private int _line_width;
    private int _textbox_spt_w;
    private int _textbox_spr_h;

    private int _page = 0;
    private int _page_number = 0;
    private List<Page> _text;
    private List<int> _text_lengt;
    private int _draw_char = 0;
    private int _text_speed = 1;

    private List<Vector2> _textbox_x_offset;

    private bool _is_space_pressed;
    private bool _is_dialogue_done;
    [SerializeField]TextMeshProUGUI _textbox;


    ~DialogueManager()
    {
        EventBus.StopListening<bool>(EventBusEvents.EventName.SPACE_KEY, OnDialogueSkip);
    }

    public void Awake()
    {
    }

    public void Update()
    {
        if (!_is_dialogue_done)
        {
            UpdateDialogue();
        }
    }

    public void LoadDialogue(Dialogue dialogue)
    {
        EventBus.StartListening<bool>(EventBusEvents.EventName.SPACE_KEY, OnDialogueSkip);

        _text = dialogue.GetDialogue();

        _page = 0;

        _is_dialogue_done = false;

        _page_number = dialogue.GetDialogue().Count;
        _textbox_x_offset.Clear();

        for (int i = 0; i < _page_number; i++)
        {
            _text_lengt[i] = _text[i].GetSentence().Length;

            switch(_text[i].GetPosition())
            {
                case DialoguePosition.LEFT:
                    _textbox_x_offset.Add(new Vector2());
                    break;

                case DialoguePosition.MIDDLE:
                    _textbox_x_offset.Add(new Vector2());
                    break;

                case DialoguePosition.RIGHT:
                    _textbox_x_offset.Add(new Vector2());
                    break;
            }
        }
    }

    public void UpdateDialogue()
    {
        if (_draw_char < _text_lengt[_page])
        {
            _draw_char += _text_speed;
            _draw_char = Math.Clamp(_draw_char, 0, _text_lengt[_page]);            
        }

        if (_is_space_pressed)
        {
            if (_draw_char == _text_lengt[_page])
            {
                if (_page < _page_number-1)
                {
                    _page++;
                    _draw_char = 0;
                }
                else
                {
                    _is_dialogue_done = true;
                }
            }
            else
            {
                _draw_char = _text_lengt[_page];
            }
        }

        string _draw_text = _text[_page].GetSentence().Substring(0, _draw_char);
        _textbox.text = _draw_text;
    }

    public void OnDialogueSkip(bool is_space_pressed)
    {
        _is_space_pressed = is_space_pressed;
    }
}