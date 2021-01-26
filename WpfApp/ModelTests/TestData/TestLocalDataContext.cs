using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;
using Model;

// namespace ViewModelTest.TestData
// {
// 	public partial class TestDataContext
// 	{
//
// 		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
//
// 		#region Extensibility Method Definitions
//
// 		partial void OnCreated();
// 		partial void InsertDepartment(Department instance);
// 		partial void UpdateDepartment(Department instance);
// 		partial void DeleteDepartment(Department instance);
// 		partial void InsertEmployeeDepartmentHistory(EmployeeDepartmentHistory instance);
// 		partial void UpdateEmployeeDepartmentHistory(EmployeeDepartmentHistory instance);
// 		partial void DeleteEmployeeDepartmentHistory(EmployeeDepartmentHistory instance);
//
// 		public TestDataContext()
// 		{
// 			
// 		}
//
// 		#endregion
//
// 		// 	
// 		// 	public TestDataContext() : 
// 		// 			base(global::Data.Properties.Settings.Default.AdventureWorks2014ConnectionString, mappingSource)
// 		// 	{
// 		// 		OnCreated();
// 		// 	}
// 		// 	
// 		// 	public TestDataContext(string connection) : 
// 		// 			base(connection, mappingSource)
// 		// 	{
// 		// 		OnCreated();
// 		// 	}
// 		// 	
// 		// 	public TestDataContext(System.Data.IDbConnection connection) : 
// 		// 			base(connection, mappingSource)
// 		// 	{
// 		// 		OnCreated();
// 		// 	}
// 		// 	
// 		// 	public TestDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
// 		// 			base(connection, mappingSource)
// 		// 	{
// 		// 		OnCreated();
// 		// 	}
// 		// 	
// 		// 	public TestDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
// 		// 			base(connection, mappingSource)
// 		// 	{
// 		// 		OnCreated();
// 		// 	}
// 		// 	
// 		// 	public System.Data.Linq.Table<Department> Departments
// 		// 	{
// 		// 		get
// 		// 		{
// 		// 			return this.GetTable<Department>();
// 		// 		}
// 		// 	}
// 		// 	
// 		// 	public System.Data.Linq.Table<EmployeeDepartmentHistory> EmployeeDepartmentHistories
// 		// 	{
// 		// 		get
// 		// 		{
// 		// 			return this.GetTable<EmployeeDepartmentHistory>();
// 		// 		}
// 		// 	}
// 		// }
// 		//
// 		
// 		// [Table(Name = "HumanResources.Department")]
// 		public partial class Department : INotifyPropertyChanging, INotifyPropertyChanged, ISerializable
// 		{
// 			public void GetObjectData(SerializationInfo info, StreamingContext context)
// 			{
// 				info.AddValue("DepartmentID", DepartmentID, typeof(short));
// 				info.AddValue("Name", Name, typeof(string));
// 				info.AddValue("GroupName", GroupName, typeof(string));
// 				info.AddValue("ModifiedDate", ModifiedDate, typeof(System.DateTime));
// 			}
//
// 			private static PropertyChangingEventArgs emptyChangingEventArgs =
// 				new PropertyChangingEventArgs(String.Empty);
//
// 			private short _DepartmentID;
//
// 			private string _Name;
//
// 			private string _GroupName;
//
// 			private System.DateTime _ModifiedDate;
//
// 			private EntitySet<EmployeeDepartmentHistory> _EmployeeDepartmentHistories;
//
// 			#region Extensibility Method Definitions
//
// 			partial void OnLoaded();
// 			partial void OnValidate(System.Data.Linq.ChangeAction action);
// 			partial void OnCreated();
// 			partial void OnDepartmentIDChanging(short value);
// 			partial void OnDepartmentIDChanged();
// 			partial void OnNameChanging(string value);
// 			partial void OnNameChanged();
// 			partial void OnGroupNameChanging(string value);
// 			partial void OnGroupNameChanged();
// 			partial void OnModifiedDateChanging(System.DateTime value);
// 			partial void OnModifiedDateChanged();
//
// 			#endregion
//
// 			public Department()
// 			{
// 				this._EmployeeDepartmentHistories = new EntitySet<EmployeeDepartmentHistory>(
// 					new Action<EmployeeDepartmentHistory>(this.attach_EmployeeDepartmentHistories),
// 					new Action<EmployeeDepartmentHistory>(this.detach_EmployeeDepartmentHistories));
// 				OnCreated();
// 			}
//
// 			[Column(Storage = "_DepartmentID", AutoSync = AutoSync.OnInsert, DbType = "SmallInt NOT NULL IDENTITY",
// 				IsPrimaryKey = true, IsDbGenerated = true)]
// 			public short DepartmentID
// 			{
// 				get { return this._DepartmentID; }
// 				set
// 				{
// 					if ((this._DepartmentID != value))
// 					{
// 						this.OnDepartmentIDChanging(value);
// 						this.SendPropertyChanging();
// 						this._DepartmentID = value;
// 						this.SendPropertyChanged("DepartmentID");
// 						this.OnDepartmentIDChanged();
// 					}
// 				}
// 			}
//
// 			[Column(Storage = "_Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
// 			public string Name
// 			{
// 				get { return this._Name; }
// 				set
// 				{
// 					if ((this._Name != value))
// 					{
// 						this.OnNameChanging(value);
// 						this.SendPropertyChanging();
// 						this._Name = value;
// 						this.SendPropertyChanged("Name");
// 						this.OnNameChanged();
// 					}
// 				}
// 			}
//
// 			[Column(Storage = "_GroupName", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
// 			public string GroupName
// 			{
// 				get { return this._GroupName; }
// 				set
// 				{
// 					if ((this._GroupName != value))
// 					{
// 						this.OnGroupNameChanging(value);
// 						this.SendPropertyChanging();
// 						this._GroupName = value;
// 						this.SendPropertyChanged("GroupName");
// 						this.OnGroupNameChanged();
// 					}
// 				}
// 			}
//
// 			[Column(Storage = "_ModifiedDate", DbType = "DateTime NOT NULL")]
// 			public System.DateTime ModifiedDate
// 			{
// 				get { return this._ModifiedDate; }
// 				set
// 				{
// 					if ((this._ModifiedDate != value))
// 					{
// 						this.OnModifiedDateChanging(value);
// 						this.SendPropertyChanging();
// 						this._ModifiedDate = value;
// 						this.SendPropertyChanged("ModifiedDate");
// 						this.OnModifiedDateChanged();
// 					}
// 				}
// 			}
//
// 			[Association(Name = "Department_EmployeeDepartmentHistory", Storage = "_EmployeeDepartmentHistories",
// 				ThisKey = "DepartmentID", OtherKey = "DepartmentID")]
// 			public EntitySet<EmployeeDepartmentHistory> EmployeeDepartmentHistories
// 			{
// 				get { return this._EmployeeDepartmentHistories; }
// 				set { this._EmployeeDepartmentHistories.Assign(value); }
// 			}
//
// 			public event PropertyChangingEventHandler PropertyChanging;
//
// 			public event PropertyChangedEventHandler PropertyChanged;
//
// 			protected virtual void SendPropertyChanging()
// 			{
// 				if ((this.PropertyChanging != null))
// 				{
// 					this.PropertyChanging(this, emptyChangingEventArgs);
// 				}
// 			}
//
// 			protected virtual void SendPropertyChanged(String propertyName)
// 			{
// 				if ((this.PropertyChanged != null))
// 				{
// 					this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
// 				}
// 			}
//
// 			private void attach_EmployeeDepartmentHistories(EmployeeDepartmentHistory entity)
// 			{
// 				this.SendPropertyChanging();
// 				entity.Department = this;
// 			}
//
// 			private void detach_EmployeeDepartmentHistories(EmployeeDepartmentHistory entity)
// 			{
// 				this.SendPropertyChanging();
// 				entity.Department = null;
// 			}
// 		}
//
// 		[Table(Name = "HumanResources.EmployeeDepartmentHistory")]
// 		public partial class EmployeeDepartmentHistory : INotifyPropertyChanging, INotifyPropertyChanged
// 		{
//
// 			private static PropertyChangingEventArgs emptyChangingEventArgs =
// 				new PropertyChangingEventArgs(String.Empty);
//
// 			private int _BusinessEntityID;
//
// 			private short _DepartmentID;
//
// 			private byte _ShiftID;
//
// 			private System.DateTime _StartDate;
//
// 			private System.Nullable<System.DateTime> _EndDate;
//
// 			private System.DateTime _ModifiedDate;
//
// 			private EntityRef<Department> _Department;
//
// 			#region Extensibility Method Definitions
//
// 			partial void OnLoaded();
// 			partial void OnValidate(System.Data.Linq.ChangeAction action);
// 			partial void OnCreated();
// 			partial void OnBusinessEntityIDChanging(int value);
// 			partial void OnBusinessEntityIDChanged();
// 			partial void OnDepartmentIDChanging(short value);
// 			partial void OnDepartmentIDChanged();
// 			partial void OnShiftIDChanging(byte value);
// 			partial void OnShiftIDChanged();
// 			partial void OnStartDateChanging(System.DateTime value);
// 			partial void OnStartDateChanged();
// 			partial void OnEndDateChanging(System.Nullable<System.DateTime> value);
// 			partial void OnEndDateChanged();
// 			partial void OnModifiedDateChanging(System.DateTime value);
// 			partial void OnModifiedDateChanged();
//
// 			#endregion
//
// 			public EmployeeDepartmentHistory()
// 			{
// 				this._Department = default(EntityRef<Department>);
// 				OnCreated();
// 			}
//
// 			[Column(Storage = "_BusinessEntityID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
// 			public int BusinessEntityID
// 			{
// 				get { return this._BusinessEntityID; }
// 				set
// 				{
// 					if ((this._BusinessEntityID != value))
// 					{
// 						this.OnBusinessEntityIDChanging(value);
// 						this.SendPropertyChanging();
// 						this._BusinessEntityID = value;
// 						this.SendPropertyChanged("BusinessEntityID");
// 						this.OnBusinessEntityIDChanged();
// 					}
// 				}
// 			}
//
// 			[Column(Storage = "_DepartmentID", DbType = "SmallInt NOT NULL", IsPrimaryKey = true)]
// 			public short DepartmentID
// 			{
// 				get { return this._DepartmentID; }
// 				set
// 				{
// 					if ((this._DepartmentID != value))
// 					{
// 						if (this._Department.HasLoadedOrAssignedValue)
// 						{
// 							throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
// 						}
//
// 						this.OnDepartmentIDChanging(value);
// 						this.SendPropertyChanging();
// 						this._DepartmentID = value;
// 						this.SendPropertyChanged("DepartmentID");
// 						this.OnDepartmentIDChanged();
// 					}
// 				}
// 			}
//
// 			[Column(Storage = "_ShiftID", DbType = "TinyInt NOT NULL", IsPrimaryKey = true)]
// 			public byte ShiftID
// 			{
// 				get { return this._ShiftID; }
// 				set
// 				{
// 					if ((this._ShiftID != value))
// 					{
// 						this.OnShiftIDChanging(value);
// 						this.SendPropertyChanging();
// 						this._ShiftID = value;
// 						this.SendPropertyChanged("ShiftID");
// 						this.OnShiftIDChanged();
// 					}
// 				}
// 			}
//
// 			[Column(Storage = "_StartDate", DbType = "Date NOT NULL", IsPrimaryKey = true)]
// 			public System.DateTime StartDate
// 			{
// 				get { return this._StartDate; }
// 				set
// 				{
// 					if ((this._StartDate != value))
// 					{
// 						this.OnStartDateChanging(value);
// 						this.SendPropertyChanging();
// 						this._StartDate = value;
// 						this.SendPropertyChanged("StartDate");
// 						this.OnStartDateChanged();
// 					}
// 				}
// 			}
//
// 			[Column(Storage = "_EndDate", DbType = "Date")]
// 			public System.Nullable<System.DateTime> EndDate
// 			{
// 				get { return this._EndDate; }
// 				set
// 				{
// 					if ((this._EndDate != value))
// 					{
// 						this.OnEndDateChanging(value);
// 						this.SendPropertyChanging();
// 						this._EndDate = value;
// 						this.SendPropertyChanged("EndDate");
// 						this.OnEndDateChanged();
// 					}
// 				}
// 			}
//
// 			[Column(Storage = "_ModifiedDate", DbType = "DateTime NOT NULL")]
// 			public System.DateTime ModifiedDate
// 			{
// 				get { return this._ModifiedDate; }
// 				set
// 				{
// 					if ((this._ModifiedDate != value))
// 					{
// 						this.OnModifiedDateChanging(value);
// 						this.SendPropertyChanging();
// 						this._ModifiedDate = value;
// 						this.SendPropertyChanged("ModifiedDate");
// 						this.OnModifiedDateChanged();
// 					}
// 				}
// 			}
//
// 			[Association(Name = "Department_EmployeeDepartmentHistory", Storage = "_Department",
// 				ThisKey = "DepartmentID", OtherKey = "DepartmentID", IsForeignKey = true)]
// 			public Department Department
// 			{
// 				get { return this._Department.Entity; }
// 				set
// 				{
// 					Department previousValue = this._Department.Entity;
// 					if (((previousValue != value)
// 					     || (this._Department.HasLoadedOrAssignedValue == false)))
// 					{
// 						this.SendPropertyChanging();
// 						if ((previousValue != null))
// 						{
// 							this._Department.Entity = null;
// 							previousValue.EmployeeDepartmentHistories.Remove(this);
// 						}
//
// 						this._Department.Entity = value;
// 						if ((value != null))
// 						{
// 							value.EmployeeDepartmentHistories.Add(this);
// 							this._DepartmentID = value.DepartmentID;
// 						}
// 						else
// 						{
// 							this._DepartmentID = default(short);
// 						}
//
// 						this.SendPropertyChanged("Department");
// 					}
// 				}
// 			}
//
// 			public event PropertyChangingEventHandler PropertyChanging;
//
// 			public event PropertyChangedEventHandler PropertyChanged;
//
// 			protected virtual void SendPropertyChanging()
// 			{
// 				if ((this.PropertyChanging != null))
// 				{
// 					this.PropertyChanging(this, emptyChangingEventArgs);
// 				}
// 			}
//
// 			protected virtual void SendPropertyChanged(String propertyName)
// 			{
// 				if ((this.PropertyChanged != null))
// 				{
// 					this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
// 				}
// 			}
// 		}
// 	}
// }

namespace ModelTests.TestData
{
    public class TestLocalDataContext
    {
        public ObservableCollection<Department> Departments = new ObservableCollection<Department>();
    }
}