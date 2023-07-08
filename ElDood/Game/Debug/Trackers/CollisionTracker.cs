using ElDood.Game.Entities;

namespace ElDood.Game.Debug.Trackers;

public class CollisionTracker : IDebugInfo {
    private readonly ICollidable _one;
    private readonly ICollidable _two;
    public CollisionTracker(ICollidable one, ICollidable two) {
        _one = one; 
        _two = two;
    }

    public string InfoText => $"Collision: {_one.Collides(_two)}";
}
