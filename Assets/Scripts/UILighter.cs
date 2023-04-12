using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UILighter : MonoBehaviour
{
    [SerializeField] private Slider comboSlider;
    private AudioLighter _audio;
    private bool _finishGame = false;

    private void Start()
    {
        comboSlider.value= 0;
        _audio = FindObjectOfType<AudioLighter>();
    }

    private void FinishGame()
    {
        if (comboSlider.value == 1 && !_finishGame)
        {
            _audio.PlayFinishSound();
            _finishGame= true;
            SceneManager.LoadScene(2);
        }
    }

    public void AddPointsSlider(float point)
    {
        comboSlider.value += point;
        FinishGame();
    }



}
