namespace ProplanetReplicationTool.Data.Models
{
    /// <summary>
    /// Represents an entity in the database.
    /// Used to define common properties for all entities.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Id of the entity
        /// </summary>
		public Guid Id { get; set; }

        /// <summary>
        /// Date when the entity was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date when the entity was last updated
        /// </summary>
        public DateTime LastUpdatedAt { get; set; }

        /// <summary>
        /// Constructor of the entity with the dates of creation and last update
        /// </summary>
        protected Entity()
        {
            UpdateCreatedAt();
            UpdateLastUpdatedAt();
        }

        /// <summary>
        /// Method to update the date of creation of the entity
        /// </summary>
        public void UpdateCreatedAt()
        {
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Method to update the date of the last update of the entity, this one is called in the repository
        /// </summary>
        public void UpdateLastUpdatedAt()
        {
            LastUpdatedAt = DateTime.UtcNow;
        }
    }
}