namespace Pickups
{
    public class StarPickup : Pickup
    {
        private LevelManager _level;

        protected override void Start()
        {
            base.Start();
            _level = FindObjectOfType<LevelManager>();
        }
        
        public override void OnPickup()
        {
            _level.LoadNextLevel();
        }

        public override void OnTouch()
        {
            //_level.LoadNextLevel();
        }
    }
}