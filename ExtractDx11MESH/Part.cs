// Decompiled with JetBrains decompiler
// Type: ExtractDx11MESH.Part
// Assembly: ExtractDx11MESH, Version=1.0.8142.30912, Culture=neutral, PublicKeyToken=null
// MVID: 29BAC945-368A-4795-B445-7620ADD62E50
// Assembly location: D:\LegoExtracted\tools\DX11ExtractNew\ExtractDx11MESH.exe

using System.Collections.Generic;

namespace ExtractDx11MESH
{
  public class Part
  {
    public List<int> VertexListReferences1 = new List<int>();
    public List<int> VertexListReferences2 = new List<int>();
    public int IndexListReference1;
    public int IndexListReference2;
    public int OffsetIndices;
    public int NumberIndices;
    public int OffsetVertices;
    public int NumberVertices;
    public int OffsetIndices2;
    public int OffsetVertices2;
    public int NumberVertices2;
  }
}
