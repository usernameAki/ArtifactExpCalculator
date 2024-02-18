using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtifactExpCalculator
{
    public class XPCalc
    {
        Random rnd = new Random();
        const int ThreeStarExp = 1260;
        const int FourStarExp = 2520;
        public int StartSimulation(int[] artifacts, int artifactPool)
        {
            var result = Upgrade(artifacts, artifactPool);
            return result;
        }
        public int[] FarmDomainForArtifacts(int resin)
        {
            int iterations = resin / 20;
            int[] result = new int[2] { 3* iterations, 2*iterations };
            int rndResult;
            for (int i = 0; i < iterations; i++)
            {
                rndResult = rnd.Next(0, 1000);
                result[0] = rndResult <= 549 ? result[0] + 1 : result[0];
                rndResult = rnd.Next(0, 1000);
                result[1] = rndResult <= 484 ? result[1] + 1 : result[1];
            }
            return result;
        }
        /// <returns>Returns Total XP from Upgrade process.</returns>
        public int Upgrade(int[] artifacts, int artifactPool)
        {
            int[] usedArtifacts = new int[2] {0,0};
            int expPool;
            int expTotal = 0;
            while (artifacts[0] != usedArtifacts[0]
                && artifacts[1] != usedArtifacts[1])
            {
                expPool = 0;
                for (int i = 0; i < artifactPool; i++)
                {
                    if (artifacts[1] != usedArtifacts[1])
                    {
                        expPool += FourStarExp;
                        usedArtifacts[1]++;
                    }
                    else if (artifacts[0] != usedArtifacts[0])
                    {
                        expPool += ThreeStarExp;
                        usedArtifacts[0]++;
                    }
                    else
                    {
                        expPool = 0;
                        break;
                    }
                }
                expTotal += expPool * BonusXP();
            }
            return expTotal;
        }
        private int BonusXP()
        {
            var rndResult = rnd.Next(0, 100);
            if (rndResult < 90) return 1;
            else if (rndResult >= 90 && rndResult <= 98) return 2;
            else if (rndResult == 99) return 5;
            else
            {
                Console.WriteLine($"Error in CalculateBonus. Wrong Number{rndResult}. Returned 0.");
                return 0;
            }
        }
    }
}
