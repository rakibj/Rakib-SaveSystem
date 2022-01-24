using System;
using System.Collections.Generic;
using SaveSystem.Core;
using UnityEngine;

namespace SaveSystem.Core
{
    public class EntityTableHolderBase<T> : MonoBehaviour where T: BaseEntity
    {
        [SerializeField] [UniqueIdentifier] private string uniqueId;
        [SerializeField] protected EntityTable<T> currentEntityTable;
        protected Saver Saver;
        private IEntityHolder<T>[] _entityHolders;

        protected virtual void Awake()
        {
            Saver = FindObjectOfType<Saver>();
        }

        protected virtual void OnEnable()
        {
            Saver.OnInitialize += LoadOrSaveDefault;
            Saver.OnSave += Save;
        }

        public void LoadOrSaveDefault()
        {
            GetReferences();
            currentEntityTable = Saver.LoadEntityDatabase<T>(GetUniqueId(), GetEntityTable());
            SetEntityTable(currentEntityTable);
        }

        public virtual void Save()
        {
            currentEntityTable = GetEntityTable();
            Saver.SaveEntityDatabase(currentEntityTable);
        }

        protected virtual string GetUniqueId()
        {
            return uniqueId;
        }

        protected virtual void GetReferences()
        {
            _entityHolders = GetComponentsInChildren<IEntityHolder<T>>(true);
        }

        protected virtual void SetEntityTable(EntityTable<T> entityTable)
        {
            for (int i = 0; i < _entityHolders.Length; i++)
                _entityHolders[i].SetEntity(entityTable.entities[i]);
        }

        protected virtual EntityTable<T> GetEntityTable()
        {
            var entities = new List<T>();
            foreach (var entityHolder in _entityHolders)
                entities.Add(entityHolder.GetEntity());
            var entityTable = new EntityTable<T>(GetUniqueId(), entities);

            return entityTable;
        }
        
        
        protected virtual void OnDisable()
        {
            Saver.OnInitialize -= LoadOrSaveDefault;
            Saver.OnSave -= Save;
        }
    }
}