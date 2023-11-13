using System.Text.Json;

using Crash.Changes.Utils;

namespace Crash.Changes.Tests.Utils
{
	public sealed class CombinedChangeUtils
	{
		private static IEnumerable ValidPayloadCombinations
		{
			get
			{
				yield return SimpleActionCombination();
				yield return NewOwner();
				yield return PayloadTransformCombination();
				/* TODO : Fill out
				yield return PayloadDataCombination();
				yield return PayloadUpdateCombination();
				yield return FullPayloadCombination();
				yield return PayloadCombination();
				*/
			}
		}

		private static IEnumerable InvalidPayloadCombinations
		{
			get
			{
				yield return NullTriplicate();
				yield return DifferentIds();
				yield return EmptyIds();
				yield return MissingTypes();
				yield return DifferentTypes();
			}
		}

		private static object[] PayloadTransformCombination()
		{
			PayloadPacket previousPacket = new() { Transform = new CTransform(3) };
			PayloadPacket newPacket = new() { Transform = new CTransform(2) };
			PayloadPacket combinedPacket = new() { Transform = new CTransform(6) };

			Guid sharedId = Guid.NewGuid();

			return new object[]
			{
				new Change { Id = sharedId, Payload = JsonSerializer.Serialize(previousPacket) },
				new Change { Id = sharedId, Payload = JsonSerializer.Serialize(newPacket) },
				new Change { Id = sharedId, Payload = JsonSerializer.Serialize(combinedPacket) }
			};
		}

		private static object[] NewOwner()
		{
			return new object[]
			{
				new Change { Owner = "Old" }, new Change { Owner = "New" }, new Change { Owner = "New" }
			};
		}

		private static object[] SimpleActionCombination()
		{
			Guid firstId = Guid.NewGuid();
			return new object[]
			{
				new Change { Id = firstId, Action = ChangeAction.Add },
				new Change { Id = firstId, Action = ChangeAction.Update },
				new Change { Id = firstId, Action = ChangeAction.Add | ChangeAction.Update }
			};
		}

		private static object[] NullTriplicate()
		{
			return new object[] { null, null };
		}

		private static object[] DifferentTypes()
		{
			Guid newId = Guid.NewGuid();
			return new object[]
			{
				new Change { Id = newId, Type = "Change.One" }, new Change { Id = newId, Type = "Change.Two" }
			};
		}

		private static object[] MissingTypes()
		{
			Guid newId = Guid.NewGuid();
			return new object[] { new Change { Id = newId }, new Change { Id = newId } };
		}

		private static object[] DifferentIds()
		{
			return new object[] { new Change { Id = Guid.NewGuid() }, new Change { Id = Guid.NewGuid() } };
		}

		private static object[] EmptyIds()
		{
			return new object[] { new Change { Id = Guid.Empty }, new Change { Id = Guid.Empty } };
		}

		[Theory]
		[TestCaseSource(nameof(ValidPayloadCombinations))]
		public void CombineValidPayloads(IChange previous, IChange @new, IChange expected)
		{
			IChange result = ChangeUtils.CombineChanges(previous, @new);
			Assert.That(expected, Is.EqualTo(result));
		}

		[Theory]
		[TestCaseSource(nameof(InvalidPayloadCombinations))]
		public void CombineInvalidPayloads(IChange previous, IChange @new)
		{
			Assert.Throws<Exception>(() => ChangeUtils.CombineChanges(previous, @new));
		}
	}
}
