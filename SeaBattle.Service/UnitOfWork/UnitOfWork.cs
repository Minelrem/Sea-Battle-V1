﻿using SeaBattle.Data.Context; 

namespace SeaBattle.Service 
{
    public class UnitOfWork
    {
        private static UnitOfWork _instance;

        private UnitOfWork()
        {
            _seaBattleContext = new SeaBattleContext();

        }

        private SeaBattleContext _seaBattleContext;
        private UserService _userService;
        private MailService _mailService;
        private BattlefieldService _battlefielldService;
        private GameService _gameService;


        public static UnitOfWork Instance => _instance ?? (_instance = new UnitOfWork());

        public UserService UserService => _userService ?? (_userService = new UserService(_seaBattleContext));
        public BattlefieldService BattlefieldService => _battlefielldService ?? (_battlefielldService = new BattlefieldService(_seaBattleContext));
        public MailService MailService => _mailService ?? (_mailService = new MailService(_seaBattleContext));
        public GameService GameService => _gameService ?? (_gameService = new GameService(_seaBattleContext));

    }
}
