using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Services
{
    public static class EditableObjectHelper
    {
        private static Dictionary<Type, IEnumerable<PropertyInfo>> _propertyInfos =
            new Dictionary<Type, IEnumerable<PropertyInfo>>();
        private static Dictionary<IEditableObject, Dictionary<PropertyInfo, object>> _propertyValues =
            new Dictionary<IEditableObject, Dictionary<PropertyInfo, object>>();

        private static IEnumerable<PropertyInfo> GetOrCreatePropertyInfos(IEditableObject model)
        {
            var modelType = model.GetType();
            if (_propertyInfos.ContainsKey(modelType))
                return _propertyInfos[modelType];
            else
                return from p in model.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                       where p.SetMethod != null
                       select p;
        }

        public static void BeginEdit(IEditableObject model)
        {
            if (_propertyValues.ContainsKey(model))
                _propertyValues.Remove(model);
            var values = new Dictionary<PropertyInfo, object>();
            _propertyValues.Add(model, values);

            var props = GetOrCreatePropertyInfos(model);
            foreach (var prop in props)
            {
                values.Add(prop, prop.GetValue(model));
            }
        }

        public static void CancelEdit(IEditableObject model)
        {
            if (!_propertyValues.ContainsKey(model))
                return;
            foreach (var item in _propertyValues[model])
            {
                item.Key.SetValue(model, item.Value);
            }
        }

        public static void EndEdit(IEditableObject model)
        {
            if (!_propertyValues.ContainsKey(model))
                _propertyValues.Remove(model);
        }
    }
}
