namespace Crash.Changes.Utils
{
	/// <summary>Utilities related to <see cref="IChange" /></summary>
	public static class Changes
	{
		public static ChangeAction combineActions(ChangeAction left, ChangeAction right)
		{
			ChangeAction result = left;

			if (right.HasFlag(ChangeAction.Add))
			{
				result &= ~ChangeAction.Remove;
				result |= ChangeAction.Add;
			}
			else if (right.HasFlag(ChangeAction.Remove))
			{
				result |= ChangeAction.Remove;
				result &= ~ChangeAction.Add;
			}

			if (right.HasFlag(ChangeAction.Update))
			{
				result |= ChangeAction.Update;
			}

			if (right.HasFlag(ChangeAction.Transform))
			{
				result |= ChangeAction.Transform;
			}

			if (right.HasFlag(ChangeAction.Locked))
			{
				result &= ~ChangeAction.Unlocked;
				result |= ChangeAction.Locked;
			}
			else if (right.HasFlag(ChangeAction.Unlocked))
			{
				result &= ~ChangeAction.Locked;
				result |= ChangeAction.Unlocked;
			}

			if (right.HasFlag(ChangeAction.Temporary))
			{
				result |= ChangeAction.Temporary;
			}

			return result;
		}
	}
}
