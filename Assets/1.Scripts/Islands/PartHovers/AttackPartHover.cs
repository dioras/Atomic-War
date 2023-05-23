namespace _1.Scripts.Islands.PartHovers
{
    public class AttackPartHover : IPartHover
    {
        private readonly IslandPart _islandPart;
        
        
        
        public AttackPartHover(IslandPart islandPart)
        {
            this._islandPart = islandPart;
        }
        
        
    
        public bool CanHover()
        {
            return !this._islandPart.IsPlayer;
        }
    }
}