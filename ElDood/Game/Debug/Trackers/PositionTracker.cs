using ElDood.Game.Entities;

namespace ElDood.Game.Debug.Trackers;

public class PositionTracker : IDebugInfo {
    private readonly IPlaceable _target;
    private readonly string _displayName;

    public PositionTracker(IPlaceable target, string displayName) {
        _target = target;
        _displayName = displayName;
    }

    public string InfoText => $"{_displayName} - {_target.Position.X}, {_target.Position.Y}";
}
