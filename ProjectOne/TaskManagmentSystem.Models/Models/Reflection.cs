using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TaskManagmentSystem.Models
{
    public static class Reflection
    {
        public static Type GetTypeOfTask(string taskName)
        {
            string fullName = Assembly.GetExecutingAssembly().GetName().Name + taskName;
            Type type = Type.GetType(fullName, false, true);
            return type;
        }
    }
}
