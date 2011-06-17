/*using System;
using PostSharp.Aspects;

namespace GenPres.Business.Aspect
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Property)]
    public class ChangeStateAttribute : LocationInterceptionAspect
    {
        public override void OnSetValue(LocationInterceptionArgs args)
        {
            if (args.GetCurrentValue() != args.Value)
            {
                if (args.Instance != null)
                {
                    if (args.Instance is IChangeTrackable)
                    {
                        ((IChangeTrackable) args.Instance).State = StatusEnum.Dirty;
                    }
                }
            }

            base.OnSetValue(args);
        }
        
    }
}*/