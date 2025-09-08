using Metroit.Ddd.EntityFrameworkCore;
using Metroit.Ddd.Presentation.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Test
{
    public class TestDiApp
    {
        private readonly ILogger _logger;
        //private readonly MyDbContext _dbContext;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        //public TestDiApp(ILogger<TestDiApp> logger, MyDbContext dbContext)
        public TestDiApp(ILogger<TestDiApp> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            //_dbContext = dbContext;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Test()
        {
            //var users = _dbContext.Users.ToList();
            //foreach (var user in users)
            //{
            //    Debug.WriteLine(user.UserId);
            //}

            //using (var scope = _serviceScopeFactory.CreateScope())
            //{
            //    var depend = scope.ServiceProvider.GetRequiredService<TestDiAppDependDbContext>();
            //    depend.Test();
            //};

            await _serviceScopeFactory.ExecuteInScopeAsync<TestUserRepository>(async (serviceProvider, service) =>
            {
                service.Test();
                var cnt = await service.Register();
                Debug.WriteLine($"登録件数：{cnt}");
                _ = await service.Update();     // InstantlyClearChangeTracker = true でトラッカーをクリアしてるから簡単に通用する
            });

        }
    }


    public class TestUserRepository : EFRepositoryBase<User, MyDbContext>
    {
        private readonly ILogger _logger;
        private readonly MyDbContext _dbContext;

        public TestUserRepository(ILogger<TestDiApp> logger, MyDbContext dbContext) : base(dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            AllwaysNoTracking = true;
        }

        public void Test()
        {
            var users = _dbContext.Users.ToList();
            foreach (var user in users)
            {
                Debug.WriteLine(user.UserId);
            }
        }

        public async Task<int> Register()
        {
            var users = new List<User>();
            for (var i = 1; i <= 1000; i++)
            {
                var user = new User
                {
                    UserId = $"user_{i:D4}"
                };
                users.Add(user);
            }

            return await AddRangeAsync(users);
        }

        public async Task<int> Update()
        {
            var user = new User() { UserId = "user_0001", CreatedAt = DateTime.Now };
            return await UpdateAsync(user);
        }

        protected override void ExecutingAdd(User entity)
        {
            entity.CreatedAt = DateAndTime.Now;
            Debug.WriteLine("Add 追加中!");
        }
    }
}
