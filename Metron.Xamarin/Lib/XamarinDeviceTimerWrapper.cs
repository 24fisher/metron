using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;


namespace Metron
{
    public class XamarinDeviceTimerWrapper
    {
        readonly Action _task;
        readonly List<TaskWrapper> _tasks = new List<TaskWrapper>();
        readonly TimeSpan _interval;
        public bool IsRecurring { get; }
        public bool IsRunning => _tasks.Any(t => t.IsRunning);

        public XamarinDeviceTimerWrapper(Action task, TimeSpan interval,
            bool isRecurring = false, bool start = false)
        {
            _task = task;
            _interval = interval;
            IsRecurring = isRecurring;
            if (start)
                Start();
        }

        public void Restart()
        {
            Stop();
            Start();
        }

        public void Start()
        {
            if (IsRunning)
                // Already Running
                return;

            var wrapper = new TaskWrapper(_task, IsRecurring, true);
            _tasks.Add(wrapper);

            Device.StartTimer(_interval, wrapper.RunTask);
        }

        public void Stop()
        {
            foreach (var task in _tasks)
                task.IsRunning = false;
            _tasks.Clear();
        }


        class TaskWrapper
        {
            public bool IsRunning { get; set; }
            bool _isRecurring;
            Action _task;
            public TaskWrapper(Action task, bool isRecurring, bool isRunning)
            {
                _task = task;
                _isRecurring = isRecurring;
                IsRunning = isRunning;
            }

            public bool RunTask()
            {
                if (IsRunning)
                {
                    _task();
                    if (_isRecurring)
                        return true;
                }

                // No longer need to recur. Stop
                return IsRunning = false;
            }
        }
    }
}