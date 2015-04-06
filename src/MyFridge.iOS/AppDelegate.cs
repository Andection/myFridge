using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Xamarin.Forms;

namespace MyFridge.iOS
{

    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        UIWindow window;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.Init();

            window = new UIWindow(UIScreen.MainScreen.Bounds);

            RootController = App.GetMainPage().CreateViewController();
            window.RootViewController = RootController;

            window.MakeKeyAndVisible();

            return true;
        }

        public static UIViewController RootController { get; private set; }
    }
}
