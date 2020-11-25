using System.Diagnostics;
/**
 *  created by  : Brian Tria
 *  date        : 24/11/2020 00:44:48
 *  description :
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.LEGO.Behaviours.Actions;
using Unity.LEGO.Behaviours.Triggers;

namespace LegoDodgeBall
{
    public class PickupRespawnManager : PickupTrigger
    {

        // TODO: set max pickup Goal = 9999999;
        // TODO: programatically spawn pickup and add listener
        // TODO: update progress onCollect
        // TODO: spawn / activate another pickup onCollect

        void PickupCollected(PickupAction pickup)
        {


            //pickup.Activate()
        }
    }
}