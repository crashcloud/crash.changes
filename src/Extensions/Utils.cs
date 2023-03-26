namespace Crash.Changes.Extensions
{

	/// <summary>Utilities to Query Change Action</summary>
	public static class Utils
    {

        /// <summary>Tests a ChangeAction for being Temporary</summary>
        public static bool IsTemporary(this IChange change)
            => change.HasFlag(ChangeAction.Temporary);

        /// <summary>Shorthand to Check a Change for the Flag</summary>
        public static bool HasFlag<T>(this IChange change, T flag) where T : Enum
            => change.Action.HasFlag(flag);

    }

}
