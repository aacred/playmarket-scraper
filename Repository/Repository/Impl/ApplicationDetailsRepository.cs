namespace Domain.Repository.Impl
{
    public class ApplicationDetailsRepository : Repository<ApplicationDetails>, IApplicationDetailsRepository
    {
        public ApplicationDetailsRepository(ApplicationContext db) : base(db)
        {
        }
    }
}