using AspectInjector.Broker;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            MyBiz biz = new MyBiz();
            decimal a = 432m, b = 123m;

            var ret = biz.DoAction(a, b);
            
            Console.WriteLine($"{a} + {b} => {ret}");

            Console.WriteLine("Press any key to continue...");
        }
    }

    class MyBiz
    {
        [CatchAndLog]
        public decimal DoAction(decimal foo, decimal bar)
        {
            Console.WriteLine("Action go...");

            var ret = foo + bar;
            throw new ApplicationException("測試例外!");

            Console.WriteLine($"Action result => {foo} + {bar} => {ret}");

            return ret;
        }
    }

    [Aspect(Scope.Global)]
    [Injection(typeof(CatchAndLog))]
    public class CatchAndLog : Attribute
    {
        [Advice(Kind.Before)] // you can have also After (async-aware), and Around(Wrap/Instead) kinds
        public void Begore([Argument(Source.Name)] string name,
            [Argument(Source.Instance)] object instane,
            [Argument(Source.Arguments)] object[] args)
        {
            //※ log enter
            Console.WriteLine();
            Console.WriteLine($"[Before] {instane.GetType().Name}.{name} => ({args[0]},{args[1]})");
            Console.WriteLine();
        }

        [Advice(Kind.After)]
        public void After([Argument(Source.Name)] string name,
            [Argument(Source.Instance)] object instane,
            [Argument(Source.Arguments)] object[] args,
            [Argument(Source.ReturnType)] Type retType, 
            [Argument(Source.ReturnValue)] object retValue)
        {
            //※ log leave/result
            Console.WriteLine();
            Console.WriteLine($"[After] {instane.GetType().Name}.{name} => ({args[0]},{args[1]}) => {retValue}:{retType.Name}");
            Console.WriteLine();
        }

        [Advice(Kind.Around)]
        public object Around([Argument(Source.Name)] string name,
            [Argument(Source.Instance)] object instane,
            [Argument(Source.Target)] Func<object[],object> func,
            [Argument(Source.Arguments)] object[] args)
        {
            try
            {
                // 可以在這裡 catch, retry, authenticate,  

                //※ around begin
                Console.WriteLine(); 
                Console.WriteLine($"[Around BEGIN] {instane.GetType().Name}.{name} => ({args[0]},{args[1]})");
                Console.WriteLine();

                var ret = func.Invoke(args);

                //※ around end
                Console.WriteLine();
                Console.WriteLine($"[Around END] {instane.GetType().Name}.{name} => ({args[0]},{args[1]})");
                Console.WriteLine();

                return (decimal)ret + 111m; //※ 可以改變處理結果。
            }
            catch (Exception ex)
            {
                //※ catch exception
                Console.WriteLine();
                Console.WriteLine($"[Around CATCH] => {ex.Message}:{ex.GetType().Name}");
                Console.WriteLine();
                throw;
            }
            finally
            {
                //※ around leave
                Console.WriteLine();
                Console.WriteLine($"[Around LEAVE] {instane.GetType().Name}.{name} => ({args[0]},{args[1]})");
                Console.WriteLine();
            }
        }
    }
}
