using UnityEngine;
using UnityEngine.UI;

public class SlideInWinnerText : MonoBehaviour
{
    private bool _isGameFinished;

    private Color[] _winnerTextColourChangeOverTime;
    private int _currentColourChangeIndex = 5;
    private float _secondsPerSingleColourChange = 0.083f;
    private float _lastSingleColourChange;
    
    private float _textPosyChangePerFrame = 4f;
    
    private RectTransform _rectTransform;
    private Text _text;
    private AudioSource _cheerSound;

    void Start()
    {
        _text = GetComponent<Text>();
        _rectTransform = GetComponent<RectTransform>();
        _cheerSound = GetComponent<AudioSource>();
        
        _text.color = new Color(0, 1, 0);
        var colourChangePerFrame = 1f / (60 * _secondsPerSingleColourChange);
        _winnerTextColourChangeOverTime = new[]
        {
            new Color(colourChangePerFrame, 0, 0),
            new Color(0, -colourChangePerFrame, 0),
            new Color(0, 0, colourChangePerFrame),
            new Color(-colourChangePerFrame, 0, 0),
            new Color(0, colourChangePerFrame, 0),
            new Color(0, 0, -colourChangePerFrame),
        };

        PutTextAboveScreen();
    }

    private void PutTextAboveScreen()
    {
        _rectTransform.anchoredPosition3D = new Vector3(0, Screen.height / 2 + 100, 0);
    }

    public void FixedUpdate()
    {
        if (_isGameFinished)
        {
            NextColour();

            if (_rectTransform.anchoredPosition3D.y > 50)
                _rectTransform.anchoredPosition3D = new Vector3(_rectTransform.anchoredPosition3D.x, _rectTransform.anchoredPosition3D.y - _textPosyChangePerFrame, _rectTransform.anchoredPosition3D.z);            
        }
    }

    private void NextColour()
    {
        if (Time.time - _secondsPerSingleColourChange > _lastSingleColourChange)
        {
            _currentColourChangeIndex = (_currentColourChangeIndex + 1) % _winnerTextColourChangeOverTime.Length;
            _lastSingleColourChange = Time.time;
        }
        _text.color += _winnerTextColourChangeOverTime[_currentColourChangeIndex];
    }

    public void SetWinner(int playerNumber)
    {
        _isGameFinished = true;
        _text.text = $"PLAYER {playerNumber} WINS!";
        _cheerSound.Play();
    }
}
