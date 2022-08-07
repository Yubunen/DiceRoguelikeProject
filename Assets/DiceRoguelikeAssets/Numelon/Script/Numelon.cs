using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Numelon
{
    private int digit = 3;
    private bool collect;
    private int[] num;

    public Numelon()
    {
        num = new int[digit];
        GenerateNumber();
        Debug.Log("" + num[0] + num[1] + num[2]);
    }

    private void GenerateNumber()
    {
        List<int> list = new List<int>{0,1,2,3,4,5,6,7,8,9};
        for (int i = 0; i < digit; i++)
        {
            int index = Random.Range(0, list.Count);
            num[i] = list[index];
            list.RemoveAt(index);
        }
    }

    public (int hit, int brow) CheckAns(int n)
    {
        int[] na = new int[digit];
        for(int i = digit-1; i >= 0; i--)
        {
            na[i] = n % 10;
            n /= 10;
        }
        return CheckAns(na);
    }

    public (int hit, int brow) CheckAns(int[] n)
    {
        return (CheckHit(n), CheckBrow(n));
    }

    private bool CheckN(int[] n)
    {
        //Checking input nunber.
        return true;
    }

    public int CheckHit(int[] n)
    {
        int count = 0;
        for (int i = 0; i < digit; i++)
        {
            if(n[i] == num[i])
            {
                count++;
            }
        }
        return count;
    }

    public int CheckBrow(int[] n)
    {
        int count = 0;
        for (int i = 0; i < digit; i++)
        {
            if (n[i] == num[(i + 1) % 3] || n[i] == num[(i + 2) % 3])
            {
                count++;
            }
        }
        return count;
    }

    public int GetAns()
    {
        int res = 0;
        for(int i = 0; i < digit; i++)
        {
            res *= 10;
            res += num[i];
        }
        return res;
    }
}
