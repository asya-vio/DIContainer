using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IocExample
{
    class Kernel
    {
        Dictionary<Type, Type> dictType = new Dictionary<Type, Type>();

        Dictionary<Type, Object> dictTypeObj = new Dictionary<Type, Object>();
        public void Bind(Type bind, Type to)
        {
            dictType.Add(bind, to);

        }

        public void BindToObject(Type bind, Object obj )
        {
            dictTypeObj.Add(bind, obj);

        }

        public T Get<T>()
        {
            return (T)Get(typeof(T));


        }

        private object Get(Type getType)
        {
            if (dictTypeObj.ContainsKey(getType))
            {
                return dictTypeObj[getType];
            }
            else
            {
                getType = dictType[getType];

                var constrInfo = Utils.GetSingleConstructor(getType);
                List<Object> parameters = new List<object>();
                foreach (var parameter in constrInfo.GetParameters())
                {
                    parameters.Add(Get(parameter.ParameterType));
                }

                return Utils.CreateInstance(getType, parameters);

            } 
        }
    }
}
