namespace NHibernate.Test.NHSpecificTest.NH3778
{
	public class Person
	{
		public virtual int Id { get; set; }

		public virtual string Name { get; set; }

		public virtual Employee Employee { get; set; }
	}
}