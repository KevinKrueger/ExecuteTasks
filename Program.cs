using System;
using System.Collections.Generic;

namespace ExecuteTasks
{
    class Program
    {
        public static List<Task> tasks = new List<Task>();

        static void Main(string[] args)
        {
            tasks.Add(new Task(){TaskID = Guid.NewGuid().GetHashCode().ToString(), isTime = false});
            tasks.Add(new Task(){TaskID = Guid.NewGuid().GetHashCode().ToString(), isTime = true});
            
            int count = 0;
            System.Threading.Tasks.Parallel.ForEach(tasks, () => 0, (item, loopState, localID)=> {
              
                CalculationTask  task = new CalculationTask();
                task.InitTask(item);
                Console.WriteLine("Thread={0}, item={1}, TaskID={2}", System.Threading.Thread.CurrentThread.ManagedThreadId, item, ((Task)item).TaskID);
                return localID;
            },
            (localID) => System.Threading.Interlocked.Add(ref count, localID));
        }
    }

    public  class Task
    {
        public string TaskID;

        public bool isTime;
    }

    class CalculationTask
    {
        // FUNCTIONS
        public void InitTask(Task task)
        {
           StartTask(task);
        }

        private void StartTask(Task task)
        {
            if(task.isTime)
                System.Threading.Thread.Sleep(5000);
            else
                System.Threading.Thread.Sleep(10000);

            Console.WriteLine("Start Task: " + task.TaskID);
            
        }
    }
}

