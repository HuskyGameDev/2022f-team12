using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssCoroutine
{
    /// <summary>
    /// "Generic" (in the general sense) Coroutine to wait for a provided Func to return true.
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public static IEnumerator WaitFor(System.Func<bool> func, bool before = true)
    {
        if (before)
        {
            while (!func.Invoke())
            {
                yield return null;
            }
        }
        else
        {
            do
            {
                yield return null;
            }
            while (!func.Invoke());
        }
    }

    public static IEnumerator WaitForFixed(System.Func<bool> func, bool before = true)
    {
        if (before)
        {
            while (!func.Invoke())
            {
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            do
            {
                yield return new WaitForFixedUpdate();
            }
            while (!func.Invoke());
        }
    }
}
