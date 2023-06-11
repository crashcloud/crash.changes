using Crash.Changes.Extensions;

namespace Crash.Changes.Tests.Extensions
{
	public sealed class UtilsTests
	{

		[Test, Combinatorial]
		public void Add_Action_IsAddedOrKept([Values(ChangeAction.None, ChangeAction.Add,
													ChangeAction.Remove, ChangeAction.Update,
													ChangeAction.Transform, ChangeAction.Locked,
													ChangeAction.Temporary,
													ChangeAction.None | ChangeAction.Temporary,
													ChangeAction.Add | ChangeAction.Temporary,
													ChangeAction.Remove | ChangeAction.Temporary,
													ChangeAction.Update | ChangeAction.Temporary,
													ChangeAction.Transform | ChangeAction.Temporary,
													ChangeAction.Locked | ChangeAction.Temporary)] ChangeAction startAction)
		{
			Change change = new Change() { Action = startAction };
			Assert.That(change.IsTemporary(), Is.EqualTo(change.HasFlag(ChangeAction.Temporary)));
		}

	}

}
