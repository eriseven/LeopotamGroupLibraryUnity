// ----------------------------------------------------------------------------
// The MIT License
// LeopotamGroupLibrary https://github.com/Leopotam/LeopotamGroupLibraryUnity
// Copyright (c) 2012-2017 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.UI;

namespace LeopotamGroup.SystemUi.DataBinding.Binders {
    [RequireComponent (typeof (Slider))]
    public sealed class DataBindSlider : SingleTokenBinderBase {
        Slider _target;

        public override void OnDataChanged (string token, object data) {
            if ((object) _target == null) {
                _target = GetComponent<Slider> ();
            }
            _target.value = Convert.ToSingle (data);
        }
    }
}