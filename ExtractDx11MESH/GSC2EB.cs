// Decompiled with JetBrains decompiler
// Type: ExtractDx11MESH.GSC2EB
// Assembly: ExtractDx11MESH, Version=1.0.8142.30912, Culture=neutral, PublicKeyToken=null
// MVID: 29BAC945-368A-4795-B445-7620ADD62E50
// Assembly location: D:\LegoExtracted\tools\DX11ExtractNew\ExtractDx11MESH.exe

using ExtractDx11MESH.MESHs;
using ExtractHelper;
using System.Text;

namespace ExtractDx11MESH
{
  public class GSC2EB
  {
    protected byte[] fileData;
    protected int iPos;
    public int version;

    public GSC2EB(byte[] fileData, int iPos)
    {
      this.fileData = fileData;
      this.iPos = iPos;
      this.version = BigEndianBitConverter.ToInt32(fileData, iPos);
      this.iPos += 4;
      ColoredConsole.WriteLineInfo("{0:x8} GSC2 Version 0x{1:x2}", (object) iPos, (object) this.version);
    }

    public int Read(ref int referencecounter)
    {
      int int32 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
      ColoredConsole.WriteLine("{0:x8}   Number of resources: 0x{1:x8}", (object) this.iPos, (object) int32);
      this.iPos += 4;
      for (int index = 0; index < int32; ++index)
      {
        this.iPos += 3;
        int int16_1 = (int) BigEndianBitConverter.ToInt16(this.fileData, this.iPos);
        this.iPos += 2;
        ColoredConsole.WriteLine("{0:x8}   {1}", (object) this.iPos, (object) this.readString(int16_1));
        int int16_2 = (int) BigEndianBitConverter.ToInt16(this.fileData, this.iPos);
        this.iPos += 2;
        ColoredConsole.WriteLine("{0:x8}     {1}", (object) this.iPos, (object) this.readString(int16_2));
      }
      this.iPos += 4;
      MESH04 mesh = (MESH04) new MESHC9(this.fileData, this.iPos);
      mesh.Read(ref referencecounter);
      int num = 0;
      foreach (Part part in mesh.Parts)
        ExtractDx11MESH.CreateObjFile(mesh, part, num++);
      return this.iPos;
    }

    protected string readString(int numberofchars)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < numberofchars; ++index)
      {
        if (this.fileData[this.iPos] != (byte) 0)
          stringBuilder.Append((char) this.fileData[this.iPos]);
        ++this.iPos;
      }
      return stringBuilder.ToString();
    }
  }
}
