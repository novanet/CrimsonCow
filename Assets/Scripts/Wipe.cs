using System.Collections;
using UnityEngine;

public class Wipe : MonoBehaviour
{
    private Rect _rect;
    
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
        _rect = _camera.rect;
    }

    void Update()
    {
        if (_wipeMode != WipeMode.None)
        StartCoroutine(WaitABitThenSwitch());

//        if (_rect.width >= 1.0f || _rect.width <= 0.0f)
//            _wipeMode = WipeMode.None;
//        
//        if (_wipeMode == WipeMode.GoFull)
//            GoTowardsFull();
//        else if (_wipeMode == WipeMode.GoAway)
//            GoTowardsNone();
    }

    private IEnumerator WaitABitThenSwitch()
    {
        yield return new WaitForSeconds(2);
        
        if (_wipeMode == WipeMode.GoFull)
            GoTowardsFull();
        else
            GoTowardsAway();
    }

    private void GoTowardsFull()
    {
        _camera.rect = new Rect(0, 0, Screen.width, Screen.height);
//        
//        _rect.x = _rect.x <= 0 ? 0 : _rect.x - _changePerFrame;
//        _rect.width += _changePerFrame; 
//        _camera.rect = _rect;
    }

    private void GoTowardsAway()
    {
        _camera.enabled = false;

//        _rect.x = _rect.x <= 0 ? 0 : _rect.x + _changePerFrame;
//        _rect.width -= _changePerFrame;
//        _camera.rect = _rect;
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
