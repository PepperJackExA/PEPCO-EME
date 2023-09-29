using System.IO;
using Sandbox.ModAPI;
using VRage;
using VRage.Game.Components;

namespace Digi.Examples
{
    [MySessionComponentDescriptor(MyUpdateOrder.NoUpdate)]
    public class Example_OverrideLocalizationKeys : MySessionComponentBase
    {
        public override void LoadData()
        {
            LoadLangOverrides();

            MyAPIGateway.Gui.GuiControlRemoved += GuiControlRemoved;
        }

        protected override void UnloadData()
        {
            MyAPIGateway.Gui.GuiControlRemoved -= GuiControlRemoved;
        }

        void LoadLangOverrides()
        {
            string folder = Path.Combine(ModContext.ModPathData, "Localization");

            // this method loads all MyCommonTexts/MyCoreTexts/MyTexts prefixed files from the given folder.
            // if culture is not null it would also load the same prefixed files with `Prefix.Culture.resx`
            // if culture and subculture are not null, aside from loading the culture one it also loads `Prefix.Culture-Subculture.resx`.
            MyTexts.LoadTexts(folder, cultureName: "override", subcultureName: null);
        }

        void GuiControlRemoved(object screen)
        {
            if (screen == null)
                return;

            // detect when options menu is closed in case player changes language
            if (screen.ToString().EndsWith("ScreenOptionsSpace"))
            {
                LoadLangOverrides();
            }
        }
    }
}