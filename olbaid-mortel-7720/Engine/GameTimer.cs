using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace olbaid_mortel_7720.Engine
{
  /// <summary>
  /// Class that represents a dispatched timer for the game loops.
  /// </summary>
  public class GameTimer
  {
    #region Properties
    private DispatcherTimer _timer;
    private static Dictionary<string, GameTickHandler> _tickHandlers = new Dictionary<string, GameTickHandler>();
    private static uint numOfIntervals = 0;
    
    /// <summary>
    /// Checks if timer is enabled.
    /// </summary>
    public bool IsRunning { get => _timer.IsEnabled; }
    
    /// <summary>
    /// Static instance of the GameTimer.
    /// Please do only use this instance!
    /// </summary>
    public static GameTimer Instance = new GameTimer();
    #endregion Properties

    #region Events
    /// <summary>
    /// Delegate for the Tick event.
    /// </summary>
    public delegate void GameTickHandler(EventArgs e);
    
    /// <summary>
    /// Event that is fired when the timer ticks.
    /// </summary>
    private event GameTickHandler? GameTick;
    
    /// <summary>
    /// Delegate for a callback that displays the current progress of the interval.
    /// </summary>
    public delegate void IntervalProgressHandler(double progress);
    #endregion Events
    
    #region Constructor
    /// <summary>
    /// Private constructor to prevent multiple instances.
    /// Please use the static Instance property instead!
    /// </summary>
    private GameTimer()
    {
      _timer = new DispatcherTimer();
      _timer.Interval = TimeSpan.FromMilliseconds(20);
      _timer.Tick += new EventHandler(TimerTick);
      _timer.Start();
    }
    #endregion Constructor

    #region Methods
    private void TimerTick(object? sender, EventArgs e)
    {
      if (GameTick != null)
        GameTick(e);
    }

    /// <summary>
    /// Start the timer (if it is not already running).
    /// </summary>
    public void Start()
    {
      _timer.Start();
    }

    /// <summary>
    /// Stop the timer (if it is running).
    /// </summary>
    public void Stop()
    {
      _timer.Stop();
    }

    /// <summary>
    /// Execute a task in a interval of game ticks.
    /// </summary>
    /// <param name="interval">Interval of ticks</param>
    /// <param name="callback">Task to be fired</param>
    /// <param name="removeAfterExecution">Task should be deleted after first execution</param>
    /// <returns>the id of the interval callback</returns>
    public static string ExecuteWithInterval(int interval, GameTickHandler callback, bool removeAfterExecution = false)
    {
      return ExecuteWithInterval(interval, callback, null, removeAfterExecution);
    }
    
    /// <summary>
    /// Execute a task in a interval of game ticks.
    /// </summary>
    /// <param name="interval">Interval of ticks</param>
    /// <param name="callback">Task to be fired</param>
    /// <param name="progress">Progress of the interval</param>
    /// <param name="removeAfterExecution">Task should be deleted after first execution</param>
    /// <returns>the id of the interval callback</returns>
    public static string ExecuteWithInterval(int interval, GameTickHandler callback, IntervalProgressHandler? progress, bool removeAfterExecution = false)
    {
      numOfIntervals++;
      string internalNumOfIntervals = $"interval-{numOfIntervals}-{callback.Target}";
      int counter = 0;
      GameTimer timer = new GameTimer();
      GameTickHandler handler = delegate (EventArgs e)
      {
        if (counter >= interval)
        {
          callback(e);
          counter = 0;

          if (removeAfterExecution)
            timer.RemoveByName(internalNumOfIntervals);
        }
        else
        {
          counter++;
        }

        if (progress != null)
          progress((double)counter / interval);
      };
      timer.Execute(handler, internalNumOfIntervals);
      return internalNumOfIntervals;
    }
    
    /// <summary>
    /// Add a task to the timer.
    /// </summary>
    /// <param name="callback">the method to add</param>
    /// <param name="name">a name for the dictionary key</param>
    public void Execute(GameTickHandler callback, string? name)
    {
      string key = name ?? callback.Target?.ToString() + Guid.NewGuid();
      _tickHandlers.Add(key, callback);
      GameTick += _tickHandlers[key];
    }
    
    /// <summary>
    /// Removes a method from the timer.
    /// </summary>
    /// <param name="name">the dictionary key to remove</param>
    /// <returns>if the callback could be removed</returns>
    public bool RemoveByName(string name)
    {
      if (_tickHandlers.ContainsKey(name))
      {
        Remove(_tickHandlers[name]);
        _tickHandlers.Remove(name);
        return true;
      }
      return false;
    }
    
    /// <summary>
    /// Removes a method from the timer.
    /// </summary>
    /// <param name="callback">the method to remove</param>
    public void Remove(GameTickHandler callback)
    {
      GameTick -= callback;
      if (_tickHandlers.ContainsKey(callback.Method.Name))
      {
        _tickHandlers.Remove(callback.Method.Name);
      }
    }
    
    /// <summary>
    /// Cleans up the timer.
    /// </summary>
    public void CleanUp()
    {
      List<string> keys = new List<string>(_tickHandlers.Keys);
      foreach (string key in keys)
      {
        RemoveByName(key);
      }
    }
    #endregion Methods
  }
}