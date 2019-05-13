/**
 * Created by Marcelo Cabezas on 2019-May-09 7:57:01 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

namespace DBW.DBWrapper.States
{
    internal interface IStateHandleable
    {
        void OpenDbConnectionWhenNotOpen(int connectionTimeout);
        void OpenDbConnectionWhenOpen();
        void CloseDbConnectionWhenOpen();

        void CloseDbConnectionWhenClose();
    }
}