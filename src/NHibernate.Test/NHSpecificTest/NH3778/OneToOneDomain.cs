namespace NHibernate.Test.NHSpecificTest.NH3778
{
	public class EmployeeOneToOne
	{
		protected virtual int Id { get; set; }

		public virtual PersonOneToOne Person { get; set; }

		protected EmployeeOneToOne() { }

		public EmployeeOneToOne(PersonOneToOne person)
		{
			Person = person;
		}
	}

	public class PersonOneToOne
	{
		protected PersonOneToOne() { }

		public PersonOneToOne(int id, string name)
		{
			Id = id;
			Name = name;
		}

		public virtual int Id { get; set; }

		public virtual string Name { get; set; }

		public virtual EmployeeOneToOne Employee { get; set; }
	}
}