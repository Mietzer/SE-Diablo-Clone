using System;

namespace olbaid_mortel_7720.Helper
{
  /// <summary>
  /// Union of two types
  /// </summary>
  /// <typeparam name="T">Type 1</typeparam>
  /// <typeparam name="S">Type 2</typeparam>
  public class Union<T, S>
  {
    private readonly T _item1;
    private readonly S _item2;
    private readonly uint _tag;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="item">Item with type 1</param>
    public Union(T item)
    {
      _item1 = item;
      _tag = 1;
    }
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="item">Item with type 2</param>
    public Union(S item)
    {
      _item2 = item;
      _tag = 2;
    }
    
    /// <summary>
    /// Apply actions for the different types
    /// </summary>
    /// <param name="action1">Action for type 1</param>
    /// <param name="action2">Action for type 2</param>
    /// <exception cref="InvalidOperationException"></exception>
    public void Switch(Action<T> action1, Action<S> action2)
    {
      switch (_tag)
      {
        case 1:
          action1(_item1);
          break;
        case 2:
          action2(_item2);
          break;
        default:
          throw new InvalidOperationException();
      }
    }
    
    /// <summary>
    /// Return functions for the different types
    /// </summary>
    /// <param name="func1">Function for type 1</param>
    /// <param name="func2">Function for type 2</param>
    /// <typeparam name="TResult">Type of the result</typeparam>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public TResult Switch<TResult>(Func<T, TResult> func1, Func<S, TResult> func2)
    {
      switch (_tag)
      {
        case 1:
          return func1(_item1);
        case 2:
          return func2(_item2);
        default:
          throw new InvalidOperationException();
      }
    }
  }
}