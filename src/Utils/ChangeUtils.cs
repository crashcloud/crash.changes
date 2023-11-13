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
			    previous.Id != @new.Id)
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
	}
}
