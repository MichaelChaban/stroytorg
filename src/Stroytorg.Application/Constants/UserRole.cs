﻿namespace Stroytorg.Application.Constants;

public static class UserRole
{
    public const string Admin = "Administrator";

    public const string Editor = "Employee";

    public const string ClientsHandler = Admin + Separator + Editor;

    private const string Separator = ", ";
}
