using System;
using System.Configuration;
using System.IO;
using log4net;
using Quartz;
using Topshelf;
using Topshelf.Quartz;

namespace RemoveFileWinService
{
    class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            HostFactory.Run(c => c.Service<ContainerService>(s =>
            {
                s.ConstructUsing(name => new ContainerService());
                s.WhenStarted((service, control) => service.Start());
                s.WhenStopped((service, control) => service.Stop());
                s.ScheduleQuartzJob(q =>
                    q.WithJob(() =>
                        JobBuilder.Create<MonthlyRemoveFileJob>().Build())
                        .AddTrigger(() =>
                            TriggerBuilder.Create()
                            //.WithSimpleSchedule(x => x.WithIntervalInSeconds(50000).RepeatForever())
                                .WithSchedule(CronScheduleBuilder.MonthlyOnDayAndHourAndMinute(1, 0, 0))
                                .Build())
                    );
                c.RunAsLocalSystem();
                c.SetDisplayName("RemoveFileWinService");
                c.SetDescription("Remove files Win Service by using Topshelf");
                c.SetServiceName("RemoveFileWinService");
            }));
        }

        class ContainerService
        {
            public bool Start()
            {
                return true;
            }
            public bool Stop()
            {
                return true;
            }
        }

        public class MonthlyRemoveFileJob : IJob
        {
            public void Execute(IJobExecutionContext context)
            {
                try
                {
                    var folderPath = ConfigurationManager.AppSettings["ContainerFolderPath"];
                    var fileExtension = ConfigurationManager.AppSettings["FileExtension"];
                    var noOlderThan = Convert.ToInt32(ConfigurationManager.AppSettings["NoOlderThan"]);
                    string[] filePaths = Directory.GetFiles(folderPath, fileExtension);
                    foreach (var filePath in filePaths)
                    {
                        if ((DateTime.Now - File.GetCreationTime(filePath)).TotalDays > noOlderThan)
                        {
                            File.Delete(filePath);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }
        }
    }
}
