using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver
}
public class GameManager : Singleton<GameManager>
{
    public GameState CurrentState { get; private set; }
}
