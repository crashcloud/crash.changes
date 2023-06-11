namespace Crash.Changes.Tests.Changes
{

	public sealed class QualityAndHashCodeTests
	{
		private static int COUNT = 100;

		[Theory]
		[TestCaseSource(nameof(IdenticalChangePairs))]
		public void HashCode_IsIdentical(ValueTuple<Change, Change> changePair)
		{
			Assert.That(changePair.Item2.GetHashCode(),
				Is.EqualTo(changePair.Item1.GetHashCode()));
		}

		[Test]
		public void HashCode_IsUnqiue()
		{
			var changes = UniqueChangePairs.Cast<Change>();
			var changeSet = changes.ToHashSet();
			Assert.That(changeSet.Count(), Is.EqualTo(changes.Count()));
		}

		[Theory]
		[TestCaseSource(nameof(IdenticalChangePairs))]
		public void Changes_AreEqual(ValueTuple<Change, Change> changePair)
		{
			Assert.That(changePair.Item2,
				Is.EqualTo(changePair.Item1));

			Assert.That(changePair.Item2.Equals(changePair.Item1));

			Assert.That(changePair.Item2.Equals((object)changePair.Item1), Is.True);

			Assert.That(changePair.Item1, Is.Not.EqualTo(null));
			Assert.That(changePair.Item1, Is.Not.EqualTo(new object()));
		}

		public static IEnumerable IdenticalChangePairs
		{
			get
			{
				for (int i = 0; i < COUNT; i++)
				{
					Guid guid = TestContext.CurrentContext.Random.NextGuid();
					string randomName = guid.ToString();
					string randomPayload = "";

					Change change_1 = new Change(guid,
												randomName,
												randomPayload)
					{
						Action = ChangeAction.Add
					};

					Change change_2 = new Change(guid,
												randomName,
												randomPayload)
					{
						Action = ChangeAction.Add
					};

					yield return (change_1, change_2);
				}
			}
		}

		// HashCode.Combine(Id, Owner, Action, Payload);
		public static IEnumerable UniqueChangePairs
		{
			get
			{
				yield return new Change(new Guid("51d4e897-587b-4d51-8a32-2b30d4bd56fd"), "James", null) { Action = ChangeAction.Add };
				yield return new Change(new Guid("51d4e897-587b-4d51-8a32-2b30d4bd56fd"), "James", "YES") { Action = ChangeAction.Update };
				yield return new Change(new Guid("51d4e897-587b-4d51-8a32-2b30d4bd56fd"), "John", "YES") { Action = ChangeAction.Add };
				yield return new Change(new Guid("20b8ff3c-f2cc-448d-bec9-a3bdf8490f48"), "James", "YES") { Action = ChangeAction.Add };
			}
		}

	}

}
