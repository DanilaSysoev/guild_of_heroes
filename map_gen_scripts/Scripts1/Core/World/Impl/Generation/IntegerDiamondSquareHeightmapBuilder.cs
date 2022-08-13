using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntegerDiamondSquareHeightmapBuilder : MonoBehaviour, IHeightmapBuilder
{
    [SerializeField]
    int minHeight;
    [SerializeField]
    int maxHeight;
    [SerializeField]
    [Min(0)]
    int offset;
    [SerializeField]
    [Min(1)]
    float offsetDivider;

    public IHeightmap BuildHeightMap(int sizeX, int sizeY)
    {
        int size = GetSize(sizeY, sizeX);

        int[,] heights = new int[size, size];
        for (int i = 0; i < size; ++i)
            for (int j = 0; j < size; ++j)
                heights[i, j] = minHeight - 1;

        heights[0, 0] = Random.Range(minHeight, maxHeight);
        heights[0, size - 1] = Random.Range(minHeight, maxHeight);
        heights[size - 1, 0] = Random.Range(minHeight, maxHeight);
        heights[size - 1, size - 1] = Random.Range(minHeight, maxHeight);

        int squareSize = size;
        int offset = this.offset;

        while (squareSize > 2)
        {
            SquareStep(size, heights, squareSize, offset);
            DiamondStep(size, heights, squareSize, offset);

            squareSize = squareSize / 2 + 1;
            offset = (int)(offset / offsetDivider);
        }

        int[,] res = new int[sizeY, sizeX];

        for (int i = 0; i < sizeY; ++i)
            for (int j = 0; j < sizeX; ++j)
                res[i, j] = heights[i, j];
        return new Heightmap(res);
    }

    private void DiamondStep(int size, int[,] heights, int squareSize, int offset)
    {
        int leftOffset = squareSize / 2; //отступ слев для следующей строки

        for (int top = 0; top < size; top += squareSize / 2)
        {
            for (int left = leftOffset; left < size; left += squareSize - 1)
            {
                int sum = 0;
                int cnt = 0;
                if (top > 0) { sum += heights[top - squareSize / 2, left]; cnt++; }
                if (left > 0) { sum += heights[top, left - squareSize / 2]; cnt++; }
                if (top < size - 1) { sum += heights[top + squareSize / 2, left]; cnt++; }
                if (left < size - 1) { sum += heights[top, left + squareSize / 2]; cnt++; }

                heights[top, left] = sum / cnt + Random.Range(-offset, offset + 1);
            }
            leftOffset = (leftOffset + squareSize / 2) % (squareSize - 1);
        }
    }
    private void SquareStep(int size, int[,] heights, int squareSize, int offset)
    {
        for (int top = 0; top < size - 1; top += squareSize - 1)
        {
            for (int left = 0; left < size - 1; left += squareSize - 1)
            {
                heights[top + squareSize / 2, left + squareSize / 2] =
                    (heights[top, left] +
                     heights[top + squareSize - 1, left] +
                     heights[top, left + squareSize - 1] +
                     heights[top + squareSize - 1, left + squareSize - 1]) / 4 + Random.Range(-offset, offset + 1);
            }
        }
    }

    private int GetSize(int height, int width)
    {
        int res = 2;
        while (res + 1 < height || res + 1 < width)
            res *= 2;
        return res + 1;
    }
}
