﻿using System;

namespace Fabrikafa.Globals.Enums;

/// <summary>
/// Used to determine operation for a page
/// </summary>
[Obsolete("Fabrikafa.Globals.Enums -> Fabrikafa.Platform.Enums")]
public enum OperationTypeEnum
{
    Add = 0,
    Update = 1,
    Remove = 2
}

//Use buttons with "asp-route-operationtypeenum" attribute
//<button type="submit" class="btn btn-info" asp-route-operationtypeenum="Add">Add To List</button>

//These attrributes are mapped to "OperationTypeEnum OperationType" as parameters of the methods.
//public async Task<IActionResult> OnPostAsync(OperationTypeEnum OperationType)
