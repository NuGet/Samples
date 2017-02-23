using Microsoft.Windows.Design.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativePackage.Design
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
