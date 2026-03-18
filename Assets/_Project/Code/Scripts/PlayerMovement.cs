using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _shiftSpeed;
    [SerializeField] private float _fallSpeed;

    private Line _line = Line.Middle;
    private CharacterController _characterController;
    private readonly Dictionary<Line, float> _linesPosition = new();

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
        _characterController.Move(Vector3.down * (_fallSpeed * Time.deltaTime));
    }
}
