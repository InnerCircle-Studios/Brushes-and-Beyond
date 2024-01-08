using System;

using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Player _Player;
    public int _numOfHearts;

    public Image[] _hearts;
    public Sprite _fullHeart;
    public Sprite _halfHeart;
    public Sprite _emptyHearth;

    void Update()
    {
        double Health = _Player.GetAttrubuteManager().GetAttributes().CurrentHealth;

        for (int i = 0; i < _hearts.Length; i++)
        {
            if (i < Health / 2)
            {
                _hearts[i].sprite = _fullHeart;
            }
            else
            {
                _hearts[i].sprite = _emptyHearth;
            }
        }
    }
}