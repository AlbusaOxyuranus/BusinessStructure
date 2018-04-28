using System.Reflection;

namespace BusinessStructure.WPF.ViewModels
{
    public class HelperVersion
    {
        private static HelperVersion _instance;

        private static string getNameDeveloper;
        private static string getProductName;

        public static HelperVersion Instance
        {
            get
            {
                if (_instance == null) _instance = new HelperVersion();
                return _instance;
            }
        }

        /*"Версия: " +
         Assembly.GetExecutingAssembly().GetName().HelperVersion + " Build " + "\n(" +
         new FileInfo(Assembly.GetExecutingAssembly().FullName).LastWriteTime.ToString("yyyy.MM.dd-HH:mm") + ")";
         */
        //static string _nameDeveloper =  (AssemblyProductAttribute)AssemblyProductAttribute.GetCustomAttribute(Assembly.GetExecutingAssembly(),
        //           typeof(AssemblyProductAttribute));
        public static string VersionValue { get; set; } = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public static string GetNameDeveloper
        {
            get
            {
                // Get all Copyright attributes on this assembly
                var attributes = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                // If there aren't any Copyright attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Copyright attribute, return its value
                getNameDeveloper = ((AssemblyCopyrightAttribute) attributes[0]).Copyright;
                return getNameDeveloper;
            }
            set => getNameDeveloper = value;
        }
        public static string GetProductName
        {
            get
            {
                // Get all Copyright attributes on this assembly
                var attributes = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyName), false);
                // If there aren't any Copyright attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Copyright attribute, return its value
                getProductName = ((AssemblyName) attributes[0]).FullName;
                return getProductName;
            }
            set => getProductName = value;
        }
    }
}

