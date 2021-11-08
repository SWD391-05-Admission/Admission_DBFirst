
using Admission.Data.IRepository;
using Admission.Data.Models;
using Admission.Data.Models.Context;
using EasyCronJob.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Admission.Bussiness.Service
{
    public class FinishTalkshowCronJob : CronJobService
    {
        private readonly ILogger<FinishTalkshowCronJob> logger;

        public FinishTalkshowCronJob(ICronConfiguration<FinishTalkshowCronJob> cronConfiguration, ILogger<FinishTalkshowCronJob> logger)
            : base(cronConfiguration.CronExpression, cronConfiguration.TimeZoneInfo)
        {
            this.logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Start");
            return base.StartAsync(cancellationToken);
        }


        protected override Task ScheduleJob(CancellationToken cancellationToken)
        {
            logger.LogInformation("Scheduled");
            return base.ScheduleJob(cancellationToken);
        }

        public override Task DoWork(CancellationToken cancellationToken)
        {
            logger.LogInformation("Do Work");
            using (var dbContext = new AdmissionsDBContext())
            {
                var talkshows = dbContext.Talkshows.ToList();
                if (talkshows != null)
                {
                    foreach (Talkshow talkshow in talkshows)
                    {
                        if (!talkshow.IsFinish && DateTime.Now >= talkshow.StartDate)
                        {
                            talkshow.IsFinish = true;
                        }
                    }
                    dbContext.SaveChanges();
                }
            }
            return base.DoWork(cancellationToken);
        }
    }
}
