using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Metroit.Ddd.Presentation.Extensions
{
    /// <summary>
    /// <see cref="IServiceProvider"/> の拡張メソッドを提供します。
    /// </summary>
    public static class IServiceProviderExtensions
    {
        /// <summary>
        /// スコープの中でDI注入したオブジェクトのアクションを実行します。
        /// </summary>
        /// <typeparam name="T">注入するインターフェースやクラス。</typeparam>
        /// <param name="serviceProvider">サービス作成ファクトリ。</param>
        /// <param name="action">注入したインターフェースやクラスを利用したアクション。</param>
        /// <exception cref="ArgumentNullException"><paramref name="serviceProvider"/> または <paramref name="action"/> が <see langword="null"/> です。</exception>
        public static void ExecuteInScope<T>(this IServiceProvider serviceProvider, Action<IServiceProvider, T> action)
            where T : class
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            using (var scope = serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<T>();
                action(scope.ServiceProvider, service);
            }
        }

        /// <summary>
        /// スコープの中でDI注入したオブジェクトのアクションを実行し、実行結果を受け取ります。
        /// </summary>
        /// <typeparam name="T1">注入するインターフェースやクラス。</typeparam>
        /// <typeparam name="T2">戻り値の型。</typeparam>
        /// <param name="serviceProvider">サービス作成ファクトリ。</param>
        /// <param name="func">注入したインターフェースやクラスを利用したアクション。</param>
        /// <returns>実行結果。</returns>
        /// <exception cref="ArgumentNullException"><paramref name="serviceProvider"/> または <paramref name="func"/> が <see langword="null"/> です。</exception>
        public static T2 ExecuteInScope<T1, T2>(this IServiceProvider serviceProvider, Func<IServiceProvider, T1, T2> func)
            where T1 : class
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            using (var scope = serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<T1>();
                var result = func(scope.ServiceProvider, service);
                return result;
            }
        }

        /// <summary>
        /// スコープの中でDI注入したオブジェクトのアクションを実行します。
        /// </summary>
        /// <typeparam name="T">注入するインターフェースやクラス。</typeparam>
        /// <param name="serviceProvider">サービス作成ファクトリ。</param>
        /// <param name="asyncAction">注入したインターフェースやクラスを利用したアクション。</param>
        /// <exception cref="ArgumentNullException"><paramref name="serviceProvider"/> または <paramref name="asyncAction"/> が <see langword="null"/> です。</exception>
        public static async Task ExecuteInScopeAsync<T>(this IServiceProvider serviceProvider, Func<IServiceProvider, T, Task> asyncAction)
            where T : class
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }
            if (asyncAction == null)
            {
                throw new ArgumentNullException(nameof(asyncAction));
            }

            await Task.Run(async () =>
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<T>();
                    await asyncAction(scope.ServiceProvider, service);
                }
            });
        }

        /// <summary>
        /// スコープの中でDI注入したオブジェクトのアクションを実行し、実行結果を受け取ります。
        /// </summary>
        /// <typeparam name="T1">注入するインターフェースやクラス。</typeparam>
        /// <typeparam name="T2">戻り値の型。</typeparam>
        /// <param name="serviceProvider">サービス作成ファクトリ。</param>
        /// <param name="asyncFunc">注入したインターフェースやクラスを利用したアクション。</param>
        /// <returns>実行結果。</returns>
        /// <exception cref="ArgumentNullException"><paramref name="serviceProvider"/> または <paramref name="asyncFunc"/> が <see langword="null"/> です。</exception>
        public static async Task<T2> ExecuteInScopeAsync<T1, T2>(this IServiceProvider serviceProvider, Func<IServiceProvider, T1, Task<T2>> asyncFunc)
            where T1 : class
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }
            if (asyncFunc == null)
            {
                throw new ArgumentNullException(nameof(asyncFunc));
            }

            var result = await Task.Run(async () =>
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<T1>();
                    return await asyncFunc(scope.ServiceProvider, service);
                }
            });
            return result;
        }
    }
}
