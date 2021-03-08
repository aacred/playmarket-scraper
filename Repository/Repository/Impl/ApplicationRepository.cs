using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repository.Impl
{
    public class ApplicationRepository : Repository<Application>, IApplicationRepository
    {
        public ApplicationRepository(ApplicationContext db) : base(db)
        {
        }

        public async Task AddDetails(Application application, ApplicationDetails details)
        {
            try
            {
                var app = _db.Applications
                    .SingleOrDefault(x => x.ApplicationId == application.ApplicationId);

                app?.Details.Add(details);
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            
        }
    }
}