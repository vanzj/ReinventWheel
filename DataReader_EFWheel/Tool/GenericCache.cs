using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataReader_EFWheel.Tool
{
    /// <summary>
    /// 泛型缓存原型  静态字段加泛型
    /// 享元模式的一种实现方式
    /// </summary>
    /// <typeparam name="T">where T: new() 无参数构造函数约束
    /// 保证可以调用Activator.CreateInstance<T>() 的有效</typeparam>
    public  class GenericCache<T> where T: new()
    { 
        /// <summary>
        /// 
        /// </summary>
        private static T _genericCache = Activator.CreateInstance<T>();
        /// <summary>
        /// 锁 保证线程安全 ??  todo 泛型缓存天生线程安全不用锁待确认
        /// </summary>
        private static readonly object _lock = new object();
        /// <summary>
        /// 获取实例
        /// </summary>
        /// <returns>对应实例</returns>
        public static T GetCache()
        {
            lock (_lock)
            {
                return _genericCache;
            }
        }
    }
}
