﻿// ----------------------------------------------------------------------------
// The MIT License
// LeopotamGroupLibrary https://github.com/Leopotam/LeopotamGroupLibraryUnity
// Copyright (c) 2012-2017 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using LeopotamGroup.Collections;
using LeopotamGroup.Common;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace LeopotamGroup.SystemUi.DataBinding {
    public sealed class DataStorage : UnitySingletonBase {
        Dictionary<string, FastList<IDataBinder>> _subscribers = new Dictionary<string, FastList<IDataBinder>> (128);

        Dictionary<string, MemberInfo> _sourceTypeFields = new Dictionary<string, MemberInfo> (128);

        IDataSource _source = null;

        public void Subscribe (string token, IDataBinder binder) {
#if UNITY_EDITOR
            if (string.IsNullOrEmpty (token)) {
                throw new ArgumentException ("token");
            }
            if (binder == null) {
                throw new ArgumentException ("binder");
            }
#endif
            FastList<IDataBinder> list;
            if (!_subscribers.TryGetValue (token, out list)) {
                list = new FastList<IDataBinder> (64);
                list.UseCastToObjectComparer (true);
                _subscribers[token] = list;
            }
            if (!list.Contains (binder)) {
                list.Add (binder);
            }
        }

        public void Unsubscribe (string token, IDataBinder binder) {
            if (string.IsNullOrEmpty (token) || binder == null) {
                return;
            }
            FastList<IDataBinder> list;
            if (_subscribers.TryGetValue (token, out list)) {
                var idx = list.IndexOf (binder);
                if (idx != -1) {
                    list.RemoveAt (idx);
                }
            }
        }

        public object GetData (string token) {
            if (_source != null) {
                MemberInfo prop;

                if (!_sourceTypeFields.TryGetValue (token, out prop)) {
                    var type = _source.GetType ();
                    prop = type.GetProperty (token);
                    if (prop == null || !(prop as PropertyInfo).CanRead) {
                        prop = type.GetField (token);
                    }
                    if (prop == null) {
                        Debug.LogWarningFormat ("Cant get member \"{0}\" of source type");
                    }
                    _sourceTypeFields[token] = prop;
                }
                if (prop != null) {
                    return prop is PropertyInfo ?
                           ((PropertyInfo) prop).GetValue (_source, null) :
                           ((FieldInfo) prop).GetValue (_source);
                }
            }
            return null;
        }

        public void SetDataSource (IDataSource source) {
            if (_source != null) {
                _source.OnDataChanged -= OnSourceChanged;
                _sourceTypeFields.Clear ();
            }
            _source = source;
            if (_source != null) {
                _source.OnDataChanged += OnSourceChanged;
            }
        }

        void OnSourceChanged (string propName) {
            Publish (propName, GetData (propName));
        }

        public void Publish (string tokenName, object tokenValue) {
            if (string.IsNullOrEmpty (tokenName)) {
                return;
            }
            FastList<IDataBinder> list;
            if (_subscribers.TryGetValue (tokenName, out list)) {
                int count;
                var items = list.GetData (out count);
                for (var i = count - 1; i >= 0; i--) {
                    items[i].OnDataChanged (tokenName, tokenValue);
                }
            }
        }
    }
}