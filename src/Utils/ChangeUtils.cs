namespace Crash.Changes.Utils
{
	/// <summary>Utilities related to <see cref="IChange" /></summary>
	public static class ChangeUtils
	{
		public static ChangeAction CombineActions(ChangeAction left, ChangeAction right)
		{
			ChangeAction result = left | right;

			if (right.HasFlag(ChangeAction.Add))
			{
				result &= ~ChangeAction.Remove;
			}
			else if (right.HasFlag(ChangeAction.Remove))
			{
				result &= ~ChangeAction.Add;
			}

			if (right.HasFlag(ChangeAction.Locked))
			{
				result &= ~ChangeAction.Unlocked;
			}

			if (right.HasFlag(ChangeAction.Unlocked))
			{
				result &= ~ChangeAction.Locked;
			}

			return result;
		}
	}
}
