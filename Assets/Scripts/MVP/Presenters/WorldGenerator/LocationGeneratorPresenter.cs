using System;
using System.Collections.Generic;
using UnityEngine;
using Configs;
using Core;
using Core.Interface.IPresenters;
using Core.ResourceLoader;
using Core.Interface.UnityLifecycle;


namespace MVP.Presenters.WorldGenerator
{
    public sealed class LocationGeneratorPresenter : IInitialisation, IFixedExecute
    {
        private const int PATTERN_LINES = 2;
        private Transform _tilesRootTransform;
        private Vector3 _currentPosition;
        private List<GameObject> _tilesList;
        private int _locationSize;
        private int _currentIndex;
        private int[,] _indexesMap;
        private int _direction;
        private int _stepCount;
        private int _steps;
        private bool _isGenerated;

        public event Action OnLocationGenerated;

        public void Initialisation()
        {
            _tilesRootTransform = GameObject.FindWithTag("TMP_TILES_ROOT").transform;
            var locationGeneratorConfig = ResourceLoadManager.GetConfig<LocationsPack>().CurrentLocation.
            LocationGeneratorConfig;
            int[,] pattern = GeneratePattern(locationGeneratorConfig.TileIndexes);
            _indexesMap = GenerateTilesArray(locationGeneratorConfig.LocationSize, pattern);
            _currentPosition = Vector3.zero;
            _tilesList = locationGeneratorConfig.TilesPrefabs;
            _locationSize = locationGeneratorConfig.LocationSize;
            _currentIndex = 0;
            _steps = 1;
            OnLocationGenerated?.Invoke();
        }

        public void FixedExecute(float fixedDeltaTime)
        {
            if (_isGenerated)
            {
                return; 
            }
            GameObject tilePrefab = _tilesList[_indexesMap[_currentIndex % _locationSize, _currentIndex / _locationSize]];
            GenerateTile(tilePrefab, _currentPosition);
            switch (_direction)
            {
                case 0:
                    _currentPosition += new Vector3(0f, 0f, -ConstantsProvider.TILE_SIZE);
                    break;
                case 1:
                    _currentPosition += new Vector3(ConstantsProvider.TILE_SIZE, 0f, 0f);
                    break;
                case 2:
                    _currentPosition += new Vector3(0f, 0f, ConstantsProvider.TILE_SIZE);
                    break;
                case 3:
                    _currentPosition += new Vector3(-ConstantsProvider.TILE_SIZE, 0f, 0f);
                    break;
            }

            _stepCount++;
            if (_stepCount >= _steps)
            {
                _stepCount = 0;
                _direction = (_direction + 1) % 4;

                if (_direction == 0 || _direction == 2)
                {
                    _steps++;
                }
            }
            _currentIndex++;
            if (_currentIndex >= _locationSize * _locationSize)
            {
                _isGenerated = true;
            }
        }

        private int[,] GeneratePattern(List<int> TileIndexes)
        {
            int lineLenght = TileIndexes.Count / PATTERN_LINES;
            int[,] pattern = new int[lineLenght, PATTERN_LINES];

            for (int i = 0; i < lineLenght; i++)
            {
                pattern[i, 0] = TileIndexes[i];
            }

            for (int i = 0; i < lineLenght; i++)
            {
                pattern[i, 1] = TileIndexes[i + lineLenght];
            }
            return pattern;
        }

        private int[,] GenerateTilesArray(int locationSize, int[,] pattern)
        {
            int[,] tileIndexesMap = new int[locationSize, locationSize];
            int patternHalfLength = pattern.Length / 2;

            for (int n = 0; n < locationSize; n++)
            {
                if (n % 2 == 0)
                {
                    for (int m = 0; m < locationSize; m++)
                    {
                        tileIndexesMap[m, n] = pattern[m % patternHalfLength, 0];
                    }
                }
                else
                {
                    for (int m = 0; m < locationSize; m++)
                    {
                        tileIndexesMap[m, n] = pattern[m % patternHalfLength, 1];
                    }
                }
            }
            return tileIndexesMap;
        }

        private void GenerateTile(GameObject tilePrefab, Vector3 position)
        {
            GameObject tileObject = GameObject.Instantiate(tilePrefab, _tilesRootTransform);
            tileObject.transform.position = position;
        }
    }
}