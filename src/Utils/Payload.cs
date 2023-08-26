using System.Text.Json;

using Crash.Changes.Extensions;
using Crash.Geometry;

namespace Crash.Changes.Utils
{
	/// <summary>Utilities related to <see cref="IChange.Payload" /> and <see cref="PayloadPacket" /> </summary>
	public static class Payload
	{
		/// <summary>
		///     Combines a base <see cref="PayloadPacket" /> and a new <see cref="PayloadPacket" />
		///     The newer packet takes priority, potentially overwriting old values on the basePacket.
		/// </summary>
		/// <param name="basePacket">The packet to add on top of</param>
		/// <param name="newPacket">The new packet</param>
		/// <returns>A combined <see cref="PayloadPacket" /></returns>
		public static PayloadPacket Combine(PayloadPacket basePacket, PayloadPacket newPacket)
		{
			string data = basePacket.Data ?? newPacket.Data;
			CTransform newTransform = CTransform.Combine(basePacket.Transform, newPacket.Transform);
			Dictionary<string, string> updates = CombineDictionaries(basePacket.Updates, newPacket.Updates);

			return new PayloadPacket { Data = data, Transform = newTransform, Updates = updates };
		}

		private static Dictionary<string, string> CombineDictionaries(Dictionary<string, string> basePacketUpdates,
			Dictionary<string, string> newPacketUpdates)
		{
			if (basePacketUpdates is null && newPacketUpdates is not null)
			{
				return newPacketUpdates;
			}

			if (newPacketUpdates is null && basePacketUpdates is not null)
			{
				return basePacketUpdates;
			}

			Dictionary<string, string> resultantUpdates = basePacketUpdates;

			foreach (KeyValuePair<string, string> keyValuePair in newPacketUpdates)
			{
				basePacketUpdates[keyValuePair.Key] = keyValuePair.Value;
			}

			return resultantUpdates;
		}

		/// <summary>Returns Payload from a Change</summary>
		/// <returns>True on success, false on nulls, invalid change or failure</returns>
		public static bool TryGetPayloadFromChange(IChange change, out PayloadPacket payload)
		{
			if (change is null)
			{
				payload = new PayloadPacket();
				return false;
			}

			if (string.IsNullOrEmpty(change.Payload))
			{
				payload = new PayloadPacket();
				return true;
			}

			try
			{
				if (change.HasFlag(ChangeAction.Add | ChangeAction.Transform | ChangeAction.Update) ||
				    change.HasFlag(ChangeAction.Add | ChangeAction.Update) ||
				    change.HasFlag(ChangeAction.Add | ChangeAction.Transform) ||
				    change.HasFlag(ChangeAction.Transform | ChangeAction.Update))
				{
					payload = JsonSerializer.Deserialize<PayloadPacket>(change.Payload);
					return true;
				}

				if (change.HasFlag(ChangeAction.Add))
				{
					payload = new PayloadPacket { Data = change.Payload };
					return true;
				}

				if (change.HasFlag(ChangeAction.Transform))
				{
					CTransform transform = JsonSerializer.Deserialize<CTransform>(change.Payload);
					payload = new PayloadPacket { Transform = transform };
					return true;
				}

				if (change.HasFlag(ChangeAction.Update))
				{
					Dictionary<string, string>? udpdates =
						JsonSerializer.Deserialize<Dictionary<string, string>>(change.Payload);
					payload = new PayloadPacket { Updates = udpdates };
					return true;
				}
			}
			catch
			{
			}

			payload = new PayloadPacket();
			return false;
		}
	}
}
