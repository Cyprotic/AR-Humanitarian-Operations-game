using System.Collections.Generic;
using UnityEngine;

public class RandomIntWithException
{
    public static int randomIntExcept( int min, int max, int except )
    {
        int result = Random.Range( min, max-1 );
        if (result >= except) result += 1;
        return result;
    }
    public static int randomIntExceptMultiples( int min, int max, List<int> exceptList )
    {
        int result = Random.Range( min, max);
        foreach (var number in exceptList)
        {
            if (result == number)
                return randomIntExceptMultiples(min, max, exceptList);
        }
        //if (result >= except) result += 1;
        return result;
    }
}
