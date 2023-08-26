namespace Crash.Changes.Tests.Changes
{
	public sealed class QualityAndHashCodeTests
	{
		private static readonly int COUNT = 100;

		public static IEnumerable IdenticalChangePairs
		{
			get
			{
				for (int i = 0; i < COUNT; i++)
				{
					Guid guid = TestContext.CurrentContext.Random.NextGuid();
					string randomName = guid.ToString();
					string randomPayload = "";

					Change change_1 = new()
					{
						Id = guid, Owner = randomName, Payload = randomPayload, Action = ChangeAction.Add
					};

					Change change_2 = new()
					{
						Id = guid, Owner = randomName, Payload = randomPayload, Action = ChangeAction.Add
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
				yield return new Change
				{
					Id = new Guid("51d4e897-587b-4d51-8a32-2b30d4bd56fd"),
					Owner = "James",
					Payload = null,
					Action = ChangeAction.Add
				};
				yield return new Change
				{
					Id = new Guid("51d4e897-587b-4d51-8a32-2b30d4bd56fd"),
					Owner = "James",
					Payload = "YES",
					Action = ChangeAction.Update
				};
				yield return new Change
				{
					Id = new Guid("51d4e897-587b-4d51-8a32-2b30d4bd56fd"),
					Owner = "John",
					Payload = "YES",
					Action = ChangeAction.Add
				};
				yield return new Change
				{
					Id = new Guid("20b8ff3c-f2cc-448d-bec9-a3bdf8490f48"),
					Owner = "James",
					Payload = "YES",
					Action = ChangeAction.Add
				};
			}
		}

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
			IEnumerable<Change> changes = UniqueChangePairs.Cast<Change>();
			HashSet<Change> changeSet = changes.ToHashSet();
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
	}
}
