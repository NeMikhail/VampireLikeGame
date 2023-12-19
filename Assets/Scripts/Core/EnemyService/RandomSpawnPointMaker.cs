using UnityEngine;
using Core.Interface.IModels;

namespace Core.EnemyService
{
    internal class RandomSpawnPointMaker
    {
        const float RADIUS_OFFSET = 0f;
        const float GROUND_HEIGHT = 0f;
        const float ENEMY_RADIUS = 0.5f;
        private Camera _camera;
        private Vector3 _frameCenter;
        private readonly IRandomUnitVectorGenerator _randomUnitVectorGenerator;
        private int maxRespawnTries = 3;

        public float radiusOfSpawn { get; private set; }
        public Vector3 Center
        {
            get
            {
                return _camera.transform.position + _frameCenter;
            }
        }

        public RandomSpawnPointMaker(IRandomUnitVectorGenerator randomUnitVectorGenerator)
        {
            _randomUnitVectorGenerator = randomUnitVectorGenerator;
            InitCamera();
        }


        public Vector3 GetPoint()
        {
            Vector3 result = Vector3.zero;
            bool goodSpawnPoint = false;
            int tries = 0;

            while (!goodSpawnPoint)
            {
                tries++;
                result = Center + _randomUnitVectorGenerator.CreateUnitVector() * radiusOfSpawn;
                Vector3 sphereCenter = new Vector3(result.x, result.y + ENEMY_RADIUS + 0.2f, result.z);

                if (!Physics.CheckSphere(sphereCenter, ENEMY_RADIUS))
                    goodSpawnPoint = true;

                if (tries > maxRespawnTries)
                    goodSpawnPoint = true;
            }

            return result;
        }

        private void InitCamera()
        {
            _camera = Camera.main;

            var plane = new Plane(Vector3.up, new Vector3(0f, 0f, GROUND_HEIGHT));

            if (_camera != null)
            {
                Vector3 center = ScreenPointToPlane(plane, _camera.scaledPixelWidth / 2, _camera.scaledPixelHeight / 2);
                _frameCenter = center - _camera.transform.position;
                radiusOfSpawn = GetMinRespawnRadius(center, plane) + RADIUS_OFFSET;
            }
        }

        private float GetMinRespawnRadius(Vector3 center, Plane plane)
        {
            int frameHight = _camera.scaledPixelHeight - 1;
            int frameWidth = _camera.scaledPixelWidth - 1;
            float minRadSqrt = Mathf.Max(
                new float[]{
                    (ScreenPointToPlane(plane, 0, frameHight) - center).sqrMagnitude,
                    (ScreenPointToPlane(plane, frameWidth, frameHight) - center).sqrMagnitude,
                    (ScreenPointToPlane(plane, 0, 0) - center).sqrMagnitude,
                    (ScreenPointToPlane(plane, frameWidth, 0) - center).sqrMagnitude
                });
            return Mathf.Sqrt(minRadSqrt);
        }

        private Vector3 ScreenPointToPlane(Plane plane, int screenWidthInPX, int screenHeightInPX)
        {
            var ray = _camera.ScreenPointToRay(new Vector3(screenWidthInPX, screenHeightInPX));
            return plane.Raycast(ray, out float hitDist) ? ray.GetPoint(hitDist) : default;
        }
    }
}