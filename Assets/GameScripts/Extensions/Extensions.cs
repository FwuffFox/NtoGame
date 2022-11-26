using System;

namespace GameScripts.Extensions
{
    public static partial class Extensions
    {
        public static T With<T>(this T self, Action<T> call)
        {
            call?.Invoke(self);
            return self;
        }
    }
}