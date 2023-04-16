using UnityEngine;
using System;
using System.Collections;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
public class ConditionalHideAttribute : PropertyAttribute
{
    #region Variables

    public string ConditionalSourceField;
    public int EnumIndex;

    #endregion

    #region Methods

    public ConditionalHideAttribute(string boolVariableName)
    {
        ConditionalSourceField = boolVariableName;
    }

    public ConditionalHideAttribute(string enumVariableName, int enumIndex)
    {
        ConditionalSourceField = enumVariableName;
        EnumIndex = enumIndex;
    }

    #endregion
}