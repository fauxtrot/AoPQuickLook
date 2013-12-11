using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.SubSystems.Conversion;
using PostSharp.Aspects;
using PostSharp.Extensibility;

namespace Common.CommonAspects
{
    [Serializable]
    [MulticastAttributeUsage]
    public class RequireSerializationValidator : PostSharp.Aspects.TypeLevelAspect
    {
        public override bool CompileTimeValidate(Type type)
        {
            var result = type.GetCustomAttributes(true).Any(x => x is SerializableAttribute);
            if (!result)
            {
                Message.Write(type, SeverityType.Error, "RemotingValidation01", "{0} is a required attribute for {1}", this.GetType().Name, type);
                throw new InvalidAnnotationException("Could not Validate type: " + type.Name);
            }
            return result;
        }

        //public override bool CompileTimeValidate(object target)
        //{
        //    var type = target.GetType();
        //    var result = type.GetCustomAttributes(true).Any(x => x is SerializableAttribute);
        //    if (!result)
        //    {
        //        Message.Write(target.GetType(), SeverityType.Error, "RemotingValidation01", "{0} is required to be ", target.GetType());
        //    }
        //    return result;
        //}
    }
}
