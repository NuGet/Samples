//
// MyCustomControl.h
// Declaration of the MyCustomControl class.
using namespace Windows::UI::Xaml::Controls;
using namespace Windows::UI::Xaml::Interop;
using namespace Windows::UI::Xaml;
using namespace Platform;

#pragma once

namespace NativePackage
{
	public ref class MyCustomControl sealed : public Windows::UI::Xaml::Controls::Control
	{
	private:
		static DependencyProperty^ myStringProperty;

	public:

		// Static DependencyProperties
		static property Windows::UI::Xaml::DependencyProperty^ MyStringProperty
		{
			Windows::UI::Xaml::DependencyProperty^ get()
			{
				return myStringProperty;
			}
		}

	public:

		// Public properties
		property String^ MyString
		{
			String ^ get()
			{
				return (String^)GetValue(MyStringProperty);
			}
			void set(String ^ value)
			{
				SetValue(MyStringProperty, value);
			}
		}
	public:
		MyCustomControl();
	};
}
