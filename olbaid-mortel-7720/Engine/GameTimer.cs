using System;
using System.Windows.Threading;

namespace olbaid_mortel_7720.Engine
{
  /// <summary>
  /// Class that represents a dispatched timer for the game loops.
  /// </summary>
  public class GameTimer
  {
    private DispatcherTimer _timer;

    /// <summary>
    /// Delegate for the Tick event.
    /// </summary>
    public delegate void GameTickHandler(EventArgs e);

    /// <summary>
    /// Event that is fired when the timer ticks.
    /// </summary>
    public event GameTickHandler? GameTick;

    public bool IsRunning { get => _timer.IsEnabled; }

    /// <summary>
    /// Static instance of the GameTimer.
    /// Please do only use this instance!
    /// </summary>
    public static GameTimer Instance = new GameTimer();

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
    public static void ExecuteWithInterval(int interval, GameTickHandler callback, bool removeAfterExecution = false)
    {
      ExecuteWithInterval(interval, callback, null, removeAfterExecution);
    }

    /// <summary>
    /// Delegate for a callback that displays the current progress of the interval.
    /// </summary>
    public delegate void IntervalProgressHandler(double progress);

    /// <summary>
    /// Execute a task in a interval of game ticks.
    /// </summary>
    /// <param name="interval">Interval of ticks</param>
    /// <param name="callback">Task to be fired</param>
    /// <param name="progress">Progress of the interval</param>
    public static void ExecuteWithInterval(int interval, GameTickHandler callback, IntervalProgressHandler? progress, bool removeAfterExecution = false)
    {
      int counter = 0;
      GameTimer timer = new GameTimer();
      GameTickHandler? handler = null;
      handler = delegate (EventArgs e)
      {
        if (counter >= interval)
        {
          callback(e);
          counter = 0;

          if (removeAfterExecution && handler != null)
            timer.GameTick -= handler;
        }
        else
        {
          counter++;
        }

        if (progress != null)
          progress((double)counter / interval);
      };
      timer.GameTick += handler;
    }
  }
}