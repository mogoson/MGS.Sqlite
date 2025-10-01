/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SqliteLogger.cs
 *  Description  :  Logger for sqlite.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  12/1/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;

namespace MGS.Sqlite
{
    /// <summary>
    /// Logger for sqlite.
    /// </summary>
    internal sealed class SqliteLogger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public static void LogException(Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}