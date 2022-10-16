using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerInput
{
    public static bool Shoot => Input.GetMouseButtonDown(0);

    public static bool EnablePiercing => Input.GetKeyDown(KeyCode.Alpha1);
    
    public static bool EnableChaining => Input.GetKeyDown(KeyCode.Alpha2);
    
    public static bool EnableForking => Input.GetKeyDown(KeyCode.Alpha3);

    public static bool SpawnEnemyAtCursor => Input.GetKeyDown(KeyCode.Q);
    public static bool SpawnEnemyRandomly => Input.GetKeyDown(KeyCode.W);
}
