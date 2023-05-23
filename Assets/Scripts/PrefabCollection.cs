using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PGGDemo
{
    [CreateAssetMenu]
    public class PrefabCollection : ScriptableObject
    {
        [SerializeField] private GameObject[] prefabs;

        private Dictionary<GameObject, int> _orderByPrefab;

        public IReadOnlyCollection<GameObject> Prefabs => prefabs;

        public Dictionary<GameObject, int> PrefabByOrder
        {
            get
            {
                if (_orderByPrefab != null) return _orderByPrefab;
                var i = 0;
                _orderByPrefab = Prefabs.ToDictionary(o => o, o => i++);
                return _orderByPrefab;
            }
        }
    }
}