// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: PointField.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from PointField.proto</summary>
public static partial class PointFieldReflection {

  #region Descriptor
  /// <summary>File descriptor for PointField.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static PointFieldReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "ChBQb2ludEZpZWxkLnByb3RvIksKClBvaW50RmllbGQSDAoEbmFtZRgBIAEo",
          "CRIOCgZvZmZzZXQYAiABKAUSEAoIZGF0YXR5cGUYAyABKAUSDQoFY291bnQY",
          "BCABKAViBnByb3RvMw=="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::PointField), global::PointField.Parser, new[]{ "Name", "Offset", "Datatype", "Count" }, null, null, null)
        }));
  }
  #endregion

}
#region Messages
public sealed partial class PointField : pb::IMessage<PointField> {
  private static readonly pb::MessageParser<PointField> _parser = new pb::MessageParser<PointField>(() => new PointField());
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<PointField> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::PointFieldReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public PointField() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public PointField(PointField other) : this() {
    name_ = other.name_;
    offset_ = other.offset_;
    datatype_ = other.datatype_;
    count_ = other.count_;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public PointField Clone() {
    return new PointField(this);
  }

  /// <summary>Field number for the "name" field.</summary>
  public const int NameFieldNumber = 1;
  private string name_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string Name {
    get { return name_; }
    set {
      name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "offset" field.</summary>
  public const int OffsetFieldNumber = 2;
  private int offset_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int Offset {
    get { return offset_; }
    set {
      offset_ = value;
    }
  }

  /// <summary>Field number for the "datatype" field.</summary>
  public const int DatatypeFieldNumber = 3;
  private int datatype_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int Datatype {
    get { return datatype_; }
    set {
      datatype_ = value;
    }
  }

  /// <summary>Field number for the "count" field.</summary>
  public const int CountFieldNumber = 4;
  private int count_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int Count {
    get { return count_; }
    set {
      count_ = value;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as PointField);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(PointField other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Name != other.Name) return false;
    if (Offset != other.Offset) return false;
    if (Datatype != other.Datatype) return false;
    if (Count != other.Count) return false;
    return true;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (Name.Length != 0) hash ^= Name.GetHashCode();
    if (Offset != 0) hash ^= Offset.GetHashCode();
    if (Datatype != 0) hash ^= Datatype.GetHashCode();
    if (Count != 0) hash ^= Count.GetHashCode();
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (Name.Length != 0) {
      output.WriteRawTag(10);
      output.WriteString(Name);
    }
    if (Offset != 0) {
      output.WriteRawTag(16);
      output.WriteInt32(Offset);
    }
    if (Datatype != 0) {
      output.WriteRawTag(24);
      output.WriteInt32(Datatype);
    }
    if (Count != 0) {
      output.WriteRawTag(32);
      output.WriteInt32(Count);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (Name.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
    }
    if (Offset != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(Offset);
    }
    if (Datatype != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(Datatype);
    }
    if (Count != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(Count);
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(PointField other) {
    if (other == null) {
      return;
    }
    if (other.Name.Length != 0) {
      Name = other.Name;
    }
    if (other.Offset != 0) {
      Offset = other.Offset;
    }
    if (other.Datatype != 0) {
      Datatype = other.Datatype;
    }
    if (other.Count != 0) {
      Count = other.Count;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          input.SkipLastField();
          break;
        case 10: {
          Name = input.ReadString();
          break;
        }
        case 16: {
          Offset = input.ReadInt32();
          break;
        }
        case 24: {
          Datatype = input.ReadInt32();
          break;
        }
        case 32: {
          Count = input.ReadInt32();
          break;
        }
      }
    }
  }

}

#endregion


#endregion Designer generated code