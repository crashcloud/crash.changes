using Crash.Changes.Extensions;

namespace Crash.Changes.Tests.Extensions
{

	[TestFixture]
	public sealed class EnumHelperTesters
	{

		[TestCase(ChangeAction.None | ChangeAction.None, ChangeAction.None)]
		[TestCase(ChangeAction.Add | ChangeAction.Add, ChangeAction.Add)]
		[TestCase(ChangeAction.Remove | ChangeAction.Remove, ChangeAction.Remove)]
		[TestCase(ChangeAction.Update | ChangeAction.Update, ChangeAction.Update)]
		[TestCase(ChangeAction.Transform | ChangeAction.Transform, ChangeAction.Transform)]
		[TestCase(ChangeAction.Lock | ChangeAction.Lock, ChangeAction.Lock)]
		[TestCase(ChangeAction.Unlock | ChangeAction.Unlock, ChangeAction.Unlock)]
		[TestCase(ChangeAction.Temporary | ChangeAction.Temporary, ChangeAction.Temporary)]
		[TestCase(ChangeAction.Camera | ChangeAction.Camera, ChangeAction.Camera)]
		public void AddChangeAction_Test(ChangeAction original, ChangeAction adder)
		{
			Change change = new()
			{
				Action = original
			};

			Assert.That(original, Is.EqualTo(change.Action));

			change.AddAction(adder);

			ChangeAction newAction = change.Action;
			Assert.Multiple(() =>
			{
				Assert.That(newAction.HasFlag(original), Is.True);
				Assert.That(newAction.HasFlag(adder), Is.True);
			});
		}

		[TestCase(ChangeAction.None | ChangeAction.None, ChangeAction.None)]
		[TestCase(ChangeAction.Add | ChangeAction.Add, ChangeAction.Add)]
		[TestCase(ChangeAction.Remove | ChangeAction.Remove, ChangeAction.Remove)]
		[TestCase(ChangeAction.Update | ChangeAction.Update, ChangeAction.Update)]
		[TestCase(ChangeAction.Transform | ChangeAction.Transform, ChangeAction.Transform)]
		[TestCase(ChangeAction.Lock | ChangeAction.Lock, ChangeAction.Lock)]
		[TestCase(ChangeAction.Unlock | ChangeAction.Unlock, ChangeAction.Unlock)]
		[TestCase(ChangeAction.Temporary | ChangeAction.Temporary, ChangeAction.Temporary)]
		[TestCase(ChangeAction.Camera | ChangeAction.Camera, ChangeAction.Camera)]
		public void RemoveChangeAction_Test(ChangeAction original, ChangeAction remover)
		{
			Change change = new()
			{
				Action = original
			};

			Assert.That(original, Is.EqualTo(change.Action));

			change.RemoveAction(remover);

			ChangeAction newAction = change.Action;
			Assert.Multiple(() =>
			{
				Assert.That(newAction.HasFlag(original), Is.False);
				Assert.That(newAction.HasFlag(remover), Is.False);
			});
		}
	}

}
