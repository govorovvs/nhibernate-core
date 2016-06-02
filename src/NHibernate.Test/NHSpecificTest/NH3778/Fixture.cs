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
				var person = new PersonOneToOne(1, "Bob");
				person.Employee = new EmployeeOneToOne(person);

				session.SaveOrUpdate(person);
				transaction.Commit();
			}

			using (ISession session = OpenSession())
			using (var transaction = session.BeginTransaction())
			{
				var person = new PersonOneToMany(1, "Bob");
				var employee = new EmployeeOneToMany(2, person);

				session.SaveOrUpdate(person);
				session.SaveOrUpdate(employee);
				transaction.Commit();
			}
		}

		protected override void OnTearDown()
		{
			using (var session = OpenSession())
			using (var transaction = session.BeginTransaction())
			{
				session.Delete("from PersonOneToOne");
				session.Delete("from EmployeeOneToOne");

				session.Delete("from PersonOneToMany");
				session.Delete("from EmployeeOneToMany");

				transaction.Commit();
			}
		}

		[Test]
		public void Performing_A_Linq_Query_On_OneToMany_Mapped_Reference()
		{
			using (ISession session = OpenSession())
			{
				var person = session.Load<PersonOneToMany>(1);

				var results = session.Query<EmployeeOneToMany>()
									 .Where(x => x.Person == person)
									 .Fetch(x => x.Person)
									 .ToList();

				Assert.That(results.Count, Is.EqualTo(1));
			}
		}

		[Test]
		public void Crash_When_Performing_A_Linq_Query_On_OneToOne_Mapped_Reference()
		{
			using (ISession session = OpenSession())
			{
				var person = session.Load<PersonOneToOne>(1);

				var results = session.Query<EmployeeOneToOne>()
									 .Where(x => x.Person == person)
									 .Fetch(x => x.Person)
									 .ToList();

				Assert.That(results.Count, Is.EqualTo(1));
			}
		}
	}
}