using ElDood.Game.Entities;

namespace ElDood.Game.Debug;

public class PositionTracker : IDebugInfo {
    private readonly IPlaceable _target;

    public PositionTracker(IPlaceable target) {
        _target = target;
    }

    public string InfoText => $"{_target.Position.X}, {_target.Position.Y}";
}
