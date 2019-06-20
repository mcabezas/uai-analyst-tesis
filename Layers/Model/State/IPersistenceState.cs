/**
 * Created by Marcelo Cabezas on 2019-Jun-19 7:49:10 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

namespace Layers.Model.State
{
    public interface IPersistenceState 
    {
        bool CanHandle(IEntity anEntity);
        object DbIdValue(IEntity anEntity);
    }
}