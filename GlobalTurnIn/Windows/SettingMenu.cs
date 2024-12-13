using Dalamud.Interface.Components;
using Dalamud.Interface.Utility.Raii;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using System.Numerics;


namespace GlobalTurnIn.Windows
{
    internal class SettingMenu : Window ,IDisposable
    {
        public static new readonly string WindowName = "GlobalTurnIn Settings";
        public SettingMenu() : base(WindowName)
        {
            Flags = ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.NoCollapse;
            ImGui.SetNextWindowSize(new Vector2(450, 0), ImGuiCond.Always);
        }
        
        public void Dispose() { }

        bool maxItem = C.MaxItem;
        bool maxArmory = C.MaxArmory;
        int maxArmoryFreeSlot = C.MaxArmoryFreeSlot;
        bool sellOilCloth = C.SellOilCloth;
        bool teleportToFC = C.TeleportToFC;
        string[] options = { "Vendor Turn-in", "Gc Turn-in" };
        int selectedOption = C.VendorTurnIn ? 0 : 1; // Map boolean state to dropdown index

        public bool ChangeArmory = C.ChangeArmory;
        public override void Draw()
        {
            ImGui.PushItemWidth(100);
            ImGui.PopItemWidth();
            ImGui.Text("Inventory Management:");
            ImGui.Separator();

            // MaxItem
            if (ImGui.Checkbox("Maximize Inventory##maxitem", ref maxItem))
            {
                C.MaxItem = maxItem;
            }
            ImGui.SameLine();
            ImGuiComponents.HelpMarker("Maximize inventory by buying one of a single item.");


            using (ImRaii.Disabled(!maxItem))
            {
                if (!maxItem) 
                    maxArmory = false;
                if (ImGui.Checkbox("Fill Armory##maxarmory", ref maxArmory))
                {
                    C.ChangeArmory = false;
                    C.MaxArmory = maxArmory;
                }
            }
            ImGui.Text("Free Main Inventory Slots");
            ImGuiComponents.HelpMarker("Select how many slots you want in the inventory open.\nGood to use if you're buying multiple stack of Oilcloth for instance.");
            ImGui.PushItemWidth(100);
            if (ImGui.InputInt("##maxarmoryfreeslot", ref maxArmoryFreeSlot))
            {
                if (maxArmoryFreeSlot < 0) maxArmoryFreeSlot = 0;
                C.MaxArmoryFreeSlot = maxArmoryFreeSlot;
            }
            ImGui.PopItemWidth();
            ImGui.NewLine();

            ImGui.Text("Turn-in Settings:");
            ImGui.Separator();

            // VendorTurnIn
            if (ImGui.Combo("", ref selectedOption, options, options.Length))
            {
                // Update the property based on the selected option
                C.VendorTurnIn = (selectedOption == 0);
            }
            using (ImRaii.Disabled(!C.VendorTurnIn))
            {
                if (!C.VendorTurnIn)
                    teleportToFC = false;
                
                if (ImGui.Checkbox("Teleport to FC##teleporttofc", ref teleportToFC))
                {
                    C.TeleportToFC = teleportToFC;
                }
            }
            if (ImGui.Checkbox("Sell OilCloth Turn-in##SellOilCloth", ref sellOilCloth))
            {
                C.SellOilCloth = sellOilCloth;
            }
            ImGui.SameLine();
            ImGuiComponents.HelpMarker("Stay off the marketboard and sell to your retainer.");
        }
    }
}
