using System.ComponentModel.Design;

namespace ArtifactExpCalculator
{
    internal class Program
    {
        const int ThreeStarExp = 1260;
        const int FourStarExp = 2520;
        const int FiveStarExp = 3780;
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            double sum = 0;
            for (int i = 0; i < 10000; i++)
            {
                var exp = CalculateArtifactExp();
                var difference = CalculateDifference(exp[0], exp[1]);
                sum += difference; 
            }
            Console.WriteLine($"Difference: {sum / 10000}.");
        }
        static int[] CalculateArtifactExp() //for 1 day
        {
            var artifacts = new int[3];
            for (int i = 0; i < 9; i++) // How many times you do domain.
            {
                var loot = LootArtifacts();
                artifacts[0] += loot[0];
                artifacts[1] += loot[1];
                artifacts[2] += loot[2];
            }
            var expGot = new int[2];
            for (int i = 0; i < 10000; i++)
            {
                int bulkExp = Upgrade(artifacts, 15);
                int unitExp = Upgrade(artifacts, 1);
                expGot[0] += bulkExp;
                expGot[1] += unitExp;
            }
            //Console.WriteLine($"Total exp from bulk {expGot[0]}.");
            //Console.WriteLine($"Total exp from unit {expGot[1]}.");
            return expGot;
        }
        static double CalculateDifference(int value1, int value2)
        {
            int bigger;
            int smaller;
            if (value1 > value2)
            {
                bigger = value1;
                smaller = value2;
            }
            else 
            { 
                bigger = value2;
                smaller = value1; 
            }
            double result = (bigger - smaller) / (smaller / 100);
            return result;
        }
        static int Upgrade(int[] artifacts, int artifactPool)
        {
            int[] usedArtifacts = new int[2];
            int expPool = 0;
            int expTotal = 0;
            while (artifacts[0] != usedArtifacts[0]
                && artifacts[1] != usedArtifacts[1])
            {
                expPool = 0;
                for (int i = 0;i < artifactPool; i++)
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
                    else break;
                }
                expTotal += expPool * CalculateBonus();
            }
            return expTotal;
        }
        static int CalculateBonus()
        {
            var rndResult = rnd.Next(0, 100);
            if (rndResult <= 90) return 1;
            else if (rndResult > 90 && rndResult <= 98) return 2;
            else if (rndResult == 99) return 5;
            else
            {
                Console.WriteLine($"Error in CalculateBonus. Wrong Number{rndResult}. Returned 0.");
                return 0;
            }
        }
        static int[] LootArtifacts() //[0] = 3*,[1] = 4*,[2] = 5*,
        {
            var rnd = new Random();
            int[] result = new int[3] { 3, 2, 1 };
            int rndResult = rnd.Next(0,1000);
            result[0] = rndResult <= 549 ? result[0]++ : result[0];
            rndResult = rnd.Next(0, 1000);
            result[1] = rndResult <= 484 ? result[1]++ : result[1];
            rndResult = rnd.Next(0, 1000);
            result[2] = rndResult <= 64 ? result[2]++ : result[2];
            return result;
        }
    }
}