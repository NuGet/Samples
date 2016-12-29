//
// MyUserControl.xaml.cpp
// Implementation of the MyUserControl class
//

#include "pch.h"
#include "MyUserControl.xaml.h"

using namespace NativePackage;

using namespace Platform;
using namespace Windows::Foundation;
using namespace Windows::Foundation::Collections;
using namespace Windows::UI::Xaml;
using namespace Windows::UI::Xaml::Controls;
using namespace Windows::UI::Xaml::Controls::Primitives;
using namespace Windows::UI::Xaml::Data;
using namespace Windows::UI::Xaml::Input;
using namespace Windows::UI::Xaml::Media;
using namespace Windows::UI::Xaml::Navigation;
using namespace Windows::ApplicationModel::Resources::Core;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

MyUserControl::MyUserControl()
{
	InitializeComponent();
	ResourceContext^ ctx = ref new Windows::ApplicationModel::Resources::Core::ResourceContext();
	ResourceMap^ libmap = ResourceManager::Current->MainResourceMap;
	TextView->Text = (String^)libmap->GetSubtree("NativePackage")->GetSubtree("Resources")->GetValue("MyNativeString", ctx)->ValueAsString;
}
