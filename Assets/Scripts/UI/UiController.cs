using System;
using UnityEngine;

namespace UI
{
    public class UiController : MonoBehaviour
    {
        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        public void SetDifficulty(int state)
        {
            _gameManager.easyMode = state == 0;
        }

        public void SetVersusCpuMode(bool state)
        {
            _gameManager.vsCpu = state;
            _gameManager.Restart();
        }

        public void SetFirstPlayerCpu(bool state)
        {
            _gameManager.cpuPlaysFirst = state;
            _gameManager.Restart();
        }
    }
}