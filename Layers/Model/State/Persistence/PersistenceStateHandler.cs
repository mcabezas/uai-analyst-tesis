/**
 * Created by Marcelo Cabezas on 2019-Jun-20 5:31:50 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using Commons.Generics;
using Commons.Generics.impl;

namespace Layers.Model.State.Persistence
{
    public class PersistenceStateHandler : IEntityStateHandler
    {
        public IPersistenceState ToHandle(IEntity anEntity)
        {
            IMCollection<IPersistenceState> states = new MCollection<IPersistenceState>
            {
                new PersistedState(),
                new NotPersistedState()
            };
            return states.Filter(state => state.CanHandle(anEntity)).GetFirst();
        }
    }
}