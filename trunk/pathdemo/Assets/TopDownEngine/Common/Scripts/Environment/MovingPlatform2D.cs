﻿using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.TopDownEngine
{
    /// <summary>
    /// A class to handle a platform that moves in 2D along a set of nodes
    /// </summary>
    public class MovingPlatform2D : MMPathMovement
    {
        protected TopDownController2D _topdDownController2D;

        /// <summary>
        /// When something collides, if it's a top down controller, we assign this platform to it
        /// </summary>
        /// <param name="collider"></param>
        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            _topdDownController2D = collider.gameObject.GetComponentNoAlloc<TopDownController2D>();
            if (_topdDownController2D != null)
            {
                _topdDownController2D.SetMovingPlatform(this);
            }
        }

        /// <summary>
        /// When something stops colliding, if it's a top down controller, we unassign this platform to it
        /// </summary>
        /// <param name="collider"></param>
        protected virtual void OnTriggerExit2D(Collider2D collider)
        {
            _topdDownController2D = collider.gameObject.GetComponentNoAlloc<TopDownController2D>();
            if (_topdDownController2D != null)
            {
                _topdDownController2D.SetMovingPlatform(null);
            }
        }
    }
}
