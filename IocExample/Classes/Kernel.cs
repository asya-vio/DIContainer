using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IocExample
{
    class Kernel
    {
        Dictionary<Type, Type> typeDictionary = new Dictionary<Type, Type>();

        Dictionary<Type, Object> typeObjectDictionary = new Dictionary<Type, Object>();

        public void Bind(Type bind, Type to)
        {
            typeDictionary.Add(bind, to);

        }

        public void BindToObject(Type type, Object toObject )
        {
            typeObjectDictionary.Add(type, toObject);

        }

        public T Get<T>()
        {
            return (T)Get(typeof(T));

        }

        private object Get(Type getType)
        {
            if (typeObjectDictionary.ContainsKey(getType))
            {
                return typeObjectDictionary[getType];
            }
            else
            {
                getType = typeDictionary[getType];

                var constructorInfo = Utils.GetSingleConstructor(getType);
                List<Object> parameters = new List<object>();

                constructorInfo.GetParameters().ToList().ForEach(parameter => {
                    parameters.Add(Get(parameter.ParameterType));
                });

                return Utils.CreateInstance(getType, parameters);

            } 
        }
    }
}
