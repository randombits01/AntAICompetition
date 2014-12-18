﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AntAICompetition.Models;
using AntAICompetition.Server;

namespace AntAICompetition.Controllers
{
    /// <summary>
    /// Api specification for the Ant AI Simulation
    /// </summary>
    public class GameController : ApiController
    {
        private readonly GameManager _gameManager;

        public GameController()
        {
            _gameManager = GameManager.Instance;
        }

        /// <summary>
        /// Gets the list of current games
        /// </summary>
        /// <returns></returns>
        [Route("api/game")]
        public IList<Game> Get()
        {
            return _gameManager.Games.Values.ToList();
        }

        /// <summary>
        /// Initiates an agent logon with the simulation server by name. Once an agent is logged on, 
        /// a logon result is returned with the id and starting time of the next game.
        /// </summary>
        /// <param name="agentName"></param>
        /// <returns>LogonResult</returns>
        [HttpPost]
        [Route("api/game/logon")]
        public LogonResult Logon(LogonRequest logon)
        {
            return _gameManager.GetGame(logon.GameId).LogonPlayer(logon.AgentName);
            
        }
        /// <summary>
        /// Returns the full status for a certain game
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/game/{id}/status/{authToken}")]
        public GameStatus Status(int id, string authToken)
        {
            var game = _gameManager.GetGame(id);
            return new GameStatus()
            {
                Hill = game.GetHillFromToken(authToken),
                TotalFood = game.GetFoodFromToken(authToken),
                FriendlyAnts = game.GetFriendlyAnts(authToken),
                EnemyAnts = game.GetVisibleAnts(authToken),
                VisibleFood = game.GetVisibleFood(authToken),
                GameId = id,
                FogOfWar = game.Board.FogOfWar,
                MillisecondsUntilNextTurn = game.TimeToNextTurn,
                Turn = game.Turn
            };
        }

        /// <summary>
        /// Returns just the turn information for a certain game
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/game/{id}/turn")]
        public TurnStatus Turn(int id)
        {
            return new TurnStatus()
            {
                MillisecondsUntilNextTurn = _gameManager.GetGame(id).TimeToNextTurn,
                Turn = _gameManager.GetGame(id).Turn
            };
        }

        /// <summary>
        /// Updates the state of an agent's ants. Can be called once per turn.
        /// </summary>
        /// <param name="updateRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/game/update")]
        public UpdateResult Update(UpdateRequest updateRequest)
        {
            if (updateRequest != null && updateRequest.MoveAntRequests != null)
            {
                return _gameManager.GetGame(updateRequest.GameId).UpdatePlayer(updateRequest);
            }
            return null;
        }


    }
}
