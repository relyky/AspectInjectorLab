using AspectInjector.Broker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net5ConaoleApp.AOP
{
    [AttributeUsage(AttributeTargets.Class)]
    [Injection(typeof(CatchAndLogAspect))]
    public class CatchAndLogAttribute : Attribute
    {
        public string Title { get; init; }
    }

    [Aspect(Scope.Global)]
    public class CatchAndLogAspect
    {
        readonly ILogger<CatchAndLogAspect> _logger;

        public CatchAndLogAspect()
        {
            using (var serviceScope = ServiceActivator.GetScope())
            {
                var services = serviceScope.ServiceProvider;
                _logger = services.GetRequiredService<ILogger<CatchAndLogAspect>>();
            }
        }

        [Advice(Kind.Before, Targets = Target.Method)] // you can have also After (async-aware), and Around(Wrap/Instead) kinds
        public void Begore([Argument(Source.Name)] string name,
            [Argument(Source.Instance)] object instane,
            [Argument(Source.Triggers)] Attribute[] triggers,
            [Argument(Source.Arguments)] object[] args)
        {
            var attr = triggers.FirstOrDefault(c => c is CatchAndLogAttribute) as CatchAndLogAttribute;

            //※ log before
            _logger.LogInformation($"[Before] {attr?.Title} {instane.GetType().Name}.{name} => args[{args.Length}]");
        }

        [Advice(Kind.After, Targets = Target.Method)]
        public void After([Argument(Source.Name)] string name,
            [Argument(Source.Instance)] object instane,
            [Argument(Source.Arguments)] object[] args,
            [Argument(Source.ReturnType)] Type retType,
            [Argument(Source.ReturnValue)] object retValue)
        {
            //※ log after
            _logger.LogInformation($"[After] {instane.GetType().Name}.{name} => args[{args.Length}] => {retValue}:{retType.Name}");
        }

        [Advice(Kind.Around, Targets = Target.Method)]
        public object Around([Argument(Source.Name)] string name,
            [Argument(Source.Instance)] object instane,
            [Argument(Source.Target)] Func<object[], object> func,
            [Argument(Source.Arguments)] object[] args)
        {
            try
            {
                // 可以在這裡 catch, retry, authenticate, .... 

                //※ around begin
                _logger.LogInformation($"[Around BEGIN] {instane.GetType().Name}.{name} => args[{args.Length}]");

                var ret = func.Invoke(args);

                //※ around end
                _logger.LogInformation($"[Around END] {instane.GetType().Name}.{name} => args[{args.Length}]");

                return ret; //※ 可以改變處理結果。
            }
            catch (Exception ex)
            {
                //※ catch exception
                _logger.LogInformation($"[Around CATCH] => {ex.Message}:{ex.GetType().Name}");
                throw;
            }
            finally
            {
                //※ around leave
                _logger.LogInformation($"[Around LEAVE] {instane.GetType().Name}.{name} => args[{args.Length}]");
            }
        }
    }

}
