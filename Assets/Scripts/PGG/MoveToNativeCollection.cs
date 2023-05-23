using System.Collections.Generic;
using FIMSpace.Generating;
using FIMSpace.Generating.Rules;
using Unity.Mathematics;
using UnityEngine;
using VContainer;

namespace PGGDemo.PGG
{
    public class MoveToNativeCollection : SpawnRuleBase, ISpawnProcedureType
    {
        public EProcedureType Type => EProcedureType.OnConditionsMet;

        public override string TitleName()
        {
            return "Move to NativeArray";
        }

        public override string Tooltip()
        {
            return "Clear all cells then move to NativeArray";
        }


        public override void OnConditionsMetAction(FieldModification mod, ref SpawnData thisSpawn, FieldSetup preset,
            FieldCell cell, FGenGraph<FieldCell, FGenPoint> grid)
        {
            if (!Application.isPlaying) return;

            var objectResolver = FindObjectOfType<SceneScope>().Container;
            var listAdapter = objectResolver.Resolve<ListAdapter>();
            var prefabByOrder = objectResolver.Resolve<PrefabCollection>().PrefabByOrder;

            var spawnDataToDelete = new List<SpawnData>();
            for (var i = 0; i < grid.AllCells.Count; i++)
            {
                var gridCell = grid.AllCells[i];
                var cellPosition = (Vector3)gridCell.Pos; // preset.GetCellWorldPosition(gridCell);
                spawnDataToDelete.Clear();
                var list = gridCell.CollectSpawns();
                for (var j = 0; j < list.Count; j++)
                {
                    var spawnData = list[j];
                    if (spawnData.Prefab == null) continue;
                    var rotation = spawnData.Prefab.transform.rotation * Quaternion.Euler(spawnData.RotationOffset);
                    var position = cellPosition + spawnData.Offset + rotation * spawnData.DirectionalOffset;
                    listAdapter.SpawnDatas.Add(new EcsSpawnData
                    {
                        PrefabId = prefabByOrder[spawnData.Prefab],
                        Rotation = new float4(rotation.x, rotation.y, rotation.z, rotation.w),
                        Position = position,
                        UniformScale = Vector3.Scale(spawnData.LocalScaleMul, spawnData.Prefab.transform.lossyScale).x
                    });
                    spawnDataToDelete.Add(spawnData);
                }

                gridCell.RemoveAllSpawnsFromCell();
            }
        }
    }
}