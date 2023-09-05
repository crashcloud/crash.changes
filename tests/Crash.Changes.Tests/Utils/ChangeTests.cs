using Crash.Changes.Utils;

// ReSharper disable HeapView.BoxingAllocation

namespace Crash.Changes.Tests.Utils
{
	public class ChangeTests
	{
		// Add/Remove defaults
		[TestCase(ChangeAction.Add | ChangeAction.Remove, ChangeAction.Add | ChangeAction.Remove,
			ExpectedResult = ChangeAction.Add)]

		// Add => Add
		[TestCase(ChangeAction.Remove, ChangeAction.Add,
			ExpectedResult = ChangeAction.Add)]

		// Identicles
		[TestCase(ChangeAction.None, ChangeAction.None,
			ExpectedResult = ChangeAction.None)]
		[TestCase(ChangeAction.Remove, ChangeAction.Remove,
			ExpectedResult = ChangeAction.Remove)]
		[TestCase(ChangeAction.Temporary, ChangeAction.Temporary,
			ExpectedResult = ChangeAction.Temporary)]
		[TestCase(ChangeAction.Release, ChangeAction.Release,
			ExpectedResult = ChangeAction.None)]
		[TestCase(ChangeAction.Update, ChangeAction.Update,
			ExpectedResult = ChangeAction.Update)]
		[TestCase(ChangeAction.Locked, ChangeAction.Locked,
			ExpectedResult = ChangeAction.Locked)]
		[TestCase(ChangeAction.Unlocked, ChangeAction.Unlocked,
			ExpectedResult = ChangeAction.Unlocked)]
		[TestCase(ChangeAction.Remove, ChangeAction.Remove,
			ExpectedResult = ChangeAction.Remove)]

		// Combinations
		[TestCase(ChangeAction.Transform, ChangeAction.Add,
			ExpectedResult = ChangeAction.Transform | ChangeAction.Add)]
		[TestCase(ChangeAction.Transform, ChangeAction.Update,
			ExpectedResult = ChangeAction.Transform | ChangeAction.Update)]
		[TestCase(ChangeAction.Temporary, ChangeAction.Remove,
			ExpectedResult = ChangeAction.Temporary | ChangeAction.Remove)]
		[TestCase(ChangeAction.Temporary, ChangeAction.Update,
			ExpectedResult = ChangeAction.Temporary | ChangeAction.Update)]

		// toggles
		[TestCase(ChangeAction.Locked, ChangeAction.Unlocked,
			ExpectedResult = ChangeAction.Unlocked)]
		[TestCase(ChangeAction.Unlocked, ChangeAction.Locked,
			ExpectedResult = ChangeAction.Locked)]
		[TestCase(ChangeAction.Unlocked | ChangeAction.Locked, ChangeAction.Unlocked | ChangeAction.Locked,
			ExpectedResult = ChangeAction.None)]
		[TestCase(ChangeAction.Temporary, ChangeAction.Release,
			ExpectedResult = ChangeAction.None)]
		[TestCase(ChangeAction.Release, ChangeAction.Temporary,
			ExpectedResult = ChangeAction.Temporary)]
		[TestCase(ChangeAction.None, ChangeAction.Temporary,
			ExpectedResult = ChangeAction.Temporary)]
		[TestCase(ChangeAction.Temporary, ChangeAction.Temporary,
			ExpectedResult = ChangeAction.Temporary)]
		public ChangeAction ActionCombinations(ChangeAction left, ChangeAction right)
		{
			return ChangeUtils.CombineActions(left, right);
		}
	}
}
