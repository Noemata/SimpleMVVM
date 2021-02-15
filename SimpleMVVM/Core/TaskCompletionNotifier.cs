﻿using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleMVVM.Core.Threading
{
    /// <summary>
    /// Watches a task and raises property-changed notifications when the task completes.
    /// </summary>
    /// <typeparam name="TResult">The type of the result of the task.</typeparam>
    public sealed class TaskCompletionNotifier<TResult> : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets the task being watched. This property never changes and is never <c>null</c>.
        /// </summary>
        public Task<TResult> Task { get; private set; }

        /// <summary>
        /// Gets the result of the task. Returns the default value of TResult if the task has not completed successfully.
        /// </summary>
        public TResult Result => (Task != null && Task.Status == TaskStatus.RanToCompletion) ? Task.Result : default(TResult);

        /// <summary>
        /// Gets whether the task has completed.
        /// </summary>
        public bool IsCompleted => Task == null ? true : Task.IsCompleted;

        /// <summary>
        /// Gets whether the task has completed successfully.
        /// </summary>
        public bool IsSuccessfullyCompleted => Task == null || Task.Status == TaskStatus.RanToCompletion;

        /// <summary>
        /// Gets whether the task has been canceled.
        /// </summary>
        public bool IsCanceled => Task == null ? false : Task.IsCanceled;

        /// <summary>
        /// Gets whether the task has faulted.
        /// </summary>
        public bool IsFaulted => Task == null ? false : Task.IsFaulted;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initialize a new instance of the <see cref="TaskCompletionNotifier{TResult}"/> class.
        /// </summary>
        /// <param name="task">The <see cref="Task"/> to run.</param>
        public TaskCompletionNotifier(Task<TResult> task)
        {
            try
            {
                Task = task;
                if (task != null && !task.IsCompleted)
                {
                    var scheduler = (SynchronizationContext.Current == null) ? TaskScheduler.Current : TaskScheduler.FromCurrentSynchronizationContext();
                    task.ContinueWith(t =>
                    {
                        var propertyChanged = PropertyChanged;
                        if (propertyChanged != null)
                        {
                            propertyChanged(this, new PropertyChangedEventArgs(nameof(IsCompleted)));
                            if (t.IsCanceled)
                            {
                                propertyChanged(this, new PropertyChangedEventArgs(nameof(IsCanceled)));
                            }
                            else if (t.IsFaulted)
                            {
                                propertyChanged(this, new PropertyChangedEventArgs(nameof(IsFaulted)));
                            }
                            else
                            {
                                propertyChanged(this, new PropertyChangedEventArgs(nameof(IsSuccessfullyCompleted)));
                                propertyChanged(this, new PropertyChangedEventArgs(nameof(Result)));
                            }
                        }
                    },
                    CancellationToken.None,
                    TaskContinuationOptions.ExecuteSynchronously,
                    scheduler);
                }
            }
            catch
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsFaulted)));
            }
        }
    }
}
