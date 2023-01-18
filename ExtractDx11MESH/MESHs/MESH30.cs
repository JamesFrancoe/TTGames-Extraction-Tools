// Decompiled with JetBrains decompiler
// Type: ExtractDx11MESH.MESHs.MESH30
// Assembly: ExtractDx11MESH, Version=1.0.8142.30912, Culture=neutral, PublicKeyToken=null
// MVID: 29BAC945-368A-4795-B445-7620ADD62E50
// Assembly location: D:\LegoExtracted\tools\DX11ExtractNew\ExtractDx11MESH.exe

using ExtractHelper;

namespace ExtractDx11MESH.MESHs
{
  public class MESH30 : MESH2F
  {
    public MESH30(byte[] fileData, int iPos)
      : base(fileData, iPos)
    {
    }

    public override int Read(ref int referencecounter)
    {
      this.iPos += 4;
      int int32 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
      ColoredConsole.WriteLine("{0:x8}   Number of Parts: 0x{1:x8}", (object) this.iPos, (object) int32);
      this.iPos += 4;
      for (int index = 0; index < int32; ++index)
      {
        ColoredConsole.WriteLine("{0:x8}   Part 0x{1:x8}", (object) this.iPos, (object) index);
        this.Parts.Add(this.ReadPart(ref referencecounter));
      }
      return this.iPos;
    }
  }
}
