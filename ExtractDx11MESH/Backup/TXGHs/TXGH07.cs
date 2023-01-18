// Decompiled with JetBrains decompiler
// Type: ExtractDx11MESH.TXGHs.TXGH07
// Assembly: ExtractDx11MESH, Version=1.0.8142.30912, Culture=neutral, PublicKeyToken=null
// MVID: 29BAC945-368A-4795-B445-7620ADD62E50
// Assembly location: D:\LegoExtracted\tools\DX11ExtractNew\ExtractDx11MESH.exe

using ExtractHelper;

namespace ExtractDx11MESH.TXGHs
{
  public class TXGH07 : TXGH06
  {
    public TXGH07(byte[] fileData, int iPos)
      : base(fileData, iPos)
    {
    }

    protected override void ReadTextureMeta()
    {
      this.iPos += 16;
      this.iPos += 4;
      this.iPos += 4;
      this.iPos += 4;
      this.iPos += 4;
      this.iPos += 4;
      this.iPos += 17;
      int int32 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
      this.iPos += 4;
      this.iPos += int32;
      this.iPos += 10;
    }
  }
}
