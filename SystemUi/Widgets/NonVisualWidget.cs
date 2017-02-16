﻿// ----------------------------------------------------------------------------
// The MIT License
// LeopotamGroupLibrary https://github.com/Leopotam/LeopotamGroupLibraryUnity
// Copyright (c) 2012-2017 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace LeopotamGroup.SystemUi.Widgets {
    /// <summary>
    /// Non visual interactive UI widget, keep fillrate / no rendering / alpha-blending.
    /// </summary>
    [RequireComponent (typeof (CanvasRenderer))]
    [RequireComponent (typeof (RectTransform))]
    public class NonVisualWidget : Graphic {
        public override void SetMaterialDirty () { return; }
        public override void SetVerticesDirty () { return; }
        public override Material material { get { return defaultMaterial; } set { } }

        public override void Rebuild (CanvasUpdate update) { return; }
    }
}