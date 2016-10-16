/* Adapted from https://github.com/AlexanderDzhoganov/ksp-advanced-flybywire/blob/master/EVAController.cs under terms of MIT license
 * Original Copyright (c) 2014 Alexander Dzhoganov
 * Modifications Copyright (c) 2015 Sean McDougall
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace EVAEnhancementsContinued
{
    class EVAController
    {

        private const float EVARotationStep = 57.29578f;
       // private List<FieldInfo> vectorFields;
        private static EVAController instance;

        public static EVAController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EVAController();
                }
                return instance;
            }
        }
#if false
        public EVAController()
        {
            LoadReflectionFields();
        }
#endif

        public void UpdateEVAFlightProperties(float pitch, float roll, float power)
        {
            KerbalEVA eva = FlightGlobals.ActiveVessel.GetComponent<KerbalEVA>();
            if (!FlightGlobals.ActiveVessel.Landed && eva.JetpackDeployed)
            {
                Quaternion rotation = Quaternion.identity;
                rotation *= Quaternion.AngleAxis(eva.turnRate * pitch * EVARotationStep * Time.deltaTime * power, -eva.transform.right);
                rotation *= Quaternion.AngleAxis(0, eva.transform.up);
                rotation *= Quaternion.AngleAxis(eva.turnRate * roll * EVARotationStep * Time.deltaTime * power, -eva.transform.forward);
                
                
                if (rotation != Quaternion.identity)
                {
                    var eva_tgtFwd = typeof(KerbalEVA).GetField("tgtFwd", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    var eva_tgtUp = typeof(KerbalEVA).GetField("tgtUp", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                    eva_tgtFwd.SetValue(eva, rotation * (Vector3)eva_tgtFwd.GetValue(eva));
                    eva_tgtUp.SetValue(eva, rotation * (Vector3)eva_tgtUp.GetValue(eva));
//                    this.vectorFields[9].SetValue(eva, rotation * (Vector3)this.vectorFields[9].GetValue(eva));
//                    this.vectorFields[13].SetValue(eva, rotation * (Vector3)this.vectorFields[13].GetValue(eva));
                }
            }
        }


        // PRIVATE METHODS //
#if false
        private void LoadReflectionFields()
        {
            List<FieldInfo> fields = new List<FieldInfo>(typeof(KerbalEVA).GetFields(
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance));
            this.vectorFields = new List<FieldInfo>(fields.Where<FieldInfo>(f => f.FieldType.Equals(typeof(Vector3))));
        }
#endif
    }
}
