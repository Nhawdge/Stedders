namespace Stedders.Components
{
    internal class Plant : Component
    {
        public Plant(string name)
        {
            Name = name;
        }
        public string Name = string.Empty;
        public int GrowthStage = 0;
        public int MaxGrowthStage = 5;
        public float GrowthRequired = 30f; //30 seconds
        public float Growth = 0f;
        public float GrowthRate = 1f; //1 per second
        public float DawnGrowthModifier = 0.5f;
        public float DayGrowthModifier = 1f;
        public float DuskGrowthModifier = 0.5f;
        public float NightGrowthModifier = 0.25f;

        public float AvailableWater = 0f;
        public float AvailableWaterMax = 100f;
        public float WaterConsumptionRate = 1f; //1 per second

        public float Water = 0f;
        public float MaxWater = 100f;

        public float PlantBody = 10f;
        public float MaxPlantBody = 100f;


    }
}
