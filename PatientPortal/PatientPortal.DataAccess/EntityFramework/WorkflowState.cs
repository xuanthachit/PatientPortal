//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PatientPortal.DataAccess.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class WorkflowState
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WorkflowState()
        {
            this.WorkflowNavigations = new HashSet<WorkflowNavigation>();
            this.WorkflowNavigations1 = new HashSet<WorkflowNavigation>();
            this.Posts = new HashSet<Post>();
        }
    
        public short Id { get; set; }
        public string Name { get; set; }
        public byte WorkflowId { get; set; }
        public bool IsActive { get; set; }
    
        public virtual Workflow Workflow { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkflowNavigation> WorkflowNavigations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkflowNavigation> WorkflowNavigations1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Post> Posts { get; set; }
    }
}
