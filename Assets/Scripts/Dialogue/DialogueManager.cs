using System;

using TMPro;

using UnityEngine;

public class DialogueManager : MonoBehaviour {
    private int _textbox_width = 200;
    private int _textbox_height = 64;
    private int _border = 8;
    private int _line_sep = 12;
    private int _line_width;
    [SerializeField] private Texture2D _textbox_spr;
    private int _textbox_spt_w;
    private int _textbox_spr_h;

    private int _page = 0;
    private int _page_number = 0;
    private string[] _text = new string[3];
    private int[] _text_lengt;
    private int _draw_char = 0;
    private int _text_speed = 1;

    private bool _setup = false;

    private Canvas _canvas;

    private float _textbox_x;
    private float _textbox_y;
    private int[] _textbox_x_offset;

    private bool _is_space_pressed;

    [SerializeField]private GameObject textbox;
    [SerializeField]private TextMeshProUGUI textMeshPro;


    ~DialogueManager() {
        EventBus.StopListening<bool>(EventBusEvents.EventName.SPACE_KEY, OnDialogueSkip);
    }

    public void Awake() {
        EventBus.StartListening<bool>(EventBusEvents.EventName.SPACE_KEY, OnDialogueSkip);

        textMeshPro.margin = new Vector4(8, 8, 8, 8);

        _text[0] = "Hallo dit is een test";
        _text[1] = "Als dit werkt zal het wel cool zijn";
        _text[2] = "maar het gaat prob niet werken";

        _line_width = _textbox_width - (_border * 2);
        _textbox_x_offset = new int[_text.Length];
        _text_lengt = new int[3]; 

        if (!_setup) {
            _page_number = _text.Length;

            for (int i = 0; i < _page_number; i++) {
                _text_lengt[i] = _text[i].Length;

                _textbox_x_offset[i] = 120;
            }
        }

        textbox.transform.Translate(new Vector2(_textbox_x_offset[_page], 0));
    }

    public void Update() {
        if (_draw_char < _text_lengt[_page]) {
            _draw_char += _text_speed;
            _draw_char = Math.Clamp(_draw_char, 0, _text_lengt[_page]);
        }

        if (_is_space_pressed) {
            EventBus.TriggerEvent(EventBusEvents.EventName.SPACE_KEY, false);
            if (_draw_char == _text_lengt[_page]) {
                if (_page < _page_number - 1) {
                    _page++;
                    _draw_char = 0;
                }
                else {
                    //Stop Displaying text
                }
            }
            else {
                _draw_char = _text_lengt[_page];
            }
        }

        _textbox_spr_h = _textbox_spr.height;
        _textbox_spt_w = _textbox_spr.width;

        Graphics.DrawTexture(new Rect(new Vector2(_textbox_x + _textbox_x_offset[_page], _textbox_y), new Vector2(_textbox_spt_w, _textbox_spr_h)), _textbox_spr);
    }

    public void OnGUI() {
        string _draw_text = _text[_page].Substring(0, _draw_char);
        textMeshPro.text = _draw_text;
    }

    public void OnDialogueSkip(bool is_space_pressed) {
        _is_space_pressed = is_space_pressed;
    }
}