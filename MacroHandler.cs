using System.Collections.Generic;
using UnityEngine;
namespace Stg.MacroTools
{
    public class MacroHandler : MonoBehaviour
    {
        public List<Macro> macros;
        private void Start()
        {
            foreach (var item in macros)
            {
                item.tries = new();
            }
        }
        private void Update()
        {
            Macro.Update(macros);
        }
    }
}

