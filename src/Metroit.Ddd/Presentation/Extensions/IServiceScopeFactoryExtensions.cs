using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Metroit.Ddd.Presentation.Extensions
{
    /// <summary>
    /// <see cref="IServiceScopeFactory"/> の拡張メソッドを提供します。
    /// </summary>
    public static class IServiceScopeFactoryExtensions
    {
        /// <summary>
        /// スコープの中でDI注入したオブジェクトのアクションを実行します。
        /// </summary>
        /// <typeparam name="T">注入するインターフェースやクラス。</typeparam>
        /// <param name="serviceScopeFactory">サービス作成ファクトリ。</param>
        /// <param name="action">注入したインターフェースやクラスを利用したアクション。</param>
        /// <exception cref="ArgumentNullException"><paramref name="serviceScopeFactory"/> または <paramref name="action"/> が <see langword="null"/> です。</exception>
        public static void ExecuteInScope<T>(this IServiceScopeFactory serviceScopeFactory, Action<IServiceProvider, T> action)
            where T : class
        {
            if (serviceScopeFactory == null)
            {
                throw new ArgumentNullException(nameof(serviceScopeFactory));
            }
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            using (var scope = serviceScopeFactory.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<T>();
                action(scope.ServiceProvider, service);
            }
        }

        /// <summary>
        /// スコープの中でDI注入したオブジェクトのアクションを実行します。
        /// </summary>
        /// <typeparam name="T">注入するインターフェースやクラス。</typeparam>
        /// <param name="serviceScopeFactory">サービス作成ファクトリ。</param>
        /// <param name="asyncAction">注入したインターフェースやクラスを利用したアクション。</param>
        /// <exception cref="ArgumentNullException"><paramref name="serviceScopeFactory"/> または <paramref name="asyncAction"/> が <see langword="null"/> です。</exception>
        public static async Task ExecuteInScopeAsync<T>(this IServiceScopeFactory serviceScopeFactory, Func<IServiceProvider, T, Task> asyncAction)
            where T : class
        {
            if (serviceScopeFactory == null)
            {
                throw new ArgumentNullException(nameof(serviceScopeFactory));
            }
            if (asyncAction == null)
            {
                throw new ArgumentNullException(nameof(asyncAction));
            }

            await Task.Run(async () =>
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<T>();
                    await asyncAction(scope.ServiceProvider, service);
                }
            });
        }
    }
}
