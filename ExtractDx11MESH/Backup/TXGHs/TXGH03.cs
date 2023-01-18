// Decompiled with JetBrains decompiler
// Type: ExtractDx11MESH.TXGHs.TXGH03
// Assembly: ExtractDx11MESH, Version=1.0.8142.30912, Culture=neutral, PublicKeyToken=null
// MVID: 29BAC945-368A-4795-B445-7620ADD62E50
// Assembly location: D:\LegoExtracted\tools\DX11ExtractNew\ExtractDx11MESH.exe

namespace ExtractDx11MESH.TXGHs
{
  public class TXGH03 : TXGH01
  {
    public TXGH03(byte[] fileData, int iPos)
      : base(fileData, iPos)
    {
    }

    protected override void ReadTextureMeta()
    {
      this.iPos += 16;
      this.iPos += 4;
      this.iPos += 4;
      this.iPos += 46;
    }
  }
}
