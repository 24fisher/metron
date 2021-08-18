using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Metron
{
    internal class TimerWin32Wrapper : IDisposable
    {
        public delegate void MmTimerCallback(uint id, uint msg, ref uint userCtx, uint rsv1, uint rsv2);
        private readonly MmTimerCallback _callback;
        private bool _disposed;
        private bool _enabled;
        private int _interval;
        private uint _timerId;
        private int _resolution;

        public TimerWin32Wrapper()
        {
            _callback = TimerCallbackMethod;
            Resolution = 0;//0 - max res
            Interval = 10;
        }
        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                if (value)
                {
                    Start();
                }
                else
                {
                    Stop();
                }
            }
        }
        public int Interval
        {
            get => _interval;
            set
            {
                if (_disposed)
                    throw new ObjectDisposedException("Timer has been disposed.");
                if (value < 0)
                    throw new Exception("Interval must be greater then 0.");
                _interval = value;
                
                if (Resolution > Interval)
                    Resolution = value;
            }
        }
        public bool IsRunning => _timerId != 0;

        // Note minimum resolution is 0, meaning highest possible resolution.
        public int Resolution
        {
            get
            {
                return _resolution;
            }
            set
            {
                CheckDisposed();

                if (value < 0)
                    throw new ArgumentOutOfRangeException("value");

                _resolution = value;
            }
        }
        private void CheckDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException("MultimediaTimer");
        }

        public void Dispose()
        {
            Dispose(true);
        }
        public event EventHandler Elapsed;
        ~TimerWin32Wrapper()
        {
            Dispose(false);
        }
        public void Start()
        {
            if (_disposed)
                throw new ObjectDisposedException("Timer has been disposed.");
            if (IsRunning)
                throw new InvalidOperationException("Timer is already running.");
            uint user = 0;
            _timerId = TimeSetEvent((uint)Interval, (uint)Resolution, _callback, ref user, 1);
            if (_timerId != 0)
                return;
            var error = Marshal.GetLastWin32Error();
            throw new Win32Exception(error);
        }
        public void Stop()
        {
            if (_disposed)
                throw new ObjectDisposedException("Timer is already running.");
            if (!IsRunning)
                throw new InvalidOperationException("Timer has not been started.");
            TimeKillEvent(_timerId);
            _timerId = 0;
        }
        private void TimerCallbackMethod(uint id, uint msg, ref uint user, uint rsv1, uint rsv2)
        {
            Elapsed?.Invoke(this, EventArgs.Empty);
        }
        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            _disposed = true;
            if (IsRunning)
            {
                TimeKillEvent(_timerId);
                _timerId = 0;
            }
            if (disposing)
            {
                Elapsed = null;
                GC.SuppressFinalize(this);
            }
        }
        [DllImport("winmm.dll", SetLastError = true, EntryPoint = "timeSetEvent")]
        public static extern uint TimeSetEvent(uint msDelay, uint msResolution, MmTimerCallback callback, ref uint user, uint eventType);
        [DllImport("winmm.dll", SetLastError = true, EntryPoint = "timeKillEvent")]
        public static extern void TimeKillEvent(uint uTimerId);
    }
}
