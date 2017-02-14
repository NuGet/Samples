using ManagedPackage;
using Microsoft.Windows.Design.Metadata;

namespace ManagedSdk.Design
{
    internal class RegisterMetadata : IProvideAttributeTable
    {
        public AttributeTable AttributeTable
        {
            get
            {
                AttributeTableBuilder attributeTableBuilder = new AttributeTableBuilder();

                attributeTableBuilder.AddCallback(typeof(MyCustomControl), delegate (AttributeCallbackBuilder builder)
                {
                    builder.AddCustomAttributes("MyString", new System.ComponentModel.CategoryAttribute("Layout"));
                });

                return attributeTableBuilder.CreateTable();
            }
        }
    }
}
