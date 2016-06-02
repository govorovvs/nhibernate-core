namespace NHibernate.Test.NHSpecificTest.NH3778
{
	public class Employee
	{
		protected virtual int Id { get; set; }

		public virtual Person Person { get; set; }

		protected Employee() { }

		public Employee(Person person)
		{
			Person = person;
		}
	}
}