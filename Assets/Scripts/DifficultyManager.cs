using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class DifficultyManager
    {
        public static int CurrentDifficultyLevel = 1; 
        public static float MaxTime = 300f;
        public static float MovementOpportunityInterval = 5f;

        public static float FlashlightDepletionMultiplier = 1f;
        public static float GeneratorDepletionMultiplier = 1f;
        public static int WindowGhostLevel = 1;
        public static int DoorGhostLevel = 1;
        public static int BathroomGhostLevel = 1;

        private static float[] MaxTimeMatrix = new float[] { 300f, 300f, 300f, 300f, 300f };
        private static float[] MovementOpportunityIntervalMatrix = new float[] { 5f, 4.7f, 4.5f, 4.2f, 4f };

        private static float[] FlashlightDepletionMultiplierMatrix = new float[] { 1f, 1f, 1.2f, 1.2f, 1.5f };
        private static float[] GeneratorDepletionMultiplierMatrix = new float[] { 1f, 1f, 1.2f, 1.2f, 1.5f };
        private static int[] WindowGhostLevelMatrix = new int[] { 1, 5, 8, 13, 15 };
        private static int[] DoorGhostLevelMatrix = new int[] { 1, 5, 8, 13, 15 };
        private static int[] BathroomGhostLevelMatrix = new int[] { 1, 5, 8, 13, 15 };

        public static void SetDifficultyLevel(int level)
        {
            level = Math.Clamp(level, 1, 5);
            CurrentDifficultyLevel = level; 
            MaxTime = MaxTimeMatrix[level - 1];
            MovementOpportunityInterval = MovementOpportunityIntervalMatrix[level - 1];

            FlashlightDepletionMultiplier = FlashlightDepletionMultiplierMatrix[level - 1];
            GeneratorDepletionMultiplier = GeneratorDepletionMultiplierMatrix[level - 1];
            WindowGhostLevel = WindowGhostLevelMatrix[level - 1];
            DoorGhostLevel = DoorGhostLevelMatrix[level - 1];
            BathroomGhostLevel = BathroomGhostLevelMatrix[level - 1];
        }
    }
}
