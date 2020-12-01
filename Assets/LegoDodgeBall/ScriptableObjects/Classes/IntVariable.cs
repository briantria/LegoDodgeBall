/* author		: Brian Tria
 * created		: Dec 14, 2019
 * source       : https://unity.com/how-to/architect-game-code-scriptable-objects
 */

using System;
using UnityEngine;


namespace LegoDodgeBall
{
    [CreateAssetMenu(fileName = "NewIntVariable", menuName = "ScriptableObject/Variables/Int", order = 51)]
    public class IntVariable : ScriptableObject
    {
        #region Properties
        public int InitValue;

        [NonSerialized] public int RuntimeValue;
        #endregion

        public void OnAfterDeserialize()
        {
            RuntimeValue = InitValue;
        }
    }
}
