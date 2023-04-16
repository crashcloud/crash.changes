namespace Crash.Changes.Extensions
{

	/// <summary>Extension methods for Modifying a Changes' ChangeAction</summary>
	public static class UpdateAction
	{

		/// <summary>Adds the give ChangeAction to this Change</summary>
		public static void AddAction(this IChange change, ChangeAction action)
		{
			ChangeAction changeAction = change.Action;
			changeAction |= action;

			change.Action = changeAction;
		}

		/// <summary>Removes the given ChangeAction from this Change</summary>
		public static void RemoveAction(this IChange change, ChangeAction action)
		{
			ChangeAction changeAction = change.Action;
			changeAction &= ~action;

			change.Action = changeAction;
		}

		/// <summary>Tggles the given ChangeAction of this Change</summary>
		public static void ToggleAction(this IChange change, ChangeAction action)
		{
			ChangeAction changeAction = change.Action;
			changeAction ^= action;

			change.Action = changeAction;
		}
	}
}
