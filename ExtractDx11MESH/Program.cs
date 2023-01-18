// Decompiled with JetBrains decompiler
// Type: ExtractDx11MESH.Program
// Assembly: ExtractDx11MESH, Version=1.0.8142.30912, Culture=neutral, PublicKeyToken=null
// MVID: 29BAC945-368A-4795-B445-7620ADD62E50
// Assembly location: D:\LegoExtracted\tools\DX11ExtractNew\ExtractDx11MESH.exe

using ExtractHelper;
using System;

namespace ExtractDx11MESH
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      try
      {
        ExtractDx11MESH extractDx11Mesh = new ExtractDx11MESH();
        extractDx11Mesh.ParseArgs(args);
        extractDx11Mesh.Extract();
      }
      catch (NotSupportedException ex)
      {
        ColoredConsole.WriteLineError("Not yet surported: " + ex.Message);
      }
      catch (NotImplementedException ex)
      {
        ColoredConsole.WriteLineError("Not yet implemented: " + ex.Message);
      }
      catch (Exception ex)
      {
        ColoredConsole.WriteLineError(ex.Message);
        ColoredConsole.WriteLineError(ex.StackTrace);
      }
    }
  }
}
