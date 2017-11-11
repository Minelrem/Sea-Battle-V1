using Interfaces;
using SeaBattle.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Service
{
    public class GameService : Base, IGame
    {
        public GameService(SeaBattleContext context) : base(context)
        {
             
        }

        private bool _gameState;

        public bool GameState { get => _gameState; set => _gameState = value; }
    }
}
