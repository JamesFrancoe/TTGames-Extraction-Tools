// Decompiled with JetBrains decompiler
// Type: ExtractDx11MESH.MESHs.MESHC9
// Assembly: ExtractDx11MESH, Version=1.0.8142.30912, Culture=neutral, PublicKeyToken=null
// MVID: 29BAC945-368A-4795-B445-7620ADD62E50
// Assembly location: D:\LegoExtracted\tools\DX11ExtractNew\ExtractDx11MESH.exe

using ExtractHelper;
using System;

namespace ExtractDx11MESH.MESHs
{
  public class MESHC9 : MESHC8
  {
    public MESHC9(byte[] fileData, int iPos)
      : base(fileData, iPos)
    {
    }

    protected override Part ReadPart(ref int referencecounter)
    {
      ColoredConsole.WriteLine("{0:x8}   Number of RNMS: 0x{1:x8}", (object) this.iPos, (object) BigEndianBitConverter.ToInt32(this.fileData, this.iPos));
      this.iPos += 4;
      this.iPos += 4;
      ColoredConsole.WriteLineInfo("{0:x8}   RNMS Version 0x{1:x2}", (object) this.iPos, (object) BigEndianBitConverter.ToInt32(this.fileData, this.iPos));
      this.iPos += 4;
      Part part = new Part();
      int int32_1 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
      ColoredConsole.WriteLine("{0:x8}     Number of Vertex Lists: 0x{1:x8}", (object) this.iPos, (object) int32_1);
      this.iPos += 4;
      for (int index = 0; index < int32_1; ++index)
      {
        ColoredConsole.WriteLine("{0:x8}       Vertex List 0x{1:x8}", (object) this.iPos, (object) index);
        part.VertexListReferences1.Add(this.GetVertexListReference(ref referencecounter));
      }
      this.iPos += 4;
      part.IndexListReference1 = this.GetIndexListReference(ref referencecounter);
      part.OffsetIndices = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
      ColoredConsole.WriteLine("{0:x8}     Offset Indices: 0x{1:x8}", (object) this.iPos, (object) part.OffsetIndices);
      this.iPos += 4;
      part.NumberIndices = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
      ColoredConsole.WriteLine("{0:x8}     Number Indices: 0x{1:x8}", (object) this.iPos, (object) part.NumberIndices);
      this.iPos += 4;
      part.OffsetVertices = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
      ColoredConsole.WriteLine("{0:x8}     Offset Vertices: 0x{1:x8}", (object) this.iPos, (object) part.OffsetVertices);
      this.iPos += 4;
      if (BigEndianBitConverter.ToInt16(this.fileData, this.iPos) != (short) 0)
        throw new NotSupportedException("ReadPart Offset Vertices + 4");
      this.iPos += 2;
      part.NumberVertices = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
      ColoredConsole.WriteLine("{0:x8}     Number Vertices: 0x{1:x8}", (object) this.iPos, (object) part.NumberVertices);
      this.iPos += 4;
      this.iPos += 4;
      int int32_2 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
      this.iPos += 4;
      if (int32_2 > 0)
      {
        ColoredConsole.Write("{0:x8}     ", (object) this.iPos);
        for (int index = 0; index < int32_2; ++index)
        {
          ColoredConsole.Write("{0:x2} ", (object) this.fileData[this.iPos]);
          ++this.iPos;
        }
        ColoredConsole.WriteLine();
        ++referencecounter;
      }
      int int32_3 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
      this.iPos += 4;
      if (int32_3 != 0)
      {
        int num = this.ReadRelativePositionList();
        referencecounter += num;
      }
      else
        this.iPos += 4;
      this.iPos += 36;
      this.iPos += 4;
      ++referencecounter;
      ++referencecounter;
      return part;
    }
  }
}
