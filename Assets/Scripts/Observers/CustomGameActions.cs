using System.Collections.Generic;
using UnityEngine;

public struct MouseDownEvent3D
{
    public GameObject MouseObject;
    public Vector3 Position;

    public MouseDownEvent3D(GameObject mouseObject, Vector3 position)
    {
        MouseObject = mouseObject;
        Position = position;
    }
}
public struct MouseUpEvent3D
{
    public GameObject MouseObject;
    public Vector3 Position;

    public MouseUpEvent3D(GameObject mouseObject, Vector3 position)
    {
        MouseObject = mouseObject;
        Position = position;
    }
}
public struct MouseEnterEvent3D
{
    public GameObject MouseObject;
    public Vector3 Position;

    public MouseEnterEvent3D(GameObject mouseObject, Vector3 position)
    {
        MouseObject = mouseObject;
        Position = position;
    }
}
public struct MouseExitEvent3D
{
    public GameObject MouseObject;
    public Vector3 Position;

    public MouseExitEvent3D(GameObject mouseObject, Vector3 position)
    {
        MouseObject = mouseObject;
        Position = position;
    }
}

public struct MouseDownEvent2D
{
    public GameObject MouseObject;
    public Vector3 Position;

    public MouseDownEvent2D(GameObject mouseObject, Vector3 position)
    {
        MouseObject = mouseObject;
        Position = position;
    }
}
public struct MouseUpEvent2D
{
    public GameObject MouseObject;
    public Vector3 Position;

    public MouseUpEvent2D(GameObject mouseObject, Vector3 position)
    {
        MouseObject = mouseObject;
        Position = position;
    }
}
public struct MouseEnterEvent2D
{
    public GameObject MouseObject;
    public Vector3 Position;

    public MouseEnterEvent2D(GameObject mouseObject, Vector3 position)
    {
        MouseObject = mouseObject;
        Position = position;
    }
}
public struct MouseExitEvent2D
{
    public GameObject MouseObject;
    public Vector3 Position;

    public MouseExitEvent2D(GameObject mouseObject, Vector3 position)
    {
        MouseObject = mouseObject;
        Position = position;
    }
}

public struct DiceRolled
{
    public List<int> Numbers;
    public DiceRolled(List<int> numbers)
    {
        this.Numbers = numbers;
    }
}
public struct DivideEffect
{
    public int Number;
    public DivideEffect(int number)
    {
        this.Number = number;
    }
}
public class RunesInitialized
{
    public List<Rune> Runes;

    public RunesInitialized(List<Rune> runes)
    {
        this.Runes = runes;
    }
}
public class OnEndOfTheRound
{
    public int RoundIndex;
    public ScoreHandler ScoreComponent;
    public OnEndOfTheRound(int roundIndex, ScoreHandler scoreHandler)
    {
        this.RoundIndex = roundIndex;
        this.ScoreComponent = scoreHandler;
    }
}