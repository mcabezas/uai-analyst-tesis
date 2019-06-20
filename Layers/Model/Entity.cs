/**
 * Created by Marcelo Cabezas on 2019-Jun-02 8:01:49 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using Layers.Model.State;
using Layers.Model.State.Persistence;

namespace Layers.Model
{
    public class Entity : IEntity
    {
        public int Id { get; set; }

        public int GetId()
        {
            return Id;
        }

        public object DbIdValue()
        {
            IEntityStateHandler stateHandler = new PersistenceStateHandler();
            IPersistenceState state = stateHandler.ToHandle(this);
            return state.DbIdValue(this);
        }
    }
}