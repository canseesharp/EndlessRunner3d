using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private float _fallSpeed;

    [Header("Shift")]
    [SerializeField] private float _shiftDuration;
    [SerializeField] private AnimationCurve _shiftCurve;
    [SerializeField] private float _shiftBuffer;

    [Header("Jump")]
    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _jumpBuffer;
    [SerializeField] private float _coyoteTime;

    [Header("Slide")]
    [SerializeField] private float _slideDuration;
    [SerializeField] private float _slideBuffer;

    public Line Line { get; private set; }

    public float FallSpeed => _fallSpeed;

    public float ShiftDuration => _shiftDuration;

    public AnimationCurve ShiftCurve => _shiftCurve;
    
    public float ShiftBuffer => _shiftBuffer;

    public AnimationCurve JumpCurve => _jumpCurve;

    public float JumpDuration => _jumpDuration;

    public float JumpHeight => _jumpHeight;

    public float JumpBuffer => _jumpBuffer;

    public float CoyoteTime => _coyoteTime;

    public float SlideDuration => _slideDuration;

    public float SlideBuffer => _slideBuffer;

    public bool TryShiftLeft()
    {
        if (Line != Line.Left)
        {
            Line = Line == Line.Middle ? Line.Left : Line.Middle;
            return true;
        }

        return false;
    }

    public bool TryShiftRight()
    {
        if (Line != Line.Right)
        {
            Line = Line == Line.Middle ? Line.Right : Line.Middle;
            return true;
        }

        return false;
    }

    public void Init()
    {
        Line = Line.Middle;
    }
}
