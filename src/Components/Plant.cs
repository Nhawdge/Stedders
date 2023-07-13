﻿namespace Stedders.Components
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
    }
}