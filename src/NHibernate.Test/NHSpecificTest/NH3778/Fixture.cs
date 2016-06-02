using System.Linq;
using NHibernate.Linq;
using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.NH3778
{
	[TestFixture]
	public class Fixture : BugTestCase
	{
		public override string BugNumber
		{
			get { return "NH3778"; }
		}

		protected override void OnSetUp()
		{
			using (ISession session = OpenSession())
			using (var transaction = session.BeginTransaction())
			{
				var person = new Person
				{
					Id = 1,
					Name = "Bob"
				};
				person.Employee = new Employee(person);

				session.SaveOrUpdate(person);
				transaction.Commit();
			}
		}

		protected override void OnTearDown()
		{
			using (var session = OpenSession())
			using (var transaction = session.BeginTransaction())
			{
				session.Delete("from Person");
				session.Delete("from Employee");

				transaction.Commit();
			}
		}

		[Test]
		public void Crash_When_Performing_A_Linq_Query_On_OneToOne_Mapped_Reference()
		{
			using (ISession session = OpenSession())
			{
				var person = session.Load<Person>(1);

				var results = session.Query<Employee>()
									 .Where(x => x.Person == person)
									 .Fetch(x => x.Person)
									 .ToList();

				Assert.That(results.Count, Is.EqualTo(1));
			}
		}
	}
}