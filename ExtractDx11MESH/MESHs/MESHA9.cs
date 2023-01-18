// Decompiled with JetBrains decompiler
// Type: ExtractDx11MESH.MESHs.MESHA9
// Assembly: ExtractDx11MESH, Version=1.0.8142.30912, Culture=neutral, PublicKeyToken=null
// MVID: 29BAC945-368A-4795-B445-7620ADD62E50
// Assembly location: D:\LegoExtracted\tools\DX11ExtractNew\ExtractDx11MESH.exe

using ExtractHelper;

namespace ExtractDx11MESH.MESHs
{
  public class MESHA9 : MESH30
  {
    public MESHA9(byte[] fileData, int iPos)
      : base(fileData, iPos)
    {
    }

    protected override VertexList ReadVertexList(int numberofvertices)
    {
      VertexList vertexList = new VertexList();
      this.iPos += 4;
      this.iPos += 4;
      int int32 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
      ColoredConsole.WriteLine("{0:x8}           Number of Vertex Definitions: {1:x8}", (object) this.iPos, (object) int32);
      this.iPos += 4;
      for (int index = 0; index < int32; ++index)
        vertexList.VertexDefinitions.Add(this.ReadVertexDefinition());
      this.iPos += 6;
      ColoredConsole.WriteLine("{0:x8}           Number of Vertices: {1:x8}", (object) this.iPos, (object) numberofvertices);
      for (int index = 0; index < numberofvertices; ++index)
        vertexList.Vertices.Add(this.ReadVertex(vertexList.VertexDefinitions));
      return vertexList;
    }
  }
}
