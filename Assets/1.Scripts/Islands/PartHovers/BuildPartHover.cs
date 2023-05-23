namespace _1.Scripts.Islands.PartHovers
{
    public class BuildPartHover : IPartHover
    {
        private readonly IslandPart _islandPart;
        
        
        
        public BuildPartHover(IslandPart islandPart)
        {
            this._islandPart = islandPart;
        }
        
        
    
        public bool CanHover()
        {
            return this._islandPart.IsPlayer;
        }
    }
}