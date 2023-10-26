namespace Lib.Suppliers.Types
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class SupplierBaseAttribute : Attribute
    {
        public string TargetProperty { get; }

        public SupplierBaseAttribute(string targetProperty)
        {
            TargetProperty = targetProperty;
        }
    }
}
