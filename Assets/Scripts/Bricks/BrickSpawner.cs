using UnityEngine;

namespace Assets.Scripts.Bricks
{
    public class BrickSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _speedBrickPrebab;
        [SerializeField] private GameObject _sizeBrickPrebab;

        [SerializeField] private GameObject[] _brickPrefabs;
        [SerializeField] private BrickManager _brickManager;

        [SerializeField] private Vector2 _topLeftSpawnPosition;
        [SerializeField] private Transform _bricksParentTransorm;

        private const float SpawnOffsetX = 1.2f;
        private const float SpawnOffsetY = SpawnOffsetX / 2;
        private const int SpawnChanceForSpecialBricks = 15;

        public void SpawnBricks()
        {
            float playAreaWidth = Mathf.Abs(_topLeftSpawnPosition.x) * 2;
            float bricksAreaWidth = SpawnOffsetX * Constants.BricksFieldSize.x;

            _topLeftSpawnPosition.x += (playAreaWidth - bricksAreaWidth) / 2 + SpawnOffsetX / 2;

            int rowsPerBrickType = Constants.BricksFieldSize.y / 3;
            int currentRow = 0;
            int brickIndex = 0;

            for (int y = 0; y < Constants.BricksFieldSize.y; y++)
            {
                if (currentRow < rowsPerBrickType)
                    currentRow++;
                for (int x = 0; x < Constants.BricksFieldSize.x; x++)
                {
                    GameObject brickToSpawn = UnityEngine.Random.Range(0, 100) > SpawnChanceForSpecialBricks ? _brickPrefabs[brickIndex] : GetRandomSpecialBrick();
                    Vector2 spawnPosition = _topLeftSpawnPosition + new Vector2(x * SpawnOffsetX, -y * SpawnOffsetY);
                    GameObject spawnedGO = Instantiate(brickToSpawn, spawnPosition, Quaternion.identity);
                    spawnedGO.GetComponent<HealthComponent>().ResetHealth();
                    spawnedGO.transform.parent = _bricksParentTransorm;
                    _brickManager.AddBrick(spawnedGO);
                }
                if (currentRow == rowsPerBrickType)
                {
                    currentRow = 0;
                    brickIndex++;
                }
            }
        }

        private GameObject GetRandomSpecialBrick()
        {
            return UnityEngine.Random.Range(0, 2) == 0 ? _speedBrickPrebab : _sizeBrickPrebab;
        }
    }
}
