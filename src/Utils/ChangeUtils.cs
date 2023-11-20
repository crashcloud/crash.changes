using System.Text.Json;

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

			if (right.HasFlag(ChangeAction.Release))
			{
				result &= ~ChangeAction.Temporary;
				result &= ~ChangeAction.Release;
			}

			if (right.HasFlag(ChangeAction.Temporary))
			{
				result &= ~ChangeAction.Release;
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

		public static IChange CombineChanges(IChange previous, IChange @new)
		{
			if (previous is null)
			{
				throw new ArgumentException($"{nameof(previous)} is null");
			}

			if (@new is null)
			{
				throw new ArgumentException($"{nameof(@new)} is null");
			}

			Guid combinedId = previous.Id;
			if (previous.Id == Guid.Empty ||
			    previous.Id != @new.Id ||
			    @new.Id == Guid.Empty)
			{
				throw new ArgumentException("Id is Invalid!");
			}

			PayloadUtils.TryGetPayloadFromChange(previous, out PayloadPacket previousPacket);
			PayloadUtils.TryGetPayloadFromChange(@new, out PayloadPacket newPacket);
			PayloadPacket payload = PayloadUtils.Combine(previousPacket, newPacket);

			return new Change
			{
				Id = combinedId,
				Stamp = DateTime.Now,
				Payload = JsonSerializer.Serialize(payload),
				Owner = @new.Owner ?? previous.Owner,
				Type = previous.Type ?? @new.Type,
				Action = CombineActions(previous.Action, @new.Action)
			};
		}

		/// <summary>Checks two changes for Equality</summary>
		/// <returns>True if everything besides the date is equal</returns>
		public static bool Equal(IChange left, IChange right)
		{
			if (left is null || right is null)
			{
				return false;
			}

			if (left.Id != right.Id)
			{
				return false;
			}

			if (left.Action != right.Action)
			{
				return false;
			}

			if (left.Payload?.Equals(right.Payload, StringComparison.InvariantCultureIgnoreCase) != true)
			{
				return false;
			}

			if (left.Type?.Equals(right.Payload, StringComparison.InvariantCultureIgnoreCase) != true)
			{
				return false;
			}

			if (left.Owner?.Equals(right.Payload, StringComparison.InvariantCultureIgnoreCase) != true)
			{
				return false;
			}

			return true;
		}
	}
}
