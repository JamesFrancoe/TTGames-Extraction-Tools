// Decompiled with JetBrains decompiler
// Type: ExtractDx11MESH.ExtractDx11MESH
// Assembly: ExtractDx11MESH, Version=1.0.8142.30912, Culture=neutral, PublicKeyToken=null
// MVID: 29BAC945-368A-4795-B445-7620ADD62E50
// Assembly location: D:\LegoExtracted\tools\DX11ExtractNew\ExtractDx11MESH.exe

using ExtractDx11MESH.IVL5;
using ExtractDx11MESH.MESHs;
using ExtractDx11MESH.TXGHs;
using ExtractHelper;
using ExtractHelper.VariableTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExtractDx11MESH
{
  public class ExtractDx11MESH
  {
    private static string directoryname;
    private string extention;
    private string filename;
    private static string filenamewithoutextension;
    private string fullPath;
    private int iPos = 0;
    private byte[] fileData;
    private int referencecounter = 5;
    private bool onlyModel = false;
    private MESH04 mesh;
    private TXGH01 txgh;
    private IVL501 ivl5;
    private bool extractMesh = true;

    public void ParseArgs(string[] args)
    {
      if (((IEnumerable<string>) args).Count<string>() < 1)
        throw new ArgumentException("No argument handed over!");
      ExtractDx11MESH.directoryname = File.Exists(args[0]) ? Path.GetDirectoryName(args[0]) : throw new ArgumentException(string.Format("File {0} does not exist!", (object) args[0]));
      this.extention = Path.GetExtension(args[0]);
      this.filename = Path.GetFileName(args[0]);
      ExtractDx11MESH.filenamewithoutextension = Path.GetFileNameWithoutExtension(args[0]);
      this.fullPath = Path.GetFullPath(args[0]);
      if (this.extention.ToUpper() == ".MODEL")
        this.onlyModel = true;
      else if (!(this.extention.ToUpper() == ".GHG") && !(this.extention.ToUpper() == ".GSC"))
        throw new ArgumentException("File extention != .ghg and != .gsc");
      for (int index = 1; index < args.Length; ++index)
      {
        switch (args[index])
        {
          case "-x":
            this.extractMesh = false;
            break;
        }
      }
    }

    public void Extract()
    {
      FileInfo fileInfo = new FileInfo(this.fullPath);
      ExtractDx11MESH.directoryname = fileInfo.DirectoryName + "\\" + filenamewithoutextension;
      Directory.CreateDirectory(directoryname);
      FileStream fileStream = File.Open(this.fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
      this.fileData = new byte[(int) fileInfo.Length];
      fileStream.Read(this.fileData, 0, (int) fileInfo.Length);
      fileStream.Close();
      if (this.onlyModel)
      {
        this.readGSC2();
      }
      else
      {
        this.readNU20();
        this.readMESH();
      }
      ColoredConsole.WriteLineInfo(this.fullPath);
    }

    private void readNU20()
    {
      while (this.fileData[this.iPos] != (byte) 48 || this.fileData[this.iPos + 1] != (byte) 50 || this.fileData[this.iPos + 2] != (byte) 85 || this.fileData[this.iPos + 3] != (byte) 78)
        ++this.iPos;
      if (this.fileData[this.iPos] != (byte) 48 && this.fileData[this.iPos + 1] != (byte) 50 && this.fileData[this.iPos + 2] != (byte) 85 && this.fileData[this.iPos + 3] != (byte) 78)
        return;
      this.iPos += 4;
      switch (BigEndianBitConverter.ToInt32(this.fileData, this.iPos))
      {
        case 67:
          this.readTXGH();
          ++this.referencecounter;
          goto case 79;
        case 78:
          this.readTXGH();
          ++this.referencecounter;
          goto case 79;
        case 79:
        case 83:
        case 86:
        case 88:
        case 92:
          break;
        default:
          throw new NotSupportedException(string.Format("NU20 Version {0:x2}", (object) BigEndianBitConverter.ToInt32(this.fileData, this.iPos)));
      }
    }

    private void readIVL5()
    {
      while (this.fileData[this.iPos] != (byte) 53 || this.fileData[this.iPos + 1] != (byte) 76 || this.fileData[this.iPos + 2] != (byte) 86 || this.fileData[this.iPos + 3] != (byte) 73)
        ++this.iPos;
      if (this.fileData[this.iPos] == (byte) 53 && this.fileData[this.iPos + 1] == (byte) 76 && this.fileData[this.iPos + 2] == (byte) 86 && this.fileData[this.iPos + 3] == (byte) 73)
        return;
      this.iPos += 4;
      this.ivl5 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos) == 1 ? new IVL501(this.fileData, this.iPos) : throw new NotSupportedException(string.Format("IVL5 Version {0:x2}", (object) BigEndianBitConverter.ToInt32(this.fileData, this.iPos)));
    }

    private void readGSC2()
    {
      while (this.fileData[this.iPos] != (byte) 50 || this.fileData[this.iPos + 1] != (byte) 67 || this.fileData[this.iPos + 2] != (byte) 83 || this.fileData[this.iPos + 3] != (byte) 71)
        ++this.iPos;
      if (this.fileData[this.iPos] != (byte) 50 && this.fileData[this.iPos + 1] != (byte) 67 && this.fileData[this.iPos + 2] != (byte) 83 && this.fileData[this.iPos + 3] != (byte) 71)
        return;
      this.iPos += 4;
      this.iPos = new GSC2EB(this.fileData, this.iPos).Read(ref this.referencecounter);
    }

    private void readMESH()
    {
      while (this.fileData[this.iPos] != (byte) 72 || this.fileData[this.iPos + 1] != (byte) 83 || this.fileData[this.iPos + 2] != (byte) 69 || this.fileData[this.iPos + 3] != (byte) 77)
        ++this.iPos;
      if (this.fileData[this.iPos] == (byte) 72 && this.fileData[this.iPos + 1] == (byte) 83 && this.fileData[this.iPos + 2] == (byte) 69 && this.fileData[this.iPos + 3] == (byte) 77)
      {
        this.iPos += 4;
        switch (BigEndianBitConverter.ToInt32(this.fileData, this.iPos))
        {
          case 4:
            this.mesh = new MESH04(this.fileData, this.iPos);
            break;
          case 5:
            this.mesh = (MESH04) new MESH05(this.fileData, this.iPos);
            break;
          case 46:
            this.mesh = (MESH04) new MESH2E(this.fileData, this.iPos);
            break;
          case 47:
            this.mesh = (MESH04) new MESH2F(this.fileData, this.iPos);
            break;
          case 48:
            this.mesh = (MESH04) new MESH30(this.fileData, this.iPos);
            break;
          case 169:
            this.mesh = (MESH04) new MESHA9(this.fileData, this.iPos);
            break;
          case 170:
            this.mesh = (MESH04) new MESHAA(this.fileData, this.iPos);
            break;
          case 175:
            this.mesh = (MESH04) new MESHAF(this.fileData, this.iPos);
            break;
          case 200:
            this.mesh = (MESH04) new MESHC8(this.fileData, this.iPos);
            break;
          case 201:
            this.referencecounter = 4;
            this.mesh = (MESH04) new MESHC9(this.fileData, this.iPos);
            break;
          default:
            throw new NotSupportedException(string.Format("MESH Version {0:x2}", (object) BigEndianBitConverter.ToInt32(this.fileData, this.iPos)));
        }
        this.iPos = this.mesh.Read(ref this.referencecounter);
        int num = 0;
        bool flag = true;
        foreach (Part part in this.mesh.Parts)
        {
          flag = false;
          ExtractDx11MESH.CreateObjFile(this.mesh, part, num++);
        }
      }
      else
        ColoredConsole.WriteLine("No MESH");
    }

    private void readTXGH()
    {
      while (this.fileData[this.iPos] != (byte) 72 || this.fileData[this.iPos + 1] != (byte) 71 || this.fileData[this.iPos + 2] != (byte) 88 || this.fileData[this.iPos + 3] != (byte) 84)
        ++this.iPos;
      if (this.fileData[this.iPos] == (byte) 72 && this.fileData[this.iPos + 1] == (byte) 71 && this.fileData[this.iPos + 2] == (byte) 88 && this.fileData[this.iPos + 3] == (byte) 84)
      {
        this.iPos += 4;
        switch (BigEndianBitConverter.ToInt32(this.fileData, this.iPos))
        {
          case 1:
            this.referencecounter = 9;
            this.txgh = new TXGH01(this.fileData, this.iPos);
            break;
          case 3:
            this.referencecounter = 9;
            this.txgh = (TXGH01) new TXGH03(this.fileData, this.iPos);
            break;
          case 4:
            this.referencecounter = 9;
            this.txgh = (TXGH01) new TXGH04(this.fileData, this.iPos);
            break;
          case 5:
            this.referencecounter = 9;
            this.txgh = (TXGH01) new TXGH05(this.fileData, this.iPos);
            break;
          case 6:
            this.referencecounter = 9;
            this.txgh = (TXGH01) new TXGH06(this.fileData, this.iPos);
            break;
          case 7:
            this.referencecounter = 9;
            this.txgh = (TXGH01) new TXGH07(this.fileData, this.iPos);
            break;
          case 8:
            this.referencecounter = 7;
            this.txgh = (TXGH01) new TXGH08(this.fileData, this.iPos);
            break;
          case 9:
            this.referencecounter = 7;
            this.txgh = (TXGH01) new TXGH09(this.fileData, this.iPos);
            break;
          case 10:
            this.referencecounter = 6;
            this.txgh = (TXGH01) new TXGH0A(this.fileData, this.iPos);
            break;
          case 12:
            this.txgh = (TXGH01) new TXGH0C(this.fileData, this.iPos);
            break;
          default:
            throw new NotSupportedException(string.Format("TXGH Version {0:x2}", (object) BigEndianBitConverter.ToInt32(this.fileData, this.iPos)));
        }
        this.iPos = this.txgh.Read(ref this.referencecounter);
      }
      else
        ColoredConsole.WriteLine("No TXGH");
    }

    private void CheckData(Part part)
    {
      bool flag1 = false;
      bool flag2 = false;
      List<Vertex> vertexList1 = (List<Vertex>) null;
      VertexList vertexList2 = this.mesh.Vertexlistsdictionary[part.VertexListReferences1[0]];
      VertexList vertexList3 = (VertexList) null;
      if (part.VertexListReferences1.Count > 1)
        vertexList3 = this.mesh.Vertexlistsdictionary[part.VertexListReferences1[1]];
      List<int> intList = this.mesh.Indexlistsdictionary[part.IndexListReference1];
      Vector3 position;
      if (vertexList2.Vertices[0].Position != null)
      {
        for (int offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
          position = vertexList2.Vertices[offsetVertices].Position;
      }
      else if (vertexList3 != null && vertexList3.Vertices[0].Position != null)
      {
        for (int offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
          position = vertexList3.Vertices[offsetVertices].Position;
      }
      Vector2 uvSet0;
      if (vertexList2.Vertices[0].UVSet0 != null)
      {
        flag2 = true;
        for (int offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
          uvSet0 = vertexList2.Vertices[offsetVertices].UVSet0;
      }
      else if (vertexList3 != null && vertexList3.Vertices[0].UVSet0 != null)
      {
        flag2 = true;
        for (int offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
          uvSet0 = vertexList3.Vertices[offsetVertices].UVSet0;
      }
      Vector3 normal;
      if (vertexList2.Vertices[0].Normal != null)
      {
        flag1 = true;
        for (int offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
          normal = vertexList2.Vertices[offsetVertices].Normal;
      }
      else if (vertexList3 != null && vertexList3.Vertices[0].Normal != null)
      {
        flag1 = true;
        for (int offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
          normal = vertexList3.Vertices[offsetVertices].Normal;
      }
      if (vertexList2.Vertices[0].ColorSet0 != null)
        vertexList1 = vertexList2.Vertices;
      else if (vertexList3 != null && vertexList3.Vertices[0].ColorSet0 != null)
        vertexList1 = vertexList3.Vertices;
    }

    public static void CreateObjFile(MESH04 mesh, Part part, int partnumber)
    {
      bool flag1 = false;
      bool flag2 = false;
      List<Vertex> vertexList1 = (List<Vertex>) null;
      VertexList vertexList2 = mesh.Vertexlistsdictionary[part.VertexListReferences1[0]];
      VertexList vertexList3 = (VertexList) null;
      if (part.VertexListReferences1.Count > 1)
        vertexList3 = mesh.Vertexlistsdictionary[part.VertexListReferences1[1]];
      List<int> intList = (List<int>) null;
      try
      {
        intList = mesh.Indexlistsdictionary[part.IndexListReference1];
      }
      catch (Exception ex)
      {
        ColoredConsole.WriteError("{0} @ Part {1:x4} Index {2:x4}", (object) ex.Message, (object) partnumber, (object) part.IndexListReference1);
      }
      StreamWriter streamWriter1 = new StreamWriter(ExtractDx11MESH.directoryname + "\\" + ExtractDx11MESH.filenamewithoutextension + string.Format("{0:0000}", (object) partnumber) + ".obj");
      streamWriter1.WriteLine("# " + ExtractDx11MESH.filenamewithoutextension);
      if (vertexList2.Vertices[0].Position != null)
      {
        for (int offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
        {
          Vector3 position = vertexList2.Vertices[offsetVertices].Position;
          streamWriter1.WriteLine(string.Format("v {0:0.000000} {1:0.000000} {2:0.000000} ", (object) position.X, (object) position.Y, (object) position.Z).Replace(',', '.'));
        }
      }
      else if (vertexList3 != null && vertexList3.Vertices[0].Position != null)
      {
        for (int offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
        {
          Vector3 position = vertexList3.Vertices[offsetVertices].Position;
          streamWriter1.WriteLine(string.Format("v {0:0.000000} {1:0.000000} {2:0.000000} ", (object) position.X, (object) position.Y, (object) position.Z).Replace(',', '.'));
        }
      }
      if (vertexList2.Vertices[0].UVSet0 != null)
      {
        flag2 = true;
        for (int offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
        {
          Vector2 uvSet0 = vertexList2.Vertices[offsetVertices].UVSet0;
          streamWriter1.WriteLine(string.Format("vt {0:0.000000} {1:0.000000} ", (object) uvSet0.X, (object) uvSet0.Y).Replace(',', '.'));
        }
      }
      else if (vertexList3 != null && vertexList3.Vertices[0].UVSet0 != null)
      {
        flag2 = true;
        for (int offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
        {
          Vector2 uvSet0 = vertexList3.Vertices[offsetVertices].UVSet0;
          streamWriter1.WriteLine(string.Format("vt {0:0.000000} {1:0.000000} ", (object) uvSet0.X, (object) uvSet0.Y).Replace(',', '.'));
        }
      }
      if (vertexList2.Vertices[0].Normal != null)
      {
        flag1 = true;
        for (int offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
        {
          Vector3 normal = vertexList2.Vertices[offsetVertices].Normal;
          streamWriter1.WriteLine(string.Format("vn {0:0.000000} {1:0.000000} {2:0.000000} ", (object) normal.X, (object) normal.Y, (object) normal.Z).Replace(',', '.'));
        }
      }
      else if (vertexList3 != null && vertexList3.Vertices[0].Normal != null)
      {
        flag1 = true;
        for (int offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
        {
          Vector3 normal = vertexList3.Vertices[offsetVertices].Normal;
          streamWriter1.WriteLine(string.Format("vn {0:0.000000} {1:0.000000} {2:0.000000} ", (object) normal.X, (object) normal.Y, (object) normal.Z).Replace(',', '.'));
        }
      }
      if (vertexList2.Vertices[0].ColorSet0 != null)
        vertexList1 = vertexList2.Vertices;
      else if (vertexList3 != null && vertexList3.Vertices[0].ColorSet0 != null)
        vertexList1 = vertexList3.Vertices;
      string format = "f {0} {1} {2}";
      if (flag2)
        format = !flag1 ? "f {0}/{0} {1}/{1} {2}/{2}" : "f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}";
      else if (flag1)
        format = "f {0}//{0} {1}//{1} {2}//{2}";
      streamWriter1.WriteLine("mtllib " + filenamewithoutextension + string.Format("{0:0000}", (object) partnumber) + ".mtl");
      StreamWriter streamWriter2 = new StreamWriter(directoryname + "\\" + filenamewithoutextension + string.Format("{0:0000}", (object) partnumber) + ".mtl");
      List<string> stringList = new List<string>();
      for (int offsetIndices = part.OffsetIndices; offsetIndices < part.OffsetIndices + part.NumberIndices; offsetIndices += 3)
        streamWriter1.WriteLine(string.Format(format, (object) (intList[offsetIndices] - part.NumberVertices), (object) (intList[offsetIndices + 1] - part.NumberVertices), (object) (intList[offsetIndices + 2] - part.NumberVertices)));
      streamWriter2.Close();
      streamWriter1.Close();
    }

    private void WriteIntoObjFile(Part part, StreamWriter streamwriter)
    {
      bool flag1 = false;
      bool flag2 = false;
      List<Vertex> vertexList1 = (List<Vertex>) null;
      VertexList vertexList2 = this.mesh.Vertexlistsdictionary[part.VertexListReferences1[0]];
      VertexList vertexList3 = (VertexList) null;
      if (part.VertexListReferences1.Count > 1)
        vertexList3 = this.mesh.Vertexlistsdictionary[part.VertexListReferences1[1]];
      List<int> intList = this.mesh.Indexlistsdictionary[part.IndexListReference1];
      if (vertexList2.Vertices[0].Position != null)
      {
        for (int offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
        {
          Vector3 position = vertexList2.Vertices[offsetVertices].Position;
          streamwriter.WriteLine(string.Format("v {0:0.000000} {1:0.000000} {2:0.000000} ", (object) position.X, (object) position.Y, (object) position.Z).Replace(',', '.'));
        }
      }
      else if (vertexList3 != null && vertexList3.Vertices[0].Position != null)
      {
        for (int offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
        {
          Vector3 position = vertexList3.Vertices[offsetVertices].Position;
          streamwriter.WriteLine(string.Format("v {0:0.000000} {1:0.000000} {2:0.000000} ", (object) position.X, (object) position.Y, (object) position.Z).Replace(',', '.'));
        }
      }
      if (vertexList2.Vertices[0].UVSet0 != null)
      {
        flag2 = true;
        for (int offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
        {
          Vector2 uvSet0 = vertexList2.Vertices[offsetVertices].UVSet0;
          streamwriter.WriteLine(string.Format("vt {0:0.000000} {1:0.000000} ", (object) uvSet0.X, (object) uvSet0.Y).Replace(',', '.'));
        }
      }
      else if (vertexList3 != null && vertexList3.Vertices[0].UVSet0 != null)
      {
        flag2 = true;
        for (int offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
        {
          Vector2 uvSet0 = vertexList3.Vertices[offsetVertices].UVSet0;
          streamwriter.WriteLine(string.Format("vt {0:0.000000} {1:0.000000} ", (object) uvSet0.X, (object) uvSet0.Y).Replace(',', '.'));
        }
      }
      if (vertexList2.Vertices[0].Normal != null)
      {
        flag1 = true;
        for (int offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
        {
          Vector3 normal = vertexList2.Vertices[offsetVertices].Normal;
          streamwriter.WriteLine(string.Format("vn {0:0.000000} {1:0.000000} {2:0.000000} ", (object) normal.X, (object) normal.Y, (object) normal.Z).Replace(',', '.'));
        }
      }
      else if (vertexList3 != null && vertexList3.Vertices[0].Normal != null)
      {
        flag1 = true;
        for (int offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
        {
          Vector3 normal = vertexList3.Vertices[offsetVertices].Normal;
          streamwriter.WriteLine(string.Format("vn {0:0.000000} {1:0.000000} {2:0.000000} ", (object) normal.X, (object) normal.Y, (object) normal.Z).Replace(',', '.'));
        }
      }
      if (vertexList2.Vertices[0].ColorSet0 != null)
        vertexList1 = vertexList2.Vertices;
      else if (vertexList3 != null && vertexList3.Vertices[0].ColorSet0 != null)
        vertexList1 = vertexList3.Vertices;
      string format = "f {0} {1} {2}";
      if (flag2)
        format = !flag1 ? "f {0}/{0} {1}/{1} {2}/{2}" : "f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}";
      else if (flag1)
        format = "f {0}//{0} {1}//{1} {2}//{2}";
      for (int offsetIndices = part.OffsetIndices; offsetIndices < part.OffsetIndices + part.NumberIndices; offsetIndices += 3)
        streamwriter.WriteLine(string.Format(format, (object) (intList[offsetIndices] - part.NumberVertices), (object) (intList[offsetIndices + 1] - part.NumberVertices), (object) (intList[offsetIndices + 2] - part.NumberVertices)));
    }
  }
}
