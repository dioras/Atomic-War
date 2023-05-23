using _1.Scripts.GameEvents.Events;

namespace _1.Scripts.GameEvents
{
    public static class EventRepository
    {
        public static BuildingBuiltEvent BuildingBuilt;
        public static StartInventoryEmptyEvent StartInventoryEmpty;
        public static GameProcessStateChangedEvent GameProcessStateChanged;
        public static OnWeaponReloadedEvent OnWeaponReloaded;
        public static BuildingDestroyedEvent BuildingDestroyed;
        public static RocketStarted RocketStarted;
        public static RocketFinished RocketFinished;
        public static FlagChangedEvent FlagChanged;



        static EventRepository()
        {
            BuildingBuilt = new BuildingBuiltEvent();
            StartInventoryEmpty = new StartInventoryEmptyEvent();
            GameProcessStateChanged = new GameProcessStateChangedEvent();
            OnWeaponReloaded = new OnWeaponReloadedEvent();
            BuildingDestroyed = new BuildingDestroyedEvent();
            RocketStarted = new RocketStarted();
            RocketFinished = new RocketFinished();
            FlagChanged = new FlagChangedEvent();
        }
    }
}