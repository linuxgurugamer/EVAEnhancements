using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EVAEnhancementsContinued
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class AddCustomModules : MonoBehaviour
    {
        private static bool initialized = false;

        public void Update()
        {
            if (!initialized)
            {
                initialized = true;
                addEVAEnhancementsModule("kerbalEVA");
                addEVAEnhancementsModule("kerbalEVAfemale");
            }

        }

        private void addEVAEnhancementsModule(string partName)
        {
            try
            {
                ConfigNode node = new ConfigNode("MODULE");
                node.AddValue("name", "EVAEnhancements");

                var partInfo = PartLoader.getPartInfoByName(partName);
                var prefab = partInfo.partPrefab;
                var module = prefab.AddModule(node);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Object reference not set"))
                {
                    print("[EVAEnhancements] addEVAEnhancementsModule to " + partName + " succeeded.");
                }
                else
                {
                    print("[EVAEnhancements] addEVAEnhancementsModule [" + Time.time + "]: Failed to add the part module to " + partName + " " + ex.Message + "\n" + ex.StackTrace);
                }
            }
        }
    }
}
