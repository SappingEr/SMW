﻿using NHibernate.Event;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace SaveMyWord.Models.Listeners
{
    [Listener]
    public class CreationDateListener : IPreInsertEventListener
    {
        public bool OnPreInsert(PreInsertEvent @event)
        {
            return SetCreationProps(@event);
        }

        public Task<bool> OnPreInsertAsync(PreInsertEvent @event, CancellationToken cancellationToken)
        {
            return new Task<bool>(() => {
                return SetCreationProps(@event);
            });
        }

        private bool SetCreationProps(PreInsertEvent @event)
        {
            if (@event.Entity != null)
            {
                var props = @event.Entity.GetType().GetProperties();
                foreach (var prop in props)
                {
                    var attr = prop.GetCustomAttribute<CreationDateAttribute>();
                    if (attr == null)
                    {
                        continue;
                    }
                    var index = Array.IndexOf(@event.Persister.PropertyNames, prop.Name);
                    if (index >= 0)
                    {
                        var val = DateTime.Now;
                        prop.SetValue(@event.Entity, val);
                        @event.State[index] = val;
                    }
                }
            }
            return false;
        }
    }
}