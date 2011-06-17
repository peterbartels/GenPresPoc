/*using System;
using PostSharp.Aspects;

namespace GenPres.Business.Aspect
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class LowerCaseAttribute : MethodInterceptionAspect
    {
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            base.OnInvoke(args);

            if (args.Arguments.Count > 0)
            {
                if (args.Arguments[0] is string)
                {
                    args.Arguments[0] = (args.Arguments[0] as string).ToLower();
                }
            }
            args.Proceed();
        }
    }
}
*/