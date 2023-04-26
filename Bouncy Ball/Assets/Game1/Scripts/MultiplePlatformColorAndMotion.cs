using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplePlatformColorAndMotion : MonoBehaviour
{
    public GameObject[] platforms;

    Color _colorGreen, _colorYellow, _colorRed;
    List<Color> colors = new List<Color>();

    public float speed = 1f;

    Renderer platformRenderer;
    int _baseColorHash;
    Vector3 _lastPos;
    public Quaternion _lastRot;

    private void OnEnable()
    {
        BallController.onTouchedPlatform += ChangeColorAndRandom;
        BallController.onChangedPosition += ChangePosition;
    }
    private void OnDisable()
    {
        BallController.onTouchedPlatform -= ChangeColorAndRandom;
        BallController.onChangedPosition -= ChangePosition;
    }
    private void Awake()
    {
        _colorGreen = Color.green;
        _colorYellow = Color.yellow;
        _colorRed = Color.red;

        colors.Add(_colorGreen);
        colors.Add(_colorYellow);
        colors.Add(_colorRed);

        _baseColorHash = Shader.PropertyToID("_BaseColor");

        platformRenderer = platforms[0].transform.GetChild(0).GetComponent<Renderer>();
        platformRenderer.sharedMaterial.SetColor(_baseColorHash, _colorGreen);

        _lastPos = platforms[platforms.Length - 1].transform.position;
    }
    Color _randomColor = new Color();
    Color _tempColor = new Color();
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !UIUpdate.isEnded)
        {
            //ChangeColorAndRandom();
            ChangeColor();
        }
        if (Input.touchCount > 0)
        {
            if(Input.touchCount==1)
            {
                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Ended && !UIUpdate.isEnded)
                {
                    ChangeColor();
                }
            }
        }
        foreach (var item in platforms)
        {
            item.transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }
        if(UIUpdate.isEnded)
        {
            //MoveThemAround();
        }
    }
    private void ChangeColorAndRandom()
    {
        while (true)
        {
            _tempColor = colors[Random.Range(0, colors.Count)];
            if (_randomColor != _tempColor)
            {
                _randomColor = _tempColor;
                platformRenderer.sharedMaterial.SetColor(_baseColorHash, _randomColor);
                speed = -speed;
                break;
            }
        }
    }
    private void ChangeColor()
    {
        while (true)
        {
            _tempColor = colors[Random.Range(0, colors.Count)];
            if (_randomColor != _tempColor)
            {
                _randomColor = _tempColor;
                platformRenderer.sharedMaterial.SetColor(_baseColorHash, _randomColor);
                break;
            }
        }
    }
    float positionMultiplier = -1f;
    private void ChangePosition(GameObject _obj)
    {
        _obj.transform.GetComponent<DestructObjects>().DestroyGameObjects();
        StartCoroutine(WaitSomeTime(_obj));

        //_obj.transform.rotation = new Quaternion(0, 0, platforms[platforms.Length - 1].transform.rotation.y - (5*positionMultiplier), 0);
    }
    private void MoveThemAround()
    {
        foreach (var platform in platforms)
        {
            if(platform.GetComponent<Rigidbody>()==null)
                platform.AddComponent<Rigidbody>();

            platform.GetComponent<Collider>().enabled = true;
            platform.GetComponent<Rigidbody>().AddExplosionForce(100f, platforms[0].transform.parent.transform.position, 5f);
        }
    }
    IEnumerator WaitSomeTime(GameObject _obj)
    {
        yield return new WaitForSeconds(2f);
        _obj.transform.GetChild(0).gameObject.SetActive(true);
        _obj.transform.position = _lastPos - new Vector3(0, 0.1f, 0);
        _lastPos = _obj.transform.position;
    }
}
