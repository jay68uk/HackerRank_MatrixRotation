using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixRotation
{
    class Program
    {
        static void matrixRotation(List<List<int>> matrix, int r)
        { 
            
            int m = matrix.Count();
            int n = matrix[0].Count();
            int ringElements = (2 * m) + (2 * n) - 4;

            List<int> flatMatrix = matrix.SelectMany(x => x).ToList();
            List<int> rotation = new List<int>(m * n);
            
            rotation.AddRange(Enumerable.Repeat(-1, rotation.Capacity));
            int rings = Math.Min(m, n) / 2;
            int rotationFactor;

            for (int ringItem = 0; ringItem < rings; ringItem++)
            {
                rotationFactor = r % ringElements;
                rotation=RotationCalc(m, n, flatMatrix,rotationFactor,ringItem);
                ringElements -= 8;
                flatMatrix.Clear();
                flatMatrix.AddRange(rotation);
            }

            

            string outText = "";
            for (int count = 1; count <= (n*m); count++)
            {
                outText += flatMatrix[count-1] + " ";

                if (count%n==0)
                {
                    Console.WriteLine(outText.Trim());
                    outText = "";
                }
            }

        }

        private static List<int> RotationCalc(int m, int n, List<int> matrixIn, int rotationFactor,int ringItem)
        {
            int mRing, nRing, ringFactorPos, ringFactorNeg;
            int rings = Math.Min(m, n) / 2;

            List<int> rotation = new List<int>(matrixIn);
            List<int> flatMatrix = new List<int>(matrixIn);

            mRing = m - (ringItem * 2);
            nRing = n - (ringItem * 2);
            ringFactorPos = ringItem * (n + 1);
            ringFactorNeg = ringItem * (n - 1);

            for (int i = 0; i < rotationFactor; i++)
            {

                for (int colStart = 0; colStart < mRing - 1; colStart++)
                {
                    rotation[((colStart + 1) * n) + ringFactorPos] = flatMatrix[(colStart * n) + ringFactorPos];
                }

                for (int rowStart = 1; rowStart < nRing; rowStart++)
                {
                    rotation[(rowStart - 1) + ringFactorPos] = flatMatrix[rowStart + ringFactorPos];
                }

                for (int colEnd = 1; colEnd < mRing; colEnd++)
                {
                    rotation[((colEnd * n) - 1) + ringFactorNeg] = flatMatrix[(((colEnd + 1) * n) - 1) + ringFactorNeg];
                }

                for (int rowEnd = 1; rowEnd < nRing; rowEnd++)
                {
                    rotation[(n * (m - 1) + rowEnd) - ringFactorNeg] = flatMatrix[(n * (m - 1) + (rowEnd - 1)) - ringFactorNeg];
                }
                flatMatrix.Clear();
                flatMatrix.AddRange(rotation);
            }

            return rotation;
        }

        static void Main(string[] args)
        {
            List<List<int>> datain = new List<List<int>>();

            datain.AddRange(new List<List<int>>()
                { new List<int>() { 1, 2, 3, 4},
                    new List<int>() { 7, 8,9,10} ,
                    new List<int>() { 13, 14, 15, 16 },
                                        new List<int>() { 19, 20,21, 22} ,
            new List<int>() { 25,26,27,28 }});

            matrixRotation(datain, 7);
            Console.ReadLine();
        }
    }
}
