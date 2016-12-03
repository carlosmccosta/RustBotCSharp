// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: Twist.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from Twist.proto</summary>
public static partial class TwistReflection {

  #region Descriptor
  /// <summary>File descriptor for Twist.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static TwistReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "CgtUd2lzdC5wcm90bxoNVmVjdG9yMy5wcm90byI8CgVUd2lzdBIYCgZsaW5l",
          "YXIYASABKAsyCC5WZWN0b3IzEhkKB2FuZ3VsYXIYAiABKAsyCC5WZWN0b3Iz",
          "YgZwcm90bzM="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { global::Vector3Reflection.Descriptor, },
        new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::Twist), global::Twist.Parser, new[]{ "Linear", "Angular" }, null, null, null)
        }));
  }
  #endregion

}
#region Messages
public sealed partial class Twist : pb::IMessage<Twist> {
  private static readonly pb::MessageParser<Twist> _parser = new pb::MessageParser<Twist>(() => new Twist());
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<Twist> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::TwistReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public Twist() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public Twist(Twist other) : this() {
    Linear = other.linear_ != null ? other.Linear.Clone() : null;
    Angular = other.angular_ != null ? other.Angular.Clone() : null;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public Twist Clone() {
    return new Twist(this);
  }

  /// <summary>Field number for the "linear" field.</summary>
  public const int LinearFieldNumber = 1;
  private global::Vector3 linear_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public global::Vector3 Linear {
    get { return linear_; }
    set {
      linear_ = value;
    }
  }

  /// <summary>Field number for the "angular" field.</summary>
  public const int AngularFieldNumber = 2;
  private global::Vector3 angular_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public global::Vector3 Angular {
    get { return angular_; }
    set {
      angular_ = value;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as Twist);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(Twist other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (!object.Equals(Linear, other.Linear)) return false;
    if (!object.Equals(Angular, other.Angular)) return false;
    return true;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (linear_ != null) hash ^= Linear.GetHashCode();
    if (angular_ != null) hash ^= Angular.GetHashCode();
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (linear_ != null) {
      output.WriteRawTag(10);
      output.WriteMessage(Linear);
    }
    if (angular_ != null) {
      output.WriteRawTag(18);
      output.WriteMessage(Angular);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (linear_ != null) {
      size += 1 + pb::CodedOutputStream.ComputeMessageSize(Linear);
    }
    if (angular_ != null) {
      size += 1 + pb::CodedOutputStream.ComputeMessageSize(Angular);
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(Twist other) {
    if (other == null) {
      return;
    }
    if (other.linear_ != null) {
      if (linear_ == null) {
        linear_ = new global::Vector3();
      }
      Linear.MergeFrom(other.Linear);
    }
    if (other.angular_ != null) {
      if (angular_ == null) {
        angular_ = new global::Vector3();
      }
      Angular.MergeFrom(other.Angular);
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
          if (linear_ == null) {
            linear_ = new global::Vector3();
          }
          input.ReadMessage(linear_);
          break;
        }
        case 18: {
          if (angular_ == null) {
            angular_ = new global::Vector3();
          }
          input.ReadMessage(angular_);
          break;
        }
      }
    }
  }

}

#endregion


#endregion Designer generated code
