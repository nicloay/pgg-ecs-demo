using FIMSpace.Generating;
using FIMSpace.Generating.Rules;
using UnityEditor;
using UnityEngine;

namespace PGGDemo.PGG
{
    public class CustomPerlinNoiseOffset : SpawnRuleBase, ISpawnProcedureType
    {
        [PGG_SingleLineSwitch("OffsetMode", 58, "Select if you want to offset postion with cell size or world units",
            140)]
        public Vector3 PosOffset = new(0f, 2f, 0f);

        [HideInInspector] public ESR_Measuring OffsetMode = ESR_Measuring.Cells;

        [PGG_SingleLineTwoProperties("Space", 88, 60, 14)]
        public ESP_OffsetMode ApplyMode = ESP_OffsetMode.OverrideOffset;

        [HideInInspector] public ESP_OffsetSpace Space = ESP_OffsetSpace.LocalSpace;

        [Space(6)] public Vector2 PerlinNoiseScale = new(16, 16);

        public Vector2 OffsetNoise = Vector2.zero;

        [Space(2)] [Tooltip("Making objects transform max by this units, setted to 2 offset will start at two units")]
        public float SquareSteps;

        // Define what your script will do
        public EProcedureType Type => EProcedureType.Event;

        // Base parameters implementation
        public override string TitleName()
        {
            return "Custom Perlin Noise Offset";
        }

        public override string Tooltip()
        {
            return "Transforming position of the spawned object with perlin noise offset, can provide landscape effect";
        }

        #region There you can do custom modifications for inspector view

#if UNITY_EDITOR
        public override void NodeBody(SerializedObject so)
        {
            // GUIIgnore.Clear(); GUIIgnore.Add("Tag"); // Custom ignores drawing properties
            base.NodeBody(so);
        }
#endif

        #endregion


        public override void CellInfluence(FieldSetup preset, FieldModification mod, FieldCell cell,
            ref SpawnData spawn, FGenGraph<FieldCell, FGenPoint> grid, Vector3? restrictDirection = null)
        {
            var tgtOffset = PosOffset;

            var xNoise =
                Mathf.PerlinNoise(cell.PosXZ.x * PerlinNoiseScale.x * 0.01f + OffsetNoise.x,
                    cell.PosXZ.y * PerlinNoiseScale.y * 0.01f + +OffsetNoise.y) + FGenerators.GetRandom(0, 0.1f);

            tgtOffset.x *= xNoise;
            tgtOffset.y *= xNoise;
            tgtOffset.z *= xNoise;

            tgtOffset = GetUnitOffset(tgtOffset, OffsetMode, preset);

            if (SquareSteps > 0f)
            {
                tgtOffset.x = Mathf.Round(tgtOffset.x * SquareSteps) * SquareSteps;
                tgtOffset.y = Mathf.Round(tgtOffset.y * SquareSteps) * SquareSteps;
                tgtOffset.z = Mathf.Round(tgtOffset.z * SquareSteps) * SquareSteps;
            }
            else
            {
                SquareSteps = 0f;
            }

            if (ApplyMode == ESP_OffsetMode.OverrideOffset)
            {
                if (Space == ESP_OffsetSpace.LocalSpace) spawn.DirectionalOffset = tgtOffset;
                else spawn.Offset = tgtOffset;
            }
            else
            {
                if (Space == ESP_OffsetSpace.LocalSpace) spawn.DirectionalOffset += tgtOffset;
                else spawn.Offset += tgtOffset;
            }
        }
    }
}