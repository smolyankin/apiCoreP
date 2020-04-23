using System.Linq;
using System.Reflection;

namespace apiCoreP.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class BaseClassExtensions
    {
        /// <summary>
        /// copy same name values from current object (from left to right)
        /// </summary>
        /// <typeparam name="TDestination">destination class</typeparam>
        /// <typeparam name="TBase"></typeparam>
        /// <param name="self"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TBase ToCopy<TBase, TDestination>(this TBase self, TDestination obj)
            where TDestination : class, new()
            where TBase : new()
        {
            foreach (var pS in self.GetType().GetProperties())
            {
                foreach (var pT in obj.GetType().GetProperties())
                {
                    if (pT.Name != pS.Name) continue;
                    (pT.GetSetMethod()).Invoke(obj, new[]
                        { pS.GetGetMethod().Invoke( self, null ) });
                }
            }
            return self;
        }

        /// <summary>
        /// one class to another (copy values)
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TDestination Transform<TDestination>(this object obj) where TDestination : new()
        {
            var tDerived = new TDestination();
            foreach (var propDerived in typeof(TDestination).GetProperties())
            {
                var propBase = obj.GetType().GetProperty(propDerived.Name);
                if (propBase != null)
                {
                    object value = propBase.GetValue(obj, null);

                    if (value != null && propBase.PropertyType.IsArray)
                    {
                        MethodInfo convertMethod = typeof(BaseClassExtensions).GetMethod("ConvertArray",
                            BindingFlags.NonPublic | BindingFlags.Static);
                        var targetType = propDerived.PropertyType.GenericTypeArguments.First();
                        MethodInfo generic = convertMethod.MakeGenericMethod(targetType);
                        value = generic.Invoke(null, new[] { value });
                    }
                    propDerived.SetValue(tDerived, value, null);
                }
            }
            return tDerived;
        }
    }
}
