namespace BulletinBoard.Services.Hangfire
{
    using BulletinBoard.Data;
    using BulletinBoard.Data.Enums;
    using global::Hangfire;
    using Microsoft.Extensions.DependencyInjection;

    public class PostHangfireService
    {
        private readonly AppDbContext _context;

        public PostHangfireService(AppDbContext context)
        {
            _context = context;
        }

        public void CheckPost()
        {
            RecurringJob.AddOrUpdate(() => CheckPostExpiriedDate(), Cron.Minutely());
        }

        public  async Task CheckPostExpiriedDate()
        {
            var posts = _context.Posts.Where(x => x.IsEnable == true).Where(x => x.ExpiredDate < DateTime.UtcNow);

            foreach (var post in posts)
            {
                post.IsEnable = false;
                post.PostStatusId = (int)PostStatuses.Inactive;
            }

           await _context.SaveChangesAsync();
        }
    }
}
