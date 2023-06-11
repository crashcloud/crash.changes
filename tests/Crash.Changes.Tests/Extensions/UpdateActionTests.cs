using Crash.Changes.Extensions;

namespace Crash.Changes.Tests.Extensions
{

	public sealed class UpdateActionTests
	{

		[Test, Combinatorial]
		public void Add_Action_IsAddedOrKept([Values(ChangeAction.None,
													ChangeAction.Add,
													ChangeAction.Remove,
													ChangeAction.Update,
													ChangeAction.Transform,
													ChangeAction.Locked,
													ChangeAction.Temporary,
													ChangeAction.Camera)] ChangeAction startAction,
												[Values(ChangeAction.Add,
													ChangeAction.Remove,
													ChangeAction.Update,
													ChangeAction.Transform,
													ChangeAction.Locked,
													ChangeAction.Temporary,
													ChangeAction.Camera)] ChangeAction action)
		{
			Change change = new Change() { Action = startAction };
			UpdateAction.AddAction(change, action);
			Assert.That(change.Action.HasFlag(action), Is.True);
		}

		[Test, Combinatorial]
		public void Remove_Action_IsAlwaysRemoved([Values(ChangeAction.None,
													ChangeAction.Add,
													ChangeAction.Remove,
													ChangeAction.Update,
													ChangeAction.Transform,
													ChangeAction.Locked,
													ChangeAction.Temporary,
													ChangeAction.Camera)] ChangeAction startAction,
												[Values(ChangeAction.Add,
													ChangeAction.Remove,
													ChangeAction.Update,
													ChangeAction.Transform,
													ChangeAction.Locked,
													ChangeAction.Temporary,
													ChangeAction.Camera)] ChangeAction action)
		{
			Change change = new Change() { Action = startAction };
			UpdateAction.RemoveAction(change, action);
			Assert.That(change.Action.HasFlag(action), Is.False);
		}

		[Test, Combinatorial]
		public void Toggle_Action_Success([Values(ChangeAction.None,
													ChangeAction.Add,
													ChangeAction.Remove,
													ChangeAction.Update,
													ChangeAction.Transform,
													ChangeAction.Locked,
													ChangeAction.Temporary,
													ChangeAction.Camera)] ChangeAction startAction,
												[Values(ChangeAction.Add,
													ChangeAction.Remove,
													ChangeAction.Update,
													ChangeAction.Transform,
													ChangeAction.Locked,
													ChangeAction.Temporary,
													ChangeAction.Camera)] ChangeAction action)
		{
			Change change = new Change() { Action = startAction };

			if (startAction.HasFlag(action))
			{

				UpdateAction.ToggleAction(change, action);
				Assert.That(change.Action.HasFlag(action), Is.False);
			}
			else
			{
				UpdateAction.ToggleAction(change, action);
				Assert.That(change.Action.HasFlag(action), Is.True);
			}
		}

	}

}
