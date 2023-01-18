// Decompiled with JetBrains decompiler
// Type: ExtractDx11MESH.TXGHs.TXGH0A
// Assembly: ExtractDx11MESH, Version=1.0.8142.30912, Culture=neutral, PublicKeyToken=null
// MVID: 29BAC945-368A-4795-B445-7620ADD62E50
// Assembly location: D:\LegoExtracted\tools\DX11ExtractNew\ExtractDx11MESH.exe

using ExtractHelper;
using System;

namespace ExtractDx11MESH.TXGHs
{
  public class TXGH0A : TXGH08
  {
    public TXGH0A(byte[] fileData, int iPos)
      : base(fileData, iPos)
    {
    }

    public override int Read(ref int referencecounter)
    {
      this.iPos += 4;
      int int32_1 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
      this.iPos += 4;
      for (int index = 0; index < int32_1; ++index)
      {
        if (BigEndianBitConverter.ToInt32(this.fileData, this.iPos) != 0)
          throw new NotSupportedException("TXGH VTOR 1");
        this.iPos += 4;
      }
      this.iPos += 4;
      int int32_2 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
      this.iPos += 4;
      ColoredConsole.WriteLine("{0:x8}   Number of Textures: 0x{1:x2}", (object) this.iPos, (object) int32_2);
      for (int index = 0; index < int32_2; ++index)
      {
        this.iPos += 16;
        int int32_3 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
        this.iPos += 4;
        this.iPos += int32_3;
        this.iPos += 4;
        if (int32_3 != 0)
          ++referencecounter;
      }
      this.iPos += 4;
      referencecounter += BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
      this.iPos += 4;
      return this.iPos;
    }
  }
}
