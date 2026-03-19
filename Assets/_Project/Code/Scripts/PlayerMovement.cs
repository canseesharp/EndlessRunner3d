using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private AnimationCurve _yAnimation;
    [SerializeField] private float _shiftSpeed;
    [SerializeField] private float _fallSpeed;

    private readonly Dictionary<Line, float> _linesPosition = new();
    private CharacterController _characterController;
    private Line _line = Line.Middle;
    private bool _isJumping = false;

    public bool TryShiftLeft()
    {
        if (_line == Line.Left)
        {
            return false;
        }

        _line = _line == Line.Middle ? Line.Left : Line.Middle;
        return true;
    }

    public bool TryShiftRight()
    {
        if (_line == Line.Right)
        {
            return false;
        }

        _line = _line == Line.Middle ? Line.Right : Line.Middle;
        return true;
    }

    public bool TryJump(Transform jumper, float duration, float height)
    {
        if (_characterController.isGrounded == true && _isJumping == false)
        {
            StartCoroutine(AnimationByTime(jumper, duration, height));
            return true;
        }

        return false;
    }

    private IEnumerator AnimationByTime(Transform jumper, float duration, float height)
    {
        _isJumping = true;
        float elapsedSeconds = 0;
        float progress = 0;

        Vector3 startPosition = jumper.position;

        while (progress < 1)
        {
            elapsedSeconds += Time.deltaTime;
            progress = elapsedSeconds / duration;
            jumper.position = startPosition + new Vector3(0, _yAnimation.Evaluate(progress) * height, 0);

            yield return null;
        }
        _isJumping = false;
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _linesPosition[Line.Left] = -3f;
        _linesPosition[Line.Middle] = 0f;
        _linesPosition[Line.Right] = 3f;
    }

    private void Update()
    {
        float x = Mathf.Lerp(transform.position.x, _linesPosition[_line], _shiftSpeed * Time.deltaTime);
        _characterController.Move(Vector3.right * (x - transform.position.x));
        if (_isJumping == false)
        {
            _characterController.Move(Vector3.down * (_fallSpeed * Time.deltaTime));
        }
    }
}
