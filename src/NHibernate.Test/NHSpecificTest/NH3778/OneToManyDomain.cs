namespace NHibernate.Test.NHSpecificTest.NH3778
{
	public class EmployeeOneToMany
	{
		protected virtual int Id { get; set; }

		public virtual PersonOneToMany Person { get; set; }

		protected EmployeeOneToMany() { }

		public EmployeeOneToMany(int id, PersonOneToMany person)
		{
			Id = id;
			Person = person;
		}
	}

	public class PersonOneToMany
	{
		protected PersonOneToMany() { }

		public PersonOneToMany(int id, string name)
		{
			Id = id;
			Name = name;
		}

		public virtual int Id { get; set; }

		public virtual string Name { get; set; }

		public virtual EmployeeOneToMany Employee { get; set; }
	}
}