// Decompiled with JetBrains decompiler
// Type: ExtractDx11MESH.IVL5.IVL501
// Assembly: ExtractDx11MESH, Version=1.0.8142.30912, Culture=neutral, PublicKeyToken=null
// MVID: 29BAC945-368A-4795-B445-7620ADD62E50
// Assembly location: D:\LegoExtracted\tools\DX11ExtractNew\ExtractDx11MESH.exe

using ExtractHelper;

namespace ExtractDx11MESH.IVL5
{
  public class IVL501
  {
    protected byte[] fileData;
    protected int iPos;
    public int version;

    public IVL501(byte[] fileData, int iPos)
    {
      this.fileData = fileData;
      this.iPos = iPos;
      this.version = BigEndianBitConverter.ToInt32(fileData, iPos);
      this.iPos += 4;
      ColoredConsole.WriteLineInfo("{0:x8} IVL5 Version 0x{1:x2}", (object) iPos, (object) this.version);
    }

    public virtual int Read(ref int referencecounter) => this.iPos;
  }
}
