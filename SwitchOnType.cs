using System;

namespace Kelemen
{
    static class TSwitch
    {
        public static void Switch(object condition, params CaseData[] cases)
        {
            var type = condition.GetType();
            foreach (var tcase in cases)
            {
                if (tcase.isDefault || tcase.TargetType.IsAssignableFrom(type))
                {
                    tcase.Action(condition);
                    break;
                }
            }
        }

        public static CaseData Case<T>(Action action)
        {
            return new CaseData()
            {
                Action = (x) => action(),
                TargetType = typeof(T)
            };
        }

        public static CaseData Case<T>(Action<T> action)
        {
            return new CaseData()
            {
                Action = (x) => action((T) x),
                TargetType = typeof(T)
            };
        }

        public static CaseData Default(Action action)
        {
            return new CaseData()
            {
                Action = (x) => action(),
                isDefault = true
            };
        }
        public class CaseData
        {
            public Type TargetType { get; set; }
            public bool isDefault { get; set; }
            public Action<object> Action { get; set; }


        }
    }
}
