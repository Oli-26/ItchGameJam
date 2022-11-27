using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class JustGroundGenerator : Node, IGenerator
{
    [Export] public Vector3i WorldBounds { get; set; } = new Vector3i(10, 3, 10);

    public void Generate(IGeneratorOutput generatorOutput)
    {
        for (var x = 0; x < WorldBounds.x; x++)
        {
            for (var z = 0; z < WorldBounds.z; z++)
            {
                generatorOutput.SetBlock(x, -1, z, BlockType.Stone);
                generatorOutput.SetBlock(x, 3, z, BlockType.Stone);
            }
        }

        generatorOutput.GenerateWallSquare(
            new Vector2i(0, 0),
            new Vector2i(WorldBounds.x - 1, WorldBounds.z - 1),
            3,
            BlockType.Stone,
            2
        );
        generatorOutput.GenerateWallSquare(
            new Vector2i(-1, -1),
            new Vector2i(WorldBounds.x, WorldBounds.z),
            3,
            BlockType.Stone,
            2
        );
    }

}
