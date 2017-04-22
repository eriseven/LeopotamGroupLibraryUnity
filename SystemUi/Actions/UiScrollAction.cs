﻿// ----------------------------------------------------------------------------
// The MIT License
// LeopotamGroupLibrary https://github.com/Leopotam/LeopotamGroupLibraryUnity
// Copyright (c) 2012-2017 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using LeopotamGroup.Common;
using LeopotamGroup.Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LeopotamGroup.SystemUi.Actions {
    /// <summary>
    /// Event data of UiScrollAction.
    /// </summary>
    public struct UiScrollActionData {
        /// <summary>
        /// Logical group for filtering events.
        /// </summary>
        public int GroupId;

        /// <summary>
        /// Event sender.
        /// </summary>
        public GameObject Sender;

        /// <summary>
        /// Event data from uGui.
        /// </summary>
        public PointerEventData EventData;
    }

    /// <summary>
    /// Ui action for processing OnScroll events.
    /// </summary>
    public sealed class UiScrollAction : UiActionBase, IScrollHandler {
        void IScrollHandler.OnScroll (PointerEventData eventData) {
            var action = new UiScrollActionData ();
            action.GroupId = GroupId;
            action.Sender = gameObject;
            action.EventData = eventData;
            Singleton.Get<UnityEventBus> ().Publish<UiScrollActionData> (action);
        }
    }
}