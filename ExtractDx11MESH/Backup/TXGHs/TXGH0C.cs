// Decompiled with JetBrains decompiler
// Type: ExtractDx11MESH.TXGHs.TXGH0C
// Assembly: ExtractDx11MESH, Version=1.0.8142.30912, Culture=neutral, PublicKeyToken=null
// MVID: 29BAC945-368A-4795-B445-7620ADD62E50
// Assembly location: D:\LegoExtracted\tools\DX11ExtractNew\ExtractDx11MESH.exe

using ExtractHelper;

namespace ExtractDx11MESH.TXGHs
{
  public class TXGH0C : TXGH0A
  {
    public TXGH0C(byte[] fileData, int iPos)
      : base(fileData, iPos)
    {
    }

    public override int Read(ref int referencecounter)
    {
      this.iPos += 4;
      int int32 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
      this.iPos += 4;
      ColoredConsole.WriteLine("{0:x8}   Number of Textures: 0x{1:x2}", (object) this.iPos, (object) int32);
      for (int index = 0; index < int32; ++index)
      {
        this.iPos += 16;
        this.iPos += 3;
        int int16 = (int) BigEndianBitConverter.ToInt16(this.fileData, this.iPos);
        this.iPos += 2;
        this.iPos += int16;
        ++this.iPos;
        if (int16 != 0)
          ++referencecounter;
      }
      return this.iPos;
    }
  }
}
