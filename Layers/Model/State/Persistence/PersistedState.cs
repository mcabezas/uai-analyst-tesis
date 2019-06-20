/**
 * Created by Marcelo Cabezas on 2019-Jun-20 1:50:06 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

namespace Layers.Model.State.Persistence
{
    public class PersistedState : IPersistenceState
    {
        public bool CanHandle(IEntity anEntity)
        {
            return anEntity.GetId()!=0;
        }

        public object DbIdValue(IEntity anEntity)
        {
            return anEntity.GetId();
        }
    }
}