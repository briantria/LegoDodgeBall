/**
 *  created by  : Brian Tria
 *  date        : 28/11/2020 22:29:56
 *  description :
 **/

using UnityEngine;

namespace LegoDodgeBall
{
    public enum GameModeFlag
    {
        Dodger = 0,
        Thrower = 1
    }


    [CreateAssetMenu(fileName = "NewGameMode", menuName = "ScriptableObject/GameMode", order = 51)]
    public class GameMode : ScriptableObject
    {
        public GameModeFlag CurrentGameMode;
        public int PickupGoal;
        public int TimeLimit;
    }

}