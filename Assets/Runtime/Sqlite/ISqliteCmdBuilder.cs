/*************************************************************************
 *  Copyright © 2025 Mogoson All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ISqliteCmdBuilder.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  11/09/2025
 *  Description  :  Initial development version.
 *************************************************************************/

#if !UNITY_STANDALONE

using System;
using Mono.Data.Sqlite;

namespace MGS.Sqlite
{
    public interface ISqliteCmdBuilder : IDisposable
    {
        SqliteCommand GetInsertCommand();

        SqliteCommand GetUpdateCommand();

        SqliteCommand GetDeleteCommand();
    }
}
#endif