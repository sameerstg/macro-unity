using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Stg.MacroTools
{
    [Serializable]
    public class Macro
    {
        public string macroCode;
        public UnityEvent Callback;
        internal List<string> tries = new();
        public Macro(string macroCode, Action macroAction)
        {
            this.macroCode = macroCode;
            AddAction(macroAction);
            tries = new();
        }
        public void AddAction(Action action)
        {
            Callback.AddListener(() => action?.Invoke());
        }
        public void Check(char c)
        {
            for (int i = 0; i < tries.Count;)
            {

                if (tries[i][0] != c)
                {
                    tries.RemoveAt(i);
                    continue;
                }

                if (tries[i].Length == 1)
                {
                    tries.RemoveAt(i);
                    Callback?.Invoke();
                    continue;
                }
                tries[i] = tries[i++][1..];

            }
            if (c == macroCode[0])
            {
                tries.Add(macroCode[1..]);
            }
        }

        public static void Update(List<Macro> cheats)
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(kcode))
                    {
                        foreach (var item in cheats)
                        {
                            if (string.IsNullOrEmpty(item.macroCode)) continue;
                            item.Check(char.ToLower(kcode.ToString()[0]));
                        }
                        return;
                    }
                }


            }
        }
        public static void Update(Macro macro)
        {
            if (string.IsNullOrEmpty(macro.macroCode)) return;
            if (Input.anyKeyDown)
            {
                foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(kcode))
                    {
                        macro.Check(char.ToLower(kcode.ToString()[0]));
                        return;
                    }
                }
                return;

            }
        }
    }
}
