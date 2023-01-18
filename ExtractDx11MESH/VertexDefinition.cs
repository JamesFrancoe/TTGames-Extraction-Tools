// Decompiled with JetBrains decompiler
// Type: ExtractDx11MESH.VertexDefinition
// Assembly: ExtractDx11MESH, Version=1.0.8142.30912, Culture=neutral, PublicKeyToken=null
// MVID: 29BAC945-368A-4795-B445-7620ADD62E50
// Assembly location: D:\LegoExtracted\tools\DX11ExtractNew\ExtractDx11MESH.exe

namespace ExtractDx11MESH
{
  public class VertexDefinition
  {
    public VertexDefinition.VariableEnum Variable;
    public VertexDefinition.VariableTypeEnum VariableType;
    public int Offset;

    public enum VariableEnum
    {
      position,
      normal,
      colorSet0,
      tangent,
      colorSet1,
      uvSet01,
      unknown6,
      uvSet2,
      unknown8,
      blendIndices0,
      blendWeight0,
      unknown11,
      lightDirSet,
      lightColSet,
    }

    public enum VariableTypeEnum
    {
      vec2float = 2,
      vec3float = 3,
      vec4float = 4,
      vec2half = 5,
      vec4half = 6,
      vec4char = 7,
      vec4mini = 8,
      color4char = 9,
    }
  }
}
