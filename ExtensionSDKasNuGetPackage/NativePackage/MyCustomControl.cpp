//
// MyCustomControl.cpp
// Implementation of the MyCustomControl class.
//

#include "pch.h"
#include "MyCustomControl.h"

using namespace NativePackage;

using namespace Platform;
using namespace Windows::Foundation;
using namespace Windows::Foundation::Collections;
using namespace Windows::UI::Xaml;
using namespace Windows::UI::Xaml::Controls;
using namespace Windows::UI::Xaml::Data;
using namespace Windows::UI::Xaml::Documents;
using namespace Windows::UI::Xaml::Input;
using namespace Windows::UI::Xaml::Interop;
using namespace Windows::UI::Xaml::Media;




// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235



		DependencyProperty^ MyCustomControl::myStringProperty = DependencyProperty::Register(
			"MyStringProperty",
			Platform::String::typeid,
			MyCustomControl::typeid,
			nullptr);

MyCustomControl::MyCustomControl()
{
	DefaultStyleKey = "NativePackage.MyCustomControl";
}
