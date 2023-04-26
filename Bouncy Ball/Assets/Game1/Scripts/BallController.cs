using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    public static event Action onTouchedPlatform = delegate { };
    public static event Action<GameObject> onChangedPosition = delegate { };

    [SerializeField] float speed = 200f;
    [SerializeField] VisualEffect _smokePoof = new VisualEffect();

    Rigidbody _rigidbody;
    Renderer _ballRenderer;

    Color _colorGreen, _colorYellow, _colorRed;
    List<Color> colors = new List<Color>();

    int _baseColorHash;
    float _yAxis;
    float _ballGoMinHeight = 0f;
    float _ballGoMaxHeight = 2f;

    List<GameObject> _brokenSphereParts = new List<GameObject>();

    private void OnEnable()
    {
        onTouchedPlatform += ChangeBallColor;
    }
    private void OnDisable()
    {
        onTouchedPlatform -= ChangeBallColor;
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
        _yAxis = _smokePoof.GetFloat("yAxis");        

        _rigidbody = GetComponent<Rigidbody>();
        _ballRenderer = transform.GetChild(0).GetComponent<Renderer>();

        _ballRenderer.sharedMaterial.SetColor(_baseColorHash, _colorGreen);

        foreach (Transform item in transform.GetChild(1).transform)
        {
            _brokenSphereParts.Add(item.gameObject);
        }
    }
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _ballGoMinHeight, _ballGoMaxHeight), transform.position.z);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            if (collision.gameObject.GetComponent<Renderer>().sharedMaterial.GetColor(_baseColorHash) == _ballRenderer.sharedMaterial.GetColor(_baseColorHash) && !UIUpdate.isEnded)
            {
                _rigidbody.AddForce(Vector3.up * speed);
                _ballGoMinHeight -= .1f;
                _ballGoMaxHeight -= .1f;
                _smokePoof.gameObject.SetActive(true);
                _smokePoof.Play();
                _smokePoof.SetFloat("yAxis", _yAxis - .1f);
                onTouchedPlatform.Invoke();
                onChangedPosition.Invoke(collision.transform.parent.gameObject);
                _yAxis = _smokePoof.GetFloat("yAxis");
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(true);
                foreach (var item in _brokenSphereParts)
                {
                    item.GetComponent<Rigidbody>().AddExplosionForce(100f, transform.GetChild(1).transform.position, 5f);
                    item.GetComponent<Collider>().isTrigger = true;
                }
                UIUpdate.isEnded = true;
            }
        }
    }
    private void ChangeBallColor()
    {
        _ballRenderer.sharedMaterial.SetColor(_baseColorHash, colors[UnityEngine.Random.Range(0, colors.Count)]);
    }
}
