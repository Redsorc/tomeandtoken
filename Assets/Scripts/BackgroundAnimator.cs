// Assets/Scripts/BackgroundAnimator.cs
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class BackgroundAnimator : MonoBehaviour
{
    public Vector2 scrollSpeed = new Vector2(0.02f, 0); // change X and/or Y speed
    private RawImage _raw;
    private Rect _uvRect;

    void Awake()
    {
        _raw = GetComponent<RawImage>();
        _uvRect = _raw.uvRect;
    }

    void Update()
    {
        // increment UV offset
        _uvRect.x += scrollSpeed.x * Time.deltaTime;
        _uvRect.y += scrollSpeed.y * Time.deltaTime;
        _raw.uvRect = _uvRect;
    }
}
