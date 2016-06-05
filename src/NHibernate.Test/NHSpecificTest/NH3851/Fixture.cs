using System.Linq;
using NHibernate.Linq;
using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.NH3851
{
	[TestFixture]
	public class Fixture : BugTestCase
	{
		public override string BugNumber
		{
			get { return "NH3851"; }
		}

		[Test]
		public void LinqQuerySkipCount()
		{
			using (ISession session = OpenSession())
			{
				var count = session.Query<Entity>().Skip(100).Count();
				Assert.That(count, Is.EqualTo(0));
			}	
		}
	}
}