using UnityEngine;

public class Wipe : MonoBehaviour
{
    private enum WipeMode
    {
        None,
        GoFull,
        GoAway
    }
    
    private Camera _camera;
    private WipeMode _wipeMode;
    private float _changePerFrame = 0.005f;

    void Start()
    {
        _wipeMode = WipeMode.None;
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (_camera.rect.width >= 1.0f || _camera.rect.width <= 0.0f)
            _wipeMode = WipeMode.None;
        
        if (_wipeMode == WipeMode.GoFull)
            GoTowardsFull();
        else if (_wipeMode == WipeMode.GoAway)
            GoTowardsNone();
    }

    private void GoTowardsFull()
    {
        var rect = _camera.rect;
        _camera.rect = new Rect(
            rect.x <= 0 ? 0 : rect.x - _changePerFrame, 
            rect.y, 
            rect.width + _changePerFrame, 
            rect.height);
    }

    private void GoTowardsNone()
    {
        var rect = _camera.rect;
        _camera.rect = new Rect(
            rect.x <= 0 ? 0 : rect.x + _changePerFrame, 
            rect.y, 
            rect.width - _changePerFrame, 
            rect.height);
    }

    public void GoFullScreen()
    {
        _wipeMode = WipeMode.GoFull;
    }

    public void GoAway()
    {
        _wipeMode = WipeMode.GoAway;
    }
}
