// --- @@Intrinsics.cs ---
using System;
namespace Metalama.Compiler
{
  internal static class Intrinsics
  {
    public static RuntimeMethodHandle GetRuntimeMethodHandle(string documentationId) => throw new InvalidOperationException("Code calling this method has to be compiled by the Metalama compiler.");
    public static RuntimeFieldHandle GetRuntimeFieldHandle(string documentationId) => throw new InvalidOperationException("Code calling this method has to be compiled by the Metalama compiler.");
    public static RuntimeTypeHandle GetRuntimeTypeHandle(string documentationId) => throw new InvalidOperationException("Code calling this method has to be compiled by the Metalama compiler.");
  }
} // --- EnumViewModel.Aspect.cs ---
using  Metalama . Framework . Advising ;  using  Metalama . Framework . Aspects ;  using  Metalama . Framework . Code ;  using  System ;  using  System . Linq ;
namespace Doc.EnumViewModel;
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
public class EnumViewModelAttribute : CompilationAspect
{
  private readonly Type _enumType;
  private readonly string _targetNamespace;
  public EnumViewModelAttribute(Type enumType, string targetNamespace)
  {
    this._enumType = enumType;
    this._targetNamespace = targetNamespace;
  }
  public override void BuildAspect(IAspectBuilder<ICompilation> builder) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
  [Template]
  public void ConstructorTemplate([CompileTime] IField underlyingValueField) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
  [Template]
  public bool IsEnumValue([CompileTime] IField enumMember, [CompileTime] IField underlyingValueField) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
} // --- EnumViewModel.cs ---
using  System ;  using  Doc . EnumViewModel ;  [ assembly :  EnumViewModel ( typeof ( DayOfWeek ) ,  "Doc.EnumViewModel" ) ]  // --- Doc.EnumViewModel.DayOfWeekViewModel.cs ---
using  System ;
namespace Doc.EnumViewModel
{
  class DayOfWeekViewModel : object
  {
    private readonly DayOfWeek _value;
    public DayOfWeekViewModel(DayOfWeek underlying)
    {
      _value = underlying;
    }
    public bool IsFriday
    {
      get
      {
        return _value == DayOfWeek.Friday;
      }
    }
    public bool IsMonday
    {
      get
      {
        return _value == DayOfWeek.Monday;
      }
    }
    public bool IsSaturday
    {
      get
      {
        return _value == DayOfWeek.Saturday;
      }
    }
    public bool IsSunday
    {
      get
      {
        return _value == DayOfWeek.Sunday;
      }
    }
    public bool IsThursday
    {
      get
      {
        return _value == DayOfWeek.Thursday;
      }
    }
    public bool IsTuesday
    {
      get
      {
        return _value == DayOfWeek.Tuesday;
      }
    }
    public bool IsWednesday
    {
      get
      {
        return _value == DayOfWeek.Wednesday;
      }
    }
  }
}