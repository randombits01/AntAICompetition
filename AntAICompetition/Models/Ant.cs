﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AntAICompetition.Server;

namespace AntAICompetition.Models
{
    public class Ant : ICoordinates
    {
        private static int _maxId = 1;

        public Ant()
        {
            Id = _maxId++;
        }

        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Owner { get; set; }

        public double GetDistance<T>(T other) where T : ICoordinates
        {
            int dx = Math.Abs(X - other.X);
            int dy = Math.Abs(Y - other.Y);
            int HALF_WIDTH = (int)Math.Floor(Game.DEFAULT_WIDTH / 2.0);
            int HALF_HEIGHT = (int)Math.Floor(Game.DEFAULT_HEIGHT / 2.0);
            if (dx > HALF_WIDTH) {
                dx = Game.DEFAULT_WIDTH - dx;
            }
            if (dy > HALF_HEIGHT) {
                dy = Game.DEFAULT_HEIGHT - dy;
            }
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public int MoveX(string direction)
        {
            if (direction == "left")
            {
                return X - 1;
            }
            if (direction == "right")
            {
                return X + 1;
            }
            return X;
        }

        public int MoveY(string direction)
        {
            if (direction == "up")
            {
                return Y - 1;
            }
            if (direction == "down")
            {
                return Y + 1;
            }
            return Y;

        }

    }
}
