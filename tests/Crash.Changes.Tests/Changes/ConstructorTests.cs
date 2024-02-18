namespace Crash.Changes.Tests.Changes
{
	public sealed class ConstructorTests
	{
		private static readonly int COUNT = 5;

		public static IEnumerable Changes
		{
			get
			{
				for (int i = 0; i < COUNT; i++)
				{
					Guid guid = TestContext.CurrentContext.Random.NextGuid();
					string randomName = guid.ToString();
					string randomPayload = "";

					Change change = new()
					{
						Id = guid, Owner = randomName, Payload = randomPayload, Action = ChangeAction.Add
					};

					yield return change;
				}
			}
		}

		[TestCaseSource(nameof(Changes))]
		public void Test_DuplicationConstructor_IsEqual(IChange change)
		{
			Change newChange = new(change);
			Assert.That(change.Payload, Is.EqualTo(newChange.Payload));
			Assert.That(change.Action, Is.EqualTo(newChange.Action));
			Assert.That(change.Stamp, Is.EqualTo(newChange.Stamp));
			Assert.That(change.Owner, Is.EqualTo(newChange.Owner));
			Assert.That(change.Type, Is.EqualTo(newChange.Type));
			Assert.That(change.Id, Is.EqualTo(newChange.Id));
		}
	}
}
