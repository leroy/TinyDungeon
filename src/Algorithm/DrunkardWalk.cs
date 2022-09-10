using System;
using System.Collections.Generic;
using Godot;
using Random = TinyDungeon.Util.Random;

namespace TinyDungeon.Algorithm
{
    public class DrunkardWalk
    {
        private List<Vector2> _steps;

        private Vector2 _currentPosition;

        private Vector2? _lastDirection = null;

        private List<Vector2> Directions => new List<Vector2>()
        {
            Vector2.Up, Vector2.Right, Vector2.Down, Vector2.Left
        };
                    
        public class DrunkardWalkSettings
        {
            public int MaxSteps = 100;
            public Vector2 Start = Vector2.Zero;
            
            public Vector2? BiasTowards = null;
            public bool BiasLastDirection = false;
            
            public double BiasInfluence = 0.5;
        }

        private DrunkardWalkSettings _settings;
        
        public DrunkardWalk(DrunkardWalkSettings settings)
        {
            _settings = settings;

            Reset();
        }

        public bool IsFinished()
        {
            return _steps.Count > _settings.MaxSteps;
        }

        public void Reset()
        {
            _steps = new List<Vector2>();
        }

        public void Step()
        {
            if (IsFinished()) return;

            Vector2 direction = GetRandomDirection();
            Vector2 nextPosition = _currentPosition + direction;

            if (!_steps.Contains(nextPosition))
            {
                _steps.Add(nextPosition);
            }

            _currentPosition = nextPosition;
            _lastDirection = direction;
        }

        public void Generate()
        {
            while (!IsFinished())
            {
                Step();
            }
        }

        public List<Vector2> GetPath()
        {
            return _steps;
        }

        private Vector2 GetRandomDirection()
        {
            var directions = Directions;

            int biasAmount = (int) Math.Round(_settings.BiasInfluence * directions.Count);

            if (_settings.BiasTowards != null)
            {
                Vector2 biasedDirection = ((Vector2)_settings.BiasTowards - _currentPosition).Normalized().Round();

                if (!directions.Contains(biasedDirection))
                {
                    return directions[Random.RandomIndex(directions)];
                }


                for (int i = 0; i < biasAmount; i++)
                {
                    directions.Add(biasedDirection);
                }
            }

            if (_lastDirection != null && _settings.BiasLastDirection)
            {
                for (int i = 0; i < biasAmount; i++)
                {
                    directions.Add((Vector2) _lastDirection);
                }
            }
            
            return directions[Random.RandomIndex(directions)];
        }
    }
}