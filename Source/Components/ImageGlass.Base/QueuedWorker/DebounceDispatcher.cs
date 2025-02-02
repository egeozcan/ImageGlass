/*
ImageGlass Project - Image viewer for Windows
Copyright (C) 2010 - 2025 DUONG DIEU PHAP
Project homepage: https://imageglass.org

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
using System.Windows.Threading;

namespace ImageGlass.Base.QueuedWorker;


/// <summary>
/// Source: <see href="https://weblog.west-wind.com/posts/2017/Jul/02/Debouncing-and-Throttling-Dispatcher-Events"/>
/// <br/>
/// Author: <c>Rick Strahl</c>.
/// </summary>
public class DebounceDispatcher
{
    private DispatcherTimer? _timer;
    private DateTime _timerStarted = DateTime.UtcNow.AddYears(-1);


    /// <summary>
    /// Takes the last called action, delays the execution after a certain amount of time has passed.
    /// </summary>
    /// <param name="interval">Timeout in Milliseconds</param>
    /// <param name="action">Action<object> to fire when debounced event fires</object></param>
    /// <param name="param">optional parameter</param>
    /// <param name="priority">optional priorty for the dispatcher</param>
    /// <param name="disp">optional dispatcher. If not passed or null CurrentDispatcher is used.</param>
    public void Debounce<T>(int interval, Action<T> action, T? param = default,
        DispatcherPriority priority = DispatcherPriority.ApplicationIdle,
        Dispatcher? disp = null)
    {
        // kill pending timer and pending ticks
        _timer?.Stop();
        _timer = null;

        if (disp == null)
            disp = Dispatcher.CurrentDispatcher;

        // timer is recreated for each event and effectively
        // resets the timeout. Action only fires after timeout has fully
        // elapsed without other events firing in between
        _timer = new DispatcherTimer(TimeSpan.FromMilliseconds(interval), priority, (s, e) =>
        {
            if (_timer == null) return;

            _timer?.Stop();
            _timer = null;

            action.Invoke(param);
        }, disp);

        _timer.Start();
    }


    /// <summary>
    /// Throttles events by allowing only 1 event to fire for the given
    /// timeout period. Only the last event fired is handled - all others are ignored.
    /// Throttle will fire events every timeout ms even if additional events are pending.
    /// </summary>
    /// <param name="interval">Timeout in Milliseconds</param>
    /// <param name="action">Action<object> to fire when debounced event fires</object></param>
    /// <param name="param">optional parameter</param>
    /// <param name="priority">optional priorty for the dispatcher</param>
    /// <param name="disp">optional dispatcher. If not passed or null CurrentDispatcher is used.</param>
    public void Throttle<T>(int interval, Action<T> action, T? param = default,
        DispatcherPriority priority = DispatcherPriority.ApplicationIdle,
        Dispatcher? disp = null)
    {
        // kill pending timer and pending ticks
        _timer?.Stop();
        _timer = null;

        if (disp == null)
            disp = Dispatcher.CurrentDispatcher;

        var curTime = DateTime.UtcNow;

        // if timeout is not up yet - adjust timeout to fire 
        // with potentially new Action parameters           
        if (curTime.Subtract(_timerStarted).TotalMilliseconds < interval)
        {
            interval -= (int)curTime.Subtract(_timerStarted).TotalMilliseconds;
        }

        _timer = new DispatcherTimer(TimeSpan.FromMilliseconds(interval), priority, (s, e) =>
        {
            if (_timer == null)
                return;

            _timer?.Stop();
            _timer = null;

            action.Invoke(param);
        }, disp);

        _timer.Start();
        _timerStarted = curTime;
    }
}
