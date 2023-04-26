using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float _lerpSpeed = .1f;
    bool _isLerping;
    private void OnEnable()
    {
        BallController.onTouchedPlatform += ChangeCameraPosition;
        Application.targetFrameRate = 60;
    }
    private void OnDisable()
    {
        BallController.onTouchedPlatform -= ChangeCameraPosition;
    }
    Vector3 pos;
    private void Update()
    {
        if(_isLerping)
        {
            pos = transform.position;
            pos.y -= .1f;
            _isLerping = false;
        }       
        if(transform.position.y != pos.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, _lerpSpeed * Time.deltaTime);
        }
    }
    private void ChangeCameraPosition()
    {
        _isLerping = true;  
    }
}
