using System;
using System.Threading.Tasks;
using FIMSpace.Generating;
using UnityEngine;

namespace PGGDemo.PGG
{
    [RequireComponent(typeof(BuildPlannerExecutor))]
    public class GenerateCtrl : MonoBehaviour
    {
        private BuildPlannerExecutor _executor;

        private void Awake()
        {
            _executor = GetComponent<BuildPlannerExecutor>();
        }

        private async void Start()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(500));
            _executor.Generate();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R)) _executor.Generate();
        }
    }
}