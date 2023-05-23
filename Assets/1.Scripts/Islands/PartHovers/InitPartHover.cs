using _1.Scripts.Buildings;

namespace _1.Scripts.Islands.PartHovers
{
    public class InitPartHover : IPartHover
    {
        private readonly IslandPart _islandPart;
        private readonly PreviewBuildBuildings _previewBuildBuildings;

        
        
        public InitPartHover(IslandPart islandPart, PreviewBuildBuildings previewBuildBuildings)
        {
            this._islandPart = islandPart;
            this._previewBuildBuildings = previewBuildBuildings;
        }

        
        
        public bool CanHover()
        {
            return this._previewBuildBuildings.IsBuilding && this._islandPart.IsPlayer;
        }
    }
}