using System;
using System.Collections.Generic;
using System.Linq;
using LexiconLMS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LexiconLMS.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var options = serviceProvider.GetRequiredService<DbContextOptions<LexiconLMSContext>>();
            using (var context = new LexiconLMSContext(options))
            {
                if (context.ActivityType.Any())
                {
                    return;

                }

                // Let's seed!
                var ActivityTypes = new List<ActivityType>()
                    { new ActivityType() { Type = "E-Learning"},
                    new ActivityType() { Type = "Lectures"},
                    new ActivityType() { Type = "Exercise" },
                    };
                //people.Add(person);

                context.ActivityType.AddRange(ActivityTypes);
                context.SaveChanges();
            }
        }

    }
}
