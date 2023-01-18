﻿// Decompiled with JetBrains decompiler
// Type: ExtractDx11MESH.IVL5.IVL502
// Assembly: ExtractDx11MESH, Version=1.0.8142.30912, Culture=neutral, PublicKeyToken=null
// MVID: 29BAC945-368A-4795-B445-7620ADD62E50
// Assembly location: D:\LegoExtracted\tools\DX11ExtractNew\ExtractDx11MESH.exe

using ExtractHelper;

namespace ExtractDx11MESH.IVL5
{
  public class IVL502 : IVL501
  {
    public IVL502(byte[] fileData, int iPos)
      : base(fileData, iPos)
    {
    }

    public override int Read(ref int referencecounter)
    {
      this.iPos += 4;
      this.iPos += 4;
      this.iPos += 4;
      ColoredConsole.WriteLine("{0:x8}   Number of HGOL: 0x{1:x8}", (object) this.iPos, (object) BigEndianBitConverter.ToInt32(this.fileData, this.iPos));
      return this.iPos;
    }
  }
}
