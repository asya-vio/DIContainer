using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IocExample
{
    class Kernel
    {
        Dictionary<Type, Type> typeDict = new Dictionary<Type, Type>();

        Dictionary<Type, Object> typeObjectDict = new Dictionary<Type, Object>();

        public void Bind(Type bind, Type to)
        {
            typeDict.Add(bind, to);

        }

        public void BindToObject(Type bind, Object obj )
        {
            typeObjectDict.Add(bind, obj);

        }

        public T Get<T>()
        {
            return (T)Get(typeof(T));

        }

        private object Get(Type getType)
        {
            if (typeObjectDict.ContainsKey(getType))
            {
                return typeObjectDict[getType];
            }
            else
            {
                getType = typeDict[getType];

                var constrInfo = Utils.GetSingleConstructor(getType);
                List<Object> parameters = new List<object>();

                constrInfo.GetParameters().ToList().ForEach(parameter => {
                    parameters.Add(Get(parameter.ParameterType));
                });

                return Utils.CreateInstance(getType, parameters);

            } 
        }
    }
}
